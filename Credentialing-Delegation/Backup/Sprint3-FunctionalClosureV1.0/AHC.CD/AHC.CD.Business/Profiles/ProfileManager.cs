using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Exceptions;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Document;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    internal class ProfileManager : IProfileManager
    {
        
        IProfileRepository profileRepository = null;
        IDocumentsManager documentManager = null;
       
        public ProfileManager(IUnitOfWork uow, IDocumentsManager documentManager)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.documentManager = documentManager;
        }

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
                    //Speciality
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoard",

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
                    "PersonalDetail.ProviderTitles.ProviderType"
                };

                var profile = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);
                //if (profile.PersonalDetail != null && profile.PersonalDetail.ProviderTitles != null)
                //{
                //    profile.PersonalDetail.ProviderTitles = profile.PersonalDetail.ProviderTitles.Where(t => t.Status.Equals(StatusType.Active.ToString())).ToList(); 
                //}

                //if (profile.ContactDetail != null && profile.ContactDetail.PhoneDetails != null)
                //{
                //    profile.ContactDetail.PhoneDetails = profile.ContactDetail.PhoneDetails.Where(t => t.Status.Equals(StatusType.Active.ToString())).ToList(); 
                //}

                //if (profile.ContactDetail != null && profile.ContactDetail.EmailIDs != null)
                //{
                //    profile.ContactDetail.EmailIDs = profile.ContactDetail.EmailIDs.Where(t => t.Status.Equals(StatusType.Active.ToString())).ToList(); 
                //}

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

        #endregion

        #region Demographics

        public async Task<string> UpdateProfileImageAsync(int profileId, DocumentDTO document)
        {
            try
            {
                //Add the profile Image
                var profileImagePath = AddDocumentInPath(DocumentRootPath.PROFILE_IMAGE_PATH, document, null, DocumentRootPath.PROFILE_IMAGE_PATH);

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
                //Create a profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.OTHER_LEGAL_NAME, null, null);
                
                //Add the document if uploaded
                otherLegalName.DocumentPath = AddDocument(profileId, DocumentRootPath.OTHER_LEGAL_NAME_PATH, document, profileDocument, null);

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
                string oldFilePath = null;

                if(document != null)
                {
                    oldFilePath = otherLegalName.DocumentPath;

                    //Create the profile document object
                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.OTHER_LEGAL_NAME, otherLegalName.DocumentPath, null); ;

                    //Add or update the document if present
                    otherLegalName.DocumentPath = AddUpdateDocument(profileId, DocumentRootPath.OTHER_LEGAL_NAME_PATH, document, oldFilePath, profileDocument);
                }

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

        #endregion

        #region Contact Detail

        public async Task UpdateContactDetailAsync(int profileId, Entities.MasterProfile.Demographics.ContactDetail contactDetail)
        {
            try
            {
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
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.PersonalIdentification.SocialSecurityNumber.Equals(personalIdentification.SSN)))
                {
                    throw new DuplicateSSNNumberException(ExceptionMessage.DRIVING_LICENSE_EXIST_EXCEPTION);
                }
                
                
                string oldDLCertificatePath = null;

                if (dlDocument != null)
                {
                    oldDLCertificatePath = personalIdentification.DLCertificatePath;
                    ProfileDocument profileDLDocument = CreateProfileDocumentObject(DocumentTitle.DL, personalIdentification.DLCertificatePath, null);
                    personalIdentification.DLCertificatePath = AddUpdateDocument(profileId, DocumentRootPath.SSN_PATH, dlDocument, oldDLCertificatePath, profileDLDocument);
                }

                string oldSSNCertificatePath = null;

                if (ssnDocument != null)
                {
                    oldSSNCertificatePath = personalIdentification.SSNCertificatePath;

                    //Create the profile document object
                    ProfileDocument profileSSNDocument = CreateProfileDocumentObject(DocumentTitle.SSN, personalIdentification.SSNCertificatePath, null);                

                    //Add or update the document if present
                    personalIdentification.SSNCertificatePath = AddUpdateDocument(profileId, DocumentRootPath.SSN_PATH, ssnDocument, oldSSNCertificatePath, profileSSNDocument);
                }

                //Update the personal identification information
                profileRepository.UpdatePersonalIdentification(profileId, personalIdentification);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldSSNCertificatePath))
                    documentManager.DeleteFile(oldSSNCertificatePath);

                if (!String.IsNullOrEmpty(oldDLCertificatePath))
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
                string oldFilePath = null;

                if (document != null)
                {
                    oldFilePath = birthInformation.BirthCertificatePath;

                    //Create the profile document object
                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.BIRTH_CERTIFICATE, birthInformation.BirthCertificatePath, null); ;

                    //Add or update the document if present
                    birthInformation.BirthCertificatePath = AddUpdateDocument(profileId, DocumentRootPath.BIRTH_CERTIFICATE_PATH, document, oldFilePath, profileDocument);
                }

                //Update the birth certificate information
                profileRepository.UpdateBirthInformation(profileId, birthInformation);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath))
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

                    if (visaDetail.VisaInfo.VisaNumber != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.GreenCardNumberStored.Equals(visaDetail.VisaInfo.GreenCardNumberStored)))
                    {
                        throw new GreenCardNumberExistException(ExceptionMessage.GREEN_CARD_NUMBER_EXISTS_EXCEPTION);
                    }

                    if (visaDetail.VisaInfo.VisaNumber != null &&
                    await profileRepository.AnyAsync(p => p.ProfileID != profileId && p.VisaDetail.VisaInfo.NationalIDNumberNumberStored.Equals(visaDetail.VisaInfo.NationalIDNumberNumberStored)))
                    {
                        throw new NationalIDNumberExistException(ExceptionMessage.NATIONAL_ID_NUMBER_EXISTS_EXCEPTION);
                    }
                    
                    oldVisaCertificatePath = visaDetail.VisaInfo.VisaCertificatePath;
                    oldGreenCardCertificatePath = visaDetail.VisaInfo.GreenCardCertificatePath;
                    oldNationalIDCertificatePath = visaDetail.VisaInfo.NationalIDCertificatePath;

                    //Create the profile document object
                    ProfileDocument profileVisaDocument = CreateProfileDocumentObject(DocumentTitle.VISA, visaDetail.VisaInfo.VisaCertificatePath, visaDetail.VisaInfo.VisaExpirationDate);
                    ProfileDocument profileGreenCardDocument = CreateProfileDocumentObject(DocumentTitle.GREEN_CARD, visaDetail.VisaInfo.GreenCardCertificatePath, null);
                    ProfileDocument profileNationalIDDocument = CreateProfileDocumentObject(DocumentTitle.NATIONAL_IDENTIFICATION, visaDetail.VisaInfo.NationalIDCertificatePath, null);

                    //Add or update the document if present
                    visaDetail.VisaInfo.VisaCertificatePath = AddUpdateDocumentInformation(profileId, DocumentRootPath.VISA_PATH, visaDocument, oldVisaCertificatePath, profileVisaDocument);
                    visaDetail.VisaInfo.GreenCardCertificatePath = AddUpdateDocument(profileId, DocumentRootPath.GREEN_CARD_PATH, greenCarddocument, oldGreenCardCertificatePath, profileGreenCardDocument);
                    visaDetail.VisaInfo.NationalIDCertificatePath = AddUpdateDocument(profileId, DocumentRootPath.NATIONAL_IDENTIFICATION_PATH, nationalIDdocument, oldNationalIDCertificatePath, profileNationalIDDocument);

                    
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
                if(profileRepository.Any(p => p.StateLicenses.Any(s => s.LicenseNumber.Equals(stateLicense.LicenseNumber))))
                {
                    throw new DuplicateStateLicenseNumberException(ExceptionMessage.STATE_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }
                
                //Create a profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.STATE_LICENSE, null, stateLicense.ExpiryDate);
                
                //Add the document if uploaded
                stateLicense.StateLicenseDocumentPath = AddDocument(profileId, DocumentRootPath.STATE_LICENSE_PATH, document, profileDocument, null);

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

                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.STATE_LICENSE, stateLicense.StateLicenseDocumentPath, stateLicense.ExpiryDate); ;

                //Add or update the document if present
                stateLicense.StateLicenseDocumentPath = AddUpdateDocumentInformation(profileId, DocumentRootPath.STATE_LICENSE_PATH, document, oldFilePath, profileDocument);

                //Update the state license information
                profileRepository.UpdateStateLicense(profileId, stateLicense);
                
                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(stateLicense.StateLicenseDocumentPath))
                    documentManager.DeleteFile(oldFilePath);
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
                stateLicense.StateLicenseDocumentPath = AddUpdateDocumentInformation(profileId, DocumentRootPath.STATE_LICENSE_PATH, document, stateLicense.StateLicenseDocumentPath, profileDocument);

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
                
                //Create a profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.DEA, null, federalDEALicense.ExpiryDate);

                //Add the document if uploaded
                federalDEALicense.DEALicenceCertPath = AddDocument(profileId, DocumentRootPath.DEA_PATH, document, profileDocument, null);

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

                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.DEA, federalDEALicense.DEALicenceCertPath, federalDEALicense.ExpiryDate); ;

                //Add or update the document if present
                federalDEALicense.DEALicenceCertPath = AddUpdateDocumentInformation(profileId, DocumentRootPath.DEA_PATH, document, oldFilePath, profileDocument);

                //Update the federal DEA license information
                profileRepository.UpdateFederalDEALicense(profileId, federalDEALicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(federalDEALicense.DEALicenceCertPath))
                    documentManager.DeleteFile(oldFilePath);
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
                federalDEALicense.DEALicenceCertPath = AddUpdateDocumentInformation(profileId, DocumentRootPath.DEA_PATH, document, federalDEALicense.DEALicenceCertPath, profileDocument);

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

        #endregion

        #region CDSC Information

        public async Task<int> AddCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense, DocumentDTO document)
        {
            try
            {
                if (profileRepository.Any(p => p.CDSCInformations.Any(s => s.CertNumber.Equals(cdscLicense.CertNumber))))
                {
                    throw new DuplicateCDSCLicenseNumberException(ExceptionMessage.CDSC_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }
                
                //Create a profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.CDSC, null, cdscLicense.ExpiryDate);

                //Add the document if uploaded
                cdscLicense.CDSCCerificatePath = AddDocument(profileId, DocumentRootPath.CDSC_PATH, document, profileDocument, null);
                
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
                if (profileRepository.Any(p => p.CDSCInformations.Any(s => s.CDSCInformationID != cdscLicense.CDSCInformationID && s.CertNumber.Equals(cdscLicense.CertNumber))))
                {
                    throw new DuplicateCDSCLicenseNumberException(ExceptionMessage.CDSC_LICENSE_NUMBER_EXISTS_EXCEPTION);
                }
                
                var oldFilePath = cdscLicense.CDSCCerificatePath;

                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.CDSC, oldFilePath, cdscLicense.ExpiryDate); ;

                //Add or update the document if present
                cdscLicense.CDSCCerificatePath = AddUpdateDocumentInformation(profileId, DocumentRootPath.CDSC_PATH, document, oldFilePath, profileDocument);

                //Update the CDSC license information
                profileRepository.UpdateCDSCLicense(profileId, cdscLicense);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(cdscLicense.CDSCCerificatePath))
                    documentManager.DeleteFile(oldFilePath);
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
                cdscLicense.CDSCCerificatePath = AddUpdateDocumentInformation(profileId, DocumentRootPath.CDSC_PATH, document, cdscLicense.CDSCCerificatePath, profileDocument);
                
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
                
                //Create a profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.MEDICARE, null, null);

                //Add the document if uploaded
                medicareInformation.CertificatePath = AddDocument(profileId, DocumentRootPath.MEDICARE_PATH, document, profileDocument, null);
                
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

                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.MEDICARE, medicareInformation.CertificatePath, null); ;

                //Add or update the document if present
                medicareInformation.CertificatePath = AddUpdateDocument(profileId, DocumentRootPath.MEDICARE_PATH, document, oldFilePath, profileDocument);
                
                //Update medicare information
                profileRepository.UpdateMedicareInformation(profileId, medicareInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(medicareInformation.CertificatePath))
                    documentManager.DeleteFile(oldFilePath);
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
                
                //Create a profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.MEDICAID, null, null);

                //Add the document if uploaded
                medicaidInformation.CertificatePath = AddDocument(profileId, DocumentRootPath.MEDICAID_PATH, document, profileDocument, null);

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

                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.MEDICAID, medicaidInformation.CertificatePath, null); ;

                //Add or update the document if present
                medicaidInformation.CertificatePath = AddUpdateDocument(profileId, DocumentRootPath.MEDICAID_PATH, document, oldFilePath, profileDocument);

                //Update medicaid information
                profileRepository.UpdateMedicaidInformation(profileId, medicaidInformation);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(medicaidInformation.CertificatePath))
                    documentManager.DeleteFile(oldFilePath);
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
                if(specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                {
                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.SPECIALITY_BOARD, null, specialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate);

                    //Add the document if uploaded
                    specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = AddDocument(profileId, DocumentRootPath.SPECIALITY_BOARD_PATH, document, profileDocument, null);
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

        public async Task UpdateSpecialtyDetailAsync(int profileId, SpecialtyDetail specialtyDetail, DocumentDTO document)
        {
            try
            {
                if (specialtyDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.SpecialtyDetails.Any(h => h.SpecialtyDetailID != specialtyDetail.SpecialtyDetailID && h.SpecialtyPreference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllSpecialityAsSecondary(profileId);
                }

                string oldFilePath = null;

                //Create a profile document object
                if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                {
                    oldFilePath = specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath;
                    
                    //Create the profile document object
                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.SPECIALITY_BOARD, oldFilePath, specialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate); ;

                    //Add or update the document if present
                    specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = AddUpdateDocumentInformation(profileId, DocumentRootPath.SPECIALITY_BOARD_PATH, document, oldFilePath, profileDocument);
                }

                //Update the state license information
                profileRepository.UpdateSpecialtyDetail(profileId, specialtyDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath))
                    documentManager.DeleteFile(oldFilePath);
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

                string oldFilePath = null;

                //Create a profile document object
                if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                {
                    oldFilePath = specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath;

                    //Create the profile document object
                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.SPECIALITY_BOARD, oldFilePath, specialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate); ;

                    //Add or update the document if present
                    specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = AddUpdateDocumentInformation(profileId, DocumentRootPath.SPECIALITY_BOARD_PATH, document, oldFilePath, profileDocument);

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
                if(await profileRepository.AnyAsync(p => p.HospitalPrivilegeInformation.HospitalPrivilegeInformationID == hospitalPrivilegeInformation.HospitalPrivilegeInformationID &&
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
                if (hospitalPrivilegeDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.Preference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllHospitalPrivilegeAsSecondary(profileId);
                }

                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.AffilicationStartDate.Equals(hospitalPrivilegeDetail.AffilicationStartDate) &&
                                                                                                                                                       h.AffiliationEndDate.Equals(hospitalPrivilegeDetail.AffiliationEndDate))))
                {
                    throw new DuplicateHospitalPrivilegeException(ExceptionMessage.HOSPITAL_PRIVILEGE_INFORMATION_EXISTS_EXCEPTION);
                }
                
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.HOSPITAL_PRIVILEGE, null, hospitalPrivilegeDetail.AffiliationEndDate);

                //Add the document if uploaded
                hospitalPrivilegeDetail.HospitalPrevilegeLetterPath = AddDocument(profileId, DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, document, profileDocument, null);                

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
                if (hospitalPrivilegeDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.HospitalPrivilegeDetailID != hospitalPrivilegeDetail.HospitalPrivilegeDetailID && h.Preference == PreferenceType.Primary.ToString())))
                {
                    profileRepository.SetAllHospitalPrivilegeAsSecondary(profileId);
                }

                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId && p.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Any(h => h.HospitalPrivilegeDetailID != hospitalPrivilegeDetail.HospitalPrivilegeDetailID &&
                                                                                                                                                       h.AffilicationStartDate.Equals(hospitalPrivilegeDetail.AffilicationStartDate) &&
                                                                                                                                                       h.AffiliationEndDate.Equals(hospitalPrivilegeDetail.AffiliationEndDate))))
                {
                    throw new DuplicateHospitalPrivilegeException(ExceptionMessage.HOSPITAL_PRIVILEGE_INFORMATION_EXISTS_EXCEPTION);
                }
                
                string oldFilePath = hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;

                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.HOSPITAL_PRIVILEGE, oldFilePath, hospitalPrivilegeDetail.AffiliationEndDate); ;

                //Add or update the document if present
                hospitalPrivilegeDetail.HospitalPrevilegeLetterPath = AddUpdateDocumentInformation(profileId, DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, document, oldFilePath, profileDocument);

                //Update the hospital privilege information
                profileRepository.UpdateHospitalPrivilegeDetail(profileId, hospitalPrivilegeDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(hospitalPrivilegeDetail.HospitalPrevilegeLetterPath))
                    documentManager.DeleteFile(oldFilePath);
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
                
                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.HOSPITAL_PRIVILEGE, hospitalPrivilegeDetail.HospitalPrevilegeLetterPath, hospitalPrivilegeDetail.AffiliationEndDate); ;

                //Add or update the document if present
                hospitalPrivilegeDetail.HospitalPrevilegeLetterPath = AddUpdateDocumentInformation(profileId, DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, document, hospitalPrivilegeDetail.HospitalPrevilegeLetterPath, profileDocument);

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

        #endregion

        #region Professional Liability

        public async Task<int> AddProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo, DocumentDTO document)
        {
            try
            {
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.PROFESSIONAL_LIABILITY, null, professionalLiabilityInfo.ExpirationDate);

                //Add the document if uploaded
                professionalLiabilityInfo.InsuranceCertificatePath = AddDocument(profileId, DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, document, profileDocument, null);

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

                //Create the profile document object
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.PROFESSIONAL_LIABILITY, oldFilePath, professionalLiabilityInfo.ExpirationDate);

                //Add or update the document if present
                professionalLiabilityInfo.InsuranceCertificatePath = AddUpdateDocumentInformation(profileId, DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, document, oldFilePath, profileDocument);

                //Update the professional liability information
                profileRepository.UpdateProfessionalLiability(profileId, professionalLiabilityInfo);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldFilePath) && !oldFilePath.Equals(professionalLiabilityInfo.InsuranceCertificatePath))
                    documentManager.DeleteFile(oldFilePath);
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

                //Add or update the document if present
                professionalLiabilityInfo.InsuranceCertificatePath = AddUpdateDocumentInformation(profileId, DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, document, professionalLiabilityInfo.InsuranceCertificatePath, profileDocument);

                //Update the professional liabiliy history information
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

        #endregion

        #region Work History

        public async Task<int> AddProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperience professionalWorkExperience, DocumentDTO document)
        {
            try
            {
                if(await profileRepository.AnyAsync(p => p.ProfileID == profileId && 
                    p.ProfessionalWorkExperiences.Any(w => w.EmployerName.Equals(professionalWorkExperience.EmployerName) &&
                                                           w.StartDate.Equals(professionalWorkExperience.StartDate) &&
                                                           w.EndDate.Value.Equals(professionalWorkExperience.EndDate.Value))))
                {
                    throw new ProfessionalWorkExperienceExistException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION);
                }
                
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.PROFESSIONAL_WORK_EXPERIENCE, null, null);

                //Add the document if uploaded
                professionalWorkExperience.WorkExperienceDocPath = AddUpdateDocument(profileId, DocumentRootPath.PROFESSIONAL_WORK_EXPERIENCE_PATH, document, null, profileDocument);

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
                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.ProfessionalWorkExperiences.Any(w => w.ProfessionalWorkExperienceID != professionalWorkExperience.ProfessionalWorkExperienceID &&
                                                           w.EmployerName.Equals(professionalWorkExperience.EmployerName) &&
                                                           w.StartDate.Equals(professionalWorkExperience.StartDate) &&
                                                           w.EndDate.Value.Equals(professionalWorkExperience.EndDate.Value))))
                {
                    throw new ProfessionalWorkExperienceExistException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_EXISTS_EXCEPTION);
                }
                
                string oldPath = null;

                if (document != null)
                {
                    oldPath = professionalWorkExperience.WorkExperienceDocPath;

                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.EDUCATION_CERTIFICATE, oldPath, null);

                    //Add the document if uploaded
                    professionalWorkExperience.WorkExperienceDocPath = AddUpdateDocument(profileId, DocumentRootPath.EDUCATION_CERTIFICATE_PATH, document, oldPath, profileDocument);
                }    
                
                //Update the professional work experience information
                profileRepository.UpdateProfessionalWorkExperience(profileId, professionalWorkExperience);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldPath) && !oldPath.Equals(professionalWorkExperience.WorkExperienceDocPath))
                    documentManager.DeleteFile(oldPath);
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

        public async Task<int> AddPublicHealthServiceAsync(int profileId, PublicHealthService publicHealthService)
        {
            try
            {
                if (await profileRepository.AnyAsync(p => p.ProfileID == profileId &&
                    p.PublicHealthServices.Any(w => w.StartDate.Equals(publicHealthService.StartDate) &&
                                                    w.EndDate.Equals(publicHealthService.EndDate))))
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
                                                    w.StartDate.Equals(publicHealthService.StartDate) &&
                                                    w.EndDate.Equals(publicHealthService.EndDate))))
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
                                                            (workGap.EndDate >= w.StartDate && workGap.EndDate <= w.EndDate))))
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
                                        (workGap.EndDate >= w.StartDate && workGap.EndDate <= w.EndDate))))
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

        #endregion

        #region Private Methods

        private string AddDocumentInPath(string docRootPath, DocumentDTO document, string oldFilePath, string docTitle)
        {
            string docPath = oldFilePath;

            //Save and add document
            if (document != null)
            {
                docPath = documentManager.SaveDocument(document, docTitle);
            }

            return docPath;
        }

        private ProfileDocument CreateProfileDocumentObject(string title, string docPath, DateTime? expiryDate)
        {
            return new ProfileDocument()
            {
                DocPath = docPath,
                Title = title,
                ExpiryDate = expiryDate
            };
        }

        private string AddUpdateDocument(int profileId, string docRootPath, DocumentDTO document, string oldFilePath, ProfileDocument profileDocument)
        {
            string newDocPath = oldFilePath;

            //Save and update Document
            if (document != null)
            {
                //Save the document in the path
                profileDocument.DocPath = newDocPath = documentManager.SaveDocument(document, docRootPath);

                //Add or update other legal name document
                if (!String.IsNullOrEmpty(oldFilePath))
                    //Update the profile document information in repository
                    profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);
                else
                    //Add the profile document information in repository
                    profileRepository.AddDocument(profileId, profileDocument);
            }

            return newDocPath;
        }

        private string AddUpdateDocumentInformation(int profileId, string docRootPath, DocumentDTO document, string oldFilePath, ProfileDocument profileDocument)
        {
            string newDocPath = oldFilePath;

            //Save and update Document
            if (document != null)
            {
                //Save the document in the path
                profileDocument.DocPath = newDocPath = documentManager.SaveDocument(document, docRootPath);

                //Add or update other legal name document
                if (!String.IsNullOrEmpty(oldFilePath))
                    //Update the profile document information in repository
                    profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);
                else
                    //Add the profile document information in repository
                    profileRepository.AddDocument(profileId, profileDocument);
            }
            else if (!String.IsNullOrEmpty(oldFilePath))
                //Update other information if document is present such as expiry date
                profileRepository.UpdateDocument(profileId, oldFilePath, profileDocument);

            return newDocPath;
        }

        private string AddDocument(int profileId, string docRootPath, DocumentDTO document, ProfileDocument profileDocument, string oldFilePath)
        {
            string newDocPath = oldFilePath;

            //Save and add document
            if (document != null)
            {
                //Save the document in the path
                newDocPath = profileDocument.DocPath = documentManager.SaveDocument(document, docRootPath);

                //Add other legal name document in database
                profileRepository.AddDocument(profileId, profileDocument);
            }

            return newDocPath;
        }

        #endregion

        #region Education History

        public async Task<int> AddEducationDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.EducationDetail educationDetail, DocumentDTO graduateDocument)
        {
            try
            {
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.EDUCATION_CERTIFICATE, null, null);                    
                
                //Add the document if uploaded
                educationDetail.CertificatePath = AddUpdateDocument(profileId, DocumentRootPath.EDUCATION_CERTIFICATE_PATH, graduateDocument, null, profileDocument);

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
                string oldGraduateFilePath = null;

                if(graduateDocument != null)
                {
                    oldGraduateFilePath = educationDetail.CertificatePath;

                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.EDUCATION_CERTIFICATE, oldGraduateFilePath, null);

                    //Add the document if uploaded
                    educationDetail.CertificatePath = AddUpdateDocument(profileId, DocumentRootPath.EDUCATION_CERTIFICATE_PATH, graduateDocument, oldGraduateFilePath, profileDocument);
                }        

                //Update the education detail information
                profileRepository.UpdateEducationDetail(profileId, educationDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldGraduateFilePath) && !oldGraduateFilePath.Equals(educationDetail.CertificatePath))
                    documentManager.DeleteFile(oldGraduateFilePath);
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

        public async Task<int> AddTrainingDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.TrainingDetail trainingDetail, IList<DocumentDTO> documents)
        {
            try
            {
                for (int q = 0; q < trainingDetail.ResidencyInternshipDetails.Count; q++)
                {
                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.RESIDENCY_INTERNSHIP_CERTIFICATE, null, null);

                    //Add the document if uploaded
                    trainingDetail.ResidencyInternshipDetails.ElementAt(q).DocumentPath = AddUpdateDocument(profileId, DocumentRootPath.RESIDENCY_INTERNSHIP_PATH, documents[q], null, profileDocument);
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
                if(residencyInternshipDetail.PreferenceType == PreferenceType.Primary &&
                    await profileRepository.AnyAsync(p => p.ProfileID == profileId && 
                                                          p.TrainingDetails.Any(t => t.ResidencyInternshipDetails.Any(r => r.Preference.Equals(PreferenceType.Primary.ToString())))))
                {
                    profileRepository.SetAllResidencyInternshipAsSecondary(profileId, trainingId);
                }
                
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.RESIDENCY_INTERNSHIP_CERTIFICATE, null, null);

                //Add the document if uploaded
                residencyInternshipDetail.DocumentPath = AddUpdateDocument(profileId, DocumentRootPath.RESIDENCY_INTERNSHIP_PATH, document, null, profileDocument);
                
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
                
                string oldPath = null;

                if (document != null)
                {
                    oldPath = residencyInternshipDetail.DocumentPath;

                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.RESIDENCY_INTERNSHIP_CERTIFICATE, oldPath, null);

                    //Add the document if uploaded
                    residencyInternshipDetail.DocumentPath = AddUpdateDocument(profileId, DocumentRootPath.RESIDENCY_INTERNSHIP_PATH, document, oldPath, profileDocument);
                }   

                //Update residencty internship detail
                profileRepository.UpdateResidencyInternshipDetail(profileId, trainingId, residencyInternshipDetail);

                //save the information in the repository
                await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldPath) && !oldPath.Equals(residencyInternshipDetail.DocumentPath))
                    documentManager.DeleteFile(oldPath);
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
                ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.CME_CERTIFICATION, null, null);

                //Add the document if uploaded
                cmeCertification.CertificatePath = AddUpdateDocument(profileId, DocumentRootPath.CME_CERTIFICATION_PATH, document, null, profileDocument);

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
                string oldPath = null;

                if (document != null)
                {
                    oldPath = cmeCertification.CertificatePath;

                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.CME_CERTIFICATION, oldPath, null);

                    //Add the document if uploaded
                    cmeCertification.CertificatePath = AddUpdateDocument(profileId, DocumentRootPath.CME_CERTIFICATION_PATH, document, oldPath, profileDocument);
                }   
                
                //Update the CME Certification information
                profileRepository.UpdateCMECertification(profileId, cmeCertification);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldPath) && !oldPath.Equals(cmeCertification.CertificatePath))
                    documentManager.DeleteFile(oldPath);
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

        public async Task UpdateECFMGDetailAsync(int profileId, Entities.MasterProfile.EducationHistory.ECFMGDetail ecfmgDetail, DocumentDTO document)
        {
            try
            {
                string oldPath = null;

                if (document != null)
                {
                    oldPath = ecfmgDetail.ECFMGCertPath;

                    ProfileDocument profileDocument = CreateProfileDocumentObject(DocumentTitle.ECFMG_CERTIFICATE, oldPath, null);

                    //Add the document if uploaded
                    ecfmgDetail.ECFMGCertPath = AddUpdateDocument(profileId, DocumentRootPath.ECFMG_PATH, document, oldPath, profileDocument);
                }

                //Update the ECFMG Certification information
                profileRepository.UpdateECFMGDetail(profileId, ecfmgDetail);

                //save the information in the repository
                var result = await profileRepository.SaveAsync();

                //Delete the previous file
                if (!String.IsNullOrEmpty(oldPath) && !oldPath.Equals(ecfmgDetail.ECFMGCertPath))
                    documentManager.DeleteFile(oldPath);
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
    }
}
