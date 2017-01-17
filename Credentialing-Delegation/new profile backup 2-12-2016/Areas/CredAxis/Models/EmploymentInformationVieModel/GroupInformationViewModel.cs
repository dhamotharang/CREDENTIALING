using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.EmploymentInformationVieModel
{
    public class GroupInformationViewModel
    {
     
        public string GroupInformationViewID { get; set; }

        [Display(Name = "IPA/GROUP NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IpaGroupName { get; set; }


        [Display(Name = "START DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StartDate { get; set; }

        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "STATUS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        [Display(Name = "GROUP TAX ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GroupTaxId { get; set; }

        [Display(Name = "GROUP NPI NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GroupNPI { get; set; }
    }
}