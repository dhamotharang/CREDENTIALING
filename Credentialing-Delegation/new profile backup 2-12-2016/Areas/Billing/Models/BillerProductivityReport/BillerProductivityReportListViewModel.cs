using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.BillerProductivityReport
{
    public class BillerProductivityReportListViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Open { get; set; }

        public double OnHold { get; set; }

        public double Rejected { get; set; }

        public double Accepted { get; set; }

        public double Generated { get; set; }

        public double Translated { get; set; }

        public double ProviderOnHold { get; set; }

        public double CommitteeReview { get; set; }

        public double BilledAmount { get; set; }

        public double TotalClaims { get; set; }

    }
}