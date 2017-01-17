using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.DashBoard
{
    public class CodedEncounterCountsViewModel
    {
        public int OpenCount { get; set; }
        public int OnHoldCount { get; set; }
        public int ReadytoAuditCount { get; set; }
        public int DraftCount { get; set; }
    }
}