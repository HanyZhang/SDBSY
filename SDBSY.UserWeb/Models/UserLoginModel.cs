using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.UserWeb.Models
{
    public class UserLoginModel
    {
        [DisplayName("手机号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string PhoneNum { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Pwd { get; set; }
        [DisplayName("验证码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string LoginCaptcha { get; set; }
    }
}