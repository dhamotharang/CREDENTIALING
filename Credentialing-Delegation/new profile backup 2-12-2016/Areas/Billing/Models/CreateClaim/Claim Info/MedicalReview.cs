using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class MedicalReview
    {
        public string DiagnosisCode { get; set; }
        public string DiagnosisDescription { get; set; }
        public string[] HCCCode { get; set; }
        public string[] HCCType { get; set; }
        public string[] HCCVersion { get; set; }
        public string[] HCCDescription { get; set; }
        public string[] HCCWeight { get; set; }
        public string BillingStatus { get; set; }
    }
}