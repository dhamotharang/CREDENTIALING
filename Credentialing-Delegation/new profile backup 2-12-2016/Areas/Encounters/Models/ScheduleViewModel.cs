using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class ScheduleViewModel : EncounterViewModel
    {
        public string ScheduleTime { get; set; }
        public string ScheduleDate { get; set; }
    }
}