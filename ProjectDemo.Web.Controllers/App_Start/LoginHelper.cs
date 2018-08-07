using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using CommonTool;
using CommonTool.Utility;
using ProjectDemo.Entity.Test.Models;

namespace ProjectDemo.Web.Controllers.App_Start
{
    public class LoginHelper
    {
        /// <summary>
        /// 系统用于存储的当前登录人安全名称
        /// </summary>
        public static string LoginUserAuthorityName
        {
            get { return "UserAuthority"; }
        }

        /// <summary>
        /// 获取登录用户信息-帐号与密码登录
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public static User GetLoginUserInfo(string account, string password = "")
        {
            var model = new User()
            {
                AccountName = "admin",
                UserName = "小王",
                Password = "123qwe"
            };
            return model;
        }

        /// <summary>
        /// 保存用户信息到Session
        /// </summary>
        /// <param name="userInfo"></param>
        public static void SaveUserInfoToSession(User user)
        {
            user.Password = string.Empty;
            //IdentityService.Instance.SignIn(user as Object,user.UserName, false);
            int effectiveTime = 30;
            IdentityService.Instance.SignIn(user as Object, user.UserName, effectiveTime, false, user.Role);
        }

        public static User GetUser()
        {
            try
            {
                if (!IdentityService.Instance.IsSignedIn())
                {
                    return null;
                }
                var account = IdentityService.Instance.GetCurrentIdentity().Name;
                if (string.IsNullOrWhiteSpace(account))
                {
                    IdentityService.Instance.SignOut();
                    //Log.Debug("系统用户鉴权", "当前登录的用户票据无效");
                    return null;
                }

                var user = IdentityService.Instance.UserSession as User;
                if (user != null && user.AccountName == account)
                {
                    return user;
                }

                IdentityService.Instance.SignOut();
                return LoginHelper.AutoLogin(account, ref user) ? user : null;
            }
            catch (Exception ex)
            {
                Log.Error("自动登录失败", ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 自动登录，未进行安全性检查，该功能假设登录前已经行过用户状态检查
        /// </summary>
        /// <param name="account">登录名</param>
        /// <param name="user">登录对象</param>
        /// <returns></returns>
        public static bool AutoLogin(string account, ref User user)
        {
            if (string.IsNullOrWhiteSpace(account) || (user = GetLoginUserInfo(account)) == null)
            {
                return false;
            }

            //登录处理
            SaveUserInfoToSession(user);

            //Log.Debug("系统用户鉴权", string.Format("当前用户{0}登录失效，已自动登录", account));

            return true;
        }
    }
}
