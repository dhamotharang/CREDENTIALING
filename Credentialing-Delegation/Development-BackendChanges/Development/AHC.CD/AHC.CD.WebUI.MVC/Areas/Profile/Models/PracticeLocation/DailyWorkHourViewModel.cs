using AHC.CD.Entities.MasterData.Enums;
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

       
        public string StartTimePeriod { get; private set; }

        [Required]
        [Display(Name = "AM/PM")]
        public TimePeriod StartTimeAMPM { get; set; }
        

        [Required]
        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        public string EndTimePeriod { get; private set; }

        [Required]
        [Display(Name = "AM/PM")]
        public TimePeriod EndTimeAMPM;

    }
}