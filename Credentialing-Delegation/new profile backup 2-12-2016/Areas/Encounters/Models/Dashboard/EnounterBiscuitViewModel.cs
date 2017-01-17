using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.Dashboard
{
    public class EnounterBiscuitViewModel
    {
        // Biscuits

        // Schedulers
        public int Scheduled { get; set; }
        public int Active { get; set; }
        public int NoShow { get; set; }
        public int ReSchedule { get; set; }
        // Encounter
        public int Open { get; set; }
        public int Draft { get; set; }
        public int ReadyToCode { get; set; }
        public int OnHold { get; set; }
    }
}