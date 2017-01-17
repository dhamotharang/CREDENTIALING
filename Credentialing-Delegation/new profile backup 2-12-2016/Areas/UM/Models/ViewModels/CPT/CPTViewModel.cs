using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.CPT
{
    public class CPTViewModel
    {
        [Display(Name = "Proc Code", ShortName = "Proc")]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("Code")]
        public string CPTCode { get; set; }

        [Display(Name = "Proc Desc")]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("Description")]
        public string CPTDesc { get; set; }

        public int? Modifier { get; set; }

        [Display(Name = "Req Units", ShortName = "Units")]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("RequestedUnits")]
        public int? RequestedUnits { get; set; }

        [Display(Name = "Range")]
        public string Range1 { get; set; }

         [Display(Name = "No Per")]
        public int? NumberPer { get; set; }

        [Display(Name = "Range")]
         public string Range2 { get; set; }

        [Display(Name = "Total Units")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? TotalUnits { get; set; }

        public string Discipline { get; set; }

        [Display(Name = "Inc Letter")]
        public string IncludeLetter { get; set; }

        public string  PlainLanguageDesc { get; set; }

        public string PlainLang { get; set; }

    }
}