using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Enums
{
    public enum PreferenceType
    {
        [Display(Name = "PRIMARY")]
        PRIMARY = 1,
        [Display(Name = "SECONDARY")]
        SECONDARY
    }
}