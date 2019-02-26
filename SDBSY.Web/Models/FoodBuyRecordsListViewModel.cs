using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class FoodBuyRecordsListViewModel
    {
        public long FoodId { get; set; }
        public FoodDTO[] Foods { get; set; }
        public FoodBuyRecordDTO[] Records  {get;set;}
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        
    }
}