using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Enums.Facility
{
    public enum YesNoOption
    {
        [Display(Name = "Yes")]
        YES = 1,
        [Display(Name = "No")]
        NO
    }
}