using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.Claims_Tracking
{
    public class RejectedClaimViewModel
    {
        public string ClaimId { get; set; }

        public string PrimaryPayer { get; set; }

        public string SecondaryPayer { get; set; }

        public string Member { get; set; }

        public string RenderingProvider { get; set; }

        public string BillingProvider { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DosFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DosTo { get; set; }

        public string ClaimedAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string ReviewedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? RejectedOn { get; set; }

        public string RejectionReason { get; set; }
    }
}