using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.PSVInfo
{
    public class ProfileVerificationDetailViewModel
    {
        public int ProfileVerificationDetailId { get; set; }

        public int? ProfileVerificationParameterId { get; set; }

        public int? VerificationResultId { get; set; }

        public VerificationResultViewModel VerificationResult { get; set; }

        public string VerificationData { get; set; }

        public int? VerifiedById { get; set; }
        
        public DateTime? VerificationDate { get; set; }
    }
}