using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class QuickUpdateManager : IQuickUpdateManager
    {
        IProfileRepository profileRepository = null;

        public QuickUpdateManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
        }

        public async Task<object> GetProfileReviewDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Demographics
                    "PersonalDetail.ProviderLevel",
                    "HomeAddresses",
                    //Identification & Licenses
                    "StateLicenses.ProviderType",
                    "StateLicenses.StateLicenseStatus",
                    //Specialty Details
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard",
                    //Hospital Privilege
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.Hospital", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactInfo", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactPerson", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.AdmittingPrivilege", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.StaffCategory",
                    //Work History
                    //Contract Information
                    "ContractInfoes.ContractGroupInfoes",
                    "ContractInfoes.ContractGroupInfoes.PracticingGroup",
                    "ContractInfoes.ContractGroupInfoes.PracticingGroup.Group",
                    // Practice Locations
                    "PracticeLocationDetails.Facility",
                    "PracticeLocationDetails.Facility.FacilityDetail",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service.PracticeType",
                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders",
                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderTypes",
                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderSpecialties",
                    "PracticeLocationDetails.PracticeProviders",

                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    PersonalDetail = profile.PersonalDetail,
                    HomeAddresses = profile.HomeAddresses.Where(h => (h.Status != StatusType.Inactive.ToString())),
                    StateLicenses = profile.StateLicenses.Where(s => s.Status != StatusType.Inactive.ToString()),
                    FederalDEALicenses = profile.FederalDEAInformations.Where(f => f.Status != StatusType.Inactive.ToString()),
                    CDSInformations = profile.CDSCInformations.Where(c => c.Status != StatusType.Inactive.ToString()),
                    SpecialtyDetails = profile.SpecialtyDetails.Where(s => s.Status != StatusType.Inactive.ToString()),
                    PracticeLocationDetails = profile.PracticeLocationDetails.Where(p => p.Status != StatusType.Inactive.ToString()),
                    HospitalPrivileges = profile.HospitalPrivilegeInformation,
                    WorkGaps = profile.WorkGaps.Where(w => w.Status != StatusType.Inactive.ToString()),
                    CV = profile.CVInformation,
                    ContractGroupInformation = profile.ContractInfoes.Where(c => !c.ContractStatus.Equals(ContractStatus.Inactive.ToString()))
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new AHC.CD.Exceptions.Profiles.ProfileManagerException(ExceptionMessage.PROFILE_DEMOGRAPHICS_BY_ID_GET_EXCEPTION, ex);
            }
        }
    }
}
