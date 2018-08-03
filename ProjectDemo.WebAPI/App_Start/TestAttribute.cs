using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProjectDemo.WebAPI
{
    /// <summary>
    /// 测试过滤器
    /// </summary>
    public class TestAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheck { get; set; }
        /// <summary>
        /// 执行Action之后，执行该方法
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            IsCheck = true;
            base.OnActionExecuted(actionExecutedContext);
        }

        /// <summary>
        /// 执行Action之前，执行该方法
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IsCheck = false;
            base.OnActionExecuting(actionContext);
        }
    }
}