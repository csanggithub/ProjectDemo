using CommonTool.Utility;
using ProjectDemo.Web.Controllers.App_Start;
using ProjectDemo.Web.Controllers.Controllers.UserInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectDemo.Web.Controllers.Controllers.UserInfo
{
    public class AccountController: Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(loginVM.Password))
                        return View();
                    var user = LoginHelper.GetLoginUserInfo(StringSafeFilter.Filter(loginVM.UserName), StringSafeFilter.Filter(loginVM.Password));
                    if (user == null)
                        //用户名或密码有误！
                        return View();
                    LoginHelper.SaveUserInfoToSession(user);
                    return RedirectToAction("Index", "Home");
                }
                catch
                {

                }
            }
            return View();
        }

        public string SingOut()
        {
            FormsAuthentication.SignOut();
            return "退出";
        }
    }
}
