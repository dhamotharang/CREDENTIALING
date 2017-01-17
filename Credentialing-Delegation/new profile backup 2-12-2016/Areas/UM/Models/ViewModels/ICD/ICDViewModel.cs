using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.ICD
{
    public class ICDViewModel
    {
        [Display(Name = "Primary DX")]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("IcdCodeNumber")]
        public string ICDCode { get; set; }

        [Display(Name = "PRIMARY DESC", ShortName = "Diag Desc")]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("Description")]
        public string ICDCodeDescription { get; set; }

        public string ICDVersion { get; set; }
    }
}