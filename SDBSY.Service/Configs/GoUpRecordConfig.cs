using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class GoUpRecordConfig: EntityTypeConfiguration<GoUpRecordEntity>
    {
        public GoUpRecordConfig()
        {
            ToTable("T_GoUpRecords");
            HasRequired(t => t.Student).WithMany().HasForeignKey(t => t.StudentId).WillCascadeOnDelete(false);
        }
    }
}
