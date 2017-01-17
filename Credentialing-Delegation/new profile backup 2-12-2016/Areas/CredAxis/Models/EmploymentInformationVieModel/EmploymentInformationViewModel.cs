using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.EmploymentInformationVieModel
{
    public class EmploymentInformationViewModel
    {
        public string EmploymentInformationID { get; set; }

        [Display(Name = "PROVIDER RELATIONSHIP")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderRelation { get; set; }

        [Display(Name = "JOINING DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string JoiningDate { get; set; }

        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "STATUS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        [Display(Name = "CONTRACT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Contract { get; set; }

        [Display(Name = "INDIVIDUAL TAX ID ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IndividualTaxid { get; set; }



    }
}