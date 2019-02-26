#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.DTO
* 项目描述 ：
* 类 名 称 ：GoodsApplyRecordDTO
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.DTO
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2019-02-26 16:04:52
* 更新时间 ：2019-02-26 16:04:52
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
    public class GoodsApplyRecordDTO:BaseDTO
    {
        public long GoodsId { get; set; }
        public string GoodsName { get; set; }
        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public long TeacherId { get; set; }
        public string TeacherName { get; set; }
        public decimal Amount { get; set; }//数量
        public int Status { get; set; }//审核状态
        public string NoPassReason { get; set; }//审核不通过原因
        public DateTime? ApplyTime { get; set; }//领取时间
        public DateTime? ReturnTime { get; set; }//归还时间
    }
}
