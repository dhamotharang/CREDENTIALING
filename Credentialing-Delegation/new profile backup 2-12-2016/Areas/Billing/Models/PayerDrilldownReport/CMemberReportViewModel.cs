using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.PayerDrilldownReport
{
    public class CMemberReportViewModel
    {
        public string ClaimSubmitted { get; set; }

        public string ClaimAccepted { get; set; }

        public string ClaimPending { get; set; }

        public string ClaimRejected { get; set; }

        public List<CMemberListViewModel> Members { get; set; }
    }
}