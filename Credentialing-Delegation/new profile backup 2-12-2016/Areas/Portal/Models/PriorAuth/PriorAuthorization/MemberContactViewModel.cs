using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization
{
    public class MemberContactViewModel
    {
        public int ContactID { get; set; }

        public string ContactType { get; set; }

        [Display(Name="PHONE NUMBER")]
        public string PrimaryContact { get; set; }

        public string AlternateContact { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string ZipCode { get; set; }

    }
}