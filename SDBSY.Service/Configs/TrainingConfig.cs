#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.Service.Configs
* 项目描述 ：
* 类 名 称 ：TrainingConfig
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.Service.Configs
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2018-12-27 13:38:12
* 更新时间 ：2018-12-27 13:38:12
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Hany 2018. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.Service.Entities;

namespace SDBSY.Service.Configs
{
    class TrainingConfig:EntityTypeConfiguration<TrainingEntity>
    {
        public TrainingConfig()
        {
            ToTable("T_Trainings");
        }
    }
}
