using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class ClaimHistoryViewModel
    {
        public string ClaimID { get; set; }

        public string EncounterID { get; set; }

        public string MemberFirstName { get; set; }

        public string MemberMiddleInitial { get; set; }

        public string MemberLastName { get; set; }

        public string ProviderFirstName { get; set; }

        public string ProviderLastName { get; set; }

        public string BillingProvider { get; set; }

        public DateTime? DateOfService { get; set; }

        public DateTime? DateOfCreation { get; set; }

        public string Age { get; set; }

        public string CreatedBy { get; set; }

        public string EncounterType { get; set; }

        public String Status { get; set; }
    }
}