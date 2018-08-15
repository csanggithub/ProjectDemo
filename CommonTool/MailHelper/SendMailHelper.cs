using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool.MailHelper
{
    public static class SendMailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailModel"></param>
        public static void SendMail(MailModel mailModel)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = mailModel.Host;
                smtp.EnableSsl = true; //开启安全连接。
                smtp.Credentials = new NetworkCredential(mailModel.FromMailAddress, mailModel.Password); //创建用户凭证
                smtp.DeliveryMethod = mailModel.DeliveryMethod;//SmtpDeliveryMethod.Network; //使用网络传送
                MailMessage message = new MailMessage();  //创建邮件
                                                          //邮件采用的编码
                message.BodyEncoding = Encoding.UTF8;
                ////设置邮件的优先级为高
                message.Priority = mailModel.EMailPriority;
                message.From = new MailAddress(mailModel.FromMailAddress, mailModel.UserName, Encoding.UTF8);
                message.IsBodyHtml = true;
                message.Subject = mailModel.Subject;
                message.Body = mailModel.Body;
                if (mailModel.ToAddress != null && mailModel.ToAddress.Any())
                {
                    foreach (var to in mailModel.ToAddress)
                    {
                        //发送
                        message.To.Add(to);
                    }
                }
                if (mailModel.CcAddress != null && mailModel.CcAddress.Any())
                {
                    foreach (var cc in mailModel.CcAddress)
                    {
                        //抄送
                        message.CC.Add(cc);
                    }
                }
                if (mailModel.BccAddress != null && mailModel.BccAddress.Any())
                {
                    foreach (var bcc in mailModel.BccAddress)
                    {
                        //密送
                        message.Bcc.Add(bcc);
                    }
                }

                if (mailModel.MailFiles != null && mailModel.MailFiles.Any())
                {
                    foreach (var fs in mailModel.MailFiles)
                    {
                        //var fsMapPath = HttpContext.Current.Server.MapPath(fs);//获得附件在本地地址
                        //将文件进行转换成Attachments
                        //Attachment data = new Attachment(fs, MediaTypeNames.Application.Octet);
                        //// Add time stamp information for the file.
                        //ContentDisposition disposition = data.ContentDisposition;
                        //disposition.CreationDate = System.IO.File.GetCreationTime(fs);
                        //disposition.ModificationDate = System.IO.File.GetLastWriteTime(fs);
                        //disposition.ReadDate = System.IO.File.GetLastAccessTime(fs);

                        //message.Attachments.Add(data);
                        //附件
                        //message.Attachments.Add(new Attachment(fs, MediaTypeNames.Application.Octet));

                        FileInfo fileInfo = new FileInfo(fs);
                        message.Attachments.Add(new Attachment(new MemoryStream(File.ReadAllBytes(fileInfo.FullName)), fileInfo.FullName, MediaTypeNames.Application.Octet));



                    }
                }
                smtp.Send(message); //发送邮件
                                    ////释放资源
                message.Dispose();
            }
            catch (SmtpFailedRecipientsException)
            {

            }
            catch (SmtpFailedRecipientException)
            {

            }
            catch (SmtpException)
            {

            }
        }
    }
}
