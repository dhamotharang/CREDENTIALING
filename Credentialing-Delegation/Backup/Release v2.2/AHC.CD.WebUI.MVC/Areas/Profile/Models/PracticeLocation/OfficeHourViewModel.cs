using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class OfficeHourViewModel
    {
        public int OfficeHourID { get; set; }

        #region AnyTimePhoneCoverage

        
        public string AnyTimePhoneCoverage { get; private set; }

        [Required]
        [Display(Name = "24/7 Phone Coverage? *")]
        public YesNoOption AnyTimePhoneCoverageYesNoOption { get; set; }
        
        #endregion

        public ICollection<DailyWorkHourViewModel> DailyWorkHours { get; set; }
    }
}