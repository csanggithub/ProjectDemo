using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.ConsoleApp.SendMail
{
    internal class Program
    {
        //private static void Main(string[] args)
        //{
        //    FileInfo fileInfo1 = new FileInfo(@"D:\新建 Microsoft Excel 工作表.xlsx");
        //    FileInfo fileInfo2 = new FileInfo(@"D:\新建 Microsoft Word 文档.docx");
        //    MailHelper mailHelper = new MailHelper("发件人地址", "邮箱密码", "smtp.qq.com");
        //    var flag = mailHelper.SendMail("收件人地址", "测试邮件发送附件", "详情见附件",
        //            new List<MailHelper.FileAttachment>
        //            {
        //        new MailHelper.FileAttachment
        //        {
        //            FileContent = File.ReadAllBytes(fileInfo1.FullName),
        //            FileName = fileInfo1.Name
        //        },
        //        new MailHelper.FileAttachment
        //        {
        //            FileContent = File.ReadAllBytes(fileInfo2.FullName),
        //            FileName = fileInfo2.Name
        //        }
        //            });
        //    Console.WriteLine(flag);
        //    Console.ReadLine();
        //}
        private class MailHelper
        {
            private String _mailAccount;
            private String _password;
            private String _smtpServer;
            private int _port;
            public MailHelper(String mailAccount, String password, String smtpServer, int port = 587)
            {
                _mailAccount = mailAccount;
                _password = password;
                _smtpServer = smtpServer;
                _port = port;
            }
            public class FileAttachment
            {
                public String FileName { get; set; }
                public byte[] FileContent { get; set; }
            }
            public bool SendMail(String toAddress, String subject, String text,
                List<FileAttachment> fileAttachments = null)
            {
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.To.Add(toAddress);
                /* 
                * msg.To.Add("b@b.com");
                * msg.To.Add("b@b.com");
                * msg.To.Add("b@b.com");可以发送给多人 
                */
                /* 
                * msg.CC.Add("c@c.com"); 
                * msg.CC.Add("c@c.com");可以抄送给多人
                */
                msg.From = new MailAddress(_mailAccount, _mailAccount, System.Text.Encoding.UTF8);
                /* 上面3个参数分别是发件人地址（可以随便写），发件人姓名，编码*/
                msg.Subject = subject;//邮件标题
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码
                msg.Body = text;//邮件内容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
                msg.IsBodyHtml = false;//是否是HTML邮件
                msg.Priority = MailPriority.High;//邮件优先级
                if (fileAttachments != null && fileAttachments.Count != 0)
                {
                    foreach (var fileAttachment in fileAttachments)
                    {
                        var aa = new MemoryStream();
                        msg.Attachments.Add(new Attachment(new MemoryStream(fileAttachment.FileContent),
                            fileAttachment.FileName));
                    }
                }
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(_mailAccount, _password);
                client.Port = _port;
                client.Host = _smtpServer;
                client.EnableSsl = true;//经过ssl加密
                try
                {
                    client.Send(msg);
                    return true;
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    return false;
                }
            }
        }
    }
}
