using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.ViewAuditing
{
    public class DiagnosisCode
    {     

        [DisplayName("Code")]
        public string ICDCode { get; set; }

        [DisplayName("Description")]
        public string ICDDescription { get; set; }

        [DisplayName("HCC Code")]
        public string HCCCode { get; set; }

        [DisplayName("HCC Type")]
        public string HCCType { get; set; }

        [DisplayName("HCC Version")]
        public string HCCVersion { get; set; }

        [DisplayName("HCC Description")]
        public string HCCDescription { get; set; }

        [DisplayName("HCC Weight")]
        public string HCCWeight { get; set; }

        [DisplayName("Review")]
        public string Review { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Remark")]
        public string Remark { get; set; }

    }
}