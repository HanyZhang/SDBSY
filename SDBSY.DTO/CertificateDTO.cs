using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.DTO
{
    public class CertificateDTO:BaseDTO
    {
        public long TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 发证单位
        /// </summary>
        public string AwardUnit { get; set; }
        /// <summary>
        /// 发证时间
        /// </summary>
        public DateTime AwardDateTime { get; set; }
        public string AwardDateTimeStr { get; set; }
        public string Number { get; set; }
        //public string Url { get; set; }
        //public string ThumbUrl { get; set; }
    }
}
