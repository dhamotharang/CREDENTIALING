using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.PracticeLocation;

namespace AHC.CD.Business.Profiles
{
    public class ProfileHistoryManager : IProfileHistoryManager
    {
        private IUnitOfWork uof = null;

        public ProfileHistoryManager(IUnitOfWork uof)
        {
            this.uof = uof;
        }

        #region Professional Affiliation

        public async Task<IEnumerable<ProfessionalAffiliationInfoHistory>> GetProfessionalAffiliationHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "ProfessionalAffiliationInfos",
                    "ProfessionalAffiliationInfos.ProfessionalAffiliationInfoHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.ProfessionalAffiliationInfos.SelectMany(x => x.ProfessionalAffiliationInfoHistory);
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PROFESSIONAL_AFFILIATION_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Reference

        public async Task<IEnumerable<ProfessionalReferenceInfoHistory>> GetProfessionalReferenceHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "ProfessionalReferenceInfos",
                    "ProfessionalReferenceInfos.ProfessionalReferenceInfoHistory",
                    "ProfessionalReferenceInfos.ProfessionalReferenceInfoHistory.Specialty",
                    "ProfessionalReferenceInfos.ProfessionalReferenceInfoHistory.ProviderType"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.ProfessionalReferenceInfos.SelectMany(x => x.ProfessionalReferenceInfoHistory);
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PROFESSIONAL_REFERENCE_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Work History

        public async Task<IEnumerable<ProfessionalWorkExperienceHistory>> GetProfessionalWorkExperienceHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "ProfessionalWorkExperiences",
                    "ProfessionalWorkExperiences.ProfessionalWorkExperienceHistory",
                    "ProfessionalWorkExperiences.ProfessionalWorkExperienceHistory.ProviderType"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.ProfessionalWorkExperiences.SelectMany(x => x.ProfessionalWorkExperienceHistory);
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<MilitaryServiceInformationHistory>> GetMilitaryServiceInformationHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "MilitaryServiceInformations",
                    "MilitaryServiceInformations.MilitaryServiceInformationHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.MilitaryServiceInformations.SelectMany(x => x.MilitaryServiceInformationHistory);
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<PublicHealthServiceHistory>> GetPublicHealthServiceHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "PublicHealthServices",
                    "PublicHealthServices.PublicHealthServiceHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.PublicHealthServices.SelectMany(x => x.PublicHealthServiceHistory);
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<WorkGapHistory>> GetWorkGapHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "WorkGaps",
                    "WorkGaps.WorkGapHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.WorkGaps.SelectMany(x => x.WorkGapHistory);
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.WORK_GAP_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Demographics

        public async Task<IEnumerable<OtherLegalNameHistory>> GetAllOtherLegalNamesHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "OtherLegalNames",
                    "OtherLegalNames.OtherLegalNameHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.OtherLegalNames.SelectMany(x => x.OtherLegalNameHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.OTHER_LEGAL_NAME_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<HomeAddressHistory>> GetAllHomeAddressesHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] {
                    "HomeAddresses",
                    "HomeAddresses.HomeAddressHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.HomeAddresses.SelectMany(x => x.HomeAddressHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.HOME_ADDRESS_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Hospital Privileges

        public async Task<IEnumerable<HospitalPrivilegeDetailHistory>> GetAllHospitalHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "HospitalPrivilegeInformation",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalPrivilegeDetailHistory",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalPrivilegeDetailHistory.HospitalContactPerson",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalPrivilegeDetailHistory.HospitalContactInfo",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalPrivilegeDetailHistory.Specialty",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalPrivilegeDetailHistory.AdmittingPrivilege",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalPrivilegeDetailHistory.StaffCategory",
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalPrivilegeDetailHistory.Hospital"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.SelectMany(x => x.HospitalPrivilegeDetailHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PROFESSIONAL_LIABILITY_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Liability

