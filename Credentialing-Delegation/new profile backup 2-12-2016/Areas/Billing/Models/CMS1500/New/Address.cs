using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Address
    {
        [Key]
        public long Address_PK_ID { get; set; }
       
        [Display(Name = "1st Address")]
        public string AddressLine1 { get; set; }

        [Display(Name = "2nd Address")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        public string Country { get; set; }

        public string County { get; set; }

        public string Street { get; set; }

         [Display(Name = "Zip")]
        public string ZipCode { get; set; }
    
    }
}