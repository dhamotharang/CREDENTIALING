using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.ViewAuditing
{
    public class ProcedureCode
    {
        [DisplayName("Code")]
        public string CPTCode { get; set; }

        [DisplayName("Description")]
        public string CPTDescription { get; set; }

        [DisplayName("Modifier")]
        public string Modifier { get; set; }

        [DisplayName("Diagnosis Pointer")]
        public string DiagnosisPointer { get; set; }

        [DisplayName("Fee")]
        public string Fee { get; set; }

        [DisplayName("Review")]
        public string Review { get; set; }
        
        [DisplayName("Category")]
        public string Category { get; set; }
        
        [DisplayName("Remark")]
        public string Remark { get; set; }

        public bool IsEandM { get; set; }
    }
}