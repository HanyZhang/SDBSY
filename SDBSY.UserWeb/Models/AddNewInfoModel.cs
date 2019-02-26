using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDBSY.UserWeb.Models
{
    public class AddNewInfoModel
    {

        public long? UserId { get; set; }
        public long? ClassId { get; set; }
        [Required(ErrorMessage = "姓名必填")]
        public string Name { get; set; }
        public bool Gender { get; set; }
        [Required(ErrorMessage = "出生日期必填")]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 幼儿证件类型
        /// </summary>
        public long IdCardTypeId { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdCardNum { get; set; }
        public long BloodTypeId { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public long CountryId { get; set; }
        public long? NationId { get; set; }
        /// <summary>
        /// 港澳台侨外
        /// </summary>
        public long IdentityId { get; set; }
        /// <summary>
        /// 出生地Id
        /// </summary>
        public long? BirthPlaceId { get; set; }
        [Required(ErrorMessage = "籍贯必填")]
        /// <summary>
        /// 籍贯
        /// </summary>
        public string PriginPlace { get; set; }
        public long? HuKouXingZhiId { get; set; }
        public long? FeiNongHuKouTypeId { get; set; }
        public long? HuKouPlaceId { get; set; }
        [Required(ErrorMessage = "家庭住址必填")]
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string HomePlace { get; set; }
        /// <summary>
        /// 就读方式
        /// </summary>
        public long StudyTypeId { get; set; }
        /// <summary>
        /// 是否独生子女
        /// </summary>
        public bool IsOnlyBaby { get; set; }
        /// <summary>
        /// 是否留守儿童，
        /// </summary>
        public long IsStayAtHomeId { get; set; }
        /// <summary>
        /// 是否进城务工子女
        /// </summary>
        public bool IsFollowWorkInCity { get; set; }
        public long HealthyTypeId { get; set; }
        /// <summary>
        /// 是否残疾
        /// </summary>
        public bool IsDisability { get; set; }
        /// <summary>
        /// 残疾类型
        /// </summary>
        public long? DisabilityTypeId { get; set; }
        /// <summary>
        /// 是否孤儿
        /// </summary>
        public bool IsOrphan { get; set; }
        [Required(ErrorMessage = "监护人姓名必填")]
        public string GuardianName { get; set; }
        /// <summary>
        /// 监护人证件类型
        /// </summary>
        public long GuardianCardTypeId { get; set; }
        [Required(ErrorMessage = "监护人证件号码必填")]
        public string GuardianCardNum { get; set; }
        public string FatherName { get; set; }
        public long FatherCardTypeId { get; set; }
        public string FatherCardNum { get; set; }
        public string FatherWorkUnit { get; set; }
        public string FatherPhoneNum { get; set; }
        public string MotherName { get; set; }
        public long MotherCardTypeId { get; set; }
        public string MotherCardNum { get; set; }
        public string MotherWorkUnit { get; set; }
        public string MotherPhoneNum { get; set; }
        [Required(ErrorMessage = "其他联系方式必填")]
        public string OtherTel { get; set; }
        public string HasInfection { get; set; }
        [Required(ErrorMessage = "银行卡号必填")]
        /// <summary>
        /// 幼儿姓名开户的建设银行卡号
        /// </summary>
        public string BankCardNum { get; set; }
        [Required(ErrorMessage = "请上传幼儿照片")]
        public string PhotoUrl { get; set; }
        [Required(ErrorMessage = "请上传全家福照片")]
        public string TijianUrl { get; set; }
    }
}