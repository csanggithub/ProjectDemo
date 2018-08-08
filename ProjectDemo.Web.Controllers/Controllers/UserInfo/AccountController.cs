using CommonTool.CryptHelper;
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
            ViewData["LoginKeyCode"] = "123qwe";
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = loginVM.UserName;
                    var password = loginVM.Password;
                    if (string.IsNullOrEmpty(userName))
                    {
                        return Json(new { Success = false, ErrorMessage = "请输入用户名" }, JsonRequestBehavior.AllowGet);
                    }
                    if (string.IsNullOrEmpty(password))
                    {
                        return Json(new { Success = false, ErrorMessage = "请输入密码" }, JsonRequestBehavior.AllowGet);
                    }

                    var checkedPass = ValidateCode == loginVM.ValidateCode;
                    //检验验证码
                    if (LoginHelper.IsAllowValidateCode && !checkedPass)
                    {
                        return Json(new { Success = false, ErrorMessage = string.IsNullOrWhiteSpace(ValidateCode) ? "验证码失效" : "验证码错误" }, JsonRequestBehavior.AllowGet);
                    }
                    //解密的密码
                    var pPassword = JSDes.DesDecrypt(password, loginVM.LoginSecretKey);
                    //将明文密码转化为MD5加密
                    password = HashEncode.HashEncoding(pPassword);

                    var user = LoginHelper.GetLoginUserInfo(StringSafeFilter.Filter(loginVM.UserName), StringSafeFilter.Filter(password.ToUpper()));
                    if (user == null)
                        //用户名或密码有误！
                        return Json(new { Success = false, ErrorMessage= "用户名或密码有误" }, JsonRequestBehavior.AllowGet);
                    LoginHelper.SaveUserInfoToSession(user);
                    return RedirectToAction("Index", "Home");
                }
                catch
                {

                }
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 登录验证码,使用Session保存
        /// </summary>
        protected string ValidateCode
        {
            get
            {
                if (null == Session["ValidateCode"])
                {
                    Session["ValidateCode"] = string.Empty;
                }
                return Session["ValidateCode"].ToString();
            }
            set
            {
                Session.Timeout = FormsAuthentication.Timeout.TotalMinutes.ObjectToInt();
                Session["ValidateCode"] = value;
            }

        }

        [HttpGet]
        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginForm(LoginVM loginVM)
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
