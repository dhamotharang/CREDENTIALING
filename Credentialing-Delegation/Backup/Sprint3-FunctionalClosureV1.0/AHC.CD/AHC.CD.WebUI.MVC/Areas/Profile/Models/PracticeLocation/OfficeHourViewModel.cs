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

        [Required]
        [Display(Name = "24/7 Phone Coverage? *")]
        public string AnyTimePhoneCoverage { get; private set; }

        //[NotMapped]
        //public YesNoOption AnyTimePhoneCoverageYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AnyTimePhoneCoverage);
        //    }
        //    set
        //    {
        //        this.AnyTimePhoneCoverage = value.ToString();
        //    }
        //}

        #endregion

        public ICollection<DailyWorkHourViewModel> DailyWorkHours { get; set; }
    }
}