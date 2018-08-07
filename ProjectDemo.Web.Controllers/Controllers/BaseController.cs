using ProjectDemo.Web.Controllers.App_Start;
using ProjectDemo.Web.Controllers.Filter;
using ProjectDemo.Entity.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectDemo.Web.Controllers.Controllers
{
    [AuthenticationAttribute("", false)]
    public class BaseController : Controller
    {
        public BaseController()
        {
            // 用户是否已登录
            ViewBag.IsLogin = IdentityService.Instance.IsSignedIn();
            var user = User;
            if (!ViewBag.IsLogin || user == null)
            {
                return;
            }
            ViewBag.UserInfo = user;
        }

        protected new User User
        {
            get
            {
                var user = LoginHelper.GetUser();
                if (user != null)
                {
                    return user;
                }
                if (IdentityService.Instance.IsSignedIn())
                {
                    IdentityService.Instance.SignOut();
                }
                //Response.Redirect("~" + FormsAuthentication.LoginUrl + "?returnUrl=" + Request.RawUrl);
                //Log.Debug("系统用户鉴权", "无法获取当前登录的用户对象");
                return null;
            }
        }

        protected IIdentity CurrentIdentity
        {
            get { return IdentityService.Instance.GetCurrentIdentity(); }
        }

        protected override HttpNotFoundResult HttpNotFound(string statusDescription)
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            Response.Clear();
            Response.Redirect("~/404.htm");
            Response.End();
            return null;
        }
    }

    /// <summary>
    /// 操作结果JsonResult对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResultObject<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public readonly bool Success;
        /// <summary>
        /// 数据
        /// </summary>
        public readonly T Data;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="success"></param>
        /// <param name="data"></param>
        public JsonResultObject(bool success, T data)
        {
            Success = success;
            Data = data;
        }
    }
}
