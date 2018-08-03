using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDemo.WebAPI.Models
{
    public class RequestWeiXinVM
    {
        /// <summary>
        /// 请求微信的内容
        /// </summary>
        public string RequestContent { get; set; }

        /// <summary>
        /// 请求方法 GET、POST
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }
}