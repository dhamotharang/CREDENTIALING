using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class MDCCodeViewModel
    {
        public int MDCCodeID { get; set; }

        [Display(Name = "MDC Code")]
        [JsonProperty("MDC")]
        public string MDCCode { get; set; }

        [Display(Name = "MDC Classification Desc")]
        [JsonProperty("Description")]
        public string MDCDescription { get; set; }
    }
}