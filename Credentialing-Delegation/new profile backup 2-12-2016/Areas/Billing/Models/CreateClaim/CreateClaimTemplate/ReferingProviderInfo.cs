using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.CreateClaimTemplate
{
    public class ReferingProviderInfo
    {
        public string ReferringProviderLastName { get; set; }

        public string ReferringProviderMiddleName { get; set; }

        public string ReferringProviderFirstName { get; set; }

        public string ReferringProviderFirstAddress { get; set; }

        public string ReferringProviderSecondAddress { get; set; }

        public string ReferringProviderCity { get; set; }

        public string ReferringProviderState { get; set; }

        public string ReferringProviderZip { get; set; }

        public string ReferringProviderPhoneNo { get; set; }

        [DisplayName("NPI")]
        public string ReferringProviderIdentifier { get; set; }

        public string ReferringProviderTaxonomy { get; set; }


        public string ReferringProviderFullName
        {
            get
            {
                return this.ReferringProviderFirstName + " " + this.ReferringProviderMiddleName + " " + this.ReferringProviderLastName;
            }
        }

        public string ReferringProviderFullAddress
        {
            get
            {
                return this.ReferringProviderFirstAddress + " " + this.ReferringProviderSecondAddress;
            }
        }

    }
}