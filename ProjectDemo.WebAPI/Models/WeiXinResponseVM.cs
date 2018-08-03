using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectDemo.WebAPI.Models
{
    public class WeiXinResponseVM
    {
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string ResultContent { get; set; }
    }
}