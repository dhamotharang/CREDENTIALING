using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CodingList
{
    public class CodingListCountsViewModel
    {
        public int OpenEncountersCount { get; set; }
        public int OnHoldEncountersCount { get; set; }
        public int ReadytoAuditEncountersCount { get; set; }
        public int DraftEncountersCount { get; set; }
        public int InactiveEncountersCount { get; set; }
        public int RejectedEncountersCount { get; set; }
    }
}