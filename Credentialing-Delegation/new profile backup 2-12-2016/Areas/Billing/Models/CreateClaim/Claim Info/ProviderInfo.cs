using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info
{
    public class ProviderInfo
    {
        public string RenderingProviderLastOrOrganizationName { get; set; }

        public string RenderingProviderMiddleName { get; set; }

        public string RenderingProviderFirstName { get; set; }

        public string RenderingProviderFirstAddress { get; set; }

        public string RenderingProviderSecondAddress { get; set; }

        public string RenderingProviderCity { get; set; }

        public string RenderingProviderState { get; set; }

        public string RenderingProviderZip { get; set; }

        public string RenderingProviderPhoneNo { get; set; }

        [DisplayName("RENDERING PROVIDER NPI")]
        public string RenderingProviderNPI { get; set; }

        [DisplayName("RENDERING PROVIDER PIN")]
        public string RenderingProviderPin { get; set; }

        public string RenderingProviderTaxonomy { get; set; }
    }
}