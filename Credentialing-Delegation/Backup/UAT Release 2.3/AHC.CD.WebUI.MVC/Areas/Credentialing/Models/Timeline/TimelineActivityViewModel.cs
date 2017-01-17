using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Timeline
{
    public class TimelineActivityViewModel
    {
        public int TimelineActivityID { get; set; }

        public int? ActivityByID { get; set; }

        public string Activity { get; set; }
    }
}