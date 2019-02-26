using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDBSY.DTO;

namespace SDBSY.TeacherWeb.Models
{
    public class TrainingEditViewModel
    {
        public TrainingDTO Training { get; set; }
        public string TrainingLevels { get; set; }
        public string TrainingTypes { get; set; }
    }
}