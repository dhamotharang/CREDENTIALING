using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class MembershipHistoryViewModel
    {
        public string SubscriberID { get; set; }

        public string InsuranceCompanyCode { get; set; }

        public string InsuranceCompanyName { get; set; }

        public string PlanCode { get; set; }

        public string PlanName { get; set; }

        public PremiumInformationViewModel PremiumInformation { get; set; }

        public string LOBCode { get; set; }

        public string LOBName { get; set; }

        public string OrganizationCode { get; set; }

        public string OrganizationName { get; set; }

        public GuarantorInformationViewModel GuarantorInformation { get; set; }

        public string MemberEffectiveDate { get; set; }

        public string MemberTerminationDate { get; set; }

        public string Status { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}