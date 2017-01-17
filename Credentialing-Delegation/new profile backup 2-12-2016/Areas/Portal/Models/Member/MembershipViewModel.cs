using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MembershipViewModel
    {
        public int MembershipID { get; set; }

        [Display(Name = "PlanName", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanName { get; set; }

        [Display(Name = "StatusMember", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        [Display(Name = "SubGroup", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string SubGroup { get; set; }
       // public DateTime? MemberEffectiveDate { get; set; }

        [Display(Name = "MemberEffectiveDate", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberEffectiveDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public DateTime? PlanEffectiveDate { get; set; }
    }
}