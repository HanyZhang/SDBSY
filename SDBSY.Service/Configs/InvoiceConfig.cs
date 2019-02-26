#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.Service.Configs
* 项目描述 ：
* 类 名 称 ：InvoiceConfig
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.Service.Configs
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2019-01-09 10:18:21
* 更新时间 ：2019-01-09 10:18:21
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Hany 2019. All rights reserved.
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
    class InvoiceConfig:EntityTypeConfiguration<InvoiceEntity>
    {
        public InvoiceConfig()
        {
            ToTable("T_Invoices");
            HasRequired(t=>t.Teacher).WithMany().HasForeignKey(t=>t.TeacherId).WillCascadeOnDelete(false);
            HasRequired(t=>t.Class).WithMany().HasForeignKey(t=>t.ClassId).WillCascadeOnDelete(false);
            Property(t => t.Detail).IsRequired().HasMaxLength(1024);
            Property(t => t.GoodsName).IsRequired().HasMaxLength(100);
        }
    }
}
