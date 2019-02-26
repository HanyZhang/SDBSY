using SDBSY.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Service.Configs
{
    class FoodConfig: EntityTypeConfiguration<FoodEntity>
    {
        public FoodConfig()
        {
            ToTable("T_Foods");
        }
    }
}
