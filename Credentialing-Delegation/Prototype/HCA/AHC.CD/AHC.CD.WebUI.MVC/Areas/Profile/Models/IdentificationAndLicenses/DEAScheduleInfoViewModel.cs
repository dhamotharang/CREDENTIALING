using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class DEAScheduleInfoViewModel
    {
        public int DEAScheduleInfoID { get; set; }

        public DEAScheduleViewModel DEASchedule { get; set; }

        public ICollection<DEAScheduleTypeViewModel> DEAScheduleTypes { get; set; }
    }
}