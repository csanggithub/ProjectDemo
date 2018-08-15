using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool.MailKit
{
    /// <summary>
    /// 跟投邮件服务API
    /// </summary>
    public static class ReceiveMailHelper
    {
        /// <summary>
        /// 设置发件人信息
        /// </summary>
        /// <returns></returns>
        public static SendServerConfigurationEntity SetSendMessage()
        {
            var sendServerConfiguration = new SendServerConfigurationEntity
            {
                SmtpHost = ConfigurationManager.AppSettings["SmtpServer"],
                SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]),
                IsSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSsl"]),
                MailEncoding = ConfigurationManager.AppSettings["MailEncoding"],
                SenderAccount = ConfigurationManager.AppSettings["SenderAccount"],
                SenderPassword = ConfigurationManager.AppSettings["SenderPassword"]
            };
            return sendServerConfiguration;
        }

        /// <summary>
        /// 接收邮件
        /// </summary>
        public static void ReceiveEmail()
        {
            var sendServerConfiguration = SetSendMessage();

            if (sendServerConfiguration == null)
            {
                throw new ArgumentNullException();
            }

            using (var client = new ImapClient(new ProtocolLogger(MailMessage.CreateMailLog())))
            {
                client.Connect(sendServerConfiguration.SmtpHost, sendServerConfiguration.SmtpPort,
                    SecureSocketOptions.SslOnConnect);
                client.Authenticate(sendServerConfiguration.SenderAccount, sendServerConfiguration.SenderPassword);
                client.Inbox.Open(FolderAccess.ReadOnly);
                var uids = client.Inbox.Search(SearchQuery.All);
                foreach (var uid in uids)
                {
                    var message = client.Inbox.GetMessage(uid);
                    message.WriteTo($"{uid}.eml");
                }

                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 下载邮件内容
        /// </summary>
        public static void DownloadBodyParts()
        {
            var sendServerConfiguration = SetSendMessage();

            using (var client = new ImapClient())
            {
                client.Connect(sendServerConfiguration.SmtpHost, sendServerConfiguration.SmtpPort,
                    SecureSocketOptions.SslOnConnect);
                client.Authenticate(sendServerConfiguration.SenderAccount, sendServerConfiguration.SenderPassword);
                client.Inbox.Open(FolderAccess.ReadOnly);

                // 搜索Subject标题包含“MimeKit”或“MailKit”的邮件
                var query = SearchQuery.SubjectContains("MimeKit").Or(SearchQuery.SubjectContains("MailKit"));
                var uids = client.Inbox.Search(query);

                // 获取搜索结果的摘要信息（我们需要UID和BODYSTRUCTURE每条消息，以便我们可以提取文本正文和附件）
                var items = client.Inbox.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure);

                foreach (var item in items)
                {
                    // 确定一个目录来保存内容
                    var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/MailBody", item.UniqueId.ToString());

                    Directory.CreateDirectory(directory);

                    // IMessageSummary.TextBody是一个便利的属性，可以为我们找到“文本/纯文本”的正文部分
                    var bodyPart = item.TextBody;

                    // 下载'text / plain'正文部分
                    var body = (TextPart)client.Inbox.GetBodyPart(item.UniqueId, bodyPart);

                    // TextPart.Text是一个便利的属性，它解码内容并将结果转换为我们的字符串
                    var text = body.Text;

                    File.WriteAllText(Path.Combine(directory, "body.txt"), text);

                    // 现在遍历所有附件并将其保存到磁盘
                    foreach (var attachment in item.Attachments)
                    {
                        // 像我们对内容所做的那样下载附件
                        var entity = client.Inbox.GetBodyPart(item.UniqueId, attachment);

                        // 附件可以是message / rfc822部件或常规MIME部件
                        var messagePart = entity as MessagePart;
                        if (messagePart != null)
                        {
                            var rfc822 = messagePart;

                            var path = Path.Combine(directory, attachment.PartSpecifier + ".eml");

                            rfc822.Message.WriteTo(path);
                        }
                        else
                        {
                            var part = (MimePart)entity;

                            // 注意：这可能是空的，但大多数会指定一个文件名
                            var fileName = part.FileName;

                            var path = Path.Combine(directory, fileName);

                            // decode and save the content to a file
                            using (var stream = File.Create(path))
                                part.Content.DecodeTo(stream);
                        }
                    }
                }

                client.Disconnect(true);
            }
        }
    }
}
