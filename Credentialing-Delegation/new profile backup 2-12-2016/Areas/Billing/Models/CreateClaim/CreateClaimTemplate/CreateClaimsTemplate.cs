using PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.CreateClaimTemplate
{
    public class CreateClaimsTemplate
    {
        public MemberResultViewModel Member { get; set; }

        public InsuredInfoViewModel InsuredDetails { get; set; }

        public SecondaryInsuredInfoViewModel SecondaryInsuredDetails { get; set; }

        public BillingProviderInfo BillingProvider { get; set; }

        public ProviderResultViewModel RenderingProvider { get; set; }

        public ReferingProviderInfo ReferringProvider { get; set; }

        public SupervisingProviderInfo SupervisingProvider { get; set; }

        public FacilityInfo Facility { get; set; }

        public MemberPayerInfo PayerInfo { get; set; }

    }
}