using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class AdminLoginModel
    {
        [Required(ErrorMessage ="账号必填")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码必填")]
        public string Password { get; set; }
        [Required(ErrorMessage = "验证码必填")]
        public string Captcha { get; set; }
    }
}