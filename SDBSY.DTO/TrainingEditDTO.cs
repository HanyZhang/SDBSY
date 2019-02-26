#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.DTO
* 项目描述 ：
* 类 名 称 ：TrainingEditDTO
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.DTO
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2018-12-28 16:34:02
* 更新时间 ：2018-12-28 16:34:02
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
    public class TrainingEditDTO
    {
        public long Id { get; set; }
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
       
        public long TrainingTypeId { get; set; }
        /// <summary>
        /// 培训学时
        /// </summary>
        public string TrainingTime { get; set; }
    }
}
