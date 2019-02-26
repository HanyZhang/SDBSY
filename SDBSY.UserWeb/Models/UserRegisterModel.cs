using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.UserWeb.Models
{
    public class UserRegisterModel
    {
        [DisplayName("手机号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string PhoneNum { get; set; }
        [DisplayName("密码")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "{0}长度必须在{1}-{2}之间")]
        public string Pwd { get; set; }
        [DisplayName("确认密码")]
        [Compare("Pwd", ErrorMessage = "两次密码不一致")]
        public string RePassword { get; set; }
        [DisplayName("验证码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string RegCaptcha { get; set; }
    }
}