using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool.Utility
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }


    public enum ContentType
    {
        /// <summary>
        /// text/plain
        /// </summary>
        [Description("text/plain")]
        Text,
        /// <summary>
        /// application/json
        /// </summary>
        [Description("application/json")]
        JSON,
        /// <summary>
        /// application/javascript
        /// </summary>
        [Description("application/javascript")]
        Javascript,
        /// <summary>
        /// application/xml
        /// </summary>
        [Description("application/xml")]
        XML,
        /// <summary>
        /// text/xml
        /// </summary>
        [Description("text/xml")]
        TextXML,
        /// <summary>
        /// text/html
        /// </summary>
        [Description("text/html")]
        HTML
    }


    public class RestApiClient
    {
        public string EndPoint { get; set; }    //请求的url地址  
        public HttpVerb Method { get; set; }    //请求的方法
        public ContentType ContentType { get; set; } //格式类型
        public string PostData { get; set; }    //传送的数据

        /// <summary>
        /// GET方式
        /// </summary>
        /// <param name="endpoint"></param>
        public RestApiClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = ContentType.JSON;
            PostData = "";
        }
        /// <summary>
        /// POST方式
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="postData"></param>
        public RestApiClient(string endpoint, string postData)
        {
            EndPoint = endpoint;
            Method = HttpVerb.POST;
            ContentType = ContentType.JSON;
            PostData = postData;
        }
        public RestApiClient(string endpoint, HttpVerb method, ContentType contentType)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = "";
        }


        public RestApiClient(string endpoint, HttpVerb method, ContentType contentType, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
        }

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";


        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }

        /// <summary>
        /// 执行HTTP请求
        /// </summary>
        /// <returns>返回HTTP请求字符串</returns>
        public string MakeRequest()
        {
            return MakeRequest("");
        }

        /// <summary>
        /// 执行HTTP请求
        /// </summary>
        /// <param name="parameters">URL参数</param>
        /// <returns>返回HTTP请求字符串</returns>
        public string MakeRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            if (EndPoint.Substring(0, 8) == "https://")
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //request.ProtocolVersion = HttpVersion.Version10;
                //ServicePointManager.CheckCertificateRevocationList = true;
                //ServicePointManager.DefaultConnectionLimit = 100;
                //ServicePointManager.Expect100Continue = false;
                request.KeepAlive = false;
            }


            request.Method = Method.ToString();
            request.UserAgent = DefaultUserAgent;
            request.ContentLength = 0;
            request.ContentType = EnumHelper.GetDescription(ContentType);


            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.PUT)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                request.ContentLength = bytes.Length;


                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;


                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }


                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }


                return responseValue;
            }
        }


        public bool CheckUrl(string parameters)
        {
            bool bResult = true;


            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            myRequest.Method = Method.ToString();             //设置提交方式可以为＂ｇｅｔ＂，＂ｈｅａｄ＂等
            myRequest.Timeout = 10000;　             //设置网页响应时间长度
            myRequest.AllowAutoRedirect = false;//是否允许自动重定向
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            bResult = (myResponse.StatusCode == HttpStatusCode.OK);//返回响应的状态


            return bResult;
        }
    }
}
