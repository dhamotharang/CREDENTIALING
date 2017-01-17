using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class BirthInformationViewModel
    {
        [Key]
        public int? BirthInformationId { get; set; }

        [Display(Name = "DATE OF BIRTH")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DateOfBirth { get; set; }

        [Display(Name = "STATE OF BIRTH")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StateOfBirth { get; set; }


        [Display(Name = "CITY OF BIRTH")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CityOfBirth { get; set; }

        [Display(Name = "COUNTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "COUNTRY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Country { get; set; }

        [Display(Name = "BIRTH CERTIFICATE")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public bool BirthCertificate { get; set; }
    }
}