using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SDBSY.TeacherWeb.Models
{
    public class SigninPostModel
    {
        [DisplayName("手机号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Account { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Password { get; set; }
        [DisplayName("验证码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Captcha { get; set; }
    }
}