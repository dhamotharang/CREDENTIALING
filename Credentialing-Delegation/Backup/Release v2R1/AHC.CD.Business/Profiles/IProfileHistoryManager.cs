using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile.Contract;

namespace AHC.CD.Business.Profiles
{
   public interface IProfileHistoryManager
    {
        Task<IEnumerable<ProfessionalAffiliationInfoHistory>> GetProfessionalAffiliationHistory(int profileId);
        Task<IEnumerable<OtherLegalNameHistory>> GetAllOtherLegalNamesHistory(int profileId);
        Task<IEnumerable<HomeAddressHistory>> GetAllHomeAddressesHistory(int profileId);
        Task<IEnumerable<ProfessionalReferenceInfoHistory>> GetProfessionalReferenceHistory(int profileId);
        Task<IEnumerable<ContractGroupInfoHistory>> GetContractGroupInfoHistory(int profileId);

        #region Work History
        Task<IEnumerable<ProfessionalWorkExperienceHistory>> GetProfessionalWorkExperienceHistory(int profileId);
        Task<IEnumerable<MilitaryServiceInformationHistory>> GetMilitaryServiceInformationHistory(int profileId);
        Task<IEnumerable<PublicHealthServiceHistory>> GetPublicHealthServiceHistory(int profileId);
        Task<IEnumerable<WorkGapHistory>> GetWorkGapHistory(int profileId);
        #endregion

        Task<IEnumerable<ProfessionalLiabilityInfoHistory>> GetAllProfessionalLiabilityInfoHistory(int profileId);
        Task<IEnumerable<HospitalPrivilegeDetailHistory>> GetAllHospitalHistory(int profileId);
        Task<IEnumerable<StateLicenseInfoHistory>> GetStateLicenseHistory(int profileId);
        Task<IEnumerable<FederalDEAInfoHistory>> GetFederalDEALicensesHistory(int profileId);
        Task<IEnumerable<MedicareInformationHistory>> GetMedicareInformationHistory(int profileId);
        Task<IEnumerable<MedicaidInformationHistory>> GetMedicaidInformationHistory(int profileId);
        Task<IEnumerable<CDSCInfoHistory>> GetCDSCInformationHistory(int profileId);
        Task<IEnumerable<PracticeLocationDetailHistory>> GetPracticeLocationDetailHistory(int profileId);

        #region Education History
        Task<IEnumerable<EducationDetailHistory>> GetEducationDetailHistory(int profileId);
        Task<IEnumerable<CMECertificationHistory>> GetCMECertificationHistory(int profileId);
        Task<IEnumerable<ProgramDetailHistory>> GetProgramDetailHistory(int profileId);

        #endregion
    }
}
