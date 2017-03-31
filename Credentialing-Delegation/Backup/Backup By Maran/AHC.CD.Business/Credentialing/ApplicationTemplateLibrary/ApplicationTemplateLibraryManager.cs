using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.ApplicationTemplateLibrary
{
    internal class ApplicationTemplateLibraryManager : IApplicationTemplateLibraryManager
    {
        private IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;

        public ApplicationTemplateLibraryManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
        }

        public async Task<string> GetProfileDataByIdAsync(int profileId)
        {
            try
            {
                string pdfFile = "";

                #region GetProfile Data

                var includeProperties = new string[]
                {
                    //Specialty
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard",
                    "SpecialtyDetails.SpecialtyBoardNotCertifiedDetail",
                    "PracticeInterest",

                    //hospital Privilege
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.Hospital", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactInfo", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactPerson", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.AdmittingPrivilege", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.StaffCategory",

                    //Visa Detail
                    "VisaDetail.VisaInfo.VisaStatus", 
                    "VisaDetail.VisaInfo.VisaType", 

                    //Professional Reference
                    "ProfessionalReferenceInfos.ProviderType", 
                    "ProfessionalReferenceInfos.Specialty",

                    //Professional Liability
                    "ProfessionalLiabilityInfoes.InsuranceCarrier",
                    "ProfessionalLiabilityInfoes.InsuranceCarrierAddress",

                    //State License
                    "StateLicenses.ProviderType",
                    "StateLicenses.StateLicenseStatus",

                    //Personal Detail                    
                    "PersonalDetail.ProviderLevel",
                    "PersonalDetail.ProviderTitles.ProviderType",

                    //PersonalIdentification
                    "PersonalIdentification",

                    //ProfileDisclosure
                    "ProfileDisclosure.ProfileDisclosureQuestionAnswers",

                    //Languages
                    "LanguageInfo.KnownLanguages",  
                  
                    //BirthInformation
                    "BirthInformation",

                    //ECFMG
                    "ECFMGDetail",

                    //MedicareInformations
                    "MedicareInformations",

                    //MedicaidInformations
                    "MedicaidInformations",

                    //FederalDEAInformations
                    "FederalDEAInformations",

                    //Resindency Internship
                    "TrainingDetails.ResidencyInternshipDetails.Specialty",
                    "ProgramDetails.Specialty",

                    //Disclosure Questions
                    //"ProfileDisclosureQuestionAnswer.Question.QuestionCategory",

                    // Practice Locations 
                    "PracticeLocationDetails.Organization",
                    "PracticeLocationDetails.Group",
                    "PracticeLocationDetails.Group.Group",
                    "PracticeLocationDetails.WorkersCompensationInformation",

                    "PracticeLocationDetails.Facility",
                    "PracticeLocationDetails.Facility.FacilityDetail",
                    "PracticeLocationDetails.Facility.FacilityDetail.Language",
                    "PracticeLocationDetails.Facility.FacilityDetail.Language.NonEnglishLanguages",
                    "PracticeLocationDetails.Facility.FacilityDetail.Accessibility",
                    "PracticeLocationDetails.Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers",
                    "PracticeLocationDetails.Facility.FacilityDetail.Service.PracticeType",

                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders",
                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderTypes",
                    "PracticeLocationDetails.Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderSpecialties",

                    "PracticeLocationDetails.Facility.FacilityDetail.PracticeOfficeHour",
                    "PracticeLocationDetails.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays",
                    "PracticeLocationDetails.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays.DailyHours",

                    "PracticeLocationDetails.OfficeHour",
                    "PracticeLocationDetails.OfficeHour.PracticeDays",
                    "PracticeLocationDetails.OfficeHour.PracticeDays.DailyHours",
                    
                    "PracticeLocationDetails.OpenPracticeStatus",
                    "PracticeLocationDetails.OpenPracticeStatus.PracticeQuestionAnswers",

                    "PracticeLocationDetails.BillingContactPerson",
                    "PracticeLocationDetails.BusinessOfficeManagerOrStaff",
                    "PracticeLocationDetails.PaymentAndRemittance",
                    "PracticeLocationDetails.PaymentAndRemittance.PaymentAndRemittancePerson",
                    "PracticeLocationDetails.PrimaryCredentialingContactPerson",
                    "PracticeLocationDetails.PracticeProviders",

                    
                    //Disclosure Questions,

                    //"ProfileDisclosureQuestionAnswer.Question.QuestionCategory",

                    //Contract Information
                    "ContractInfoes.ContractGroupInfoes",
                      "ContractInfoes.ContractGroupInfoes.PracticingGroup",
                        "ContractInfoes.ContractGroupInfoes.PracticingGroup.Group",

                    //work history
                    //"WorkExperience.ProviderType"
                    "ProfessionalWorkExperiences.ProviderType"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile.StateLicenses.Count > 0)
                    profile.StateLicenses = profile.StateLicenses.Where(s => (s.Status != null && s.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.CDSCInformations.Count > 0)
                    profile.CDSCInformations = profile.CDSCInformations.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.CMECertifications.Count > 0)
                    profile.CMECertifications = profile.CMECertifications.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.MedicaidInformations.Count > 0)
                    profile.MedicaidInformations = profile.MedicaidInformations.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.MedicareInformations.Count > 0)
                    profile.MedicareInformations = profile.MedicareInformations.Where(c => (c.Status != null && c.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProgramDetails.Count > 0)
                    profile.ProgramDetails = profile.ProgramDetails.Where(p => (p.Status != null && p.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.EducationDetails.Count > 0)
                    profile.EducationDetails = profile.EducationDetails.Where(e => (e.Status != null && e.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.SpecialtyDetails.Count > 0)
                    profile.SpecialtyDetails = profile.SpecialtyDetails.Where(s => (s.Status != null && s.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.PracticeLocationDetails.Count > 0)
                    profile.PracticeLocationDetails = profile.PracticeLocationDetails.Where(p => (p.Status != null && p.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.HospitalPrivilegeInformation != null && profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Count > 0)
                    profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(h => (h.Status != null && h.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.PublicHealthServices.Count > 0)
                    profile.PublicHealthServices = profile.PublicHealthServices.Where(p => (p.Status != null && p.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.MilitaryServiceInformations.Count > 0)
                    profile.MilitaryServiceInformations = profile.MilitaryServiceInformations.Where(m => (m.Status != null && m.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalWorkExperiences.Count > 0)
                    profile.ProfessionalWorkExperiences = profile.ProfessionalWorkExperiences.Where(w => (w.Status != null && w.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalLiabilityInfoes.Count > 0)
                    profile.ProfessionalLiabilityInfoes = profile.ProfessionalLiabilityInfoes.Where(l => (l.Status != null && l.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.OtherLegalNames.Count > 0)
                    profile.OtherLegalNames = profile.OtherLegalNames.Where(o => (o.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.HomeAddresses.Count > 0)
                    profile.HomeAddresses = profile.HomeAddresses.Where(h => (h.Status != null && h.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.WorkGaps.Count > 0)
                    profile.WorkGaps = profile.WorkGaps.Where(w => (w.Status != null && w.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalReferenceInfos.Count > 0)
                    profile.ProfessionalReferenceInfos = profile.ProfessionalReferenceInfos.Where(r => (r.Status != null && r.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ProfessionalAffiliationInfos.Count > 0)
                    profile.ProfessionalAffiliationInfos = profile.ProfessionalAffiliationInfos.Where(a => (a.Status != null && a.Status != StatusType.Inactive.ToString())).ToList();

                if (profile.ContractInfoes.Count > 0)
                    profile.ContractInfoes = profile.ContractInfoes.Where(c => c.ContractStatus != null && !c.ContractStatus.Equals(ContractStatus.Inactive.ToString())).ToList();

                if (profile.FederalDEAInformations.Count > 0)
                    profile.FederalDEAInformations = profile.FederalDEAInformations.Where(c => c.Status != null && !c.Status.Equals(StatusType.Inactive.ToString())).ToList();

                #endregion

                return pdfFile;

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
