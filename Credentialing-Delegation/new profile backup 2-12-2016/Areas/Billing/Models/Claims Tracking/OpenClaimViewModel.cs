using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.Claims_Tracking
{
    public class OpenClaimViewModel
    {
        public string ClaimNumber { get; set; }
        public string MemLastName { get; set; }
        public string MemFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public string ProviderFirstName { get; set; }
        public string PayerName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DOSFrom { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DOSTo { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DateCreated { get; set; }
        public double Amount { get; set; }
        public string ClaimType { get; set; }
        public int? AgeOfDOC { get; set; }
        public int? AgeOfDOS { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public int? IncomingFileId { get; set; }



    }
}