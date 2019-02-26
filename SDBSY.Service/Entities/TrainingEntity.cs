#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.Service.Entities
* 项目描述 ：
* 类 名 称 ：CultivateEntity
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.Service.Entities
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2018-12-27 13:27:20
* 更新时间 ：2018-12-27 13:27:20
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

namespace SDBSY.Service.Entities
{
    public class TrainingEntity : BaseEntity//培训
    {
        public long TeacherId { get; set; }
        public virtual TeacherEntity Teacher { get; set; }
        public string Year { get; set; }
        /// <summary>
        /// 培训机构名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 培训项目
        /// </summary>
        public string TrainingContent { get; set; }
        public long TrainingLevelId { get; set; }
        public virtual DataDictionaryEntity TrainingLevel { get; set; }
        public long TrainingTypeId { get; set; }
        public virtual DataDictionaryEntity TrainingType { get; set; }
        /// <summary>
        /// 培训学时
        /// </summary>
        public string TrainingTime { get; set; }
    }
}
