using CommonTool;
using ProjectDemo.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectDemo.WebAPI.Controllers
{
    /// <summary>
    /// 微信接口控制器
    /// </summary>
    public class WeiXinController : ApiController
    {
        /// <summary>
        /// 微信接口
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        [Route("WeiXinInterface")]
        [Test]
        public WeiXinResponseVM POSTWeiXinInterface([FromBody]RequestWeiXinVM vm)
        {
            var result = new WeiXinResponseVM()
            {
                ResultCode = "99",
                ResultMessage = "未知错误！",
                ResultContent = "",
            };
            try
            {
                //string appid = "appid";
                //string secret = "appsecret";
                //string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
                result.ResultContent = "success";
            }
            catch (Exception ex)
            {
                Log.Error("系统错误！", ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 微信接口
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [AcceptVerbs("GET")]
        [Route("WeiXinInterface")]
        public WeiXinResponseVM GETWeiXinInterface([FromUri]RequestWeiXinVM vm)
        {
            var result = new WeiXinResponseVM()
            {
                ResultCode = "99",
                ResultMessage = "未知错误！",
                ResultContent = "",
            };
            try
            {
                result.ResultContent = "success";
            }
            catch (Exception ex)
            {
                Log.Error("系统错误！", ex.ToString());
            }
            return result;
        }
    }
}