        public async Task<IEnumerable<ProfessionalLiabilityInfoHistory>> GetAllProfessionalLiabilityInfoHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "ProfessionalLiabilityInfoes",
                    "ProfessionalLiabilityInfoes.ProfessionalLiabilityInfoHistory",
                    "ProfessionalLiabilityInfoes.ProfessionalLiabilityInfoHistory.InsuranceCarrier",
                    "ProfessionalLiabilityInfoes.ProfessionalLiabilityInfoHistory.InsuranceCarrierAddress"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.ProfessionalLiabilityInfoes.SelectMany(x => x.ProfessionalLiabilityInfoHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PROFESSIONAL_LIABILITY_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Identification & Licenses

        public async Task<IEnumerable<StateLicenseInfoHistory>> GetStateLicenseHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "StateLicenses",
                    "StateLicenses.StateLicenseInfoHistory",
                    "StateLicenses.StateLicenseInfoHistory.ProviderType",
                    "StateLicenses.StateLicenseInfoHistory.StateLicenseStatus"

                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.StateLicenses.SelectMany(x => x.StateLicenseInfoHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.STATE_LICENSE_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<FederalDEAInfoHistory>> GetFederalDEALicensesHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "FederalDEAInformations",
                    "FederalDEAInformations.FederalDEAInfoHistory",
                    "FederalDEAInformations.FederalDEAInfoHistory.DEAScheduleInfoHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.FederalDEAInformations.SelectMany(x => x.FederalDEAInfoHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.FEDERAL_DEA_LICENSE_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<MedicareInformationHistory>> GetMedicareInformationHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "MedicareInformations",
                    "MedicareInformations.MedicareInformationHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.MedicareInformations.SelectMany(x => x.MedicareInformationHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.MEDICARE_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<MedicaidInformationHistory>> GetMedicaidInformationHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "MedicaidInformations",
                    "MedicaidInformations.MedicaidInformationHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.MedicaidInformations.SelectMany(x => x.MedicaidInformationHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.MEDICAID_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<CDSCInfoHistory>> GetCDSCInformationHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "CDSCInformations",
                    "CDSCInformations.CDSCInfoHistory"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.CDSCInformations.SelectMany(x => x.CDSCInfoHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.CDSC_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Practice Location

        public async Task<IEnumerable<PracticeLocationDetailHistory>> GetPracticeLocationDetailHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    // Practice Locations
                    "PracticeLocationDetails.PracticeLocationDetailHistory",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Organization",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Group",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Group.Group",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.WorkersCompensationInformation",

                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Language",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Language.NonEnglishLanguages",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Accessibility",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.Question",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Service",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.Question",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.Service.PracticeType",

                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.FacilityPracticeProviders",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderTypes",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderSpecialties",

                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.PracticeOfficeHour",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays.DailyHours",

                    "PracticeLocationDetails.PracticeLocationDetailHistory.OfficeHour",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.OfficeHour.PracticeDays",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.OfficeHour.PracticeDays.DailyHours",
                    
                    "PracticeLocationDetails.PracticeLocationDetailHistory.OpenPracticeStatus",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.OpenPracticeStatus.PracticeQuestionAnswers",

                    "PracticeLocationDetails.PracticeLocationDetailHistory.BillingContactPerson",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.BusinessOfficeManagerOrStaff",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.PaymentAndRemittance",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.PaymentAndRemittance.PaymentAndRemittancePerson",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.PrimaryCredentialingContactPerson",
                    "PracticeLocationDetails.PracticeLocationDetailHistory.PracticeProviders",
                };

                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.PracticeLocationDetails.SelectMany(x => x.PracticeLocationDetailHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PRACTICE_LOCATION_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Education History

        public async Task<IEnumerable<EducationDetailHistory>> GetEducationDetailHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] {
                    "EducationDetails",
                    "EducationDetails.EducationDetailHistory",
                    "EducationDetails.EducationDetailHistory.SchoolInformation"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.EducationDetails.SelectMany(c => c.EducationDetailHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.EDUCATION_DETAIL_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<CMECertificationHistory>> GetCMECertificationHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] {
                    "CMECertifications",
                    "CMECertifications.CMECertificationHistory",
                    "CMECertifications.CMECertificationHistory.SchoolInformation"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.CMECertifications.SelectMany(c => c.CMECertificationHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.CME_CERTIFICATION_HISTORY_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<ProgramDetailHistory>> GetProgramDetailHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] {
                    "ProgramDetails",
                    "ProgramDetails.ProgramDetailHistory",
                    "ProgramDetails.ProgramDetailHistory.Specialty",
                    "ProgramDetails.ProgramDetailHistory.SchoolInformation"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.ProgramDetails.SelectMany(c => c.ProgramDetailHistory);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.PROGRAM_DETAIL_HISTORY_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Contact Information

        public async Task<IEnumerable<ContractGroupInfoHistory>> GetContractGroupInfoHistory(int profileId)
        {
            try
            {
                var includeProperties = new string[] { 
                    "ContractInfoes",
                    "ContractInfoes.ContractGroupInfoes",
                    "ContractInfoes.ContractGroupInfoes.ContractGroupInfoHistory",
                    "ContractInfoes.ContractGroupInfoes.ContractGroupInfoHistory.PracticingGroup",
                    "ContractInfoes.ContractGroupInfoes.ContractGroupInfoHistory.PracticingGroup.Group"
                };
                var profile = await uof.GetProfileRepository().FindAsync(p => p.ProfileID == profileId, includeProperties);
                return profile.ContractInfoes.SelectMany(c => c.ContractGroupInfoes.SelectMany(cgi => cgi.ContractGroupInfoHistory));
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileHistoryException(ExceptionMessage.CONTRACT_INFORMATION_GROUP_HISTORY_GET_EXCEPTION, ex);
            }
        }


   #endregion

    }
}
