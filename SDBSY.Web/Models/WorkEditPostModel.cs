using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class WorkEditPostModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "单位名称必填")]
        public string UnitName { get; set; }
        [Required(ErrorMessage = "工作职务必填")]
        public string JobName { get; set; }
        [Required(ErrorMessage = "起始时间必填")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "结束时间必填")]
        public DateTime EndTime { get; set; }
    }
}