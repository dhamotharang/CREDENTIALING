using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class FacilityInfo
    {
        public string FacilityName { get; set; }

        public string FacilityAddress1 { get; set; }

        public string FacilityAddress2 { get; set; }

        public string FacilityCity { get; set; }

        public string FacilityState { get; set; }

        public string FacilityZip { get; set; }

        [DisplayName("NPI")]
        public string FacilityIdentifier1 { get; set; }

        [DisplayName(" FACILITY ID")]
        public string FacilityIdentifier2 { get; set; }

    }
}