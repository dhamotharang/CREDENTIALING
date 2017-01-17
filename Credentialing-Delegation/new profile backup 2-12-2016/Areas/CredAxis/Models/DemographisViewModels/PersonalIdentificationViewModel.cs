using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class PersonalIdentificationViewModel
    {
        [Key]
        public int? PersonalIdentificationId { get; set; }


        [Display(Name = "SOCIAL SECURITY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SocialSecurityNumber { get; set; }

        [Display(Name = "DRIVER'S LICENSE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DriversLicense { get; set; }

        [Display(Name = "ISSUE STATE OF DL")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueStateOfDL { get; set; }
    }
}