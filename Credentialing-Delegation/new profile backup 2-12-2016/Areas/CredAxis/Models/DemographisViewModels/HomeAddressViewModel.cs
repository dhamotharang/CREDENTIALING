using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class HomeAddressViewModel
    {
        [Key]
        public int? HomeAddressId { get; set; }

        [Display(Name = "PRIMARY HOME ADDRESS")]
        [DisplayFormat(NullDisplayText="-")]
        public bool PrimaryHomeAddress { get; set; }


        [Display(Name = "APARTMENT/UNIT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ApartmentOrUnit { get; set; }

        [Display(Name = "STREET/P. O. BOX ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StreetOrPOBox { get; set; }

        [Display(Name = "CITY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "STATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "COUNTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "COUNTRY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Country { get; set; }

        [Display(Name = "ZIP CODE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "PRESENT HOME ADDRESS")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool PresnetHomeAddress { get; set; }

        [Display(Name = "LIVING HERE FROM")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LivingHereFrom { get; set; }
    }
}