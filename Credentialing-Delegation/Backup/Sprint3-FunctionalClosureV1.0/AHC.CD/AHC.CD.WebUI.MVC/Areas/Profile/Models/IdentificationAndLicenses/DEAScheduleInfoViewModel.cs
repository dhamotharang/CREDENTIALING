using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class DEAScheduleInfoViewModel
    {
        public int DEAScheduleInfoID { get; set; }

        #region DEASchedule

        [Required]
        public int DEAScheduleID { get; set; }
        
    //    public DEAScheduleViewModel DEASchedule { get; set; }

        #endregion

        #region IsEligible

        [Display(Name = "Eligibility *")]
        public string IsEligible { get; set; }

        [Required]
        [Display(Name = "Eligibility*")]
        public YesNoOption YesNoOption { get; set; }
        #endregion
    }
}