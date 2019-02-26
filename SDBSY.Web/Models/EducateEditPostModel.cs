using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class EducateEditPostModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "起始时间必填")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "结束时间必填")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "学校名称必填")]
        public string SchoolName { get; set; }
        /// <summary>
        /// 类型(学历教育1  培训2  其他3) 
        /// </summary>
        public int Type { get; set; }
    }
}