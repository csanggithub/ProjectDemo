using CommonTool.MailKit;
using ProjectDemo.ConsoleApp.SendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //SendMail.SendMail.Send163Demo();
                //SendMail.Send163();
                //SendMail.SendQQ();
                //SendMail.SendHotmailDemo();

                var ccList = new List<string>();
                ccList.Add("171810091@qq.com");
                var recipients = new List<string>();
                recipients.Add("907500395@qq.com");
                var mailBodyEntity = new MailBodyEntity()
                {
                    Body = "这是一个测试邮件body内容<a href='http://www.baidu.com'>123</a>",
                    Cc = ccList,
                    //MailBodyType = "html",
                    //MailFiles = new List<MailFile>(new MailFile { MailFilePath = "", MailFileSubType = "", MailFileType = "" }),
                    MailFiles = new List<MailFile>() {
                        new MailFile { MailFilePath = @"D:\文档\Visual Studio 2017\Projects\EMailDemo\EMailDemo.ConsoleApp\bin\File\20180807165402.png", MailFileSubType = "png", MailFileType = "image" },
                        new MailFile { MailFilePath = @"D:\文档\Visual Studio 2017\Projects\EMailDemo\EMailDemo.ConsoleApp\bin\File\TIM截图20180807165402.png", MailFileSubType = "png", MailFileType = "image" }
                    },
                    //MailFilePath = @"D:\文档\Visual Studio 2017\Projects\EMailDemo\EMailDemo.ConsoleApp\bin\File\20180807165402.png",
                    //MailFileSubType = "png",
                    //MailFileType = "image",
                    //MailTextBody = "这里是邮件文本内容<a href='http://www.baidu.com'>2345</a>",
                    Recipients = recipients,
                    Sender = "邮件的发件人",
                    SenderAddress = "1390193352@qq.com",
                    Subject = "测试邮件是否可以发送的标题",
                };
                var sendServerConfiguration = new SendServerConfigurationEntity()
                {
                    SenderPassword = "lbhagelgdsofifaj",
                    SmtpPort = 465,
                    IsSsl = true,
                    MailEncoding = "utf-8",
                    SenderAccount = "1390193352@qq.com",
                    SmtpHost = "smtp.qq.com",

                };

                var mailModel = new MailModel()
                {
                    Password = "lbhagelgdsofifaj",
                    Port = 465,
                    EMailPriority = MailPriority.High,
                    CcAddress = ccList,
                    ToAddress=recipients,
                    Body= "这是一个测试邮件body内容<a href='http://www.baidu.com'>123</a>",
                    DeliveryMethod= SmtpDeliveryMethod.Network,
                    EnableSsl=true,
                    FromMailAddress= "1390193352@qq.com",
                    Host= "smtp.qq.com",
                    Subject="test mail send",
                    UserName="小二郎",
                    MailFiles= new List<string>()
                    {
                        @"D:\文档\Visual Studio 2017\Projects\EMailDemo\EMailDemo.ConsoleApp\bin\File\20180807165402.png",@"D:\文档\Visual Studio 2017\Projects\EMailDemo\EMailDemo.ConsoleApp\bin\File\TIM截图20180807165402.png"
                    },
                };

                //var result=SeedMailHelper.SendMail(mailBodyEntity, sendServerConfiguration);
                //ReceiveEmailHelper.ReceiveEmail();
                //ReceiveEmailHelper.DownloadBodyParts();

                test3.SendMail(mailModel);
                Console.WriteLine("成功！");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
