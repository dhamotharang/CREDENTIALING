using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class OtherLegalNameViewModel
    {
        [Key]
        public int OtherLegalNameID { get; set; }

        [Display(Name = "HAVE YOU USED OTHER LEGAL NAMES ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsLegalName { get; set; }

        [Display(Name = "FIRST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "MIDDLE NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MiddleName { get; set; }

        [Display(Name = "LAST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "SUFFIX")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Suffix { get; set; }

        [Display(Name = "WHEN DID YOU START USING OTHER NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LegalnameStartDate { get; set; }

        [Display(Name = "WHEN DID YOU STOP USING OTHER NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LegalnameEndDate { get; set; }

    }
}