using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.ICDCPTMapping
{
    public class ICDCPTCodemappingViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double Fee { get; set; }
        public string Modifier1 { get; set; }
        public string Modifier2 { get; set; }
        public string Modifier3 { get; set; }
        public string Modifier4 { get; set; }
        public string DiagnosisPointer1 { get; set; }
        public string DiagnosisPointer2 { get; set; }
        public string DiagnosisPointer3 { get; set; }
        public string DiagnosisPointer4 { get; set; }
        public Boolean isEnM { get; set; }
    }
}