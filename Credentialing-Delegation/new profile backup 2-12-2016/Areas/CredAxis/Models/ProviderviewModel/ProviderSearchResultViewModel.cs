using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProviderviewModel
{
    public class ProviderSearchResultViewModel
    {
        public string ProviderNPI { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Specialty { get; set; }
        public string ProviderLevel { get; set; }
        public string GroupIPA { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Plan { get; set; }
        public string Status { get; set; }
        public string Relationship { get; set; }
        public string Typecount { get; set; }
        public string Groupcount { get; set; }
        public string PlanCount { get; set; }
        public string EMPCOUNT { get; set; }
        public string AFFCOUNT { get; set; }
        public string Network { get; set; }

    }
}