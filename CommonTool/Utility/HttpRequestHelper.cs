using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonTool.Utility
{
    public class HttpRequestHelper
    {
        /// <summary>
        /// 执行HTTP POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>HTTP响应</returns>
        public static string HttpPost(string requestUrl)
        {
            string result = string.Empty;
            Stream stream = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "POST";
                var response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                //获取内容
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(stream != null)
                {
                    stream.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行HTTP POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="dic">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string HttpPost(string url, Dictionary<string, string> dic)
        {
            string result = string.Empty;
            Stream stream = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                #region 添加Post 参数
                var builder = new StringBuilder();
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                request.ContentLength = data.Length;
                using (var reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }
                #endregion
                var response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                //获取响应内容
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(stream!=null)
                {
                    stream.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行HTTP POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="content">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string HttpPost(string url, string content)
        {
            string result = string.Empty;
            Stream stream = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                request.ContentLength = data.Length;
                using (var reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                }
                #endregion

                var response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                //获取响应内容
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行HTTP POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="contentType">获取或设置 Content-type HTTP 标头的值。例如;"application/x-www-form-urlencoded";</param>
        /// <param name="postDataStr">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string HttpPost(string url, string contentType, string postDataStr)
        {
            string result = null;
            HttpWebResponse webResp = null;
            Stream answer = null;
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(postDataStr);
                var webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = "POST";
                webReq.ContentType = contentType;
                webReq.ContentLength = buffer.Length;
                using (var postData = webReq.GetRequestStream())
                {
                    postData.Write(buffer, 0, buffer.Length);
                }
                webResp = (HttpWebResponse)webReq.GetResponse();
                answer = webResp.GetResponseStream();
                using (var answerData = new StreamReader(answer, Encoding.UTF8))
                {
                    result = answerData.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(answer!=null)
                {
                    answer.Close();
                }
                if (webResp != null)
                {
                    webResp.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行HTTP GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>HTTP响应</returns>
        public static string HttpGet(string url)
        {
            string result = "";
            Stream stream = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                //获取内容
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                if(stream!=null)
                {
                    stream.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行HTTP GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="dic">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string HttpGet(string url, Dictionary<string, string> dic)
        {
            string result = "";
            var builder = new StringBuilder();
            Stream stream = null;
            try
            {
                builder.Append(url);
                if (dic.Count > 0)
                {
                    builder.Append("?");
                    int i = 0;
                    foreach (var item in dic)
                    {
                        if (i > 0)
                            builder.Append("&");
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                var request = (HttpWebRequest)WebRequest.Create(builder.ToString());
                request.Method = "GET";
                //req.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
                //添加参数
                var response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                //获取内容
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                if(stream!=null)
                {
                    stream.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 执行HTTP GET请求
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <param name="contentType">获取或设置 Content-type HTTP 标头的值。例如;"application/x-www-form-urlencoded";</param>
        /// <param name="postDataStr">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string HttpGet(string Url, string contentType, string postDataStr)
        {
            string result = string.Empty;
            Stream stream = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Method = "GET";
                request.ContentType = contentType;
                var response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return result;
        }
    }
}
