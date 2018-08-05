using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ProjectDemo.Web.Controllers.Filter
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string role = string.Empty;
            var cookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                role = ticket.UserData;
                if (role.Length == 0 || role == "Admin")
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "UserLogin", action = "Login" }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "UserLogin", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
