using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.Claims_Tracking
{
    public class AcceptedClaimViewModel
    {
        public string ClaimId { get; set; }
        public string Payer { get; set; }
        public string Member { get; set; }
        public string Provider { get; set; }
        public string ClaimedAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? CreatedOn { get; set; }
        public int Age { get; set; }
        public string Account { get; set; }

        public string IsProcessing { get; set; }
    }
}