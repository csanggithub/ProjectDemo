using ProjectDemo.Entity.Test.Models;
using ProjectDemo.Web.Controllers.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectDemo.Web.Controllers.Filter
{
    /// <summary>
    /// 权限校验过滤器
    /// 权限粒度分三级：Controller、Action
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        private ActionExecutingContext _currentContext;

        private string _rightCode;

        /// <summary>
        /// 权限编码
        /// </summary>
        public string RightCode
        {
            get
            {
                //需要校验权限但未定义，则使用默认值
                if (_rightCode == string.Empty)
                {
                    var controllerName = _currentContext.RouteData.Values["controller"].ToString();
                    var actionName = _currentContext.RouteData.Values["action"].ToString();
                    _rightCode = controllerName + "_" + actionName;
                }
                return _rightCode;
            }
        }

        /// <summary>
        /// 是否校验登录
        /// </summary>
        public bool IsCheckLogin { get; set; }

        /// <summary>
        /// 权限控制
        /// </summary>
        /// <param name="isCheckLogin">是否校验登录</param>
        /// <param name="rightCode">允许权限编码，支持多个权限编码，用逗号分开</param>
        protected AuthorizeFilterAttribute(bool isCheckLogin, string rightCode)
        {
            IsCheckLogin = isCheckLogin;
            _rightCode = rightCode;
        }

        /// <summary>
        /// 权限控制(默认校验登录)
        /// </summary>
        public AuthorizeFilterAttribute() : this(true, null)
        {

        }

        /// <summary>
        /// 权限控制(是否校验登录)
        /// </summary>
        /// <param name="isCheckLogin">是否校验登录</param>
        public AuthorizeFilterAttribute(bool isCheckLogin) : this(isCheckLogin, null)
        {

        }

        /// <summary>
        /// 权限控制(默认校验登录的权限)
        /// </summary>
        /// <param name="rightCode">允许权限编码，支持多个权限编码，用逗号分开</param>
        public AuthorizeFilterAttribute(string rightCode) : this(true, rightCode)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _currentContext = filterContext ?? throw new ArgumentNullException("filterContext");

            //如果不检验登录
            if (!IsCheckLogin)
            {
                return;
            }

            //如果用户未登录
            if (User == null)
            {
                var request = _currentContext.Controller.ControllerContext.HttpContext.Request;
                if (request.RawUrl != FormsAuthentication.LoginUrl)
                {
                    filterContext.Result = new RedirectResult("~/UnLogon.htm?returnUrl=" + request.RawUrl);
                }
                return;
            }

            //如果没有权限
            if (!CheckHasRightNew(RightCode))
            {
                //跳转到未授权页面
                filterContext.Result = new RedirectResult("~/NoAuthority.htm");
            }
        }

        /// <summary>
        ///   当前登录的用户
        /// </summary>
        protected User User
        {
            get
            {
                return LoginHelper.GetUser();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected IIdentity CurrentIdentity
        {
            get { return IdentityService.Instance.GetCurrentIdentity(); }
        }

        /// <summary>
        /// 用户权限列表（新）
        /// </summary>
        protected virtual List<string> GetOwnRightList()
        {
            //if (CurrentIdentity.IsAuthenticated && User != null)
            //{
            //    return User.OwnRightList;
            //}
            return new List<string>(0);
        }

        /// <summary>
        /// 检查是否有权限（新）
        /// </summary>
        /// <param name="allowRightCode">权限编码</param>
        /// <returns></returns>
        protected bool CheckHasRightNew(string allowRightCode)
        {
            if (string.IsNullOrEmpty(allowRightCode))
            {
                return true;
            }
            var allowRightCodes = allowRightCode.Split(',');
            var hasRightCodes = GetOwnRightList();
            bool isPass = (from ar in allowRightCodes
                           from hr in hasRightCodes
                           where ar == hr
                           select ar).Any();
            return isPass;
        }
    }
}
