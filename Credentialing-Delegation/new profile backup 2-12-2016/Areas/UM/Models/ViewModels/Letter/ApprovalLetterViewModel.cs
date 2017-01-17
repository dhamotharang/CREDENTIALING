using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Letter
{
    public class ApprovalLetterViewModel
    {

        public int? AuthorizationID { get; set; }

        public string AuthorizationNumber { get; set; }

        public string PlanLogo { get; set; }

        public string PlanAddress1 { get; set; }

        public string PlanAddress2 { get; set; }

        public string PlanContact { get; set; }

        public string MemberID { get; set; }

        public string MemberFirstName { get; set; }

        public string MemberLastName { get; set; }

        public string MemberAddress1 { get; set; }

        public string MemberAddress2 { get; set; }

        public DateTime? RequestedDate { get; set; }

        public string ServiceRequested { get; set; }

        public string RequestedBy { get; set; }

        public string ServiceProvider { get; set; }

        public string FacilityName { get; set; }

        public DateTime? DateOfPlanDecision { get; set; }

    }
}