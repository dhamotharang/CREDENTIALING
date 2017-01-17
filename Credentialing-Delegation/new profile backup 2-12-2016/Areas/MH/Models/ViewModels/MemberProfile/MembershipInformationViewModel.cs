using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class MembershipInformationViewModel
    {
        public string SubscriberID { get; set; }

        public MemberProfileViewModel Subscriber { get; set; }

        public string InsuranceCompanyCode { get; set; }

        [Display(Name = "Insurance Company")]
        [DisplayFormat(NullDisplayText = "-")]
        public string InsuranceCompanyName { get; set; }

        public string PlanCode { get; set; }

        [Display(Name = "Plan Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanName { get; set; }

        [Display(Name = "Policy Number")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PBPCode { get; set; }

        public string PBPName { get; set; }

        public PremiumInformationViewModel PremiumInformation { get; set; }

        public string LOBCode { get; set; }

        public string LOBName { get; set; }

        public string OrganizationCode { get; set; }

        public string OrganizationName { get; set; }

        public string Status { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; } 

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

        public GuarantorInformationViewModel GuarantorInformation { get; set; }

    }
}