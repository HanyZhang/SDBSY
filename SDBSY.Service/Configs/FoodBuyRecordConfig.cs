using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class FoodBuyRecordConfig: EntityTypeConfiguration<FoodBuyRecordEntity>
    {
        public FoodBuyRecordConfig()
        {
            ToTable("T_FoodBuyRecords");
            HasRequired(t => t.Food).WithMany(t => t.FoodBuyRecords).HasForeignKey(t => t.FoodId).WillCascadeOnDelete(false);
        }
    }
}
