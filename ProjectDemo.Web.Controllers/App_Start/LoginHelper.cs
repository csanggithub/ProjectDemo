using ProjectDemo.Web.Controllers.Controllers.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using CommonTool.Utility;

namespace ProjectDemo.Web.Controllers.App_Start
{
    public class LoginHelper
    {
        /// <summary>
        /// 系统用于存储的当前登录人安全名称
        /// </summary>
        public static string LoginUserAuthorityName => "UserAuthority";

        /// <summary>
        /// Session获取用户信息
        /// </summary>
        /// <returns></returns>
        public static LoginVM GetUserFromSession()
        {
            if (HttpContext.Current != null
                && HttpContext.Current.Session != null
                && HttpContext.Current.Session[LoginUserAuthorityName] != null)
            {
                return HttpContext.Current.Session[LoginUserAuthorityName] as LoginVM;
            }
            return null;
        }

        /// <summary>
        /// 保存用户信息到Session
        /// </summary>
        /// <param name="userInfo"></param>
        public static void SaveUserInfoToSession(LoginVM userInfo)
        {
            ClearUserFromSession();
            if (userInfo == null || HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return;
            }
            FormsAuthentication.SetAuthCookie(string.Format("{0}@{1}@{2}", userInfo.UserName, userInfo.Name, userInfo.RememberMe), false);
            HttpContext.Current.Session.Timeout = FormsAuthentication.Timeout.TotalMinutes.ObjectToInt();
            HttpContext.Current.Session[LoginUserAuthorityName] = userInfo;
        }

        /// <summary>
        /// 清除Session
        /// </summary>
        public static void ClearUserFromSession()
        {
            if (HttpContext.Current != null
                && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[LoginUserAuthorityName] = null;
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        public static void SingOut()
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
                
            }
        }
    }
}
