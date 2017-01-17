using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.ProviderInfo
{
    public class ProfessionalIdentificationInfo
    {
        public int ProfessionalIdentificationInfoID { get; set; }
        public string IdentificationNo { get; set; }
        public IdentificationType IdentificationType { get; set; }
    }
}
