using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.CreateClaimTemplate
{
    public class SupervisingProviderInfo
    {
        public string SupervisingProviderLastName { get; set; }

        public string SupervisingProviderMiddleName { get; set; }

        public string SupervisingProviderFirstName { get; set; }

        public string SupervisingProviderFirstAddress { get; set; }

        public string SupervisingProviderSecondAddress { get; set; }

        public string SupervisingProviderCity { get; set; }

        public string SupervisingProviderState { get; set; }

        public string SupervisingProviderZip { get; set; }

        public string SupervisingProviderPhoneNo { get; set; }

        [DisplayName("SUPERVISING PHYSICIAN NPI")]
        public string SupervisingProviderIdentifier1 { get; set; }

        [DisplayName("SUPERVISING PHYSICIAN ID")]
        public string SupervisingProviderIdentifier2 { get; set; }

        public string SupervisingProviderTaxonomy { get; set; }


        public string SupervisingProviderFullName
        {
            get
            {
                return this.SupervisingProviderFirstName + " " + this.SupervisingProviderMiddleName + " " + this.SupervisingProviderLastName;
            }
        }

        public string SupervisingProviderFullAddress
        {
            get
            {
                return this.SupervisingProviderFirstAddress + " " + this.SupervisingProviderSecondAddress;
            }
        }

    }
}