using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.DTO;
using AHC.CD.Business.Profiles;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Document;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    internal class ProfileManager : IProfileManager
    {
        IProfileRepository profileRepository = null;
        IDocumentsManager documentManager = null;
        private IRepositoryManager repositoryManager = null;
        ProfileDocumentManager profileDocumentManager = null;
        IUnitOfWork uow = null;

        public ProfileManager(IUnitOfWork uow, IDocumentsManager documentManager, IRepositoryManager repositoryManager)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.documentManager = documentManager;
            this.repositoryManager = repositoryManager;
            this.profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
            this.uow = uow;
        }

        #region Get Profile Status

        public string GetProfileStatus(int profileID)
        {
            string profileStatus = null;

            try
            {
                Profile profile = profileRepository.Find(x => x.ProfileID == profileID);
                profileStatus = profile.Status;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_BY_ID_GET_EXCEPTION, ex);
            }

            return profileStatus;
        }

        #endregion

        #region Get Profiles

        public async Task<IEnumerable<Profile>> GetAllActiveAsync()
        {
            try
            {

                return await profileRepository.GetAsync(p => p.Status.Equals(StatusType.Active.ToString()));

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ACTIVE_PROFILES_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<Profile>> GetAllAync()
        {
            try
            {
                return await profileRepository.GetAllAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ALL_PROFILE_GET_EXCEPTION, ex);
            }
        }

        public async Task<Profile> GetByIdAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Profiles
                    "OtherIdentificationNumber",
                    "PersonalDetail",
                    "ContractInfoes",
                    "ContractInfoes.ContractGroupInfoes",
                    //Specialty
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard",

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

                profile.StateLicenses = profile.StateLicenses.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList();
                profile.CDSCInformations = profile.CDSCInformations.Where(c => (c.Status != StatusType.Inactive.ToString())).ToList();
                profile.CMECertifications = profile.CMECertifications.Where(c => (c.Status != StatusType.Inactive.ToString())).ToList();
                profile.ProgramDetails = profile.ProgramDetails.Where(p => (p.Status != StatusType.Inactive.ToString())).ToList();
                profile.EducationDetails = profile.EducationDetails.Where(e => (e.Status != StatusType.Inactive.ToString())).ToList();
                profile.SpecialtyDetails = profile.SpecialtyDetails.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList();
                profile.PracticeLocationDetails = profile.PracticeLocationDetails.Where(p => (p.Status != StatusType.Inactive.ToString())).ToList();
                if (profile.HospitalPrivilegeInformation != null)
                {
                    profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Where(h => (h.Status != StatusType.Inactive.ToString())).ToList();
                }
                profile.PublicHealthServices = profile.PublicHealthServices.Where(p => (p.Status != StatusType.Inactive.ToString())).ToList();
                profile.MilitaryServiceInformations = profile.MilitaryServiceInformations.Where(m => (m.Status != StatusType.Inactive.ToString())).ToList();
                profile.ProfessionalWorkExperiences = profile.ProfessionalWorkExperiences.Where(w => (w.Status != StatusType.Inactive.ToString())).ToList();
                profile.ProfessionalLiabilityInfoes = profile.ProfessionalLiabilityInfoes.Where(l => (l.Status != StatusType.Inactive.ToString())).ToList();
                profile.OtherLegalNames = profile.OtherLegalNames.Where(o => (o.Status != StatusType.Inactive.ToString())).ToList();
                profile.HomeAddresses = profile.HomeAddresses.Where(h => (h.Status != StatusType.Inactive.ToString())).ToList();
                profile.WorkGaps = profile.WorkGaps.Where(w => (w.Status != StatusType.Inactive.ToString())).ToList();
                profile.ProfessionalReferenceInfos = profile.ProfessionalReferenceInfos.Where(r => (r.Status != StatusType.Inactive.ToString())).ToList();
                profile.ProfessionalAffiliationInfos = profile.ProfessionalAffiliationInfos.Where(a => (a.Status != StatusType.Inactive.ToString())).ToList();
                profile.ContractInfoes = profile.ContractInfoes.Where(c => !c.ContractStatus.Equals(ContractStatus.Inactive.ToString())).ToList();
                profile.FederalDEAInformations = profile.FederalDEAInformations.Where(c => !c.Status.Equals(StatusType.Inactive.ToString())).ToList();


                return profile;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_BY_ID_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<Object>> GetAllProfileForOperationAsync()
        {
            try
            {
                List<Object> profiles = new List<Object>();

                foreach (var item in await profileRepository.GetAsync(p => p.Status.Equals(StatusType.Active.ToString())))
                {
                    profiles.Add(new { ProfileID = item.ProfileID, PersonalDetail = item.PersonalDetail });
                }

                return profiles;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ACTIVE_PROFILES_GET_EXCEPTION, ex);
            }
        }

        public async Task<bool> ProfileExistAsync(int profileId)
        {
            try
            {
                return await profileRepository.AnyAsync(p => p.ProfileID == profileId);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_BY_ID_GET_EXCEPTION, ex);
            }
        }

        public async Task<IEnumerable<Profile>> GetAllProfileByProviderLevel(string providerLevel, int profileId)
        {
            try
            {
                List<Profile> midLevels = new List<Profile>();

                var practitioners = from provider in await profileRepository.GetAllAsync("PersonalDetail.ProviderLevel,PersonalDetail.ProviderTitles.ProviderType,OtherIdentificationNumber,SpecialtyDetails.Specialty, PracticeLocationDetails.Facility")
                                    where provider.PersonalDetail.ProviderLevel.Name.Equals(providerLevel) &&
                                          provider.ProfileID != profileId
                                    select provider;

                //var practitioners = from provider in await profileRepository.GetAllAsync("PersonalDetail.ProviderTitles.ProviderType.ProviderLevel,OtherIdentificationNumber,SpecialtyDetails.Specialty")
                //                    where provider.PersonalDetail.ProviderTitles != null &&
                //                          provider.PersonalDetail.ProviderTitles.Any(pt => pt.ProviderType.ProviderLevel.Name.Equals(providerLevel)) &&
                //                          provider.ProfileID != profileId
                //                    select provider;  

                return practitioners;

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ALL_PROFILE_GET_EXCEPTION, ex);
            }
        }

        public async Task<string> GetAllProviderLevelByProfileId(int profileID)
        {
            try
            {
                IEnumerable<Profile> profiles = await profileRepository.GetAsync(p => p.ProfileID == profileID, "PersonalDetail.ProviderLevel");

                Profile profile = profiles.FirstOrDefault();

                return profile.PersonalDetail.ProviderLevel.Name;

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ALL_PROFILE_GET_EXCEPTION, ex);
            }

        }

        #endregion

        #region Deactivate Profile

        public async Task DeactivateProfile(int profileID)
        {
            try
            {
                //Assigning deactivate status
                var status = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;

                //Deactivate profile
                profileRepository.DeactivateProfile(profileID, status);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.UNABLE_TO_DEACTIVATE_PROFILE, ex);
            }
        }

        #endregion

        #region Deactivate Profile

        public async Task ReactivateProfile(int profileID)
        {
            try
            {
                //Assigning deactivate status
                var status = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                //Deactivate profile
                profileRepository.DeactivateProfile(profileID, status);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.UNABLE_TO_REACTIVATE_PROFILE, ex);
            }
        }

        #endregion

        #region Demographics

        public async Task<string> UpdateProfileImageAsync(int profileId, DocumentDTO document)
        {
            try
            {
                //Add the profile Image
                document.DocRootPath = DocumentRootPath.PROFILE_IMAGE_PATH;
                var profileImagePath = profileDocumentManager.AddDocumentInPath(document);

                //Update the image information
                var oldImagePath = profileRepository.UpdateProfileImage(profileId, profileImagePath);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldImagePath))
                    documentManager.DeleteFile(oldImagePath);

                return profileImagePath;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_IMAGE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveProfileImageAsync(int profileId, string imagePath)
        {
            //Delete the previous file
            if (!String.IsNullOrEmpty(imagePath))
                documentManager.DeleteFile(imagePath);

            //Update the image information
            var oldImagePath = profileRepository.UpdateProfileImage(profileId, null);

            //save the information in the repository
            await profileRepository.SaveAsync();
        }

        #region Personal Detail

        public async Task UpdatePersonalDetailAsync(int profileId, Entities.MasterProfile.Demographics.PersonalDetail personalDetail)
        {
            try
            {
                //Add personal detail information
                profileRepository.UpdatePersonalDetail(profileId, personalDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PERSONAL_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Other Legal Name

        public async Task<int> AddOtherLegalNamesAsync(int profileId, Entities.MasterProfile.Demographics.OtherLegalName otherLegalName, DocumentDTO document)
        {
            try
            {
                otherLegalName.DocumentPath = AddDocument(DocumentRootPath.OTHER_LEGAL_NAME_PATH, DocumentTitle.OTHER_LEGAL_NAME, null, document, profileId);

                //Add other legal name information
                profileRepository.AddOtherLegalNames(profileId, otherLegalName);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return otherLegalName.OtherLegalNameID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_LEGAL_NAME_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateOtherLegalNamesAsync(int profileId, Entities.MasterProfile.Demographics.OtherLegalName otherLegalName, DocumentDTO document)
        {
            try
            {
                string oldFilePath = otherLegalName.DocumentPath;

                otherLegalName.DocumentPath = AddUpdateDocument(DocumentRootPath.OTHER_LEGAL_NAME_PATH, otherLegalName.DocumentPath, DocumentTitle.OTHER_LEGAL_NAME, null, document, profileId);

                //Update the other legal name information
                profileRepository.UpdateOtherLegalNames(profileId, otherLegalName);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(otherLegalName.DocumentPath))
                    documentManager.DeleteFile(oldFilePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_LEGAL_NAME_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveOtherLegalNameAsync(int profileId, OtherLegalName otherLegalName)
        {
            try
            {
                //Add to history
                profileRepository.AddOtherLegalNameHistory(profileId, otherLegalName.OtherLegalNameID);

                //Remove Other Legal Name
                profileRepository.RemoveOtherLegalName(profileId, otherLegalName);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_LEGAL_NAME_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Home Address

        public async Task<int> AddHomeAddressAsync(int profileId, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {
            try
            {
                if (homeAddress.AddressPreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.HomeAddresses.Any(h => h.AddressPreference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllHomeAddressAsSecondary(profileId);
                }

                //Add home address information
                profileRepository.AddHomeAddress(profileId, homeAddress);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return homeAddress.HomeAddressID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOME_ADDRESS_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateHomeAddressAsync(int profileId, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {
            try
            {
                if (homeAddress.AddressPreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.HomeAddresses.Any(h => h.HomeAddressID != homeAddress.HomeAddressID && h.AddressPreference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllHomeAddressAsSecondary(profileId);
                }

                //Update home address information
                profileRepository.UpdateHomeAddress(profileId, homeAddress);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOME_ADDRESS_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveHomeAddressAsync(int profileId, HomeAddress homeAddress)
        {
            try
            {
                //Add to history
                profileRepository.AddHomeAddressHistory(profileId, homeAddress.HomeAddressID);

                //Remove Other Legal Name
                profileRepository.RemoveHomeAddress(profileId, homeAddress);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOME_ADDRESS_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Contact Detail

        public async Task UpdateContactDetailAsync(int profileId, Entities.MasterProfile.Demographics.ContactDetail contactDetail)
        {
            try
            {
                var duplicateKeys = contactDetail.PhoneDetails.GroupBy(x => x.PhoneNumber).Where(group => group.Count() > 1).Select(group => group.Key);
                if (duplicateKeys.Count() > 0)
                    throw new DuplicateContactDetailException(String.Format(ExceptionMessage.DUPLICATE_CONTACT_DETIAL_EXCEPTION, String.Join(", ", duplicateKeys)));

                duplicateKeys = contactDetail.EmailIDs.GroupBy(x => x.EmailAddress).Where(group => group.Count() > 1).Select(group => group.Key);
                if (duplicateKeys.Count() > 0)
                    throw new DuplicateContactDetailException(String.Format(ExceptionMessage.DUPLICATE_CONTACT_DETIAL_EXCEPTION, String.Join(", ", duplicateKeys)));


                var phoneDetails = await repositoryManager.GetAllAsync<PhoneDetail>();

                foreach (var item in contactDetail.PhoneDetails)
                {
                    if (phoneDetails.Any(h => h.PhoneDetailID != item.PhoneDetailID && h.PhoneNumber == item.PhoneNumber))
                        DuplicateNumber(item);

                    if (await profileRepository.AnyAsync(p => p.ContactDetail.PersonalPager.Equals(item.PhoneNumber)))
                        DuplicateNumber(item);
                }

                if (contactDetail.PersonalPager != null &&
                    await profileRepository.AnyAsync(p => p.ContactDetail.ContactDetailID != contactDetail.ContactDetailID &&
                                                          p.ContactDetail.PersonalPager.Equals(contactDetail.PersonalPager)))
                    throw new DuplicatePagerNumberException(String.Format(ExceptionMessage.PAGER_NUMBER_EXISTS_EXCEPTION, contactDetail.PersonalPager));

                if (contactDetail.PersonalPager != null &&
                    phoneDetails.Any(p => p.PhoneNumber.Equals(contactDetail.PersonalPager)))
                    throw new DuplicatePagerNumberException(String.Format(ExceptionMessage.PAGER_NUMBER_EXISTS_EXCEPTION, contactDetail.PersonalPager));

                var emails = await repositoryManager.GetAllAsync<EmailDetail>();

                foreach (var item in contactDetail.EmailIDs)
                {
                    if (emails.Any(h => h.EmailDetailID != item.EmailDetailID && h.EmailAddress.Equals(item.EmailAddress)))
                        throw new DuplicateEmailException(String.Format(ExceptionMessage.EMAIL_EXISTS_EXCEPTION, item.EmailAddress));
                }

                //Update contact detail information
                profileRepository.UpdateContactDetail(profileId, contactDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CONTACT_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        private static void DuplicateNumber(PhoneDetail item)
        {
            switch (item.PhoneTypeEnum)
            {
                case PhoneTypeEnum.Home:
                    throw new DuplicateHomeNumberException(String.Format(ExceptionMessage.HOME_NUMBER_EXISTS_EXCEPTION, item.PhoneNumber));
                case PhoneTypeEnum.Fax:
                    throw new DuplicateFaxNumberException(String.Format(ExceptionMessage.FAX_NUMBER_EXISTS_EXCEPTION, item.PhoneNumber));
                case PhoneTypeEnum.Mobile:
                    throw new DuplicateMobileNumberException(String.Format(ExceptionMessage.MOBILE_NUMBER_EXISTS_EXCEPTION, item.PhoneNumber));
                default:
                    break;
            }
        }

        #endregion

        #region Personal Identification

        public async Task UpdatePersonalIdentificationAsync(int profileId, Entities.MasterProfile.Demographics.PersonalIdentification personalIdentification, DocumentDTO dlDocument, DocumentDTO ssnDocument)
        {
            try
            {
                if (personalIdentification.DL != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.PersonalIdentification.DL.Equals(personalIdentification.DL)))
                {
                    throw new DuplicateDLNumberException(ExceptionMessage.DRIVING_LICENSE_EXIST_EXCEPTION);
                }

                if (personalIdentification.SSN != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.PersonalIdentification.SocialSecurityNumber.Equals(personalIdentification.SocialSecurityNumber)))
                {
                    throw new DuplicateSSNNumberException(ExceptionMessage.SSN_EXISTS);
                }


                string oldDLCertificatePath = personalIdentification.DLCertificatePath;
                personalIdentification.DLCertificatePath = AddUpdateDocument(DocumentRootPath.DL_PATH, personalIdentification.DLCertificatePath, DocumentTitle.DL, null, dlDocument, profileId);

                string oldSSNCertificatePath = personalIdentification.SSNCertificatePath;
                personalIdentification.SSNCertificatePath = AddUpdateDocument(DocumentRootPath.SSN_PATH, personalIdentification.SSNCertificatePath, DocumentTitle.SSN, null, ssnDocument, profileId);

                //Update the personal identification information
                profileRepository.UpdatePersonalIdentification(profileId, personalIdentification);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldSSNCertificatePath) && !oldSSNCertificatePath.Equals(personalIdentification.SSNCertificatePath))
                    documentManager.DeleteFile(oldSSNCertificatePath);

                if (!String.IsNullOrEmpty(oldDLCertificatePath) && !oldDLCertificatePath.Equals(personalIdentification.DLCertificatePath))
                    documentManager.DeleteFile(oldDLCertificatePath);

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PERSONAL_IDENTIFICATION_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Birth Information

        public async Task UpdateBirthInformationAsync(int profileId, Entities.MasterProfile.Demographics.BirthInformation birthInformation, DocumentDTO document)
        {
            try
            {
                string oldFilePath = birthInformation.BirthCertificatePath;
                birthInformation.BirthCertificatePath = AddUpdateDocument(DocumentRootPath.BIRTH_CERTIFICATE_PATH, birthInformation.BirthCertificatePath, DocumentTitle.BIRTH_CERTIFICATE, null, document, profileId);

                //Update the birth certificate information
                profileRepository.UpdateBirthInformation(profileId, birthInformation);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(birthInformation.BirthCertificatePath))
                    documentManager.DeleteFile(oldFilePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.BIRTH_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Visa Detail

        public async Task UpdateVisaInformationAsync(int profileId, Entities.MasterProfile.Demographics.VisaDetail visaDetail, DocumentDTO visaDocument, DocumentDTO greenCarddocument, DocumentDTO nationalIDdocument)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.VisaDetail.VisaDetailID == visaDetail.VisaDetailID &&
                                                         !p.VisaDetail.IsResidentOfUSA.Equals(visaDetail.IsResidentOfUSA)))
                {
                    profileRepository.AddVisaInformationHistory(profileId);
                }

                string oldVisaCertificatePath = null;
                string oldGreenCardCertificatePath = null;
                string oldNationalIDCertificatePath = null;

                if (visaDetail.VisaInfo != null)
                {
                    if (visaDetail.VisaInfo.VisaNumber != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.VisaNumberStored.Equals(visaDetail.VisaInfo.VisaNumberStored)))
                    {
                        throw new VisaNumberExistException(ExceptionMessage.VISA_NUMBER_EXISTS_EXCEPTION);
                    }

                    if (visaDetail.VisaInfo.GreenCardNumberStored != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.GreenCardNumberStored.Equals(visaDetail.VisaInfo.GreenCardNumberStored)))
                    {
                        throw new GreenCardNumberExistException(ExceptionMessage.GREEN_CARD_NUMBER_EXISTS_EXCEPTION);
                    }

                    if (visaDetail.VisaInfo.NationalIDNumberNumberStored != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.NationalIDNumberNumberStored.Equals(visaDetail.VisaInfo.NationalIDNumberNumberStored)))
                    {
                        throw new NationalIDNumberExistException(ExceptionMessage.NATIONAL_ID_NUMBER_EXISTS_EXCEPTION);
                    }

                    oldVisaCertificatePath = visaDetail.VisaInfo.VisaCertificatePath;
                    oldGreenCardCertificatePath = visaDetail.VisaInfo.GreenCardCertificatePath;
                    oldNationalIDCertificatePath = visaDetail.VisaInfo.NationalIDCertificatePath;

                    visaDetail.VisaInfo.VisaCertificatePath = AddUpdateDocumentInformation(DocumentRootPath.VISA_PATH, visaDetail.VisaInfo.VisaCertificatePath, DocumentTitle.VISA, visaDetail.VisaInfo.VisaExpirationDate, visaDocument, profileId);
                    visaDetail.VisaInfo.GreenCardCertificatePath = AddUpdateDocument(DocumentRootPath.GREEN_CARD_PATH, visaDetail.VisaInfo.GreenCardCertificatePath, DocumentTitle.GREEN_CARD, null, greenCarddocument, profileId);
                    visaDetail.VisaInfo.NationalIDCertificatePath = AddUpdateDocument(DocumentRootPath.NATIONAL_IDENTIFICATION_PATH, visaDetail.VisaInfo.NationalIDCertificatePath, DocumentTitle.NATIONAL_IDENTIFICATION, null, nationalIDdocument, profileId);
                }

                //Update the personal identification information
                profileRepository.UpdateVisaInformation(profileId, visaDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldVisaCertificatePath) && !oldVisaCertificatePath.Equals(visaDetail.VisaInfo.VisaCertificatePath))
                    documentManager.DeleteFile(oldVisaCertificatePath);

                if (!String.IsNullOrEmpty(oldGreenCardCertificatePath) && !oldGreenCardCertificatePath.Equals(visaDetail.VisaInfo.GreenCardCertificatePath))
                    documentManager.DeleteFile(oldGreenCardCertificatePath);

                if (!String.IsNullOrEmpty(oldNationalIDCertificatePath) && !oldNationalIDCertificatePath.Equals(visaDetail.VisaInfo.NationalIDCertificatePath))
                    documentManager.DeleteFile(oldNationalIDCertificatePath);

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.VISA_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Language

        public async Task UpdateLanguageInformationAsync(int profileId, Entities.MasterProfile.Demographics.LanguageInfo languageInformation)
        {
            try
            {
                //Update language information
                profileRepository.UpdateLanguageInformation(profileId, languageInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.LANGUAGE_INFO_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #endregion

        #region Identification And License

        #region State License

        public async Task<int> AddStateLicenseAsync(int profileId, Entities.MasterProfile.IdentificationAndLicenses.StateLicenseInformation stateLicense, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.StateLicenses.Any(s => s.LicenseNumber.Equals(stateLicense.LicenseNumber))))
                {
                    throw new DuplicateStateLicenseNumberException(ExceptionMessage.STATE_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }

                stateLicense.StateLicenseDocumentPath = AddDocument(DocumentRootPath.STATE_LICENSE_PATH, DocumentTitle.STATE_LICENSE, stateLicense.ExpiryDate, document, profileId);

                //Add state license information
                profileRepository.AddStateLicense(profileId, stateLicense);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return stateLicense.StateLicenseInformationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.STATE_LICENSE_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateStateLicenseAsync(int profileId, Entities.MasterProfile.IdentificationAndLicenses.StateLicenseInformation stateLicense, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.StateLicenses.Any(s => s.StateLicenseInformationID != stateLicense.StateLicenseInformationID && s.LicenseNumber.Equals(stateLicense.LicenseNumber))))
                {
                    throw new DuplicateStateLicenseNumberException(ExceptionMessage.STATE_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }

                var oldFilePath = stateLicense.StateLicenseDocumentPath;
                stateLicense.StateLicenseDocumentPath = AddUpdateDocumentInformation(DocumentRootPath.STATE_LICENSE_PATH, oldFilePath, DocumentTitle.STATE_LICENSE, stateLicense.ExpiryDate, document, profileId);

                //Update the state license information
                profileRepository.UpdateStateLicense(profileId, stateLicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, stateLicense.StateLicenseDocumentPath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.STATE_LICENSE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RenewStateLicenseAsync(int profileId, Entities.MasterProfile.IdentificationAndLicenses.StateLicenseInformation stateLicense, DocumentDTO document)
        {
            try
            {
                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.STATE_LICENSE, stateLicense.StateLicenseDocumentPath, stateLicense.ExpiryDate); ;

                //Add or update the document if present
                stateLicense.StateLicenseDocumentPath = AddUpdateDocumentInformation(DocumentRootPath.STATE_LICENSE_PATH, stateLicense.StateLicenseDocumentPath, DocumentTitle.STATE_LICENSE, stateLicense.ExpiryDate, document, profileId);

                //Update the state license history information
                profileRepository.AddStateLicenseHistory(profileId, stateLicense.StateLicenseInformationID);

                //Update the state license information
                profileRepository.UpdateStateLicense(profileId, stateLicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.STATE_LICENSE_RENEW_EXCEPTION, ex);
            }
        }

        public async Task RemoveStateLicenseAsync(int profileId, StateLicenseInformation stateLicense)
        {
            try
            {
                //Save record into History
                profileRepository.AddStateLicenseHistoryForRemoval(profileId, stateLicense.StateLicenseInformationID);

                //Remove Specialty Details
                profileRepository.RemoveStateLicense(profileId, stateLicense);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.STATE_LICENSE_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Federal DEA License

        public async Task<int> AddFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.FederalDEAInformations.Any(s => s.DEANumber.Equals(federalDEALicense.DEANumber))))
                {
                    throw new DuplicateFederalDEALicenseNumberException(ExceptionMessage.FEDERAL_DEA_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }

                federalDEALicense.DEALicenceCertPath = AddDocument(DocumentRootPath.DEA_PATH, DocumentTitle.DEA, federalDEALicense.ExpiryDate, document, profileId);

                //Add Federal DEA license information
                profileRepository.AddFederalDEALicense(profileId, federalDEALicense);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return federalDEALicense.FederalDEAInformationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.FEDERAL_DEA_LICENSE_CREATE_EXCEPTION, ex);
            }
        }

        private static void IsScheduleInfoDuplicate(FederalDEAInformation federalDEALicense, List<FederalDEAInformation> federalDEA)
        {
            if (federalDEA != null)
            {
                bool allScedulesMatched = false;

                foreach (var item in federalDEA)
                {
                    bool status = true;

                    foreach (var item1 in item.DEAScheduleInfoes)
                    {
                        if (!federalDEALicense.DEAScheduleInfoes.Any(d => d.DEAScheduleID == item1.DEAScheduleID && d.IsEligible == item1.IsEligible))
                        {
                            status = false;
                            break;
                        }
                    }

                    if (status)
                    {
                        allScedulesMatched = true;
                        break;
                    }
                }

                if (allScedulesMatched)
                    throw new DuplicateFederalDEALicenseNumberException(ExceptionMessage.FEDERAL_DEA_LICENSE_EXISTS_EXCEPTION);
            }
        }

        public async Task UpdateFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.FederalDEAInformations.Any(s => s.FederalDEAInformationID != federalDEALicense.FederalDEAInformationID && s.DEANumber.Equals(federalDEALicense.DEANumber))))
                {
                    throw new DuplicateFederalDEALicenseNumberException(ExceptionMessage.FEDERAL_DEA_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }

                var oldFilePath = federalDEALicense.DEALicenceCertPath;
                federalDEALicense.DEALicenceCertPath = AddUpdateDocumentInformation(DocumentRootPath.DEA_PATH, oldFilePath, DocumentTitle.DEA, federalDEALicense.ExpiryDate, document, profileId);

                //Update the federal DEA license information
                profileRepository.UpdateFederalDEALicense(profileId, federalDEALicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, federalDEALicense.DEALicenceCertPath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.FEDERAL_DEA_LICENSE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RenewFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense, DocumentDTO document)
        {
            try
            {
                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.DEA, federalDEALicense.DEALicenceCertPath, federalDEALicense.ExpiryDate); ;

                //Add or update the document if present
                federalDEALicense.DEALicenceCertPath = AddUpdateDocumentInformation(DocumentRootPath.DEA_PATH, federalDEALicense.DEALicenceCertPath, DocumentTitle.DEA, federalDEALicense.ExpiryDate, document, profileId);

                //Update the federal DEA license history information
                profileRepository.AddFederalDEALicenseHistory(profileId, federalDEALicense.FederalDEAInformationID);

                //Update the federal DEA license information
                profileRepository.UpdateFederalDEALicense(profileId, federalDEALicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.FEDERAL_DEA_LICENSE_RENEW_EXCEPTION, ex);
            }
        }

        public async Task RemoveFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense)
        {
            try
            {
                //Save record into History
                profileRepository.AddFederalDEALicenseHistoryForRemoval(profileId, federalDEALicense.FederalDEAInformationID);

                profileRepository.RemoveFederalDEALicense(profileId, federalDEALicense);
                await profileRepository.SaveAsync();
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_REFRENCE_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region CDSC Information

        public async Task<int> AddCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.CDSCInformations.Any(s => s.CertNumber.Equals(cdscLicense.CertNumber) && !s.Status.Equals(StatusType.Inactive.ToString()))))
                {
                    throw new DuplicateCDSCLicenseNumberException(ExceptionMessage.CDSC_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }

                cdscLicense.CDSCCerificatePath = AddDocument(DocumentRootPath.CDSC_PATH, DocumentTitle.CDSC, cdscLicense.ExpiryDate, document, profileId);

                //Add CDSC license license information
                profileRepository.AddCDSCLicense(profileId, cdscLicense);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return cdscLicense.CDSCInformationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CDSC_LICENSE_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.CDSCInformations.Any(s => s.CDSCInformationID != cdscLicense.CDSCInformationID && s.CertNumber.Equals(cdscLicense.CertNumber) && !s.Status.Equals(StatusType.Inactive.ToString()))))
                {
                    throw new DuplicateCDSCLicenseNumberException(ExceptionMessage.CDSC_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }

                var oldFilePath = cdscLicense.CDSCCerificatePath;
                cdscLicense.CDSCCerificatePath = AddUpdateDocumentInformation(DocumentRootPath.CDSC_PATH, oldFilePath, DocumentTitle.CDSC, cdscLicense.ExpiryDate, document, profileId);

                //Update the CDSC license information
                profileRepository.UpdateCDSCLicense(profileId, cdscLicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, cdscLicense.CDSCCerificatePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CDSC_LICENSE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RenewCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense, DocumentDTO document)
        {
            try
            {
                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.CDSC, cdscLicense.CDSCCerificatePath, cdscLicense.ExpiryDate); ;

                //Add or update the document if present
                cdscLicense.CDSCCerificatePath = AddUpdateDocumentInformation(DocumentRootPath.CDSC_PATH, cdscLicense.CDSCCerificatePath, DocumentTitle.CDSC, cdscLicense.ExpiryDate, document, profileId);

                //Update the CDSC license history information
                profileRepository.AddCDSCLicenseHistory(profileId, cdscLicense.CDSCInformationID);

                //Update the CDSC license information
                profileRepository.UpdateCDSCLicense(profileId, cdscLicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CDSC_LICENSE_RENEW_EXCEPTION, ex);
            }
        }

        public async Task RemoveCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense)
        {
            try
            {
                //Save record into History
                profileRepository.AddCDSCLicenseHistoryForRemoval(profileId, cdscLicense.CDSCInformationID);

                //Remove Specialty Details
                profileRepository.RemoveCDSCLicense(profileId, cdscLicense);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CDSC_LICENSE_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Medicare Information

        public async Task<int> AddMedicareInformationAsync(int profileId, MedicareInformation medicareInformation, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.MedicareInformations.Any(s => s.LicenseNumber.Equals(medicareInformation.LicenseNumber))))
                {
                    throw new DuplicateMedicareNumberException(ExceptionMessage.MEDICARE_NUMBER_EXISTS_EXCEPTION);
                }

                medicareInformation.CertificatePath = AddDocument(DocumentRootPath.MEDICARE_PATH, DocumentTitle.MEDICARE, null, document, profileId);

                //Add medicare information
                profileRepository.AddMedicareInformation(profileId, medicareInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return medicareInformation.MedicareInformationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.MEDICARE_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateMedicareInformationAsync(int profileId, MedicareInformation medicareInformation, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.MedicareInformations.Any(s => s.MedicareInformationID != medicareInformation.MedicareInformationID && s.LicenseNumber.Equals(medicareInformation.LicenseNumber))))
                {
                    throw new DuplicateMedicareNumberException(ExceptionMessage.MEDICARE_NUMBER_EXISTS_EXCEPTION);
                }

                string oldFilePath = medicareInformation.CertificatePath;
                medicareInformation.CertificatePath = AddUpdateDocument(DocumentRootPath.MEDICARE_PATH, oldFilePath, DocumentTitle.MEDICARE, null, document, profileId);

                //Update medicare information
                profileRepository.UpdateMedicareInformation(profileId, medicareInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, medicareInformation.CertificatePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.MEDICARE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveMedicareInformationAsync(int profileId, MedicareInformation medicareInformation)
        {
            //Save record into History
            profileRepository.AddMedicareInformationHistory(profileId, medicareInformation.MedicareInformationID);

            profileRepository.RemoveMedicareInformation(profileId, medicareInformation);

            await profileRepository.SaveAsync();
        }

        #endregion

        #region Medicaid Information

        public async Task<int> AddMedicaidInformationAsync(int profileId, MedicaidInformation medicaidInformation, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.MedicaidInformations.Any(s => s.LicenseNumber.Equals(medicaidInformation.LicenseNumber))))
                {
                    throw new DuplicateMedicaidNumberException(ExceptionMessage.MEDICAID_NUMBER_EXISTS_EXCEPTION);
                }

                medicaidInformation.CertificatePath = AddDocument(DocumentRootPath.MEDICAID_PATH, DocumentTitle.MEDICAID, null, document, profileId);

                //Add medicaid information
                profileRepository.AddMedicaidInformation(profileId, medicaidInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return medicaidInformation.MedicaidInformationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.MEDICARE_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateMedicaidInformationAsync(int profileId, MedicaidInformation medicaidInformation, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.MedicaidInformations.Any(s => s.MedicaidInformationID != medicaidInformation.MedicaidInformationID && s.LicenseNumber.Equals(medicaidInformation.LicenseNumber))))
                {
                    throw new DuplicateMedicaidNumberException(ExceptionMessage.MEDICAID_NUMBER_EXISTS_EXCEPTION);
                }

                string oldFilePath = medicaidInformation.CertificatePath;
                medicaidInformation.CertificatePath = AddUpdateDocument(DocumentRootPath.MEDICAID_PATH, oldFilePath, DocumentTitle.MEDICAID, null, document, profileId);

                //Update medicaid information
                profileRepository.UpdateMedicaidInformation(profileId, medicaidInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, medicaidInformation.CertificatePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.MEDICAID_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveMedicaidInformationAsync(int profileId, MedicaidInformation medicaidInformation)
        {
            //Save record into History
            profileRepository.AddMedicaidInformationHistory(profileId, medicaidInformation.MedicaidInformationID);

            profileRepository.RemoveMedicaidInformation(profileId, medicaidInformation);

            await profileRepository.SaveAsync();
        }

        #endregion

        #region Other Identification Number

        public async Task UpdateOtherIdentificationNumberAsync(int profileId, OtherIdentificationNumber otherIdentificationNumber)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.OtherIdentificationNumber.NPINumber.Equals(otherIdentificationNumber.NPINumber)))
                {
                    throw new DuplicateNPINumberException(ExceptionMessage.NPI_NUMEBR_EXIST_EXCEPTION);
                }

                if (otherIdentificationNumber.CAQHNumber != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.OtherIdentificationNumber.CAQHNumber.Equals(otherIdentificationNumber.CAQHNumber)))
                {
                    throw new DuplicateCAQHNumberException(ExceptionMessage.CAQH_NUMEBR_EXIST_EXCEPTION);
                }

                if (otherIdentificationNumber.UPINNumber != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.OtherIdentificationNumber.UPINNumber.Equals(otherIdentificationNumber.UPINNumber)))
                {
                    throw new DuplicateUPINNumberException(ExceptionMessage.UPIN_NUMEBR_EXIST_EXCEPTION);
                }

                if (otherIdentificationNumber.USMLENumber != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.OtherIdentificationNumber.USMLENumber.Equals(otherIdentificationNumber.USMLENumber)))
                {
                    throw new DuplicateUSMLENumberException(ExceptionMessage.USMLE_NUMEBR_EXIST_EXCEPTION);
                }

                if (otherIdentificationNumber.NPIUserName != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.OtherIdentificationNumber.NPIUserName.Equals(otherIdentificationNumber.NPIUserName)))
                {
                    throw new DuplicateNPIUsernameException(ExceptionMessage.NPI_USERNAME_EXIST_EXCEPTION);
                }

                if (otherIdentificationNumber.CAQHUserName != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.OtherIdentificationNumber.CAQHUserName.Equals(otherIdentificationNumber.CAQHUserName)))
                {
                    throw new DuplicateCAQHUserNameException(ExceptionMessage.CAQH_USERNAME_EXIST_EXCEPTION);
                }

                //Update other identification information
                profileRepository.UpdateOtherIdentificationNumber(profileId, otherIdentificationNumber);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_IDENTIFICATION_NUMBER_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #endregion

        #region Specialty/Board

        #region Specialty Detail

        public async Task<int> AddSpecialtyDetailAsync(int profileId, SpecialtyDetail specialtyDetail, DocumentDTO document)
        {
            try
            {
                if (specialtyDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.SpecialtyDetails.Any(h => h.SpecialtyPreference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllSpecialityAsSecondary(profileId);
                }

                //Create a profile document object
                if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                {
                    specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = AddDocument(DocumentRootPath.SPECIALITY_BOARD_PATH, DocumentTitle.SPECIALITY_BOARD, specialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate, document, profileId);
                }

                //Add specialty board information
                profileRepository.AddSpecialtyDetail(profileId, specialtyDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return specialtyDetail.SpecialtyDetailID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.SPECIALITY_BOARD_CREATE_EXCEPTION, ex);
            }
        }

        public async Task<SpecialtyDetail> UpdateSpecialtyDetailAsync(int profileId, SpecialtyDetail specialtyDetail, DocumentDTO document)
        {
            try
            {
                if (specialtyDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.SpecialtyDetails.Any(h => h.SpecialtyDetailID != specialtyDetail.SpecialtyDetailID && h.SpecialtyPreference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllSpecialityAsSecondary(profileId);
                }

                string oldFilePath = null;
                string newFilePath = null;

                //Create a profile document object
                if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                {
                    oldFilePath = specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath;
                    newFilePath = specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = AddUpdateDocumentInformation(DocumentRootPath.SPECIALITY_BOARD_PATH, oldFilePath, DocumentTitle.SPECIALITY_BOARD, specialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate, document, profileId);
                }

                //Update the state license information
                SpecialtyDetail updatedSpecialityDetail = profileRepository.UpdateSpecialtyDetail(profileId, specialtyDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, newFilePath);

                return updatedSpecialityDetail;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.SPECIALITY_BOARD_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RenewSpecialtyDetailAsync(int profileId, SpecialtyDetail specialtyDetail, DocumentDTO document)
        {
            try
            {
                if (specialtyDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.SpecialtyDetails.Any(h => h.SpecialtyID != specialtyDetail.SpecialtyDetailID && h.SpecialtyPreference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllSpecialityAsSecondary(profileId);
                }

                //Create a profile document object
                if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                {
                    specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = AddUpdateDocumentInformation(DocumentRootPath.SPECIALITY_BOARD_PATH, specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath, DocumentTitle.SPECIALITY_BOARD, specialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate, document, profileId);

                    //Add the board specialty history information
                    profileRepository.AddSpecialtyBoardCertifiedDetailHistory(profileId, specialtyDetail.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID);
                }

                //Update the state license information
                profileRepository.UpdateSpecialtyDetail(profileId, specialtyDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.STATE_LICENSE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveSpecialityDetailAsync(int profileId, SpecialtyDetail specialtyDetail)
        {
            try
            {
                //Remove Specialty Details
                profileRepository.RemoveSpecialityDetail(profileId, specialtyDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.SPECIALITY_BOARD_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Practice Of Interest

        public async Task UpdatePracticeInterestAsync(int profileId, Entities.MasterProfile.BoardSpecialty.PracticeInterest practiceInterest)
        {
            try
            {
                //Update practice of interest information
                profileRepository.UpdatePracticeInterest(profileId, practiceInterest);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_IDENTIFICATION_NUMBER_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #endregion

        #region Hospital Privilege

        public async Task UpdateHospitalPrivilegeInformationAsync(int profileId, HospitalPrivilegeInformation hospitalPrivilegeInformation)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.HospitalPrivilegeInformation.HospitalPrivilegeInformationID == hospitalPrivilegeInformation.HospitalPrivilegeInformationID &&
                                                         !p.HospitalPrivilegeInformation.HasHospitalPrivilege.Equals(hospitalPrivilegeInformation.HasHospitalPrivilege)))
                {
                    profileRepository.AddHospitalPrivilegeInformationHistory(profileId);
                }

                //Add hospital privilege information
                profileRepository.UpdateHospitalPrivilegeInformation(profileId, hospitalPrivilegeInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOSPITAL_PRIVILEGE_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddHospitalPrivilegeDetailAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail, DocumentDTO document)
        {
            try
            {
                if (hospitalPrivilegeDetail.AffilicationStartDate != null && hospitalPrivilegeDetail.AffiliationEndDate != null && await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                          p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.AffilicationStartDate.Value.Equals(hospitalPrivilegeDetail.AffilicationStartDate.Value) &&
                                                                                                                           h.AffiliationEndDate.Value.Equals(hospitalPrivilegeDetail.AffiliationEndDate.Value))))
                {
                    throw new DuplicateHospitalPrivilegeException(ExceptionMessage.HOSPITAL_PRIVILEGE_INFORMATION_EXISTS_EXCEPTION);
                }

                if (hospitalPrivilegeDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                          p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.Preference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllHospitalPrivilegeAsSecondary(profileId);
                }

                hospitalPrivilegeDetail.HospitalPrevilegeLetterPath = AddDocument(DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, DocumentTitle.HOSPITAL_PRIVILEGE, hospitalPrivilegeDetail.AffiliationEndDate, document, profileId);

                //Add hospital privilege information
                profileRepository.AddHospitalPrivilegeDetail(profileId, hospitalPrivilegeDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return hospitalPrivilegeDetail.HospitalPrivilegeDetailID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateHospitalPrivilegeDetailAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail, DocumentDTO document)
        {
            try
            {
                if (hospitalPrivilegeDetail.AffilicationStartDate != null && hospitalPrivilegeDetail.AffiliationEndDate != null && await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                        p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.HospitalPrivilegeDetailID != hospitalPrivilegeDetail.HospitalPrivilegeDetailID &&
                                                                                                                         h.AffilicationStartDate.Value.Equals(hospitalPrivilegeDetail.AffilicationStartDate.Value) &&
                                                                                                                         h.AffiliationEndDate.Value.Equals(hospitalPrivilegeDetail.AffiliationEndDate.Value))))
                {
                    throw new DuplicateHospitalPrivilegeException(ExceptionMessage.HOSPITAL_PRIVILEGE_INFORMATION_EXISTS_EXCEPTION);
                }

                if (hospitalPrivilegeDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                        p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.HospitalPrivilegeDetailID != hospitalPrivilegeDetail.HospitalPrivilegeDetailID &&
                                                                                                                         h.Preference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllHospitalPrivilegeAsSecondary(profileId);
                }

                string oldFilePath = hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;
                hospitalPrivilegeDetail.HospitalPrevilegeLetterPath = AddUpdateDocumentInformation(DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, oldFilePath, DocumentTitle.HOSPITAL_PRIVILEGE, hospitalPrivilegeDetail.AffiliationEndDate, document, profileId);

                //Update the hospital privilege information
                profileRepository.UpdateHospitalPrivilegeDetail(profileId, hospitalPrivilegeDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, hospitalPrivilegeDetail.HospitalPrevilegeLetterPath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RenewHospitalPrivilegeDetailAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail, DocumentDTO document)
        {
            try
            {
                if (hospitalPrivilegeDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.HospitalPrivilegeDetailID != hospitalPrivilegeDetail.HospitalPrivilegeDetailID && h.Preference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllHospitalPrivilegeAsSecondary(profileId);
                }
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.HOSPITAL_PRIVILEGE, hospitalPrivilegeDetail.HospitalPrevilegeLetterPath, hospitalPrivilegeDetail.AffiliationEndDate); ;

                //Add or update the document if present
                hospitalPrivilegeDetail.HospitalPrevilegeLetterPath = AddUpdateDocumentInformation(DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, hospitalPrivilegeDetail.HospitalPrevilegeLetterPath, DocumentTitle.HOSPITAL_PRIVILEGE, hospitalPrivilegeDetail.AffiliationEndDate, document, profileId);

                //Update the hospital privilege history information
                profileRepository.AddHospitalPrivilegeDetailHistory(profileId, hospitalPrivilegeDetail.HospitalPrivilegeDetailID);

                //Update the hospital privilege information
                profileRepository.UpdateHospitalPrivilegeDetail(profileId, hospitalPrivilegeDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_RENEW_EXCEPTION, ex);
            }
        }

        public async Task RemoveHospitalPrivilegeAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail)
        {
            try
            {
                //Save record into History
                profileRepository.AddHospitalPrivilegeHistory(profileId, hospitalPrivilegeDetail.HospitalPrivilegeDetailID);

                //Update hospital privilege
                profileRepository.RemoveHospitalPrivilege(profileId, hospitalPrivilegeDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Liability

        public async Task<int> AddProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo, DocumentDTO document)
        {
            try
            {
                professionalLiabilityInfo.InsuranceCertificatePath = AddDocument(DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, DocumentTitle.PROFESSIONAL_LIABILITY, professionalLiabilityInfo.ExpirationDate, document, profileId);

                //Add professional liability information
                profileRepository.AddProfessionalLiability(profileId, professionalLiabilityInfo);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return professionalLiabilityInfo.ProfessionalLiabilityInfoID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_LIABILITY_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo, DocumentDTO document)
        {
            try
            {
                string oldFilePath = professionalLiabilityInfo.InsuranceCertificatePath;
                professionalLiabilityInfo.InsuranceCertificatePath = AddUpdateDocumentInformation(DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, oldFilePath, DocumentTitle.PROFESSIONAL_LIABILITY, professionalLiabilityInfo.ExpirationDate, document, profileId);

                //Update the professional liability information
                profileRepository.UpdateProfessionalLiability(profileId, professionalLiabilityInfo);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldFilePath, professionalLiabilityInfo.InsuranceCertificatePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_LIABILITY_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RenewProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo, DocumentDTO document)
        {
            try
            {
                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.PROFESSIONAL_LIABILITY, professionalLiabilityInfo.InsuranceCertificatePath, professionalLiabilityInfo.ExpirationDate); ;

                professionalLiabilityInfo.InsuranceCertificatePath = AddUpdateDocumentInformation(DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, professionalLiabilityInfo.InsuranceCertificatePath, DocumentTitle.PROFESSIONAL_LIABILITY, professionalLiabilityInfo.ExpirationDate, document, profileId);

                //Add the professional liabiliy history information
                profileRepository.AddProfessionalLiabilityHistory(profileId, professionalLiabilityInfo.ProfessionalLiabilityInfoID);

                //Update the professional liability information
                profileRepository.UpdateProfessionalLiability(profileId, professionalLiabilityInfo);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_LIABILITY_RENEW_EXCEPTION, ex);
            }
        }

        public async Task RemoveProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo)
        {
            try
            {
                //Add to history
                profileRepository.AddProfessionalLiabilityHistoryOnRemoval(profileId, professionalLiabilityInfo.ProfessionalLiabilityInfoID);

                //Update professional reference information
                profileRepository.RemoveProfessionalLiability(profileId, professionalLiabilityInfo);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_REFRENCE_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Affilaition

        public async Task<int> AddProfessionalAffiliationAsync(int profileId, ProfessionalAffiliationInfo professionalAffiliation)
        {
            try
            {
                //Add professional affiliation information
                profileRepository.AddProfessionalAffiliation(profileId, professionalAffiliation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return professionalAffiliation.ProfessionalAffiliationInfoID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_AFFILIATION_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateProfessionalAffiliationAsync(int profileId, ProfessionalAffiliationInfo professionalAffiliation)
        {
            try
            {
                //Update professional affiliation information
                profileRepository.UpdateProfessionalAffiliation(profileId, professionalAffiliation);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_AFFILIATION_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveProfessionalAffiliationAsync(int profileId, ProfessionalAffiliationInfo professionalAffiliation)
        {
            try
            {
                //Save record into History
                profileRepository.AddProfessionalAffiliationHistory(profileId, professionalAffiliation.ProfessionalAffiliationInfoID);

                //Update professional affiliation information
                profileRepository.RemoveProfessionalAffiliation(profileId, professionalAffiliation);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_AFFILIATION_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Contract Information

        public async Task<int> AddContractInformationAsync(int profileId, Entities.MasterProfile.Contract.ContractInfo contractInfo, DocumentDTO document)
        {
            try
            {

                contractInfo.ContractFilePath = AddDocument(DocumentRootPath.CONTRACT_DOCUMENT_PATH, DocumentTitle.CONTRACT, contractInfo.ExpiryDate, document, profileId);
                profileRepository.AddContractInformation(profileId, contractInfo);
                await profileRepository.SaveAsync();

                return contractInfo.ContractInfoID;
            }

            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CONTRACT_INFORMATION_ADD_EXCEPTION, ex);
            }


        }

        public async Task UpdateContractInformationAsync(int profileId, Entities.MasterProfile.Contract.ContractInfo contractInfo, DocumentDTO document)
        {
            try
            {
                string oldFilePath = contractInfo.ContractFilePath;
                contractInfo.ContractFilePath = AddUpdateDocumentInformation(DocumentRootPath.CONTRACT_DOCUMENT_PATH, oldFilePath, DocumentTitle.CONTRACT, contractInfo.ExpiryDate, document, profileId);
                profileRepository.UpdateContractInformation(profileId, contractInfo);
                var result = await profileRepository.SaveAsync();

                RemoveDocument(oldFilePath, contractInfo.ContractFilePath);

            }

            catch (ApplicationException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CONTRACT_INFORMATION_UPDATE_EXCEPTION, ex);
            }

        }

        public async Task UpdateContractGroupInformationAsync(int profileId, int contractInfoId, Entities.MasterProfile.Contract.ContractGroupInfo contractGroupInfo)
        {
            try
            {

                profileRepository.UpdateGroupInformation(profileId, contractInfoId, contractGroupInfo);
                await profileRepository.SaveAsync();

            }

            catch (ApplicationException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task AddContractGroupInformationAsync(int profileId, int contractInfoId, Entities.MasterProfile.Contract.ContractGroupInfo contractGroupInfo)
        {
            try
            {

                var contractInfo = profileRepository.Find(profileId).ContractInfoes.FirstOrDefault(x => x.ContractInfoID == contractInfoId);
                var practicingGroups = contractInfo.ContractGroupInfoes.Where(g => !g.Status.Equals(StatusType.Inactive.ToString()));


                if (practicingGroups.Any(x => x.PracticingGroupId == contractGroupInfo.PracticingGroupId))
                {
                    throw new DuplicateGroupNameException(ExceptionMessage.DUPLICATE_GROUP_INFORMATION_EXCEPTION);
                }

                else if (contractInfo.ContractStatus.Equals("Inactive"))
                {
                    throw new InactiveContractExeption(ExceptionMessage.INACTIVE_CONTRACT_INFORMATION_EXCEPTION);
                }

                else
                {
                    profileRepository.AddGroupInformation(profileId, contractInfoId, contractGroupInfo);

                    await profileRepository.SaveAsync();
                }

            }
            catch (ApplicationException)
            {
                throw;
            }

            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_ADD_EXCEPTION, ex);
            }

        }

        public async Task RemoveContractGroupInformationAsync(int profileId, int contractInfoId, ContractGroupInfo contractGroupInfo)
        {
            try
            {
                //Save record into History
                profileRepository.AddContractGroupInformationHistory(profileId, contractInfoId, contractGroupInfo.ContractGroupInfoId);

                //Remove contract group info
                profileRepository.RemoveContractGroupInformation(profileId, contractInfoId, contractGroupInfo);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Reference

        public async Task<int> AddProfessionalReferenceAsync(int profileId, Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo professionalReference)
        {
            try
            {
                //Add professional reference information
                profileRepository.AddProfessionalReference(profileId, professionalReference);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return professionalReference.ProfessionalReferenceInfoID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_REFERENCE_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateProfessionalReferenceAsync(int profileId, Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo professionalReference)
        {
            try
            {
                //Update professional reference information
                profileRepository.UpdateProfessionalReference(profileId, professionalReference);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_REFERENCE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task SetProfessionalReferenceStatusAsync(int profileId, int professionalReferenceId, StatusType status)
        {
            try
            {
                if (status == StatusType.Inactive &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && (p.ProfessionalReferenceInfos.Count(s => s.Status == StatusType.Active.ToString()) > BusinessRule.PROFESSIONAL_REFERENCE_COUNT)))
                {
                    throw new ProfessionaReferenceCountException(ExceptionMessage.PROFESSIONAL_REFRENCE_COUNT_EXCEPTION);
                }

                //Update professional reference information
                profileRepository.SetProfessionalReferenceStatus(profileId, professionalReferenceId, status);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_REFERENCE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveProfessionalReferenceAsync(int profileId, ProfessionalReferenceInfo professionalReference)
        {
            try
            {
                //Save record into History
                profileRepository.AddProfessionalReferenceHistory(profileId, professionalReference.ProfessionalReferenceInfoID);

                //Update professional reference information
                profileRepository.RemoveProfessionalReference(profileId, professionalReference);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_REFRENCE_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Work History

        public async Task<int> AddProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperience professionalWorkExperience, DocumentDTO document)
        {
            try
            {
                if (professionalWorkExperience.StartDate != null && professionalWorkExperience.EndDate != null && await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.ProfessionalWorkExperiences.Any(w => w.EmployerName.Equals(professionalWorkExperience.EmployerName) &&
                                                           w.StartDate.Value.Equals(professionalWorkExperience.StartDate.Value) &&
                                                           w.EndDate.Value.Equals(professionalWorkExperience.EndDate.Value))))
                {
                    throw new ProfessionalWorkExperienceExistException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION);
                }

                professionalWorkExperience.WorkExperienceDocPath = AddDocument(DocumentRootPath.PROFESSIONAL_WORK_EXPERIENCE_PATH, DocumentTitle.PROFESSIONAL_WORK_EXPERIENCE, null, document, profileId);

                //Add professsional work experience information
                profileRepository.AddProfessionalWorkExperience(profileId, professionalWorkExperience);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return professionalWorkExperience.ProfessionalWorkExperienceID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperience professionalWorkExperience, DocumentDTO document)
        {
            try
            {
                if (professionalWorkExperience.StartDate == null && professionalWorkExperience.EndDate == null && await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.ProfessionalWorkExperiences.Any(w => w.ProfessionalWorkExperienceID != professionalWorkExperience.ProfessionalWorkExperienceID &&
                                                           w.EmployerName.Equals(professionalWorkExperience.EmployerName) &&
                                                           w.StartDate.Value.Equals(professionalWorkExperience.StartDate.Value) &&
                                                           w.EndDate.Value.Equals(professionalWorkExperience.EndDate.Value))))
                {
                    throw new ProfessionalWorkExperienceExistException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION);
                }

                string oldPath = professionalWorkExperience.WorkExperienceDocPath;
                professionalWorkExperience.WorkExperienceDocPath = AddUpdateDocument(DocumentRootPath.EDUCATION_CERTIFICATE_PATH, oldPath, DocumentTitle.EDUCATION_CERTIFICATE, null, document, profileId);

                //Update the professional work experience information
                profileRepository.UpdateProfessionalWorkExperience(profileId, professionalWorkExperience);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldPath, professionalWorkExperience.WorkExperienceDocPath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperience professionalWorkExperience)
        {
            try
            {
                //Save record into History
                profileRepository.AddProfessionalWorkExperienceHistory(profileId, professionalWorkExperience.ProfessionalWorkExperienceID);

                //Update professional Work Experience information
                profileRepository.RemoveProfessionalWorkExperience(profileId, professionalWorkExperience);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_REMOVE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformation militaryServiceInformation)
        {
            try
            {
                //Add military service information
                profileRepository.AddMilitaryServiceInformation(profileId, militaryServiceInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return militaryServiceInformation.MilitaryServiceInformationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformation militaryServiceInformation)
        {
            try
            {
                //Add military service information
                profileRepository.UpdateMilitaryServiceInformation(profileId, militaryServiceInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformation militaryServiceInformation)
        {
            try
            {
                //Save record into History
                profileRepository.AddMilitaryServiceInformationHistory(profileId, militaryServiceInformation.MilitaryServiceInformationID);

                //Update professional Work Experience information
                profileRepository.RemoveMilitaryServiceInformation(profileId, militaryServiceInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_REMOVE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddPublicHealthServiceAsync(int profileId, PublicHealthService publicHealthService)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.PublicHealthServices.Any(w => w.StartDate.Value.Equals(publicHealthService.StartDate.Value) &&
                                                    w.EndDate.Value.Equals(publicHealthService.EndDate.Value) &&
                                                    !w.Status.Equals(StatusType.Inactive.ToString()))))
                {
                    throw new PublicHealthServiceExistException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_EXISTS_EXCEPTION);
                }

                //Add public heath service information
                profileRepository.AddPublicHealthService(profileId, publicHealthService);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return publicHealthService.PublicHealthServiceID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdatePublicHealthServiceAsync(int profileId, PublicHealthService publicHealthService)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.PublicHealthServices.Any(w => w.PublicHealthServiceID != publicHealthService.PublicHealthServiceID &&
                                                    w.StartDate.Value.Equals(publicHealthService.StartDate.Value) &&
                                                    w.EndDate.Value.Equals(publicHealthService.EndDate.Value) &&
                                                    !w.Status.Equals(StatusType.Inactive.ToString()))))
                {
                    throw new PublicHealthServiceExistException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_EXISTS_EXCEPTION);
                }

                //Add public heath service information
                profileRepository.UpdatePublicHealthService(profileId, publicHealthService);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemovePublicHealthServiceAsync(int profileId, PublicHealthService publicHealthService)
        {
            try
            {
                //Save record into History
                profileRepository.AddPublicHealthServiceHistory(profileId, publicHealthService.PublicHealthServiceID);

                //Update professional Work Experience information
                profileRepository.RemovePublicHealthService(profileId, publicHealthService);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_REMOVE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddWorkGapAsync(int profileId, WorkGap workGap)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.ProfessionalWorkExperiences.Any(w => (workGap.StartDate >= w.StartDate && workGap.StartDate <= w.EndDate) &&
                                                            (workGap.EndDate >= w.StartDate && workGap.EndDate <= w.EndDate))))
                {
                    throw new ProfessionalWorkExperienceExistException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION);
                }

                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.WorkGaps.Any(w => (workGap.StartDate >= w.StartDate && workGap.StartDate <= w.EndDate) &&
                                                            (workGap.EndDate >= w.StartDate && workGap.EndDate <= w.EndDate) &&
                                                             !w.Status.Equals(StatusType.Inactive.ToString()))))
                {
                    throw new WorkGapExistException(ExceptionMessage.WORK_GAP_EXISTS_EXCEPTION);
                }

                //Add work gap information
                profileRepository.AddWorkGap(profileId, workGap);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return workGap.WorkGapID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.WORK_GAP_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateWorkGapAsync(int profileId, WorkGap workGap)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.ProfessionalWorkExperiences.Any(w => (workGap.StartDate >= w.StartDate && workGap.StartDate <= w.EndDate) &&
                                                            (workGap.EndDate >= w.StartDate && workGap.EndDate <= w.EndDate))))
                {
                    throw new ProfessionalWorkExperienceExistException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION);
                }

                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.WorkGaps.Any(w => w.WorkGapID != workGap.WorkGapID &&
                                        (workGap.StartDate >= w.StartDate && workGap.StartDate <= w.EndDate) &&
                                        (workGap.EndDate >= w.StartDate && workGap.EndDate <= w.EndDate) &&
                                        !w.Status.Equals(StatusType.Inactive.ToString()))))
                {
                    throw new WorkGapExistException(ExceptionMessage.WORK_GAP_EXISTS_EXCEPTION);
                }

                //Update work gap information
                profileRepository.UpdateWorkGap(profileId, workGap);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.WORK_GAP_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveWorkGapAsync(int profileId, WorkGap workGap)
        {
            try
            {
                //Save record into History
                profileRepository.AddWorkGapHistory(profileId, workGap.WorkGapID);

                //Update professional affiliation information
                profileRepository.RemoveWorkGap(profileId, workGap);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.WORK_GAP_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Private Methods

        //private string AddDocumentInPath(string docRootPath, DocumentDTO document, string oldFilePath, string docTitle)
        //{
        //    string docPath = oldFilePath;

        //    //Save and add document
        //    if (document != null)
        //    {
        //        docPath = documentManager.SaveDocument(document, docTitle);
        //    }

        //    return docPath;
        //}

        private ProfileDocument CreateProfileDocumentObject(string title, string docPath, DateTime? expiryDate)
        {
            return new ProfileDocument()
            {
                DocPath = docPath,
                Title = title,
                ExpiryDate = expiryDate
            };
        }

        //private string AddUpdateDocument(int profileId, string docRootPath, DocumentDTO document, string oldFilePath, ProfileDocument profileDocument)
        //{
        //    string newDocPath = oldFilePath;

        //    //Save and update Document
        //    if (document != null)
        //    {
        //        //Save the document in the path
        //        profileDocument.DocPath = newDocPath = documentManager.SaveDocument(document, docRootPath);

        //        if (!String.IsNullOrEmpty(oldFilePath))
        //            //Update the profile document information in repository
        //            profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);
        //        else
        //            //Add the profile document information in repository
        //            profileRepository.AddDocument(profileId, profileDocument);
        //    }

        //    return newDocPath;
        //}

        //private string AddUpdateDocumentInformation(int profileId, string docRootPath, DocumentDTO document, string oldFilePath, ProfileDocument profileDocument)
        //{
        //    string newDocPath = oldFilePath;

        //    //Save and update Document
        //    if (document != null)
        //    {
        //        //Save the document in the path
        //        profileDocument.DocPath = newDocPath = documentManager.SaveDocument(document, docRootPath);

        //        //Add or update other legal name document
        //        if (!String.IsNullOrEmpty(oldFilePath))
        //            //Update the profile document information in repository
        //            profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);
        //        else
        //            //Add the profile document information in repository
        //            profileRepository.AddDocument(profileId, profileDocument);
        //    }
        //    else if (!String.IsNullOrEmpty(oldFilePath))
        //        //Update other information if document is present such as expiry date
        //        profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);

        //    return newDocPath;
        //}

        //private string AddDocument(int profileId, string docRootPath, DocumentDTO document, ProfileDocument profileDocument, string oldFilePath)
        //{
        //    string newDocPath = oldFilePath;

        //    //Save and add document
        //    if (document != null)
        //    {
        //        //Save the document in the path
        //        newDocPath = profileDocument.DocPath = documentManager.SaveDocument(document, docRootPath);

        //        //Add other legal name document in database
        //        profileRepository.AddDocument(profileId, profileDocument);
        //    }

        //    return newDocPath;
        //}




        private string AddDocument(string docRootPath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            //Create a profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, null, expiryDate);

            //Assign the Doc root path
            document.DocRootPath = docRootPath;

            //Add the document if uploaded
            return profileDocumentManager.AddDocument(profileId, document, profileDocument);
        }

        private string AddUpdateDocument(string docRootPath, string oldFilePath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            document.DocRootPath = docRootPath;
            document.OldFilePath = oldFilePath;

            //Create the profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, oldFilePath, expiryDate);
            return profileDocumentManager.AddUpdateDocument(profileId, document, profileDocument);
        }

        private string AddUpdateDocumentInformation(string docRootPath, string oldFilePath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            document.DocRootPath = docRootPath;
            document.OldFilePath = oldFilePath;

            //Create the profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, oldFilePath, expiryDate);
            return profileDocumentManager.AddUpdateDocumentInformation(profileId, document, profileDocument);
        }

        private void RemoveDocument(string oldFilePath, string newFilePath)
        {
            //Delete the previous file
            if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(newFilePath))
                documentManager.DeleteFile(oldFilePath);
        }

        #endregion

        #region Education History

        public async Task<int> AddEducationDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.EducationDetail educationDetail, DocumentDTO graduateDocument)
        {
            try
            {
                educationDetail.CertificatePath = AddDocument(DocumentRootPath.EDUCATION_CERTIFICATE_PATH, DocumentTitle.EDUCATION_CERTIFICATE, null, graduateDocument, profileId);

                //Add education detail information
                profileRepository.AddEducationDetail(profileId, educationDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return educationDetail.EducationDetailID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.EDUCATION_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateEducationDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.EducationDetail educationDetail, DocumentDTO graduateDocument)
        {
            try
            {
                string oldGraduateFilePath = educationDetail.CertificatePath;
                educationDetail.CertificatePath = AddUpdateDocument(DocumentRootPath.EDUCATION_CERTIFICATE_PATH, oldGraduateFilePath, DocumentTitle.EDUCATION_CERTIFICATE, null, graduateDocument, profileId);

                //Update the education detail information
                profileRepository.UpdateEducationDetail(profileId, educationDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldGraduateFilePath, educationDetail.CertificatePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.EDUCATION_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveEducationDetailAsync(int profileId, EducationDetail educationDetail)
        {
            try
            {
                //Save record into History
                profileRepository.AddEducationDetailHistory(profileId, educationDetail.EducationDetailID);

                //Remove Education Detail
                Profile profile = profileRepository.Get(p => p.ProfileID == profileId, "EducationDetails.SchoolInformation").FirstOrDefault();
                var removeEducationDetail = profile.EducationDetails.FirstOrDefault(e => e.EducationDetailID == educationDetail.EducationDetailID);
                removeEducationDetail.StatusType = educationDetail.StatusType;

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.EDUCATION_DETAIL_REMOVE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddTrainingDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.TrainingDetail trainingDetail, IList<DocumentDTO> documents)
        {
            try
            {
                for (int q = 0; q < trainingDetail.ResidencyInternshipDetails.Count; q++)
                {
                    trainingDetail.ResidencyInternshipDetails.ElementAt(q).DocumentPath = AddDocument(DocumentRootPath.RESIDENCY_INTERNSHIP_PATH, DocumentTitle.RESIDENCY_INTERNSHIP_CERTIFICATE, null, documents[q], profileId);
                }

                //Add training detail
                profileRepository.AddTrainingDetail(profileId, trainingDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return trainingDetail.TrainingDetailID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.TRAINING_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateTrainingDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.TrainingDetail trainingDetail)
        {
            try
            {
                //Update training detail
                profileRepository.UpdateTrainingDetail(profileId, trainingDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.TRAINING_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddResidencyInternshipDetailAsync(int profileId, int trainingId, Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail residencyInternshipDetail, DocumentDTO document)
        {
            try
            {
                if (residencyInternshipDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                          p.TrainingDetails.Any(t => t.ResidencyInternshipDetails.Any(r => r.Preference.Equals(PreferenceType.Primary.ToString())))))
                {
                    profileRepository.SetAllResidencyInternshipAsSecondary(profileId, trainingId);
                }

                residencyInternshipDetail.DocumentPath = AddDocument(DocumentRootPath.RESIDENCY_INTERNSHIP_PATH, DocumentTitle.RESIDENCY_INTERNSHIP_CERTIFICATE, null, document, profileId);

                //Add residency internship detail
                profileRepository.AddResidencyInternshipDetail(profileId, trainingId, residencyInternshipDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return residencyInternshipDetail.ResidencyInternshipDetailID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.RESIDENCY_INTERNSHIP_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateResidencyInternshipDetailAsync(int profileId, int trainingId, Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail residencyInternshipDetail, DocumentDTO document)
        {
            try
            {
                if (residencyInternshipDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                          p.TrainingDetails.Any(t => t.ResidencyInternshipDetails.Any(r => r.ResidencyInternshipDetailID != residencyInternshipDetail.ResidencyInternshipDetailID &&
                                                                                                                           r.Preference.Equals(PreferenceType.Primary.ToString())))))
                {
                    profileRepository.SetAllResidencyInternshipAsSecondary(profileId, trainingId);
                }

                string oldPath = residencyInternshipDetail.DocumentPath;
                residencyInternshipDetail.DocumentPath = AddUpdateDocument(DocumentRootPath.RESIDENCY_INTERNSHIP_PATH, oldPath, DocumentTitle.RESIDENCY_INTERNSHIP_CERTIFICATE, null, document, profileId);

                //Update residencty internship detail
                profileRepository.UpdateResidencyInternshipDetail(profileId, trainingId, residencyInternshipDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldPath, residencyInternshipDetail.DocumentPath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.RESIDENCY_INTERNSHIP_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddCMECertificationAsync(int profileId, Entities.MasterProfile.EducationHistory.CMECertification cmeCertification, DocumentDTO document)
        {
            try
            {
                cmeCertification.CertificatePath = AddDocument(DocumentRootPath.CME_CERTIFICATION_PATH, DocumentTitle.CME_CERTIFICATION, null, document, profileId);

                //Add CME Certification information
                profileRepository.AddCMECertification(profileId, cmeCertification);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return cmeCertification.CMECertificationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CME_CERTIFICATION_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateCMECertificationAsync(int profileId, Entities.MasterProfile.EducationHistory.CMECertification cmeCertification, DocumentDTO document)
        {
            try
            {
                string oldPath = cmeCertification.CertificatePath;
                cmeCertification.CertificatePath = AddUpdateDocument(DocumentRootPath.CME_CERTIFICATION_PATH, oldPath, DocumentTitle.CME_CERTIFICATION, null, document, profileId);

                //Update the CME Certification information
                profileRepository.UpdateCMECertification(profileId, cmeCertification);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldPath, cmeCertification.CertificatePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CME_CERTIFICATION_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveCertificationDetailAsync(int profileId, CMECertification cmeCertification)
        {
            try
            {
                //Save record into History
                profileRepository.AddCertificationDetailHistory(profileId, cmeCertification.CMECertificationID);

                //Remove CME Certification
                profileRepository.RemoveCertificationDetail(profileId, cmeCertification);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.CME_CERTIFICATION_REMOVE_EXCEPTION, ex);
            }
        }

        public async Task UpdateECFMGDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.ECFMGDetail ecfmgDetail, DocumentDTO document)
        {
            try
            {
                string oldPath = ecfmgDetail.ECFMGCertPath;
                ecfmgDetail.ECFMGCertPath = AddUpdateDocument(DocumentRootPath.ECFMG_PATH, oldPath, DocumentTitle.ECFMG_CERTIFICATE, null, document, profileId);

                //Update the ECFMG Certification information
                profileRepository.UpdateECFMGDetail(profileId, ecfmgDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldPath, ecfmgDetail.ECFMGCertPath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.ECFMG_CERTIFICATION_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddProgramDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.ProgramDetail programDetail, DocumentDTO document)
        {
            try
            {
                if (programDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                          p.ProgramDetails.Any(r => r.Preference.Equals(PreferenceType.Primary.ToString()))))
                {
                    profileRepository.SetAllProgramAsSecondary(profileId, programDetail.ProgramDetailID);
                }

                programDetail.DocumentPath = AddDocument(DocumentRootPath.PROGRAM_PATH, DocumentTitle.PROGRAM_CERTIFICATE, null, document, profileId);

                //Add residency internship detail
                profileRepository.AddProgramDetail(profileId, programDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return programDetail.ProgramDetailID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROGRAM_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateProgramDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.ProgramDetail programDetail, DocumentDTO document)
        {
            try
            {
                if (programDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                                                          p.ProgramDetails.Any(r => r.ProgramDetailID != programDetail.ProgramDetailID &&
                                                                                                                           r.Preference.Equals(PreferenceType.Primary.ToString()))))
                {
                    profileRepository.SetAllProgramAsSecondary(profileId, programDetail.ProgramDetailID);
                }

                string oldPath = programDetail.DocumentPath;
                programDetail.DocumentPath = AddUpdateDocument(DocumentRootPath.PROGRAM_PATH, oldPath, DocumentTitle.PROGRAM_CERTIFICATE, null, document, profileId);

                //Update residencty internship detail
                profileRepository.UpdateProgramDetail(profileId, programDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                RemoveDocument(oldPath, programDetail.DocumentPath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROGRAM_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveProgramDetailAsync(int profileId, ProgramDetail programDetail)
        {
            try
            {
                //Save record into History
                profileRepository.AddProgramDetailHistory(profileId, programDetail.ProgramDetailID);

                //Remove Program Detail
                profileRepository.RemoveProgramDetail(profileId, programDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROGRAM_DETAIL_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Document Repository

        public async Task<int> AddOtherDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument otherDocument, DocumentDTO document)
        {
            try
            {
                otherDocument.DocumentPath = AddDocument(DocumentRootPath.OTHER_DOCUMENT_PATH, DocumentTitle.OTHER_DOCUMENT, null, document, profileId);

                //Add other legal name information
                profileRepository.AddOtherDocument(profileId, otherDocument);

                //save the information in the repository
                await profileRepository.SaveAsync();

                return otherDocument.OtherDocumentID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_DOCUMENT_CREATE_EXCEPTION, ex);
            }
        }

        public async Task UpdateOtherDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument otherDocument, DocumentDTO document)
        {
            try
            {
                string oldFilePath = otherDocument.DocumentPath;

                otherDocument.DocumentPath = AddUpdateDocument(DocumentRootPath.OTHER_DOCUMENT_PATH, otherDocument.DocumentPath, DocumentTitle.OTHER_DOCUMENT, null, document, profileId);

                //Update the other legal name information
                profileRepository.UpdateOtherDocument(profileId, otherDocument);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(otherDocument.DocumentPath))
                    documentManager.DeleteFile(oldFilePath);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_DOCUMENT_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task RemoveOtherDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument otherDocument)
        {

            try
            {
                //Add to history
                profileRepository.AddOtherDocumentHistory(profileId, otherDocument.OtherDocumentID);

                //Remove Other Legal Name
                profileRepository.RemoveOtherDocument(profileId, otherDocument);

                //save the information in the repository
                await profileRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.OTHER_DOUCUMENT_REMOVE_EXCEPTION, ex);
            }

        }

        public int GetCredentialingUserId(string UserAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == UserAuthId);

            return user.CDUserID;
        }

        #endregion

        public async Task SaveProfileTemp(Profile profile)
        {
            try
            {
                profileRepository.Create(profile);
                await profileRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Disclosure Questions and Answers

        public async Task<int> AddEditDisclosureQuestionAnswersAsync(int profileId, AHC.CD.Entities.MasterProfile.DisclosureQuestions.ProfileDisclosure disclosureQuestionAnswers)
        {
            try
            {
                //Add Update the Disclosure Questions and Answer information
                foreach (var questionAnswers in disclosureQuestionAnswers.ProfileDisclosureQuestionAnswers)
                {
                    if (Convert.ToInt32(questionAnswers.AnswerYesNoOption) == (int)YesNoOption.NO)
                        questionAnswers.Reason = null;
                }
                profileRepository.AddEditDisclosureQuestionAnswers(profileId, disclosureQuestionAnswers);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                return result;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.DISCLOSURE_QUESTIONS_CREATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Get Profiles as Section Wise

        #region Demographics

        public async Task<object> GetDemographicsProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Personal Detail
                    "PersonalDetail.ProviderLevel",
                    "PersonalDetail.ProviderTitles.ProviderType",
                    "OtherLegalNames",
                    "HomeAddresses",
                    "ContactDetail",
                    "PersonalIdentification",
                    "BirthInformation",
                    "VisaDetail",
                    "LanguageInfo"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    PersonalDetail = profile.PersonalDetail,
                    OtherLegalNames = profile.OtherLegalNames.Where(o => (o.Status != StatusType.Inactive.ToString())),
                    HomeAddresses = profile.HomeAddresses.Where(h => (h.Status != StatusType.Inactive.ToString())),
                    ContactDetail = profile.ContactDetail,
                    PersonalIdentification = profile.PersonalIdentification,
                    BirthInformation = profile.BirthInformation,
                    VisaDetail = profile.VisaDetail,
                    LanguageInfo = profile.LanguageInfo,
                    ProfilePhotoPath = profile.ProfilePhotoPath
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_DEMOGRAPHICS_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Identification And Licenses

        public async Task<object> GetIdentificationAndLicensesProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //State License
                    "StateLicenses.ProviderType",
                    "StateLicenses.StateLicenseStatus"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    StateLicenses = profile.StateLicenses.Where(s => (s.Status != StatusType.Inactive.ToString())),
                    FederalDEAInformations = profile.FederalDEAInformations.Where(f => (f.Status != StatusType.Inactive.ToString())),
                    MedicareInformations = profile.MedicareInformations.Where(m => (m.Status != StatusType.Inactive.ToString())),
                    MedicaidInformations = profile.MedicaidInformations.Where(m => (m.Status != StatusType.Inactive.ToString())),
                    CDSCInformations = profile.CDSCInformations.Where(c => (c.Status != StatusType.Inactive.ToString())),
                    OtherIdentificationNumber = profile.OtherIdentificationNumber,
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_IDENTIFICATION_AND_LICENSES_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Specialty/Board

        public async Task<object> GetBoardSpecialtiesProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Specialty
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    SpecialtyDetails = profile.SpecialtyDetails.Where(s => (s.Status != StatusType.Inactive.ToString())),
                    PracticeInterest = profile.PracticeInterest,
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_SPECIALTY_BOARD_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Hospital Privileges

        public async Task<object> GetHospitalPrivilegesProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {                    
                    //hospital Privilege
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.Hospital", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactInfo", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.HospitalContactPerson", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.AdmittingPrivilege", 
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails.StaffCategory"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    HospitalPrivilegeInformation = profile.HospitalPrivilegeInformation,
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_HOSPITAL_PRIVILEGE_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Liability

        public async Task<object> GetProfessionalLiabilitiesProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Professional Liability
                    "ProfessionalLiabilityInfoes.InsuranceCarrier",
                    "ProfessionalLiabilityInfoes.InsuranceCarrierAddress"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    ProfessionalLiabilityInfoes = profile.ProfessionalLiabilityInfoes.Where(l => (l.Status != StatusType.Inactive.ToString())),
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_PROFESSIONAL_LIABILITY_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Reference

        public async Task<object> GetProfessionalReferencesProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Professional Reference
                    "ProfessionalReferenceInfos.ProviderType", 
                    "ProfessionalReferenceInfos.Specialty"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    ProfessionalReferenceInfos = profile.ProfessionalReferenceInfos.Where(r => (r.Status != StatusType.Inactive.ToString())),
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_PROFESSIONAL_REFERENCE_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Education History

        public async Task<object> GetEducationHistoriesProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Resindency Internship
                    "TrainingDetails.ResidencyInternshipDetails.Specialty",
                    "ProgramDetails.Specialty"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    EducationDetails = profile.EducationDetails.Where(e => (e.Status != StatusType.Inactive.ToString())),
                    TrainingDetails = profile.TrainingDetails,
                    ProgramDetails = profile.ProgramDetails.Where(p => (p.Status != StatusType.Inactive.ToString())),
                    CMECertifications = profile.CMECertifications.Where(c => (c.Status != StatusType.Inactive.ToString())),
                    ECFMGDetail = profile.ECFMGDetail,
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_EDUCATION_HISTORY_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Work History

        public async Task<object> GetWorkHistoriesProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Work History
                    "ProfessionalWorkExperiences.ProviderType"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    ProfessionalWorkExperiences = profile.ProfessionalWorkExperiences.Where(w => (w.Status != StatusType.Inactive.ToString())),
                    MilitaryServiceInformations = profile.MilitaryServiceInformations.Where(m => (m.Status != StatusType.Inactive.ToString())),
                    PublicHealthServices = profile.PublicHealthServices.Where(p => (p.Status != StatusType.Inactive.ToString())),
                    WorkGaps = profile.WorkGaps.Where(w => (w.Status != StatusType.Inactive.ToString())),
                    CVInformation = profile.CVInformation
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_WORK_HISTORY_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Affiliation

        public async Task<object> GetProfessionalAffiliationsProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    ProfessionalAffiliationInfos = profile.ProfessionalAffiliationInfos.Where(a => (a.Status != StatusType.Inactive.ToString())),
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_PROFESSIONAL_AFFILIATION_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Practice Locations

        public async Task<object> GetPracticeLocationsProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
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
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    PracticeLocationDetails = profile.PracticeLocationDetails.Where(p => (p.Status != StatusType.Inactive.ToString())),
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_PRACTICE_LOCATION_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Disclosure Question

        public async Task<object> GetDisclosureQuestionsProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    ProfileDisclosure = profile.ProfileDisclosure,
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_DISCLOSURE_QUESTIONS_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Contract Info

        public async Task<object> GetContractInfoProfileDataAsync(int profileId)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Contract Information
                    "ContractInfoes.ContractGroupInfoes",
                    "ContractInfoes.ContractGroupInfoes.PracticingGroup",
                    "ContractInfoes.ContractGroupInfoes.PracticingGroup.Group",
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                return new
                {
                    ContractInfoes = profile.ContractInfoes.Where(c => !c.ContractStatus.Equals(ContractStatus.Inactive.ToString())),
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_CONTRACT_INFORAMTION_BY_ID_GET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Document Repository

        public async Task<object> GetDocumentRepositoryDataAsync(int profileId, int CDUserId, bool isCCO)
        {
            try
            {
                var includeProperties = new string[]
                {
                    //Personal Detail
                    "PersonalIdentification",

                    "BirthInformation",

                    "OtherLegalNames",

                    "CVInformation",
                    
                    "VisaDetail",

                    "StateLicenses",
                    
                    "FederalDEAInformations",
                     
                    "MedicareInformations",
                    
                    "MedicaidInformations",
                    
                    "CDSCInformations",
                    
                    "EducationDetails",
                    
                    "ECFMGDetail",
                    
                    "ProgramDetails",
                    
                    "CMECertifications",
                    
                    "SpecialtyDetails",
                    
                    "HospitalPrivilegeInformation.HospitalPrivilegeDetails",

                    "ProfessionalLiabilityInfoes",

                    "ProfessionalWorkExperiences",
                    
                    "ContractInfoes",

                    "OtherDocuments"
                   
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                if (profile == null)
                    throw new Exception("Invalid Profile");

                IEnumerable<ProfileVerificationInfo> profileVerificationInfo = new List<ProfileVerificationInfo>();
                if (isCCO)
                {

                    profileVerificationInfo = await uow.GetGenericRepository<ProfileVerificationInfo>().GetAsync(p => p.ProfileID == profileId, "ProfileVerificationDetails.VerificationResult, ProfileVerificationDetails.ProfileVerificationParameter");

                }

                //includeProperties = new string[]{

                //    "ProfileVerificationDetails.VerificationResult"

                //};

                return new
                {
                    PersonalIdentification = profile.PersonalIdentification,
                    BirthInformation = profile.BirthInformation,
                    OtherLegalNames = profile.OtherLegalNames.Where(o => (o.Status != StatusType.Inactive.ToString())).ToList(),
                    CVInformation = profile.CVInformation,
                    VisaDetail = profile.VisaDetail,
                    StateLicenses = profile.StateLicenses.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList(),
                    FederalDEAInformations = profile.FederalDEAInformations.Where(f => (f.Status != StatusType.Inactive.ToString())).ToList(),
                    MedicareInformations = profile.MedicareInformations.Where(m => (m.Status != StatusType.Inactive.ToString())).ToList(),
                    MedicaidInformations = profile.MedicaidInformations.Where(m => (m.Status != StatusType.Inactive.ToString())).ToList(),
                    CDSCInformations = profile.CDSCInformations.Where(c => (c.Status != StatusType.Inactive.ToString())).ToList(),
                    EducationDetails = profile.EducationDetails.Where(e => (e.Status != StatusType.Inactive.ToString())).ToList(),
                    ECFMGDetail = profile.ECFMGDetail,
                    ProgramDetails = profile.ProgramDetails.Where(p => (p.Status != StatusType.Inactive.ToString())).ToList(),
                    CMECertifications = profile.CMECertifications.Where(c => (c.Status != StatusType.Inactive.ToString())).ToList(),
                    SpecialtyDetails = profile.SpecialtyDetails.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList(),
                    HospitalPrivilegeInformation = profile.HospitalPrivilegeInformation,
                    ProfessionalLiabilityInfoes = profile.ProfessionalLiabilityInfoes.Where(l => (l.Status != StatusType.Inactive.ToString())).ToList(),
                    ProfessionalWorkExperiences = profile.ProfessionalWorkExperiences.Where(w => (w.Status != StatusType.Inactive.ToString())).ToList(),
                    ContractInfoes = profile.ContractInfoes.Where(c => !c.ContractStatus.Equals(ContractStatus.Inactive.ToString())).ToList(),
                    OtherDocuments = profile.OtherDocuments.Where(p => (p.Status != StatusType.Inactive.ToString())).Where(p => (p.IsPrivate == true && p.ModifiedBy == CDUserId.ToString()) || (p.IsPrivate == false)).ToList(),
                    ProfileVerificationInfo = profileVerificationInfo.ToList(),
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PROFILE_CONTRACT_INFORAMTION_BY_ID_GET_EXCEPTION, ex);
            }

        }

        #endregion

        #endregion

    }
}
