using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.TeacherWeb.Models
{
    public class SignupPostModel
    {
        [DisplayName("手机号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public  string Account { get; set; }
        public int Role { get; set; }
        [DisplayName("密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Password { get; set; }
        [DisplayName("确认密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0}不能为空")]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致.")]
        public string RePassword { get; set; }
        [DisplayName("验证码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public  string Captcha { get; set; }
    }
}