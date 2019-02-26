using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class StudentEditDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="姓名必填")]
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
        /// <summary>
        /// 籍贯
        /// </summary>
        public string PriginPlace { get; set; }
        public long? HuKouXingZhiId { get; set; }
        public long? FeiNongHuKouTypeId { get; set; }
        public long? HuKouPlaceId { get; set; }
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
        /// <summary>
        /// 监护人
        /// </summary>
        public long GuardianId { get; set; }
        public string GuardianName { get; set; }
        public string GuardianPhoneNum { get; set; }
        /// <summary>
        /// 监护人证件类型
        /// </summary>
        public long GuardianCardTypeId { get; set; }
        public string GuardianCardNum { get; set; }
        public long? FatherId { get; set; }
        public string FatherName { get; set; }
        public string FatherWorkUnit { get; set; }
        public string FatherPhoneNum { get; set; }
        /// <summary>
        /// 父母证件类型
        /// </summary>
        public long? FatherCardTypeId { get; set; }
        public string FatherCardNum { get; set; }
        public long? MotherId { get; set; }
        public string MotherName { get; set; }
        public string MotherWorkUnit { get; set; }
        public string MotherPhoneNum { get; set; }
        /// <summary>
        /// 父母证件类型
        /// </summary>
        public long? MotherCardTypeId { get; set; }
        public string MotherCardNum { get; set; }
        public string OtherTel { get; set; }
        /// <summary>
        /// 传染病史
        /// </summary>
        public string HasInfection { get; set; }
        /// <summary>
        /// 幼儿姓名开户的建设银行卡号
        /// </summary>
        public string BankCardNum { get; set; }
        public string PhotoUrl { get; set; }
        public string TijianUrl { get; set; }
    }
}
