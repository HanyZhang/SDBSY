using SDBSY.TeacherWeb.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.TeacherWeb.Models
{
    public class CertificateAddPostModel
    {
        public long TeacherId { get; set; }
        [DisplayName("证书名称")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Name { get; set; }
        [DisplayName("发证单位")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string AwardUnit { get; set; }
        [DisplayName("发证时间")]
        [Required(ErrorMessage = "{0}不能为空")]
        public DateTime AwardDateTime { get; set; }
        [DisplayName("证书编号")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Number { get; set; }
        [Display(Name = "证书资料")]
        [Required(ErrorMessage = "请选择{0}")]
        [ValidateFile]
        public HttpPostedFileBase[] UpFiles { get; set; }
        
    }
}