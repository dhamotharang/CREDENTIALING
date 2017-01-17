using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.BillerProductivityReport
{
    public class AverageBillerProductivityReportViewModel
    {
        public double TotalBillers { get; set; }

        public double TotalClaims { get; set; }

        public double TotalDays { get; set; }

        public double TotalBilledAmounts { get; set; }

        public double AverageClaims { get; set; }

        public double AverageBilledAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? FromDateofCreation { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? ToDateofCreation { get; set; }
    }
}