using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProviderviewModel
{
    public class ProviderSearch
    {
        public string ProviderNPI { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Specialty { get; set; }
        public string ProviderLevel { get; set; }
        public string Relationship { get; set; }
        public string GroupIPA { get; set; }
        public string plan { get; set; }
    }
}   