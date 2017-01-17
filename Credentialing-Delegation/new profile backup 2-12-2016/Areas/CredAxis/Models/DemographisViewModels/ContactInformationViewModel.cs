using PortalTemplate.Areas.CredAxis.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class ContactInformationViewModel
    {
        [Key]
        public int? ContactInfoId { get; set; }

        [Display(Name = "MOBILE")]
        [DisplayFormat(NullDisplayText="-")]
        public string PhoneNumber { get; set; }


        [Display(Name = "HOME PHONE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string HomePhoneNumber { get; set; }

        [Display(Name = "EMAIL ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EmailId { get; set; }

        [Display(Name = "HOME FAX")]
        [DisplayFormat(NullDisplayText = "-")]
        public string HomeFaxNumber { get; set; }

        [Display(Name = "PAGER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Pager { get; set; }

        [Display(Name = "PREFERRED CONTACT METHOD")]
        [DisplayFormat(NullDisplayText = "-")]
        public PreferedMethod PreferredMethod { get; set; }
    }
}