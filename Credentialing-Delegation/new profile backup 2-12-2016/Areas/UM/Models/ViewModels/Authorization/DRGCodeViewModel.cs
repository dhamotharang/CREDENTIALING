using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class DRGCodeViewModel
    {
        public int DRGCodeID { get; set; }

        [Display(Name = "DRG", ShortName = "RUG")]
        [JsonProperty("DRG_Code")]
        public string DRGCode { get; set; }

        [Display(Name = "DRG Desc", ShortName = "RUG Desc")]
        [JsonProperty("Description")]
        public string DRGDescription { get; set; }

    }
}