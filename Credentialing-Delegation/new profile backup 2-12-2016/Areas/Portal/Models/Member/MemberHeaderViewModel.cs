using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{

    public class MemberHeaderViewModel
    {
        [Display(Name = "REFHEader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string REFID { get; set; }

        [Display(Name = "PlanName", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanName { get; set; }

        [Display(Name = "SubscriberIDHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string SubscriberID { get; set; }

        [Display(Name = "MemberNameHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberName { get; set; }

        [Display(Name = "GenderHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "DateOfBirthHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "AgeHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public int Age { get; set; }

        [Display(Name = "PlanSubGroupHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanSubGroup { get; set; }

        public string PlanSubGroupLink { get; set; }

        [Display(Name = "EffDateHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EffDate { get; set; }

        [Display(Name = "EligibilityDateHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? EligibilityDate { get; set; }

        [Display(Name = "IsEligibleHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsEligible { get; set; }

        [Display(Name = "PCPNAMEHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PCPNAME { get; set; }

        [Display(Name = "PCPPHONEHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PCPPHONE { get; set; }

        [Display(Name = "MBRPHONEHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MBRPHONE { get; set; }

        [Display(Name = "MBRCountyHeader", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string COUNTY { get; set; }

    }
}