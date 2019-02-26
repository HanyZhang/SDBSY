using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class StudentAddNewDTO
    {

        public string Name { get; set; }
        public long? ClassId { get; set; }
        public bool Gender { get; set; }
        /// <summary>
        /// 是否需要留班（留级）
        /// </summary>
        public bool IsStayInClass { get; set; } = false;
        /// <summary>
        /// 是否已毕业
        /// </summary>
        public bool IsFinishSchool { get; set; } = false;
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
        /// 入园时间，学校填写？
        /// </summary>
        public DateTime InKindergartenDate { get; set; }
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
        //public string GuardianPhoneNum { get; set; }
        /// <summary>
        /// 监护人证件类型
        /// </summary>
        //public long GuardianIdCardTypeId { get; set; }
        //public string GuardianIdCardNum { get; set; }
        public long? FatherId { get; set; }
        //public string FatherWorkUnit { get; set; }
        //public string FatherPhoneNum { get; set; }
        /// <summary>
        /// 父母证件类型
        /// </summary>
        //public long? FatherIdCardTypeId { get; set; }
        //public string FatherIdCardNum { get; set; }
        public long? MotherId { get; set; }
        //public string MotherWorkUnit { get; set; }
        //public string MotherPhoneNum { get; set; }
        /// <summary>
        /// 父母证件类型
        /// </summary>
        //public long? MotherIdCardTypeId { get; set; }
        //public string MotherIdCardNum { get; set; }
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
    }
}
