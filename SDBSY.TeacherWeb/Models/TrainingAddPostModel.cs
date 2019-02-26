using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.TeacherWeb.Models
{
    public class TrainingAddPostModel
    {
        public long TeacherId { get; set; }
        [DisplayName("培训年度")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Year { get; set; }

        /// <summary>
        /// 培训机构名称
        /// </summary>
        [DisplayName("培训机构")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string UnitName { get; set; }
        /// <summary>
        /// 培训项目
        /// </summary>
        [DisplayName("培训项目")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string TrainingContent { get; set; }
        [DisplayName("培训级别")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long TrainingLevelId { get; set; }
        [DisplayName("培训形式")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long TrainingTypeId { get; set; }
        /// <summary>
        /// 培训学时
        /// </summary>
        [DisplayName("培训学时")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string TrainingTime { get; set; }
    }
}