using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class EncounterListCountViewModel
    {
        public int ScheduleCount { get; set; }
        public int ActiveCount { get; set; }
        public int OpenCount { get; set; }
        public int DraftCount { get; set; }
        public int ReadyToCodeCount { get; set; }
        public int InactiveCount { get; set; }
        public int RejectedCount { get; set; }
    }
}