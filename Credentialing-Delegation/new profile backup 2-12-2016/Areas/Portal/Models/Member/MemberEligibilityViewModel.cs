using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberEligibilityViewModel
    {
        [Display(Name = "EffectiveDateEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EffectiveDate { get; set; }

        [Display(Name = "TermDateEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TermDate { get; set; }

        [Display(Name = "CategoryEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Category { get; set; }

        [Display(Name = "PlanIDEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanID { get; set; }

        [Display(Name = "PlanNameEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanName { get; set; }

        [Display(Name = "ReasonEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Reason { get; set; }

        [Display(Name = "SubsidyEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Subsidy { get; set; }

         [Display(Name = "ExplanationEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Explanation { get; set; }

         [Display(Name = "EventEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Event { get; set; }

         [Display(Name = "VoidEventEligibility", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
         public string Type { get; set; }
    }
}
