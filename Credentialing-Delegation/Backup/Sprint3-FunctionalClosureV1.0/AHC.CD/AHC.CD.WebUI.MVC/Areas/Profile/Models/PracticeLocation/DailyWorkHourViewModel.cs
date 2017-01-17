using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class DailyWorkHourViewModel
    {
        public int DailyWorkHourID { get; set; }

        [Required]
        [Display(Name = "Day")]
        public string DayName { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public string StartTime { get; set; }

        #region StartTimePeriod

        [Required]
        [Display(Name = "AM/PM")]
        public string StartTimePeriod { get; private set; }

        //[NotMapped]
        //public TimePeriod StartTimeAMPM
        //{
        //    get
        //    {
        //        return (TimePeriod)Enum.Parse(typeof(TimePeriod), this.StartTimePeriod);
        //    }
        //    set
        //    {
        //        this.StartTimePeriod = value.ToString();
        //    }
        //}

        #endregion

        [Required]
        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        #region EndTimePeriod

        [Required]
        [Display(Name = "AM/PM")]
        public string EndTimePeriod { get; private set; }

        //[NotMapped]
        //public TimePeriod EndTimeAMPM
        //{
        //    get
        //    {
        //        return (TimePeriod)Enum.Parse(typeof(TimePeriod), this.EndTimePeriod);
        //    }
        //    set
        //    {
        //        this.EndTimePeriod = value.ToString();
        //    }
        //}

        #endregion
    }
}