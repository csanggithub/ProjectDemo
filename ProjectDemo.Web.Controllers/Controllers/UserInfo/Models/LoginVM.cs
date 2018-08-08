using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDemo.Web.Controllers.Controllers.UserInfo.Models
{
    public class LoginVM
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 加密密码
        /// </summary>
        [Required(ErrorMessage = @"密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 明文密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "密码")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string OriginalPassword { get; set; }

        /// <summary>
        /// 登录密钥
        /// </summary>
        public string LoginSecretKey { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidateCode { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 记住我RememberMe
        /// </summary>
        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}
