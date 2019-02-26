#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：SDBSY.Service.Entities
* 项目描述 ：
* 类 名 称 ：GoodsEntity
* 类 描 述 ：
* 所在的域 ：DESKTOP-6IEC48T
* 命名空间 ：SDBSY.Service.Entities
* 机器名称 ：DESKTOP-6IEC48T 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：Hany
* 创建时间 ：2019-02-26 15:15:23
* 更新时间 ：2019-02-26 15:15:23
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

namespace SDBSY.Service.Entities
{
    public class GoodsEntity:BaseEntity
    {
        public string Name { get; set; }//名称
        public string Unit { get; set; }//计量单位
        public string Format { get; set; }//规格
        public string Seller { get; set; }//卖家
        public string Maker { get; set; }//制造商
        public string ImgUrl { get; set; }//图片
        public virtual ICollection<GoodsBuyRecordEntity> GoodsBuyRecords { get; set; } = new List<GoodsBuyRecordEntity>();
        public virtual ICollection<GoodsApplyRecordEntity> GoodsApplyRecords { get; set; }=new List<GoodsApplyRecordEntity>();
    }
}
