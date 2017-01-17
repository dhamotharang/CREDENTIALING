using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.PayerDrilldownReport
{
    public class CRenderingProviderListViewModel
    {
        public int ID { get; set; }

        public string RenderingProvider { get; set; }

        public string MemberCount { get; set; }

        public string BillingProviderCount { get; set; }

        public string ClaimsSubmitted { get; set; }

        public string ClaimsAccepted { get; set; }

        public string ClaimsRejected { get; set; }

        public string ClaimsPending { get; set; }
    }
}