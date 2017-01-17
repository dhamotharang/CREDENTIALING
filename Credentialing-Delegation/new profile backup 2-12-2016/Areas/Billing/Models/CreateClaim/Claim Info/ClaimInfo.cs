using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class ClaimInfo
    {
        public ClaimsInfoViewModels ClaimsInfo { get; set; }

        public MemberInfo MemberInfo { get; set; }

        public FacilityInfo FacilityInfo { get; set; }

        public ProviderInfo ProviderInfo { get; set; }

        public PayerInfo PayerInfo { get; set; }

        public BillingProviderInfo BillingProviderInfo { get; set; }
    }
}