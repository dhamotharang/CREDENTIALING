using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.CreateClaimTemplate
{
    public class FacilityResult
    {
        public int Id { get; set; }

        public string FacilityId { get; set; }

        public string FacilityName { get; set; }

        public string FacilityAddress1 { get; set; }

        public string FacilityAddress2 { get; set; }
    }
}