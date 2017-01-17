using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.ICD
{
    public class ICDViewModel
    {
        [Display(Name = "PRIMARY DX")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ICDCode { get; set; }

        [Display(Name = "PRIMARY DESC", ShortName = "DIAG DESC")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ICDCodeDescription { get; set; }
    }
}