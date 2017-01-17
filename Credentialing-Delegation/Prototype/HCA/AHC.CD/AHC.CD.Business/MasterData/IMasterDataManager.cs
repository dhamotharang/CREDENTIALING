using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.MasterData
{
    public interface IMasterDataManager
    {
        Task<IEnumerable<StateLicenseStatus>> GetAllLicenseStatusAsync();

        Task<IEnumerable<StaffCategory>> GetAllStaffCategoryAsync();

        Task<IEnumerable<SpecialityBoard>> GetAllspecialityBoardAsync();

        Task<IEnumerable<Speciality>> GetAllSpecialityAsync();

        Task<IEnumerable<School>> GetAllSchoolAsync();

        Task<IEnumerable<QualificationDegree>> GetAllQualificationDegreeAsync();

        Task<IEnumerable<ProviderType>> GetAllProviderTypeAsync();

        Task<IEnumerable<ProfileDisclosureQuestion>> GetAllProfileDisclosureQuestionAsync();

        Task<IEnumerable<PracticeType>> GetAllPracticeTypeAsync();

        Task<IEnumerable<PracticeServiceQuestion>> GetAllPracticeServiceQuestionAsync();

        Task<IEnumerable<PracticeOpenStatusQuestion>> GetAllPracticeOpenStatusQuestionAsync();

        Task<IEnumerable<PracticeAccessibilityQuestion>> GetAllPracticeAccessibilityQuestionAsync();

        Task<IEnumerable<MilitaryRank>> GetAllMilitaryRankAsync();

        Task<IEnumerable<MilitaryPresentDuty>> GetAllMilitaryPresentDutyAsync();

        Task<IEnumerable<MilitaryDischarge>> GetAllMilitaryDischargeAsync();

        Task<IEnumerable<MilitaryBranch>> GetAllMilitaryBranchAsync();

        Task<IEnumerable<InsuranceCarrier>> GetAllInsuranceCarrierAsync();

        Task<IEnumerable<HospitalContactPerson>> GetAllHospitalContactPersonAsync();

        Task<IEnumerable<HospitalContactInfo>> GetAllHospitalContactInfoAsync();

        Task<IEnumerable<Hospital>> GetAllHospitalAsync();

        Task<IEnumerable<DEASchedule>> GetAllDEAScheduleAsync();

        Task<IEnumerable<DEAScheduleType>> GetAllDEAScheduleTypeAsync();

        Task<IEnumerable<Certification>> GetAllCertificationAsync();

        Task<IEnumerable<AdmittingPrivilege>> GetAllAdmittingPrivilegeAsync();
    }
}
