using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class EducateConfig: EntityTypeConfiguration<EducateEntity>
    {
        public EducateConfig() {
            ToTable("T_Educates");
            HasRequired(t => t.Teacher).WithMany(t => t.Educates).HasForeignKey(t => t.TeacherId).WillCascadeOnDelete(false);
        }
    }
}
