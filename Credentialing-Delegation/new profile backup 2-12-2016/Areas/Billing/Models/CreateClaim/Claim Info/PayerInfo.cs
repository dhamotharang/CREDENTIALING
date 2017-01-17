using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class PayerInfo
    {
        
        public string PayerName { get; set; }

        public string PayerID { get; set; }

        public string PayerFirstAddress { get; set; }

        public string PayerSecondAddress { get; set; }

        public string PayerCity { get; set; }

        public string PayerState { get; set; }

        public string PayerZip { get; set; }
    }
}