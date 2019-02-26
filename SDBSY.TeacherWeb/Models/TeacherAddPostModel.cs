using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.TeacherWeb.Models
{
    public class TeacherAddPostModel
    {
        public long AdminUserId { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Name { get; set; }

        [DisplayName("出生日期")]
        [Required(ErrorMessage = "{0}不能为空")]
        public DateTime BirthDay { get; set; }

        public bool Gender { get; set; } 

        [DisplayName("联系电话")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string TelPhone { get; set; }

        /// <summary>
        /// 身份证件类型
        /// </summary>
        [DisplayName("身份证件类型")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long IdCardTypeId { get; set; }

        public string IdCardNum { get; set; }

        /// <summary>
        /// 毕业学校
        /// </summary>
        [DisplayName("毕业学校")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string SchoolName { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        [DisplayName("最高学历")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long HighestEducationId { get; set; }

        //public string HuKouPlace { get; set; }
        ///// <summary>
        ///// 籍贯
        ///// </summary>
        //public string PriginPlace { get; set; }


        /// <summary>
        /// 现住址，联系地址
        /// </summary>
        [DisplayName("联系地址")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string HomePlace { get; set; }

        /// <summary>
        /// 教职工类别
        /// </summary>
        [DisplayName("教职工类别")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long TeacherTypeId { get; set; }

        /// <summary>
        /// 任职岗位
        /// </summary>
        [DisplayName("任职岗位")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long PostTypeId { get; set; }

        /// <summary>
        /// 园长资格
        /// </summary>
        public bool IsLeader { get; set; }

        /// <summary>
        /// 园长资格认证单位
        /// </summary>
        public string LeaderAuthUnit { get; set; }

        /// <summary>
        /// 是否幼教专业
        /// </summary>
        public bool IsPreschoolEducation { get; set; }
        [DisplayName("职称类型")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long ZhiChengId { get; set; }

        /// <summary>
        /// 教师类型（雇佣关系）
        /// </summary>
        [DisplayName("教师类型")]
        [Required(ErrorMessage = "{0}不能为空")]
        public long EmployTypeId { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BasePay { get; set; }

        public bool IsYangLao { get; set; }
        public bool IsYiLiao { get; set; }
        public bool IsShiYe { get; set; }
        public bool IsGongShang { get; set; }
        public bool IsShengYu { get; set; }
        public bool IsGongJiJin { get; set; }

        /// <summary>
        /// 来园工作时间，进本园时间
        /// </summary>
        [DisplayName("进园时间")]
        [Required(ErrorMessage = "{0}不能为空")]
        public DateTime ComeDate { get; set; }

        /// <summary>
        /// 任职开始时间
        /// </summary>
        public DateTime? WorkStartTime { get; set; }

        /// <summary>
        /// 任职结束时间
        /// </summary>
        public DateTime? WorkEndTime { get; set; }

        /// <summary>
        /// 是否取得教师资格证
        /// </summary>
        public bool HasTeacherCard { get; set; }

        /// <summary>
        /// 教师资格证种类
        /// </summary>
        public long? TeacherCardTypeId { get; set; }

        /// <summary>
        /// 教师资格证号码
        /// </summary>
        public string TeacherCardNum { get; set; }

        /// <summary>
        /// 教师资格证书颁发机构
        /// </summary>
        public string TeacherCardAwardUnit { get; set; }

        /// <summary>
        /// 教师资格证颁发时间
        /// </summary>
        public DateTime? TeacherCardAwardTime { get; set; }

        /// <summary>
        /// 是否党员
        /// </summary>
        public bool IsPartyMember { get; set; } 

    }
}