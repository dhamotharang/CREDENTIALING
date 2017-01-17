using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.MasterData
{
    internal class MasterDataManager : IMasterDataManager
    {
        private IRepositoryManager repositoryManager = null;


        public MasterDataManager(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }
        
        public async Task<IEnumerable<Entities.MasterData.Tables.StateLicenseStatus>> GetAllLicenseStatusAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<StateLicenseStatus>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.StaffCategory>> GetAllStaffCategoryAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<StaffCategory>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.SpecialityBoard>> GetAllspecialityBoardAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<SpecialityBoard>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.Speciality>> GetAllSpecialityAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<Speciality>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.School>> GetAllSchoolAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<School>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.QualificationDegree>> GetAllQualificationDegreeAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<QualificationDegree>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.ProviderType>> GetAllProviderTypeAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<ProviderType>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.ProfileDisclosureQuestion>> GetAllProfileDisclosureQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<ProfileDisclosureQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.PracticeType>> GetAllPracticeTypeAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<PracticeType>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.PracticeServiceQuestion>> GetAllPracticeServiceQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<PracticeServiceQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.PracticeOpenStatusQuestion>> GetAllPracticeOpenStatusQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<PracticeOpenStatusQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.PracticeAccessibilityQuestion>> GetAllPracticeAccessibilityQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<PracticeAccessibilityQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryRank>> GetAllMilitaryRankAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<MilitaryRank>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryPresentDuty>> GetAllMilitaryPresentDutyAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<MilitaryPresentDuty>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryDischarge>> GetAllMilitaryDischargeAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<MilitaryDischarge>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryBranch>> GetAllMilitaryBranchAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<MilitaryBranch>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.InsuranceCarrier>> GetAllInsuranceCarrierAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<InsuranceCarrier>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.HospitalContactPerson>> GetAllHospitalContactPersonAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<HospitalContactPerson>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.HospitalContactInfo>> GetAllHospitalContactInfoAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<HospitalContactInfo>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.Hospital>> GetAllHospitalAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<Hospital>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.DEASchedule>> GetAllDEAScheduleAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<DEASchedule>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.DEAScheduleType>> GetAllDEAScheduleTypeAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<DEAScheduleType>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.Certification>> GetAllCertificationAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<Certification>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.AdmittingPrivilege>> GetAllAdmittingPrivilegeAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<AdmittingPrivilege>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
