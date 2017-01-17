using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.PayerDrilldownReport
{
    public class AMemberReportViewModel
    {
        public string AmountBilled { get; set; }

        public string AmountPaid { get; set; }

        public string AmountPending { get; set; }

        public string AmountDenied { get; set; }

        public List<AMemberListViewModel> Members { get; set; }

    }
}