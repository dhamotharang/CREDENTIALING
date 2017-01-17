using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.PayerDrilldownReport
{
    public class CBillingProviderListViewModel
    {
        public int ID { get; set; }

        public string BillingProvider { get; set; }

        public string RenderingProviderCount { get; set; }

        public string PayerCount { get; set; }

        public string ClaimsSubmitted { get; set; }

        public string ClaimsAccepted { get; set; }

        public string ClaimsRejected { get; set; }

        public string ClaimsPending { get; set; }

    }
}