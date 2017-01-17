using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Billing.Models.PayerDrilldownReport
{
    public class APayerListViewModel
    {
        public int ID { get; set; }

        public string Payer { get; set; }

        public string BillingProviderCount { get; set; }

        public string MemberCount { get; set; }

        public string Billed { get; set; }

        public string Allowed { get; set; }

        public string Paid { get; set; }

        public string Adj { get; set; }

        public string Ded { get; set; }

        public string Pending { get; set; }

        public string Denied { get; set; }
    }
}
