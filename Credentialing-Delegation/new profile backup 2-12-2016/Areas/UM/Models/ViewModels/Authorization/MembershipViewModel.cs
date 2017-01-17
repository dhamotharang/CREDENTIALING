using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class MembershipViewModel
    {
        public int MembershipID { get; set; }

        public string PlanName { get; set; }

        public DateTime? MemberEffectiveDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public DateTime? PlanEffectiveDate { get; set; }
    }
}