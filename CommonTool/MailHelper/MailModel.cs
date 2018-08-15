using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool.MailHelper
{
    public class MailModel
    {
        /// <summary>
        /// 用户名,根据需要看是否需要带@hotmail.com
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 邮箱账户
        /// </summary>
        public string FromMailAddress { get; set; }

        /// <summary>
        /// 目标账户
        /// </summary>
        public IList<string> ToAddress { get; set; }

        /// <summary>
        /// 抄送账户
        /// </summary>
        public IList<string> CcAddress { get; set; }

        /// <summary>
        /// 密送账户
        /// </summary>
        public IList<string> BccAddress { get; set; }

        /// <summary>
        /// 是否开启安全连接
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 传输协议
        /// </summary>
        public SmtpDeliveryMethod DeliveryMethod { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 附件集合
        /// </summary>
        public IList<string> MailFiles { get; set; }

        public MailPriority EMailPriority { get; set; }

        public MailModel()
        {
            ToAddress = new List<string>();
        }

        public MailModel(
            string userName,
            string password,
            string host,
            string mailAddress,
            IList<string> toAddress,
            bool enableSsl,
            SmtpDeliveryMethod deliveryMethod,
            int port,
            string title,
            string content,
            IList<string> mailFiles,
            IList<string> ccAddress,
            IList<string> bccAddress,
            MailPriority eMailPriority
            )
        {
            UserName = userName;
            Password = password;
            Host = host;
            FromMailAddress = mailAddress;
            ToAddress = toAddress;
            EnableSsl = enableSsl;
            DeliveryMethod = deliveryMethod;
            Port = port;
            Subject = title;
            Body = content;
            MailFiles = mailFiles;
            CcAddress = ccAddress;
            BccAddress = bccAddress;
            EMailPriority = eMailPriority;

        }
    }
}
