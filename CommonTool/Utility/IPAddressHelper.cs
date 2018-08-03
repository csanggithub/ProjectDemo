using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonTool.Utility
{
    public class IPAddressHelper
    {
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            string address;
            var xForwardedFor = HttpContext.Current.Request.Headers["x-forwarded-for"];
            if (!string.IsNullOrEmpty(xForwardedFor))
            {
                var arr = xForwardedFor.Split(new[] { ',' });
                address = arr.Length > 1 ? arr[arr.Length - 2] : arr[0];
            }
            else
            {
                address = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return address;
        }

        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIp()
        {
            string address =
                Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList.Where(r => r.AddressFamily.ToString() == "InterNetwork")
                    .Select(r => r.ToString())
                    .FirstOrDefault();
            return address;
        }
    }
}
