using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class ChangePasswordPostModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="旧密码必填")]
        [MaxLength(18),MinLength(6)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "新密码必填")]
        [MaxLength(18), MinLength(6)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "确认新密码必填")]
        [MaxLength(18), MinLength(6)]
        [Compare(nameof(NewPassword),ErrorMessage ="确认新密码必须和新密码一致")]
        public string NewPassword2 { get; set; }
    }
}