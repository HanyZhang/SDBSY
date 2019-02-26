#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.DTO
* 项目描述 ：
* 类 名 称 ：GoodsBuyRecordDTO
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.DTO
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2019-02-26 15:59:41
* 更新时间 ：2019-02-26 15:59:41
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
    public class GoodsBuyRecordDTO:BaseDTO
    {
        public long GoodsId { get; set; }
        public string GoodsName { get; set; }
        public DateTime BuyTime { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
