using SDBSY.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDBSY.Web.Models
{
    public class EditInfoViewModel
    {

        public StudentDTO Student { get; set; }
        public DataDictionaryDTO[] Classes { get; set; }
        public DataDictionaryDTO[] IdCardTypes { get; set; }
        public DataDictionaryDTO[] BloodTypes { get; set; }//血型
        public CountryDTO[] Countrties { get; set; }
        public NationDTO[] Nations { get; set; }
        /// <summary>
        /// 港澳台侨外
        /// </summary>
        public DataDictionaryDTO[] Identities { get; set; }
        public NewPlaceDTO[] Places { get; set; }
        public DataDictionaryDTO[] HuKouXingZhi { get; set; }
        public DataDictionaryDTO[] FeiNongHuKouTypes { get; set; }
        public DataDictionaryDTO[] StudyTypes { get; set; }
        public DataDictionaryDTO[] IsStayAtHome { get; set; }
        public DataDictionaryDTO[] HealthyTypes { get; set; }
        public DataDictionaryDTO[] DisabilityTypes { get; set; }
        public DataDictionaryDTO[] AdultIdCardTypes { get; set; }
    }
}