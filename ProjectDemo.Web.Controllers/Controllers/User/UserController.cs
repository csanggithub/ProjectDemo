using ProjectDemo.Web.Controllers.Controllers.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectDemo.Web.Controllers.Controllers.User
{
    public class UserController: Controller
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
                var login = new LoginVM { UserName = "Admin", Password = "123qwe" };
                //ModelState.AddModelError("UserName", "用户名不存在");
                if (loginVM.UserName == login.UserName && loginVM.Password == login.Password)
                {

                    //FormsAuthenticationTicket 
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        login.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(20),
                        false,
                        "Admin"
                        );
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                    cookie.HttpOnly = true;
                    HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
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
