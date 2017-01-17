using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class ProviderResultViewModel
    {
        public string ProviderUniqueId { get; set; }

        public string ProviderNPI { get; set; }

        public string ProviderFirstName { get; set; }

        public string ProviderMiddleName { get; set; }

        public string ProviderLastName { get; set; }

        public string Speciality { get; set; }

        public string Taxonomy { get; set; }

        public string TaxId { get; set; }

        public string ProviderFirstAddress { get; set; }

        public string ProviderSecondAddress { get; set; }

        public string ProviderCity { get; set; }

        public string ProviderState { get; set; }

        public string ProviderZip { get; set; }

        public string ProviderPhoneNo { get; set; }

        public string ProviderFullName
        {
            get
            {
                return this.ProviderFirstName + " " + this.ProviderMiddleName + " " + this.ProviderLastName;
            }
        }

        public string ProviderFullAddress
        {
            get
            {
                return this.ProviderFirstAddress + " " + this.ProviderSecondAddress;
            }
        }
    }
}