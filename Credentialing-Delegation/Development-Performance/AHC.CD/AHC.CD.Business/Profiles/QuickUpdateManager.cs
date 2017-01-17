using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfileReviewSection;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Exceptions.Profiles;
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
        IUnitOfWork uow = null;

        public QuickUpdateManager(IUnitOfWork uow)
        {
            this.uow = uow;
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
                
                PersonalDetail returningPersonalDetail = profile.PersonalDetail;
                List<HomeAddress> returningHomeAddresses = profile.HomeAddresses.Where(h => (h.Status != StatusType.Inactive.ToString())).ToList();
                List<StateLicenseInformation> returningStateLicenses = profile.StateLicenses.Where(s => s.Status != StatusType.Inactive.ToString()).ToList();
                List<FederalDEAInformation> returningFederalDEALicenses = profile.FederalDEAInformations.Where(f => f.Status != StatusType.Inactive.ToString()).ToList();
                List<CDSCInformation> returningCDSInformations = profile.CDSCInformations.Where(c => c.Status != StatusType.Inactive.ToString()).ToList();
                List<SpecialtyDetail> returningSpecialtyDetails = profile.SpecialtyDetails.Where(s => s.Status != StatusType.Inactive.ToString()).ToList();
                
                List<AHC.CD.Entities.MasterProfile.PracticeLocation.PracticeLocationDetail> returningPracticeLocationDetails = profile.PracticeLocationDetails.Where(p => p.Status != StatusType.Inactive.ToString()).ToList();
                List<AHC.CD.Entities.MasterData.Account.Branch.Facility> returningFacilities = new List<AHC.CD.Entities.MasterData.Account.Branch.Facility>();
                if (returningPracticeLocationDetails != null)
                {
                    returningFacilities = returningPracticeLocationDetails.FindAll(p => p.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString()).Select(p => p.Facility).ToList();
                }
                List<AHC.CD.Entities.MasterData.Account.Service.FacilityService> returningServices = new List<AHC.CD.Entities.MasterData.Account.Service.FacilityService>();
                if (returningPracticeLocationDetails != null)
                {
                    returningServices = returningPracticeLocationDetails.FindAll(p => p.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString()).Select(p => p.Facility.FacilityDetail.Service).ToList();
                }
                List<AHC.CD.Entities.MasterProfile.PracticeLocation.PracticeProvider> returningCoveringPhysicians = new List<AHC.CD.Entities.MasterProfile.PracticeLocation.PracticeProvider>();
                if (returningPracticeLocationDetails != null)
                {
                    var ret = returningPracticeLocationDetails.FindAll(p => p.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
                    foreach (var p in ret)
                    {
                        foreach (var cp in p.PracticeProviders) {
                            returningCoveringPhysicians.Add(cp);
                        }
                    }
                }
                
                HospitalPrivilegeInformation returningHospitalPrivilegeInfoes = profile.HospitalPrivilegeInformation;
                List<HospitalPrivilegeDetail> returningHospitalPrivileges = new List<HospitalPrivilegeDetail>();
                if (returningHospitalPrivilegeInfoes != null)
                {
                    returningHospitalPrivileges = returningHospitalPrivilegeInfoes.HospitalPrivilegeDetails.Where(s => s.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();
                }
                List<WorkGap> returningWorkGaps = profile.WorkGaps.Where(w => w.Status != StatusType.Inactive.ToString()).ToList();
                CVInformation returningCV = profile.CVInformation;
                List<ContractInfo> returningContractGroupInformation = profile.ContractInfoes.Where(c => !c.ContractStatus.Equals(ContractStatus.Inactive.ToString())).ToList();                

                #region Checking for Not Applicable sections

                var profileReviewSections = await uow.GetGenericRepository<ProfileReviewSection>().GetAsync(p => p.ProfileID == profileId, "ProfileSection");

                if (profileReviewSections != null)
                {
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Personal Details")))
                    {
                        returningPersonalDetail = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Home Address")))
                    {
                        returningHomeAddresses = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("State License")))
                    {
                        returningStateLicenses = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Federal DEA Information")))
                    {
                        returningFederalDEALicenses = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("CDS Information")))
                    {
                        returningCDSInformations = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Specialty Details")))
                    {
                        returningSpecialtyDetails = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Covering Physicians")))
                    {
                        returningCoveringPhysicians = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Facility Details")))
                    {
                        returningFacilities = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Services")))
                    {
                        returningServices = null;
                    } 
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Hospital Privilege Detail")))
                    {
                        returningHospitalPrivileges = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Work Gap")))
                    {
                        returningWorkGaps = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("CV Information")))
                    {
                        returningCV = null;
                    }
                    if (profileReviewSections.Any(p => p.ProfileSection.ProfileSectionName.Equals("Group Information")))
                    {
                        returningContractGroupInformation = null;
                    }                   
                }

                #endregion

                return new
                {
                    PersonalDetail = returningPersonalDetail,
                    HomeAddresses = returningHomeAddresses,
                    StateLicenses = returningStateLicenses,
                    FederalDEALicenses = returningFederalDEALicenses,
                    CDSInformations = returningCDSInformations,
                    SpecialtyDetails = returningSpecialtyDetails,
                    Facilities = returningFacilities,
                    CoveringPhysicians = returningCoveringPhysicians,
                    Services = returningServices,
                    HospitalPrivileges = returningHospitalPrivileges,
                    WorkGaps = returningWorkGaps,
                    CV = returningCV,
                    ContractGroupInformation = returningContractGroupInformation
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

        public void DisplayProfileSectionNotApplicableAsync(Entities.MasterProfile.ProfileReviewSection.ProfileReviewSection profileReview)
        {
            try
            {
                var profileReviewSections = uow.GetGenericRepository<ProfileReviewSection>();
                profileReviewSections.Create(profileReview);
                profileReviewSections.Save();

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.SECTION_NOTAPPLICABLE, ex);
            }
        }
    }
}
