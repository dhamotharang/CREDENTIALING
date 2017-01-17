using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class BillingProviderInfo
    {
        public string BillingProviderLastOrOrganizationName { get; set; }

        public string BillingProviderMiddleName { get; set; }

        public string BillingProviderFirstName { get; set; }

        public string BillingProviderFirstAddress { get; set; }

        public string BillingProviderSecondAddress { get; set; }

        public string BillingProviderCity { get; set; }

        public string BillingProviderState { get; set; }

        public string BillingProviderZip { get; set; }

        [DisplayName("TELEPHONE")]
        public string BillingProviderPhoneNo { get; set; }

        [DisplayName("TAXONOMY")]
        public string BillingProviderTaxonomy { get; set; }

        public string BillingGroupNPI { get; set; }

        public string BillingProviderFullName
        {
            get
            {
                return this.BillingProviderFirstName + " " + this.BillingProviderMiddleName + " " + this.BillingProviderLastOrOrganizationName;
            }
        }
        public string BillingProviderFullAddress
        {
            get
            {
                return this.BillingProviderFirstAddress + " " + this.BillingProviderSecondAddress +" "+this.BillingProviderCity+" "+this.BillingProviderState+" "+this.BillingProviderZip;
            }
        }
    }
}