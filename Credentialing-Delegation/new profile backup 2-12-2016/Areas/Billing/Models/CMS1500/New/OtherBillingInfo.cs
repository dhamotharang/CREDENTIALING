using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class OtherBillingInfo
    {
        public int Id { get; set; }

        [Display(Name = "Additional Claim Information (Designated by NUCC)")]
        public string AdditionalClaimInformation { get; set; }

        [Display(Name = "Resubmission Code")]
        public string ResubmissionCode { get; set; }

        [Display(Name = "Reference Number")]
        public string ResubmissionReferenceNumber { get; set; }

        [Display(Name = "Prior Authorization Number")]
        public string PriorAuthorizationNumber { get; set; }
    }
}