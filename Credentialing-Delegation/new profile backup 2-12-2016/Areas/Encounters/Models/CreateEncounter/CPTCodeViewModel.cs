using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class CPTCodeViewModel
    {
        public string CPTCode { get; set; }

        public string CPTDescription { get; set; }

        public string Modifier1 { get; set; }

        public string Modifier2 { get; set; }

        public string Modifier3 { get; set; }

        public string Modifier4 { get; set; }

        public string DiagnosisPointer1 { get; set; }

        public string DiagnosisPointer2 { get; set; }

        public string DiagnosisPointer3 { get; set; }

        public string DiagnosisPointer4 { get; set; }

        public string Fee { get; set; }

    }
}