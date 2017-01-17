using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.BillerProductivityReport
{
    public class BillerProductivityReportViewModel
    {

        public double TotalOpen { get; set; }

        public double TotalOnHold { get; set; }

        public double TotalRejected { get; set; }

        public double TotalAccepted { get; set; }

        public double TotalGenerated { get; set; }

        public double TotalTranslated { get; set; }

        public double TotalProviderOnHold { get; set; }

        public double TotalCommitteeReview { get; set; }

        public double TotalBilledAmount { get; set; }

        public double TotalTotalClaims { get; set; }

        public List<BillerProductivityReportListViewModel> BillerProductivityReportList { get; set; }
    }
}