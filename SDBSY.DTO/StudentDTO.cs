using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class StudentDTO : BaseDTO
    {
        [DisplayName("姓名")]
        public string Name { get; set; }
        public bool Gender { get; set; }
        [DisplayName("性别")]
        public string GenderName { get; set; }
        public long? ClassId { get; set; }
        [DisplayName("班级")]
        public string ClassName { get; set; }
        /// <summary>
        /// 是否需要留班（留级）
        /// </summary>
        public bool IsStayInClass { get; set; }
        /// <summary>
        /// 是否已毕业
        /// </summary>
        public bool IsFinishSchool { get; set; }
        public DateTime? FinishSchoolTime { get; set; }
        [DisplayName("出生日期")]
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 幼儿证件类型
        /// </summary>
        public long IdCardTypeId { get; set; }
        [DisplayName("证件类型")]
        public string IdCardTypeName { get; set; }

        [DisplayName("证件号码")]
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdCardNum { get; set; }
        public long BloodTypeId { get; set; }
        [DisplayName("血型")]
        public string BloodTypeName { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public long CountryId { get; set; }
        [DisplayName("国籍")]
        public string CountryName { get; set; }
        public long? NationId { get; set; }
        [DisplayName("民族")]
        public string NationName { get; set; }
        /// <summary>
        /// 港澳台侨外
        /// </summary>
        public long IdentityId { get; set; }
        [DisplayName("是否港澳台侨外")]
        public string IdentityName { get; set; }
        /// <summary>
        /// 出生地Id
        /// </summary>
        public long? BirthPlaceId { get; set; }
        [DisplayName("出生地代码")]
        public string BirthPlaceCode { get; set; }
        [DisplayName("出生地名称")]
        public string BirthPlaceName { get; set; }
        [DisplayName("籍贯")]
        /// <summary>
        /// 籍贯
        /// </summary>
        public string PriginPlace { get; set; }
        public long? HuKouXingZhiId { get; set; }
        [DisplayName("户口性质")]
        public string HuKouXingZhiName { get; set; }
        public long? FeiNongHuKouTypeId { get; set; }
        [DisplayName("非农户口类型")]
        public string FeiNongHuKouTypeName { get; set; }
        public long? HuKouPlaceId { get; set; }
        [DisplayName("户口所在地代码")]
        public string  HuKouPlaceCode { get; set; }
        [DisplayName("户口所在地名称")]
        public string HuKouPlaceName { get; set; }
        [DisplayName("家庭住址")]
        public string HomePlace { get; set; }
        [DisplayName("入园时间")]
        /// <summary>
        /// 入园时间，学校填写？
        /// </summary>
        public DateTime InKindergartenDate { get; set; }
        /// <summary>
        /// 就读方式
        /// </summary>
        public long StudyTypeId { get; set; }
        [DisplayName("就读方式")]
        public string StudyTypeName { get; set; }
        [DisplayName("是否独生子女")]
        /// <summary>
        /// 是否独生子女
        /// </summary>
        public bool IsOnlyBaby { get; set; }
        /// <summary>
        /// 是否留守儿童，
        /// </summary>
        public long IsStayAtHomeId { get; set; }
        [DisplayName("是否留守儿童")]
        public string IsStayAtHomeName { get; set; }
        [DisplayName("是否进城务工子女")]
        /// <summary>
        /// 是否进城务工子女
        /// </summary>
        public bool IsFollowWorkInCity { get; set; }
        public long HealthyTypeId { get; set; }
        [DisplayName("健康状况")]
        public string  HealthyTypeName { get; set; }
        [DisplayName("是否残疾")]
        /// <summary>
        /// 是否残疾
        /// </summary>
        public bool IsDisability { get; set; }
        /// <summary>
        /// 残疾类型
        /// </summary>
        public long? DisabilityTypeId { get; set; }
        [DisplayName("残疾类型")]
        public string DisabilityTypeName { get; set; }
        [DisplayName("是否孤儿")]
        /// <summary>
        /// 是否孤儿
        /// </summary>
        public bool IsOrphan { get; set; }
        /// <summary>
        /// 监护人Id
        /// </summary>
        public long GuardianId { get; set; }
        [DisplayName("监护人姓名")]
        public string GuardianName { get; set; }
        public long GuardianCardTypeId { get; set; }
        [DisplayName("监护人证件类型")]
        public string GuardianCardTypeName { get; set; }
        [DisplayName("监护人证件号码")]
        public string GuardianCardNum { get; set; }
        public long? FatherId { get; set; }
        [DisplayName("父亲姓名")]
        public string FatherName { get; set; }
        [DisplayName("父亲工作单位")]
        public string FatherWorkUnit { get; set; }
        [DisplayName("父亲电话号码")]
        public string FatherPhoneNum { get; set; }
        public long? FatherCardTypeId { get; set; }
        [DisplayName("父亲证件类型")]
        public string FatherCardTypeName { get; set; }
        [DisplayName("父亲证件号码")]
        public string FatherCardNum { get; set; }
        public long? MotherId { get; set; }
        [DisplayName("母亲姓名")]
        public string MotherName { get; set; }
        [DisplayName("母亲工作单位")]
        public string MotherWorkUnit { get; set; }
        [DisplayName("母亲电话号码")]
        public string MotherPhoneNum { get; set; }
        public long? MotherCardTypeId { get; set; }
        [DisplayName("母亲证件类型")]
        public string MotherCardTypeName { get; set; }
        [DisplayName("母亲证件号码")]
        public string MotherCardNum { get; set; }
        [DisplayName("其他联系电话")]
        public string OtherTel { get; set; }
        [DisplayName("有无传染病史")]
        /// <summary>
        /// 传染病史
        /// </summary>
        public string HasInfection { get; set; }
        [DisplayName("银行卡号")]
        /// <summary>
        /// 幼儿姓名开户的建设银行卡号
        /// </summary>
        public string BankCardNum { get; set; }
        [DisplayName("幼儿照片")]
        public string PhotoUrl { get; set; }
        [DisplayName("幸福一家人")]
        public string TijianUrl { get; set; }
        public long? UserId { get; set; }
        public string UserPhoneNum { get; set; }//用户登录手机号
        public int Status { get; set; }
    }
}
