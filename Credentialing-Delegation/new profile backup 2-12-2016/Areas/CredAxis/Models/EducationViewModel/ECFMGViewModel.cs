using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.EducationViewModel
{
    public class ECFMGViewModel
    {
        [Key]
        public int? Id { get; set; }


        [Display(Name = "ECFMG NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Number { get; set; }

        [Display(Name = "ECFMG ISSUE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueDate { get; set; }

        [Display(Name = "DOCUMENT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Document { get; set; }


    }
}