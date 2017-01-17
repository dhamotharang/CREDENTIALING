using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.PayerDrilldownReport
{
    public class CPayerReportViewModel
    {
        public string SubmittedClaims { get; set; }

        public string AcceptedClaims { get; set; }

        public string PendingClaims { get; set; }

        public string RejectedClaims { get; set; }

        public List<CPayerListViewModel> PayerList { get; set; }
    }
}