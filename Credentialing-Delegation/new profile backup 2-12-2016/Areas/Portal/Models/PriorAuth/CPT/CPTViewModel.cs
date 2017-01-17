using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.CPT
{
    public class CPTViewModel
    {
        [Display(Name = "CPT-4 / HCPCS CODE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CPTCode { get; set; }

        [Display(Name = "DESCRIPTION OF PROCEDURE OR SERVICES", ShortName="PROC DESC")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CPTDesc { get; set; }

        [Display(Name = "MODIFIER")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? Modifier { get; set; }

        [Display(Name = "VISITS / FREQUENCY", ShortName="REQ UNITS")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? RequestedUnits { get; set; }

        [Display(Name = "RANGE")]
        public int? Range1 { get; set; }

         [Display(Name = "NO PER")]
        public int? NumberPer { get; set; }

        [Display(Name = "RANGE")]
         public int? Range2 { get; set; }

        [Display(Name = "TOTAL UNITS")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? TotalUnits { get; set; }

        [Display(Name = "DISCIPLINE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Discipline { get; set; }

        [Display(Name = "INC LETTER")]
        public bool IncludeLetter { get; set; }

    }
}