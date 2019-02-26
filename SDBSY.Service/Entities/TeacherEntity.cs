using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Entities
{
    public class TeacherEntity:BaseEntity
    {
        public long AdminUserId { get; set; }
        //public virtual AdminUserEntity AdminUser { get; set; }
        //public long TeacherSigninId { get; set; }
        public virtual AdminUserEntity AdminUser { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public bool Gender { get; set; } = false;
        public string TelPhone { get; set; }
        /// <summary>
        /// 身份证件类型
        /// </summary>
        public long IdCardTypeId { get; set; }
        public virtual DataDictionaryEntity IdCardType { get; set; }
        public string IdCardNum { get; set; }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 最高学历
        /// </summary>
        public long HighestEducationId { get; set; }
        public virtual  DataDictionaryEntity HighestEducation { get; set; }
        //public string HuKouPlace { get; set; }
        ///// <summary>
        ///// 籍贯
        ///// </summary>
        //public string PriginPlace { get; set; }
        /// <summary>
        /// 现住址，联系地址
        /// </summary>
        public string HomePlace { get; set; }
        /// <summary>
        /// 教职工类别
        /// </summary>
        public long TeacherTypeId { get; set; }
        public virtual  DataDictionaryEntity TeacherType { get; set; }
        /// <summary>
        /// 任职岗位
        /// </summary>
        public long PostTypeId { get; set; }
        public virtual DataDictionaryEntity PostType { get; set; }
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
        public long ZhiChengId { get; set; }
        public virtual  DataDictionaryEntity ZhiCheng { get; set; }
        /// <summary>
        /// 教师类型（雇佣关系）
        /// </summary>
        public long EmployTypeId { get; set; }
        public DataDictionaryEntity EmployType { get; set; }
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
        public DateTime ComeDate { get; set; }
        /// <summary>
        /// 任职开始时间
        /// </summary>
        public DateTime? WorkStartTime { get; set; }
        /// <summary>
        /// 任职结束时间
        /// </summary>
        public  DateTime? WorkEndTime { get; set; }
        /// <summary>
        /// 是否取得教师资格证
        /// </summary>
        public bool HasTeacherCard { get; set; }
        /// <summary>
        /// 教师资格证种类
        /// </summary>
        public long? TeacherCardTypeId { get; set; }
        public virtual DataDictionaryEntity TeacherCardType { get; set; }
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
        
        //public bool IsOfficial { get; set; } = false;
        public bool IsPartyMember { get; set; } = false;
        public virtual ICollection<WorkEntity> Works { get; set; } = new List<WorkEntity>();
        public virtual ICollection<EducateEntity> Educates { get; set; } = new List<EducateEntity>();
        public virtual ICollection<CertificateEntity> Certificates { get; set; } = new List<CertificateEntity>();
    }
}
