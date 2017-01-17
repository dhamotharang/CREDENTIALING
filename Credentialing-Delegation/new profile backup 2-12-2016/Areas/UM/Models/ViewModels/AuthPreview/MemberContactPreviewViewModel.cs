using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class MemberContactPreviewViewModel
    {
        [Display(Name = "PH: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PrimaryContact { get; set; }

        [Display(Name = "Cnty:")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }
    }
}