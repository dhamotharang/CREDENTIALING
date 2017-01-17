using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class CPTCodeViewModel
    {
        public string CPTCode { get; set; }
        public string CPTDescription { get; set; }
        public double Fee { get; set; }
        public Boolean isEnM { get; set; }
    }
}