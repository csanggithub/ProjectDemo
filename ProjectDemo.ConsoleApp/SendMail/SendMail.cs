using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;

namespace ProjectDemo.ConsoleApp.SendMail
{
    public class SendMail
    {
        public SendMail() { }

        private static readonly string user = "1390193352@qq.com";//替换成你的hotmail用户名
        private static readonly string password = "lbhagelgdsofifaj";//替换成你的hotmail密码
        private static readonly string host = "smtp.qq.com";//设置邮件的服务器
        private static readonly string mailAddress = "1390193352@qq.com"; //替换成你的hotmail账户
        private static readonly string ToAddress = "907500395@qq.com";//目标邮件地址。
        /// <summary>
        /// 最基本的发送邮件的方法
        /// </summary>
        public static void Send163Demo()
        {
            //string user = "1390193352@qq.com";//替换成你的hotmail用户名
            //string password = "lbhagelgdsofifaj";//替换成你的hotmail密码
            //string host = "smtp.qq.com";//设置邮件的服务器
            //string mailAddress = "1390193352@qq.com"; //替换成你的hotmail账户
            //string ToAddress = "907500395@163.com";//目标邮件地址。

            SmtpClient smtp = new SmtpClient(host);
            smtp.EnableSsl = true; //开启安全连接。
            smtp.Credentials = new NetworkCredential(user, password); //创建用户凭证
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //使用网络传送
            MailMessage message = new MailMessage(mailAddress, ToAddress, "testmail", "testmail<a href='http://www.baidu.com'>123</a>");  //创建邮件
            ////邮件采用的编码
            message.BodyEncoding = Encoding.UTF8;

            ////设置邮件的优先级为高
            message.Priority = MailPriority.High;
            //var SUpFile = HttpContext.Current.Server.MapPath("~/File/TIM截图20180807165402.png");//获得附件在本地地址
            //                                                    //将文件进行转换成Attachments
            //Attachment data = new Attachment(SUpFile, MediaTypeNames.Application.Octet);
            //// Add time stamp information for the file.
            //ContentDisposition disposition = data.ContentDisposition;
            //disposition.CreationDate = System.IO.File.GetCreationTime(SUpFile);
            //disposition.ModificationDate = System.IO.File.GetLastWriteTime(SUpFile);
            //disposition.ReadDate = System.IO.File.GetLastAccessTime(SUpFile);

            //message.Attachments.Add(data);
            //System.Net.Mime.ContentType ctype = new System.Net.Mime.ContentType();

            message.From = new MailAddress(mailAddress, user, Encoding.UTF8);
            //// 添加附件
            message.Attachments.Add(new Attachment("File/TIM截图20180807165402.png", MediaTypeNames.Application.Octet));
            message.Attachments.Add(new Attachment("File/TIM截图20180807165402.zip", MediaTypeNames.Application.Octet));
            message.IsBodyHtml = true;

            //可以发送给多人 
            message.To.Add("b@b.com");
            message.To.Add("b@b.com");
            message.To.Add("b@b.com");
            //可以抄送给多人
            message.CC.Add("c@c.com");
            message.CC.Add("c@c.com");
            //message.Attachments.Add(new Attachment(new MemoryStream(fileAttachment.FileContent),
            //            fileAttachment.FileName));


            smtp.EnableSsl = true;//经过ssl加密
            smtp.Send(message); //发送邮件
            ////释放资源
            message.Dispose();
            Console.WriteLine("邮件发送成功！");
        }

        //可以
        public static void SendHotmail()
        {
            //string user = "*****@hotmail.com";
            //string password = "*****";
            //string host = "smtp.live.com";
            //string mailAddress = "*****@hotmail.com";
            IList<string> toAddress = new List<string>();
            toAddress.Add(ToAddress);
            bool enableSsl = true;
            SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network;
            int post = 465;
            string title = "标题";
            string content = "发送内容";
            MailModel mailModel = new MailModel(user, password, host, mailAddress, toAddress,
                enableSsl, deliveryMethod, post, title, content);

            SennMail(mailModel);
        }

        //可以
        public static void Send163()
        {
            //string user = "*****@163.com";
            //string password = "*****";
            //string host = "smtp.163.com";
            //string mailAddress = "*****@163.com";
            IList<string> toAddress = new List<string>();
            toAddress.Add(ToAddress);
            bool enableSsl = true;
            SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network;
            int post = 465;
            string title = "标题";
            string content = "发送内容";
            MailModel mailModel = new MailModel(user, password, host, mailAddress, toAddress,
                enableSsl, deliveryMethod, post, title, content);

            SennMail(mailModel);
        }

        public static void SendQQ()
        {
            //string user = "*****@qq.com";
            //string password = "*****";
            //string host = "smtp.qq.com";
            //string mailAddress = "*****@qq.com";
            IList<string> toAddress = new List<string>();
            toAddress.Add(ToAddress);
            bool enableSsl = true;
            SmtpDeliveryMethod deliveryMethod = SmtpDeliveryMethod.Network;
            int post = 465;
            string title = "标题";
            string content = "发送内容";
            MailModel mailModel = new MailModel(user, password, host, mailAddress, toAddress,
                enableSsl, deliveryMethod, post, title, content);

            SennMail(mailModel); ;
        }

        public static void SennMail(MailModel mailModel)
        {
            SmtpClient smtp = new SmtpClient(mailModel.Host);
            smtp.EnableSsl = mailModel.EnableSsl;
            smtp.Credentials = new NetworkCredential(mailModel.UserName, mailModel.Password);
            smtp.DeliveryMethod = mailModel.DeliveryMethod;
            //smtp.Port = mailModel.Port;//有些时候设置端口会出现很奇怪的问题，这个与服务器的设置有关，建议不要设置端口。
            MailAddress mailAddress = new MailAddress(mailModel.FromMailAddress);

            MailMessage message = new MailMessage();
            message.From = mailAddress;
            foreach (string toAddress in mailModel.ToAddress)
            {
                message.To.Add(toAddress);
            }
            message.Subject = mailModel.Subject;
            message.Body = mailModel.Body;
            message.IsBodyHtml = true;
            smtp.Send(message);
            Console.WriteLine("邮件发送成功！");
        }
    }
}
