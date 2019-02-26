#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.DTO
* 项目描述 ：
* 类 名 称 ：TeacherEditDTO
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.DTO
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2018-12-26 8:41:25
* 更新时间 ：2018-12-26 8:41:25
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Hany 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class TeacherEditDTO
    {
        public long Id { get; set; }
        //public string AdminUserName { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public long IdCardTypeId { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string IdCardNum { get; set; }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public long HighestEducationId { get; set; }
        public bool Gender { get; set; }
        public string TelPhone { get; set; }
        public string HomePlace { get; set; }
        /// <summary>
        /// 教职工类别
        /// </summary>
        public long TeacherTypeId { get; set; }
        /// <summary>
        /// 任职岗位
        /// </summary>
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
        public long ZhiChengId { get; set; }
        /// <summary>
        /// 教师类型（雇佣关系）
        /// </summary>
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
        /// 来园工作时间
        /// </summary>
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
        /// 是否中共党员
        /// </summary>
        public bool IsPartyMember { get; set; }
    }
}
