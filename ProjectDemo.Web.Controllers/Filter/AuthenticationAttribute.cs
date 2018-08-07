using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ProjectDemo.Web.Controllers.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 角色
        /// </summary>
        public string[] Role { get; set; }

        /// <summary>
        /// 是否启用角色校验
        /// </summary>
        public bool IsCheckRole { get; set; }

        /// <summary>
        /// 是否启用登录校验
        /// </summary>
        public bool IsCheckLogin { get; set; }

        public AuthenticationAttribute() : this(null, false)
        {
        }
        public AuthenticationAttribute(string role, bool isCheckRole)
        {
            Role = role.Split(',');
            IsCheckRole = isCheckRole;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var role = ticket.UserData.Split(',');
                if (!role.Any() || !Role.Intersect(role).Any())
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "UserLogin", action = "Login" }));
                }
            }
            else
            {
                //跳转到未授权页面
                filterContext.Result = new RedirectResult("~/NoLogin.htm");
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "UserLogin", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
