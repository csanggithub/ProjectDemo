using CommonTool;
using CommonTool.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace ProjectDemo.Web.Controllers.App_Start
{
    public class IdentityService
    {
        private IdentityService()
        {

        }

        private static IdentityService _identityService;

        public static IdentityService Instance
        {
            get { return _identityService ?? (_identityService = new IdentityService()); }
        }

        /// <summary>
        /// 系统用于存储的当前登录人安全名称
        /// </summary>
        public static string LoginUserAuthorityName
        {
            get { return "UserAuthority"; }
        }

        /// <summary>
        /// 登录 创建提供的用户名称的身份验证票证，并将其添加到的响应 cookie 集合或 URL 如果您使用的无 cookie 的身份验证。
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createPersistentCookie"></param>
        public void SignIn(Object value, string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            HttpContext.Current.Session.Timeout = FormsAuthentication.Timeout.TotalMinutes.ObjectToInt();
            HttpContext.Current.Session[LoginUserAuthorityName] = value;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="AccountName">账号名</param>
        /// <param name="effectiveTime"></param>
        /// <param name="isPersistent"></param>
        /// <param name="userData">要存储在票证的特定于用户的数据。 如：角色 Role</param>
        public void SignIn(Object value, string AccountName, int effectiveTime = 30, bool isPersistent = false, string userData = "")
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                       1,
                       AccountName,
                       DateTime.Now,
                       DateTime.Now.AddMinutes(effectiveTime),
                       isPersistent,
                       userData
                       );
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                HttpOnly = true,
                //cookie.Secure = false;
                Expires = ticket.Expiration,
                Path = FormsAuthentication.FormsCookiePath
            };
            HttpContext.Current.Session.Timeout = FormsAuthentication.Timeout.TotalMinutes.ObjectToInt();
            HttpContext.Current.Session[LoginUserAuthorityName] = value;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session.RemoveAll();
                    HttpContext.Current.Session.Abandon();
                }
                HttpContext.Current.Response.Cookies.Clear();
                FormsAuthentication.SignOut();
            }
            catch (Exception ex)
            {
                Log.Error("退出失败", ex.ToString());
            }
        }

        public bool IsSignedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public IIdentity GetCurrentIdentity()
        {
            return HttpContext.Current.User.Identity;
        }

        /// <summary>
        /// 获取Session
        /// </summary>
        public object UserSession
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[LoginHelper.LoginUserAuthorityName] != null)
                {
                    return HttpContext.Current.Session[LoginUserAuthorityName];
                }
                return null;
            }
        }

        /// <summary>
        /// 清除Session
        /// </summary>
        public void ClearUserSession()
        {
            if (HttpContext.Current != null
                && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[LoginUserAuthorityName] = null;
            }
        }
    }
}
