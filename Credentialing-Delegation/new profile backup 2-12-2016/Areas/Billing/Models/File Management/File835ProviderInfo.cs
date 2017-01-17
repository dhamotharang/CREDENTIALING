using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class File835ProviderInfo
    {
        public string RenderingProviderNPI { get; set; }

        public string RenderingProviderFirstName { get; set; }

        public string RenderingProviderMiddleName { get; set; }

        public string RenderingProviderLastName { get; set; }

        public int? NoOfClaims { get; set; }

        public float? AmountClaimed { get; set; }

        public float? AmountReceived { get; set; }

        public int? InterKey { get; set; }

    }
}