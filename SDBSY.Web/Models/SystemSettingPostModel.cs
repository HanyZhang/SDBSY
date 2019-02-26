using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class SystemSettingPostModel
    {
        /// <summary>
        /// 功能开放、关闭
        /// </summary>
        public string SystemOpen { get; set; }
        /// <summary>
        /// 信息采集时是否可以选择班级
        /// </summary>
        public string ChooseClass { get; set; }
    }
}