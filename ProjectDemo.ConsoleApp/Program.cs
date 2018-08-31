using CommonTool.MailKit;
using ProjectDemo.ConsoleApp.SendMail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
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


                Console.WriteLine("成功！");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 旧值还原.
        /// </summary>
        public void CoverModelField<T>(T model, List<TestHistory> changeRecordList, bool isUseNewVal = false) where T : class
        {
            PropertyInfo[] props = null;
            try
            {
                var type = typeof(T);
                props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            catch
            { }

            foreach (var p in props)
            {
                var record = changeRecordList.Where(x => x.FieldCode == p.Name).FirstOrDefault();
                if (record != null)
                {
                    Type pt = p.PropertyType;

                    //判断type类型是否为泛型，因为nullable是泛型类,
                    if (pt.IsGenericType && pt.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断convertsionType是否为nullable泛型类
                    {
                        //如果type为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                        NullableConverter nullableConverter = new NullableConverter(pt);
                        //将type转换为nullable对的基础基元类型
                        pt = nullableConverter.UnderlyingType;

                        if (isUseNewVal)
                        {
                            if (record.NewValue == null)
                            {
                                p.SetValue(model, record.NewValue, null);
                            }
                            else
                            {
                                p.SetValue(model, Convert.ChangeType(record.NewValue, pt), null);
                            }

                        }
                        else
                        {
                            if (record.OldValue == null)
                            {
                                p.SetValue(model, record.OldValue, null);
                            }
                            else
                            {
                                p.SetValue(model, Convert.ChangeType(record.OldValue, pt), null);
                            }
                        }
                    }
                    else
                    {
                        if (isUseNewVal)
                        {
                            p.SetValue(model, Convert.ChangeType(record.NewValue, pt), null);
                        }
                        else
                        {
                            p.SetValue(model, Convert.ChangeType(record.OldValue, pt), null);
                        }
                    }
                }
            }
        }
    }

    public class TestHistory
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 数据变更记录主键
        /// </summary>
        public int DemoId { get; set; }

        /// <summary>
        /// 字段编码
        /// </summary>
        public string FieldCode { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 旧值
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
