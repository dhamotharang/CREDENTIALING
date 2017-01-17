using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class ICDPreviewViewModel
    {
        [Display(Name = "PR DX")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ICDCode { get; set; }

        [Display(Name = "PR DX DESC", ShortName = "Diag Desc")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ICDCodeDescription { get; set; }
    }
}