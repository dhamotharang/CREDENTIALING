using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberRatingsOverrideViewModel
    {
        [Display(Name = "EffectiveDateOveride", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EffectiveDate { get; set; } //Have to be datetime

        [Display(Name = "PlanIDOveride", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanID { get; set; }

        [Display(Name = "IdentificationTypeOveride", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string IdentificationType { get; set; }

        [Display(Name = "UnitsOveride", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public int Units { get; set; }

        [Display(Name = "FactorsOveride", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Factors { get; set; }

        [Display(Name = "AmountOveride", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public double Amount { get; set; }
    }
}