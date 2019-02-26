using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class CertificateConfig : EntityTypeConfiguration<CertificateEntity>
    {
        public CertificateConfig()
        {
            ToTable("T_Certificates");
            HasRequired(t => t.Teacher).WithMany(t => t.Certificates).HasForeignKey(t => t.TeacherId).WillCascadeOnDelete(false);
            //Property(p => p.ThumbUrl).HasMaxLength(1024);
            //Property(p => p.Url).HasMaxLength(1024);
        }
    }
}
