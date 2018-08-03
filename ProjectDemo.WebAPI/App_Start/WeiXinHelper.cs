using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace ProjectDemo.WebAPI
{
    /// <summary>
    /// WeiXinHelper
    /// </summary>
    public class WeiXinHelper
    {
        /// <summary>
        /// 请求微信接口
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        internal static string RequestWinXinApi(string strUrl, string method)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            request.Method = string.IsNullOrEmpty(method) ?"GET": method;
            string content = string.Empty;
            using (WebResponse wr = request.GetResponse())
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = reader.ReadToEnd();
                //var result = reader.ReadToEnd();
                //var errMsg = JsonConvert.DeserializeObject<ErrInfoVM>(result);
                //content = JsonConvert.SerializeObject(errMsg);
            }
            return content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sgin"></param>
        /// <returns></returns>
        internal static bool CheckSgin(string sgin)
        {
            if (sgin == "1")
                return true;
            return false;
        }
    }
}