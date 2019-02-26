using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDBSY.DTO;

namespace SDBSY.Service.Entities
{
    public class CertificateEntity:BaseEntity//证书,针对教育培训需要有证书
    {
        //public long? EducateId { get; set; }
        //public virtual EducateEntity Educate { get; set; }
        public long TeacherId { get; set; }
        public  virtual TeacherEntity Teacher { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 发证单位
        /// </summary>
        public string AwardUnit { get; set; }
        /// <summary>
        /// 发证时间
        /// </summary>
        public DateTime AwardDateTime { get; set; }
        public string Number { get; set; }
        //public string Url { get; set; }
        //public string ThumbUrl { get; set; }
    }
}
