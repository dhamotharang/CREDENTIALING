using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Facility.Models
{
    public class OrganizationViewModel
    {
        [Display(Name = "Organization ID")]
        public string OrganizationID { get; set; }

        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Display(Name = "Organization Type")]
        public string OrganizationType { get; set; }

        [Display(Name = "Organization NPI")]
        public string OrganizationNPI { get; set; }
    }
}