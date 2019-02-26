#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.DTO
* 项目描述 ：
* 类 名 称 ：InvoiceDTO
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.DTO
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2019-01-09 10:24:37
* 更新时间 ：2019-01-09 10:24:37
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Hany 2019. All rights reserved.
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
    public class InvoiceDTO:BaseDTO
    {
        public long TeacherId { get; set; }//购买人
        public string TeacherName { get; set; }
        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public string GoodsName { get; set; }//物品名称
        public DateTime BuyDateTime { get; set; }//购买日期
        public string BuyDateTimeStr { get; set; }
        public decimal Total { get; set; }
        public string TotalStr { get; set; }
        public string Detail { get; set; }//明细
        public int Status { get; set; }
        public  string StatusStr { get; set; }
         public  string NoPassReason { get; set; }
    }
}
