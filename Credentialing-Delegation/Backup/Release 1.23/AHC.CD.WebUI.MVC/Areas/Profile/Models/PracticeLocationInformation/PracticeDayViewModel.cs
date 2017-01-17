using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticeDayViewModel
    {
        public int PracticeDayID { get; set; }

        
        public string DayName { get; set; }

        public DaysOfWeek? DayOfWeek { get; set; }

        public string DayOff { get; set; }

        public ICollection<PracticeDailyHourViewModel> DailyHours { get; set; }
    }
}