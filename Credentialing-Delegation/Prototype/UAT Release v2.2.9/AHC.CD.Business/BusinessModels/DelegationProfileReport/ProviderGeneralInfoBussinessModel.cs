using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.DelegationProfileReport
{
    public class ProviderGeneralInfoBussinessModel
    {
        public string ProviderName { get; set; }

        public string NPINumber { get; set; }

        public string TIN { get; set; }

        public string CredentialDate { get; set; }

        public string ReCredentialCycle1Date { get; set; }

        public string ReCredentialCycle2Date { get; set; }

        public List<string> MedicalLicense { get; set; }

        public List<string> DEA { get; set; }

        public string SSN { get; set; }

        public string DOB { get; set; }

        public string EthnicOrigin { get; set; }

        public string Gender { get; set; }

        public List<string> Languages { get; set; }

        public List<string> CoveringPhysicians { get; set; }

        public List<ProviderHospitalAffiliationBusinessModel> ProviderHospitalAffiliations{ get; set; }

        public List<ProviderMedicalEducationBusinessModel> ProviderMedicalEducations { get; set; }

        public List<ProviderPracitceInfoBusinessModel> ProviderPracitceInfos { get; set; }

        public List<ProviderProfessionalDetailBusinessModel> ProviderProfessionalDetails { get; set; }

        public List<ProviderProgramDetailBusinessModel> ProviderProgramDetails { get; set; }
    }
}
