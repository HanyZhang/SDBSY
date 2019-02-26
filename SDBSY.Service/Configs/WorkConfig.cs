using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class WorkConfig: EntityTypeConfiguration<WorkEntity>
    {
        public WorkConfig()
        {
            ToTable("T_Works");
            HasRequired(t => t.Teacher).WithMany(t => t.Works).HasForeignKey(t => t.TeacherId).WillCascadeOnDelete(false);
        }
    }
}
