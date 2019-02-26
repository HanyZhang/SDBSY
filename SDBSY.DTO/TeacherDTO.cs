using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class TeacherDTO:BaseDTO
    {
        public long AdminUserId { get; set; }
        //public string AdminUserName { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public long IdCardTypeId { get; set; }
        public string IdCardTypeName { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdCardNum { get; set; }
        public bool Gender { get; set; }
        public string GenderName { get; set; }
        public string TelPhone { get; set; }
        public string HuKouPlace { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string PriginPlace { get; set; }
        public string HomePlace { get; set; }
        /// <summary>
        /// 教职工类别
        /// </summary>
        public long TeacherTypeId { get; set; }
        public string TeacherTypeName { get; set; }
        /// <summary>
        /// 任职岗位
        /// </summary>
        public long PostTypeId { get; set; }
        public string PostTypeName { get; set; }
        /// <summary>
        /// 园长资格
        /// </summary>
        public bool IsLeader { get; set; }
        public string IsLeaderStr { get; set; }
        /// <summary>
        /// 园长资格认证单位
        /// </summary>
        public string LeaderAuthUnit { get; set; }
        /// <summary>
        /// 是否幼教专业
        /// </summary>
        public bool IsPreschoolEducation { get; set; }
        public string IsPreschoolEducationStr { get; set; }
        public long ZhiChengId { get; set; }
        public string ZhiChengName { get; set; }
        /// <summary>
        /// 教师类型（雇佣关系）
        /// </summary>
        public long EmployTypeId { get; set; }
        public string EmployTypeName { get; set; }
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BasePay { get; set; }
        public string BasePayStr { get; set; } 
        public bool IsYangLao { get; set; }
        public string IsYangLaoStr { get; set; }
        public bool IsYiLiao { get; set; }
        public string IsYiLiaoStr { get; set; }
        public bool IsShiYe { get; set; }
        public string IsShiYeStr { get; set; }
        public bool IsGongShang { get; set; }
        public string IsGongShangStr { get; set; }
        public bool IsShengYu { get; set; }
        public string IsShengYuStr { get; set; }
        public bool IsGongJiJin { get; set; }
        public string IsGongJiJinStr { get; set; }
        public string WeiGouMai { get; set; }
        /// <summary>
        /// 来园工作时间
        /// </summary>
        public DateTime ComeDate { get; set; }
        public string ComeDateStr { get; set; }
        /// <summary>
        /// 任职开始时间
        /// </summary>
        public DateTime? WorkStartTime { get; set; }
        public string WorkStartTimeStr { get; set; }
        /// <summary>
        /// 任职结束时间
        /// </summary>
        public DateTime? WorkEndTime { get; set; }
        public string WorkEndTimeStr { get; set; }
        /// <summary>
        /// 是否取得教师资格证
        /// </summary>
        public bool HasTeacherCard { get; set; }
        public string HasTeacherCardStr { get; set; }
        /// <summary>
        /// 教师资格证种类
        /// </summary>
        public long? TeacherCardTypeId { get; set; }
        public string TeacherCardTypeName { get; set; }
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
        
        public string TeacherCardAwardTimeStr { get; set; }

        /// <summary>
        /// 是否中共党员
        /// </summary>
        public bool IsPartyMember { get; set; }
        public string IsPartyMemberStr { get; set; }
        public string SchoolName { get; set; }
        public long HighestEducationId { get; set; }
        public string HighestEducationName { get; set; }
    }
}
