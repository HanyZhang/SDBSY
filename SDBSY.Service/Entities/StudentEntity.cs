using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
   public class StudentEntity:BaseEntity
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        public long? ClassId { get; set; }
        public virtual DataDictionaryEntity Class { get; set; }
        /// <summary>
        /// 是否需要留班（留级）
        /// </summary>
        public bool IsStayInClass { get; set; }
        /// <summary>
        /// 是否已毕业
        /// </summary>
        public bool IsFinishSchool { get; set; } 
        public DateTime? FinishSchoolTime { get; set; }
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 幼儿证件类型
        /// </summary>
        public long IdCardTypeId { get; set; }
        public virtual DataDictionaryEntity IdCardType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdCardNum { get; set; }
        public long BloodTypeId { get; set; }
        public virtual DataDictionaryEntity BloodType { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public long CountryId { get; set; }
        public virtual CountryEntity Country { get; set; }
        public long? NationId { get; set; }
        public virtual NationEntity Nation { get; set; }
        /// <summary>
        /// 港澳台侨外
        /// </summary>
        public long IdentityId { get; set; }
        public virtual DataDictionaryEntity Identity { get; set; }
        /// <summary>
        /// 出生地Id
        /// </summary>
        public long? BirthPlaceId { get; set; }
        public virtual NewPlaceEntity BirthPlace { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string PriginPlace { get; set; }
        public long? HuKouXingZhiId { get; set; }
        public virtual DataDictionaryEntity HuKouXingZhi { get; set; }
        public long? FeiNongHuKouTypeId { get; set; }
        public virtual DataDictionaryEntity FeiNongHuKouType { get; set; }
        public long? HuKouPlaceId { get; set; }
        public virtual NewPlaceEntity HuKouPlace { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string HomePlace { get; set; }
        /// <summary>
        /// 入园时间，学校填写？
        /// </summary>
        public DateTime InKindergartenDate { get; set; }
        /// <summary>
        /// 就读方式
        /// </summary>
        public long StudyTypeId { get; set; }
        public virtual DataDictionaryEntity StudyType { get; set; }
        /// <summary>
        /// 是否独生子女
        /// </summary>
        public bool IsOnlyBaby { get; set; }
        /// <summary>
        /// 是否留守儿童，
        /// </summary>
        public long IsStayAtHomeId { get; set; }
        public virtual DataDictionaryEntity IsStayAtHome { get; set; }
        /// <summary>
        /// 是否进城务工子女
        /// </summary>
        public bool IsFollowWorkInCity { get; set; }
        public long HealthyTypeId { get; set; }
        public virtual DataDictionaryEntity HealthyType { get; set; }
        /// <summary>
        /// 是否残疾
        /// </summary>
        public bool IsDisability { get; set; }
        /// <summary>
        /// 残疾类型
        /// </summary>
        public long? DisabilityTypeId { get; set; }
        public virtual DataDictionaryEntity DisabilityType { get; set; }
        /// <summary>
        /// 是否孤儿
        /// </summary>
        public bool IsOrphan { get; set; }
        /// <summary>
        /// 监护人Id
        /// </summary>
        public long GuardianId { get; set; }
        public virtual GuardianEntity Guardian { get; set; }
        public long? FatherId { get; set; }
        public virtual ParentEntity Father { get; set; }
        public long? MotherId { get; set; }
        public virtual ParentEntity Mother { get; set; }
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
        public long? UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public int Status { get; set; }
    }
}
