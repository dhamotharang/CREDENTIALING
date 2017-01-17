using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Enums
{
    public enum ProgramType
    {
        [Display(Name = "INTERNSHIP")]
        INTERNSHIP = 1,
        [Display(Name = "FELLOWSHIP")]
        FELLOWSHIP,
        [Display(Name = "RESIDENCY")]
        RESIDENCY,
        [Display(Name = "OTHER")]
        OTHER
    }
}