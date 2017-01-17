using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class NonResidentInfo
    {
        public int NonResidentInfoID { get; set; }
        public string IdentificationNoIssueCountry { get; set; }
        public bool IsAuthorizedToWorkInCountry { get; set; }
        public string NationalIdentificationNo { get; set; }
        public string PermanentResidentNo { get; set; }
        public virtual VisaInformation VisaInformation { get; set; }

    }
}
