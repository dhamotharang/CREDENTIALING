using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Exceptions.Profiles;
using System.Data.Entity.Validation;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Rules;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterProfile.Contract;

namespace AHC.CD.Data.EFRepository.Profiles
{
    internal class ProfileEFRepository : EFGenericRepository<Profile>, IProfileRepository
    {
        #region Demographics

        public string UpdateProfileImage(int profileId, string imagePath)
        {
            string oldFilePath = null;

            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    oldFilePath = profile.ProfilePhotoPath;
                    profile.ProfilePhotoPath = imagePath;

                    Update(profile);
                }

                return oldFilePath;
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PERSONAL_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        #region Personal Detail

        /// <summary>
        /// Updates Personal Detail
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="personalDetail"></param>
        /// <returns>void</returns>
        public void UpdatePersonalDetail(int profileId, Entities.MasterProfile.Demographics.PersonalDetail personalDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updatePersonalDetails = profile.PersonalDetail;

                    if (updatePersonalDetails == null)
                    {
                        //personalDetail.ProviderTypes = MapProviderType(personalDetail.ProviderTypes.ToList());
                        profile.PersonalDetail = personalDetail;
                    }
                    else
                    {
                        updatePersonalDetails = AutoMapper.Mapper.Map<PersonalDetail, PersonalDetail>(personalDetail, updatePersonalDetails);

                        foreach (var providerTitle in personalDetail.ProviderTitles)
                        {
                            if (providerTitle.ProviderTitleID != 0)
                            {
                                var title = updatePersonalDetails.ProviderTitles.FirstOrDefault(t => t.ProviderTitleID == providerTitle.ProviderTitleID);
                                if (title != null)
                                {
                                    title = AutoMapper.Mapper.Map<ProviderTitle, ProviderTitle>(providerTitle, title);
                                }
                                else
                                    updatePersonalDetails.ProviderTitles.Add(providerTitle);
                            }
                            else
                                updatePersonalDetails.ProviderTitles.Add(providerTitle);


                        }
                    }

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PERSONAL_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Other Legal Name

        public void AddOtherLegalNames(int profileId, Entities.MasterProfile.Demographics.OtherLegalName otherLegalName)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.OtherLegalNames == null)
                        profile.OtherLegalNames = new List<OtherLegalName>();

                    profile.OtherLegalNames.Add(otherLegalName);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_LEGAL_NAME_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateOtherLegalNames(int profileId, OtherLegalName otherLegalName)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateOtherLegalName = profile.OtherLegalNames.Where(d => d.OtherLegalNameID == otherLegalName.OtherLegalNameID).FirstOrDefault();
                    updateOtherLegalName = AutoMapper.Mapper.Map<OtherLegalName, OtherLegalName>(otherLegalName, updateOtherLegalName);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_LEGAL_NAME_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Home Address

        public void AddHomeAddress(int profileId, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    if (profile.HomeAddresses == null)
                        profile.HomeAddresses = new List<HomeAddress>();

                    profile.HomeAddresses.Add(homeAddress);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOME_ADDRESS_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateHomeAddress(int profileId, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateHomeAddress = profile.HomeAddresses.Where(h => h.HomeAddressID == homeAddress.HomeAddressID).FirstOrDefault();
                    updateHomeAddress = AutoMapper.Mapper.Map<HomeAddress, HomeAddress>(homeAddress, updateHomeAddress);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOME_ADDRESS_UPDATE_EXCEPTION, ex);
            }
        }

        public void SetAllHomeAddressAsSecondary(int profileId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var primaryHomeAddress = profile.HomeAddresses.FirstOrDefault(h => h.AddressPreference.Equals(PreferenceType.Primary.ToString()));
                    primaryHomeAddress.AddressPreferenceType = PreferenceType.Secondary;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOME_ADDRESS_PREFERENCE_SET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Contact Detail

        /// <summary>
        /// Updates Contact Detail
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="hospitalPrivil"></param>
        /// <returns>Provider</returns>
        public void UpdateContactDetail(int profileId, Entities.MasterProfile.Demographics.ContactDetail contactDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateContactDetails = profile.ContactDetail;

                    if (updateContactDetails == null)
                        profile.ContactDetail = contactDetail;
                    else
                    {
                        updateContactDetails.PersonalPager = contactDetail.PersonalPager;

                        foreach (var email in contactDetail.EmailIDs)
                        {
                            if (email.EmailDetailID != 0)
                            {
                                var updateEmail = updateContactDetails.EmailIDs.FirstOrDefault(e => e.EmailDetailID == email.EmailDetailID);
                                if (updateEmail == null)
                                    updateContactDetails.EmailIDs.Add(email);
                                else
                                    updateEmail = AutoMapper.Mapper.Map<EmailDetail, EmailDetail>(email, updateEmail);
                            }
                            else
                                updateContactDetails.EmailIDs.Add(email);

                        }

                        foreach (var phone in contactDetail.PhoneDetails)
                        {
                            if (phone.PhoneDetailID != 0)
                            {
                                var updatePhone = updateContactDetails.PhoneDetails.FirstOrDefault(e => e.PhoneDetailID == phone.PhoneDetailID);
                                if (updatePhone == null)
                                    updateContactDetails.PhoneDetails.Add(phone);
                                else
                                    updatePhone = AutoMapper.Mapper.Map<PhoneDetail, PhoneDetail>(phone, updatePhone);
                            }
                            else
                                updateContactDetails.PhoneDetails.Add(phone);


                        }

                        foreach (var pContacts in contactDetail.PreferredContacts)
                        {
                            if (pContacts.PreferredContactID != 0)
                            {
                                var updatePreferredContacts = updateContactDetails.PreferredContacts.FirstOrDefault(e => e.PreferredContactID == pContacts.PreferredContactID);
                                if (updatePreferredContacts == null)
                                    updateContactDetails.PreferredContacts.Add(pContacts);
                                else
                                    updatePreferredContacts = AutoMapper.Mapper.Map<PreferredContact, PreferredContact>(pContacts, updatePreferredContacts);
                            }
                            else
                                updateContactDetails.PreferredContacts.Add(pContacts);


                        }

                        foreach (var pWrittenContacts in contactDetail.PreferredWrittenContacts)
                        {
                            if (pWrittenContacts.PreferredWrittenContactID != 0)
                            {
                                var updatePreferredWrittenContacts = updateContactDetails.PreferredWrittenContacts.FirstOrDefault(e => e.PreferredWrittenContactID == pWrittenContacts.PreferredWrittenContactID);
                                if (updatePreferredWrittenContacts == null)
                                    updateContactDetails.PreferredWrittenContacts.Add(pWrittenContacts);
                                else
                                    updatePreferredWrittenContacts = AutoMapper.Mapper.Map<PreferredWrittenContact, PreferredWrittenContact>(pWrittenContacts, updatePreferredWrittenContacts);
                            }
                            else
                                updateContactDetails.PreferredWrittenContacts.Add(pWrittenContacts);


                        }
                    }

                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CONTACT_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Personal Identification

        public void UpdatePersonalIdentification(int profileId, Entities.MasterProfile.Demographics.PersonalIdentification personalIdentification)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updatePersonalIdentification = profile.PersonalIdentification;

                    if (updatePersonalIdentification == null)
                        profile.PersonalIdentification = personalIdentification;
                    else
                        updatePersonalIdentification = AutoMapper.Mapper.Map<PersonalIdentification, PersonalIdentification>(personalIdentification, updatePersonalIdentification);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PERSONAL_IDENTIFICATION_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Birth Certificate

        /// <summary>
        /// Updates BirthInformation
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="birthInformation"></param>
        /// <returns>void</returns>
        public void UpdateBirthInformation(int profileId, Entities.MasterProfile.Demographics.BirthInformation birthInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateBirthInformation = profile.BirthInformation;

                    if (updateBirthInformation == null)
                        profile.BirthInformation = birthInformation;
                    else
                        updateBirthInformation = AutoMapper.Mapper.Map<BirthInformation, BirthInformation>(birthInformation, updateBirthInformation);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.BIRTH_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Visa Detail

        // <summary>
        /// Updates Visa Information
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="visaInformation"></param>
        /// <returns>void</returns>

        public void UpdateVisaInformation(int profileId, Entities.MasterProfile.Demographics.VisaDetail visaInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateVisaInformation = profile.VisaDetail;

                    if (updateVisaInformation == null)
                        profile.VisaDetail = visaInformation;
                    else
                    {
                        updateVisaInformation = AutoMapper.Mapper.Map<VisaDetail, VisaDetail>(visaInformation, updateVisaInformation);

                        if (updateVisaInformation.VisaInfo == null && visaInformation.VisaInfo != null)
                        {
                            updateVisaInformation.VisaInfo = visaInformation.VisaInfo;
                        }
                        else
                            updateVisaInformation.VisaInfo = AutoMapper.Mapper.Map<VisaInfo, VisaInfo>(visaInformation.VisaInfo, updateVisaInformation.VisaInfo);
                    }


                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.VISA_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddVisaInformationHistory(int profileId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateVisaInformation = profile.VisaDetail;
                    if (updateVisaInformation.VisaDetailHistory == null)
                    {
                        updateVisaInformation.VisaDetailHistory = new List<VisaDetailHistory>();
                    }

                    updateVisaInformation.VisaDetailHistory.Add(new VisaDetailHistory()
                    {
                        IsResidentOfUSAYesNoOption = updateVisaInformation.IsResidentOfUSAYesNoOption,
                        IsAuthorizedToWorkInUSYesNoOption = updateVisaInformation.IsAuthorizedToWorkInUSYesNoOption
                    });
                    Update(profile);
                    Save();

                    updateVisaInformation.IsResidentOfUSAYesNoOption = null;
                    updateVisaInformation.IsAuthorizedToWorkInUSYesNoOption = null;
                    Update(profile);

                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.VISA_DETAIL_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Language

        /// <summary>
        /// Updates Language Information
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="languageInformation"></param>
        /// <returns>void</returns>
        public void UpdateLanguageInformation(int profileId, Entities.MasterProfile.Demographics.LanguageInfo languageInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateLanguageInformation = profile.LanguageInfo;

                    if (updateLanguageInformation == null)
                        profile.LanguageInfo = languageInformation;
                    else
                        updateLanguageInformation = AutoMapper.Mapper.Map<LanguageInfo, LanguageInfo>(languageInformation, updateLanguageInformation);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.LANGUAGE_INFO_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #endregion

        #region Identification and License

        #region State License

        public void AddStateLicense(int profileId, StateLicenseInformation stateLicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.StateLicenses == null)
                        profile.StateLicenses = new List<StateLicenseInformation>();

                    profile.StateLicenses.Add(stateLicense);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.STATE_LICENSE_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateStateLicense(int profileId, StateLicenseInformation stateLicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateStateLicense = profile.StateLicenses.FirstOrDefault(d => d.StateLicenseInformationID == stateLicense.StateLicenseInformationID);
                    updateStateLicense = AutoMapper.Mapper.Map<StateLicenseInformation, StateLicenseInformation>(stateLicense, updateStateLicense);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.STATE_LICENSE_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddStateLicenseHistory(int profileId, int stateLicenseId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateStateLicense = profile.StateLicenses.FirstOrDefault(d => d.StateLicenseInformationID == stateLicenseId);
                    updateStateLicense.StateLicenseInfoHistory.Add(new StateLicenseInfoHistory()
                    {
                        CurrentIssueDate = updateStateLicense.CurrentIssueDate.Value,
                        ExpiryDate = updateStateLicense.ExpiryDate.Value,
                        StateLicenseDocumentPath = updateStateLicense.StateLicenseDocumentPath
                    });
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.STATE_LICENSE_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateStateLicenseHistory(int profileId, int stateLicenseId, StateLicenseInfoHistory stateLicenseHistory)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateStateLicense = profile.StateLicenses.FirstOrDefault(d => d.StateLicenseInformationID == stateLicenseId);
                    var updateStateLicenseHistory = updateStateLicense.StateLicenseInfoHistory.FirstOrDefault(h =>
                                                            h.ExpiryDate.Date.Equals(updateStateLicense.ExpiryDate.Value.Date) &&
                                                            h.CurrentIssueDate.Date.Equals(updateStateLicense.CurrentIssueDate.Value.Date));

                    if (updateStateLicenseHistory != null)
                    {
                        stateLicenseHistory.StateLicenseInfoHistoryID = updateStateLicenseHistory.StateLicenseInfoHistoryID;
                        updateStateLicenseHistory = AutoMapper.Mapper.Map<StateLicenseInfoHistory, StateLicenseInfoHistory>(stateLicenseHistory, updateStateLicenseHistory);
                        Update(profile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.STATE_LICENSE_HISTORY_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Federal DEA

        public void AddFederalDEALicense(int profileId, FederalDEAInformation federalDEALicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.FederalDEAInformations == null)
                        profile.FederalDEAInformations = new List<FederalDEAInformation>();

                    profile.FederalDEAInformations.Add(federalDEALicense);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.FEDERAL_DEA_LICENSE_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateFederalDEALicense(int profileId, FederalDEAInformation federalDEALicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateFederalDEALicense = profile.FederalDEAInformations.FirstOrDefault(d => d.FederalDEAInformationID == federalDEALicense.FederalDEAInformationID);
                    updateFederalDEALicense = AutoMapper.Mapper.Map<FederalDEAInformation, FederalDEAInformation>(federalDEALicense, updateFederalDEALicense);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.FEDERAL_DEA_LICENSE_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddFederalDEALicenseHistory(int profileId, int federalDEALicenseId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateFederalDEALicense = profile.FederalDEAInformations.FirstOrDefault(d => d.FederalDEAInformationID == federalDEALicenseId);

                    List<DEAScheduleInfoHistory> scheduleHistory = new List<DEAScheduleInfoHistory>();
                    foreach (var item in updateFederalDEALicense.DEAScheduleInfoes)
                    {
                        scheduleHistory.Add(new DEAScheduleInfoHistory() { DEAScheduleID = item.DEAScheduleID, YesNoOption = item.YesNoOption });
                    }

                    updateFederalDEALicense.FederalDEAInfoHistory.Add(new FederalDEAInfoHistory()
                    {
                        ExpiryDate = updateFederalDEALicense.ExpiryDate.Value,
                        IssueDate = updateFederalDEALicense.IssueDate.Value,
                        FederalDEADocumentPath = updateFederalDEALicense.DEALicenceCertPath,
                        DEAScheduleInfoHistory = scheduleHistory
                    });
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.FEDERAL_DEA_LICENSE_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateFederalDEALicenseHistory(int profileId, int federalDEALicenseId, FederalDEAInfoHistory federalDEALicenseHistory)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateFederalDEALicense = profile.FederalDEAInformations.FirstOrDefault(d => d.FederalDEAInformationID == federalDEALicenseId);
                    if (updateFederalDEALicense.FederalDEAInfoHistory != null)
                    {
                        var updateFederalDEALicenseHistory = updateFederalDEALicense.FederalDEAInfoHistory.FirstOrDefault(h =>
                                                                            h.ExpiryDate.Date.Equals(federalDEALicenseHistory.ExpiryDate.Date) &&
                                                                            h.IssueDate.Date.Equals(federalDEALicenseHistory.IssueDate.Date));

                        if (updateFederalDEALicenseHistory != null)
                        {
                            federalDEALicenseHistory.FederalDEAInfoHistoryID = updateFederalDEALicenseHistory.FederalDEAInfoHistoryID;
                            updateFederalDEALicenseHistory = AutoMapper.Mapper.Map<FederalDEAInfoHistory, FederalDEAInfoHistory>(federalDEALicenseHistory, updateFederalDEALicenseHistory);
                            Update(profile);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.FEDERAL_DEA_LICENSE_HISTORY_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region CDSC Information

        public void AddCDSCLicense(int profileId, CDSCInformation cdscLicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.CDSCInformations == null)
                        profile.CDSCInformations = new List<CDSCInformation>();

                    profile.CDSCInformations.Add(cdscLicense);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CDSC_LICENSE_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateCDSCLicense(int profileId, CDSCInformation cdscLicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateCDSCLicense = profile.CDSCInformations.FirstOrDefault(d => d.CDSCInformationID == cdscLicense.CDSCInformationID);
                    updateCDSCLicense = AutoMapper.Mapper.Map<CDSCInformation, CDSCInformation>(cdscLicense, updateCDSCLicense);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CDSC_LICENSE_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddCDSCLicenseHistory(int profileId, int cdscLicenseId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateCDSCLicense = profile.CDSCInformations.FirstOrDefault(d => d.CDSCInformationID == cdscLicenseId);
                    updateCDSCLicense.CDSCInfoHistory.Add(new CDSCInfoHistory()
                    {
                        ExpiryDate = updateCDSCLicense.ExpiryDate.Value,
                        IssueDate = updateCDSCLicense.IssueDate.Value,
                        CDSCCerificatePath = updateCDSCLicense.CDSCCerificatePath
                    });
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CDSC_LICENSE_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateCDSCLicenseHistory(int profileId, int cdscLicenseId, CDSCInfoHistory cdscLicenseHistory)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateCDSCLicense = profile.CDSCInformations.FirstOrDefault(d => d.CDSCInformationID == cdscLicenseId);
                    var updateCSSCLicenseHistory = updateCDSCLicense.CDSCInfoHistory.FirstOrDefault(h =>
                                                            h.ExpiryDate.Date.Equals(cdscLicenseHistory.ExpiryDate.Date) &&
                                                            h.IssueDate.Date.Equals(cdscLicenseHistory.IssueDate.Date));

                    if (updateCSSCLicenseHistory != null)
                    {
                        cdscLicenseHistory.CDSCInfoHistoryID = updateCSSCLicenseHistory.CDSCInfoHistoryID;
                        updateCSSCLicenseHistory = AutoMapper.Mapper.Map<CDSCInfoHistory, CDSCInfoHistory>(cdscLicenseHistory, updateCSSCLicenseHistory);
                        Update(profile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CDSC_LICENSE_HISTORY_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Medicare Information

        public void AddMedicareInformation(int profileId, MedicareInformation medicareInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.MedicareInformations == null)
                        profile.MedicareInformations = new List<MedicareInformation>();

                    profile.MedicareInformations.Add(medicareInformation);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICARE_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateMedicareInformation(int profileId, MedicareInformation medicareInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateMedicareInformation = profile.MedicareInformations.FirstOrDefault(d => d.MedicareInformationID == medicareInformation.MedicareInformationID);
                    updateMedicareInformation = AutoMapper.Mapper.Map<MedicareInformation, MedicareInformation>(medicareInformation, updateMedicareInformation);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICARE_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Medicaid Information

        public void AddMedicaidInformation(int profileId, MedicaidInformation medicaidInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.MedicaidInformations == null)
                        profile.MedicaidInformations = new List<MedicaidInformation>();

                    profile.MedicaidInformations.Add(medicaidInformation);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICAID_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateMedicaidInformation(int profileId, MedicaidInformation medicaidInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateMedicaidInformation = profile.MedicaidInformations.FirstOrDefault(d => d.MedicaidInformationID == medicaidInformation.MedicaidInformationID);
                    updateMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformation, MedicaidInformation>(medicaidInformation, updateMedicaidInformation);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICAID_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Other Identification Number

        public void UpdateOtherIdentificationNumber(int profileId, OtherIdentificationNumber otherIdentificationNumber)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateOtherIdentificationNumber = profile.OtherIdentificationNumber;

                    if (updateOtherIdentificationNumber == null)
                        profile.OtherIdentificationNumber = otherIdentificationNumber;
                    else
                        updateOtherIdentificationNumber = AutoMapper.Mapper.Map<OtherIdentificationNumber, OtherIdentificationNumber>(otherIdentificationNumber, updateOtherIdentificationNumber);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_IDENTIFICATION_NUMBER_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #endregion

        #region Specialty/Board

        #region Specialty Detail

        public void AddSpecialtyDetail(int profileId, Entities.MasterProfile.BoardSpecialty.SpecialtyDetail specialtyDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.SpecialtyDetails == null)
                        profile.SpecialtyDetails = new List<SpecialtyDetail>();

                    profile.SpecialtyDetails.Add(specialtyDetail);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.SPECIALITY_BOARD_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateSpecialtyDetail(int profileId, Entities.MasterProfile.BoardSpecialty.SpecialtyDetail specialtyDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateSpecialtyDetail = profile.SpecialtyDetails.FirstOrDefault(d => d.SpecialtyDetailID == specialtyDetail.SpecialtyDetailID);

                    //if (updateSpecialtyDetail.SpecialtyBoardCertifiedDetail != null)
                    //{
                    //    updateSpecialtyDetail.SpecialtyBoardCertifiedDetail = AutoMapper.Mapper.Map<SpecialtyBoardCertifiedDetail, SpecialtyBoardCertifiedDetail>(specialtyDetail.SpecialtyBoardCertifiedDetail, updateSpecialtyDetail.SpecialtyBoardCertifiedDetail);
                    //}


                    //if (updateSpecialtyDetail.SpecialtyBoardNotCertifiedDetail != null)
                    //    updateSpecialtyDetail.SpecialtyBoardNotCertifiedDetail = AutoMapper.Mapper.Map<SpecialtyBoardNotCertifiedDetail, SpecialtyBoardNotCertifiedDetail>(specialtyDetail.SpecialtyBoardNotCertifiedDetail, updateSpecialtyDetail.SpecialtyBoardNotCertifiedDetail);

                    updateSpecialtyDetail = AutoMapper.Mapper.Map<SpecialtyDetail, SpecialtyDetail>(specialtyDetail, updateSpecialtyDetail);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.SPECIALITY_BOARD_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddSpecialtyBoardCertifiedDetailHistory(int profileId, int specialtyBoardCertifiedDetailID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateSpecialtyDetail = profile.SpecialtyDetails.Where( m => m.SpecialtyBoardCertifiedDetail != null ).FirstOrDefault(d => d.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID == specialtyBoardCertifiedDetailID);
                    updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory.Add(new SpecialtyBoardCertifiedDetailHistory()
                    {
                        IssueDate = updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.LastReCerificationDate.Value,
                        ExpiryDate = updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate.Value,
                        BoardCertificatePath = updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath
                    });
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.SPECIALITY_BOARD_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateSpecialtyBoardCertifiedDetailHistory(int profileId, int specialtyBoardCertifiedDetailID, Entities.MasterProfile.BoardSpecialty.SpecialtyBoardCertifiedDetailHistory specialtyBoardCertifiedDetailHistory)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateSpecialtyDetail = profile.SpecialtyDetails.FirstOrDefault(d => d.SpecialtyBoardCertifiedDetail != null && d.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID == specialtyBoardCertifiedDetailID);
                    if (updateSpecialtyDetail != null)
                    {
                        var updateSpecialtyDetailHistory = updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory.FirstOrDefault(h =>
                                                            h.ExpiryDate.Equals(updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate) &&
                                                            h.IssueDate.Equals(updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.LastReCerificationDate.Value));

                        if (updateSpecialtyDetailHistory != null)
                        {
                            specialtyBoardCertifiedDetailHistory.SpecialtyBoardCertifiedDetailHistoryID = updateSpecialtyDetailHistory.SpecialtyBoardCertifiedDetailHistoryID;
                            updateSpecialtyDetailHistory = AutoMapper.Mapper.Map<SpecialtyBoardCertifiedDetailHistory, SpecialtyBoardCertifiedDetailHistory>(specialtyBoardCertifiedDetailHistory, updateSpecialtyDetailHistory);
                            Update(profile);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.SPECIALITY_BOARD_HISTORY_UPDATE_EXCEPTION, ex);
            }
        }

        public void SetAllSpecialityAsSecondary(int profileId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var primaryBoardSpecialty = profile.SpecialtyDetails.FirstOrDefault(h => h.SpecialtyPreference.Equals(PreferenceType.Primary.ToString()));
                    primaryBoardSpecialty.PreferenceType = PreferenceType.Secondary;
                    primaryBoardSpecialty.PercentageOfTime = BusinessRule.BOARD_SPECIALTY_PERCENTAGE;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.BOARD_SPECIALITY_PREFERENCE_SET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Practice Interest

        public void UpdatePracticeInterest(int profileId, Entities.MasterProfile.BoardSpecialty.PracticeInterest practiceInterest)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updatePracticeInterest = profile.PracticeInterest;

                    if (updatePracticeInterest == null)
                        profile.PracticeInterest = practiceInterest;
                    else
                        updatePracticeInterest = AutoMapper.Mapper.Map<PracticeInterest, PracticeInterest>(practiceInterest, updatePracticeInterest);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PRACTICE_INTERSET_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #endregion

        #region Hospital Privilege

        public void UpdateHospitalPrivilegeInformation(int profileId, HospitalPrivilegeInformation hospitalPrivilegeInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    var updateHospitalPrivilegeInformation = profile.HospitalPrivilegeInformation;

                    if (updateHospitalPrivilegeInformation == null)
                        profile.HospitalPrivilegeInformation = hospitalPrivilegeInformation;
                    else
                    {
                        hospitalPrivilegeInformation.HospitalPrivilegeDetails = updateHospitalPrivilegeInformation.HospitalPrivilegeDetails;
                        updateHospitalPrivilegeInformation = AutoMapper.Mapper.Map<HospitalPrivilegeInformation, HospitalPrivilegeInformation>(hospitalPrivilegeInformation, updateHospitalPrivilegeInformation);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddHospitalPrivilegeDetail(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.HospitalPrivilegeInformation == null)
                    {
                        profile.HospitalPrivilegeInformation = new HospitalPrivilegeInformation() { HospitalPrivilegeYesNoOption = Entities.MasterData.Enums.YesNoOption.YES };
                        profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails = new List<HospitalPrivilegeDetail>();
                    }

                    profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.Add(hospitalPrivilegeDetail);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateHospitalPrivilegeDetail(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateHospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(d => d.HospitalPrivilegeDetailID == hospitalPrivilegeDetail.HospitalPrivilegeDetailID);
                    updateHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetail, HospitalPrivilegeDetail>(hospitalPrivilegeDetail, updateHospitalPrivilegeDetail);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddHospitalPrivilegeDetailHistory(int profileId, int hospitalPrivilegeDetailId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateHospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(d => d.HospitalPrivilegeDetailID == hospitalPrivilegeDetailId);
                    updateHospitalPrivilegeDetail.HospitalPrivilegeDetailHistory.Add(new HospitalPrivilegeDetailHistory()
                    {
                        IssueDate = updateHospitalPrivilegeDetail.AffilicationStartDate.Value,
                        ExpiryDate = updateHospitalPrivilegeDetail.AffiliationEndDate.Value,
                        HospitalPrevilegeLetterPath = updateHospitalPrivilegeDetail.HospitalPrevilegeLetterPath
                    });
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateHospitalPrivilegeDetailHistory(int profileId, int hospitalPrivilegeDetailId, HospitalPrivilegeDetailHistory hospitalPrivilegeDetailHistory)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateHospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(d => d.HospitalPrivilegeDetailID == hospitalPrivilegeDetailId);
                    var updateHospitalPrivilegeDetailHistory = updateHospitalPrivilegeDetail.HospitalPrivilegeDetailHistory.FirstOrDefault(h =>
                                                            h.ExpiryDate.Date.Equals(updateHospitalPrivilegeDetail.AffiliationEndDate.Value) &&
                                                            h.IssueDate.Equals(updateHospitalPrivilegeDetail.AffilicationStartDate));

                    if (updateHospitalPrivilegeDetailHistory != null)
                    {
                        hospitalPrivilegeDetailHistory.HospitalPrivilegeDetailHistoryID = updateHospitalPrivilegeDetailHistory.HospitalPrivilegeDetailHistoryID;
                        updateHospitalPrivilegeDetailHistory = AutoMapper.Mapper.Map<HospitalPrivilegeDetailHistory, HospitalPrivilegeDetailHistory>(hospitalPrivilegeDetailHistory, updateHospitalPrivilegeDetailHistory);
                        Update(profile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_HISTORY_UPDATE_EXCEPTION, ex);
            }
        }

        public void SetAllHospitalPrivilegeAsSecondary(int profileId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var primaryHospitalPrivilege = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(h => h.Preference.Equals(PreferenceType.Primary.ToString()));
                    primaryHospitalPrivilege.PreferenceType = PreferenceType.Secondary;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_PREFERENCE_SET_EXCEPTION, ex);
            }
        }

        public void AddHospitalPrivilegeInformationHistory(int profileId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateHospitalPrivilegeInformation = profile.HospitalPrivilegeInformation;
                    if (updateHospitalPrivilegeInformation.HospitalPrivilegeInformationHistory == null)
                    {
                        updateHospitalPrivilegeInformation.HospitalPrivilegeInformationHistory = new List<HospitalPrivilegeInformationHistory>();
                    }

                    updateHospitalPrivilegeInformation.HospitalPrivilegeInformationHistory.Add(new HospitalPrivilegeInformationHistory()
                    {
                        HasHospitalPrivilege = updateHospitalPrivilegeInformation.HasHospitalPrivilege,
                        OtherAdmittingArrangements = updateHospitalPrivilegeInformation.OtherAdmittingArrangements,
                        HospitalPrivilegeDetails = updateHospitalPrivilegeInformation.HospitalPrivilegeDetails,
                    });
                    Update(profile);
                    Save();

                    updateHospitalPrivilegeInformation.HospitalPrivilegeYesNoOption = null;
                    updateHospitalPrivilegeInformation.OtherAdmittingArrangements = null;
                    updateHospitalPrivilegeInformation.HospitalPrivilegeDetails = null;
                    Update(profile);

                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Liability

        public void AddProfessionalLiability(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.ProfessionalLiabilityInfoes == null)
                        profile.ProfessionalLiabilityInfoes = new List<ProfessionalLiabilityInfo>();

                    profile.ProfessionalLiabilityInfoes.Add(professionalLiabilityInfo);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_LIABILITY_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateProfessionalLiability(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfessionalLiabilityInfo = profile.ProfessionalLiabilityInfoes.FirstOrDefault(d => d.ProfessionalLiabilityInfoID == professionalLiabilityInfo.ProfessionalLiabilityInfoID);
                    updateProfessionalLiabilityInfo = AutoMapper.Mapper.Map<ProfessionalLiabilityInfo, ProfessionalLiabilityInfo>(professionalLiabilityInfo, updateProfessionalLiabilityInfo);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_LIABILITY_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddProfessionalLiabilityHistory(int profileId, int professionalLiabilityInfoId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfessionalLiabilityInsurance = profile.ProfessionalLiabilityInfoes.FirstOrDefault(d => d.ProfessionalLiabilityInfoID == professionalLiabilityInfoId);
                 
                    updateProfessionalLiabilityInsurance.ProfessionalLiabilityInfoHistory.Add(new ProfessionalLiabilityInfoHistory()
                    {
                        IssueDate = updateProfessionalLiabilityInsurance.EffectiveDate.Value,
                        ExpiryDate = updateProfessionalLiabilityInsurance.ExpirationDate.Value,
                        InsuranceCertificatePath = updateProfessionalLiabilityInsurance.InsuranceCertificatePath
                    });
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_LIABILITY_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateProfessionalLiabilityHistory(int profileId, int professionalLiabilityInfoId, ProfessionalLiabilityInfoHistory professionalLiabilityInfoHistory)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateprofessionalLiabilityInfo = profile.ProfessionalLiabilityInfoes.FirstOrDefault(d => d.ProfessionalLiabilityInfoID == professionalLiabilityInfoId);
                    var updateProfessionalLiabilityInfoHistory = updateprofessionalLiabilityInfo.ProfessionalLiabilityInfoHistory.FirstOrDefault(h =>
                                                            h.ExpiryDate.Date.Equals(updateprofessionalLiabilityInfo.EffectiveDate) &&
                                                            h.IssueDate.Date.Equals(updateprofessionalLiabilityInfo.ExpirationDate));

                    if (updateProfessionalLiabilityInfoHistory != null)
                    {
                        professionalLiabilityInfoHistory.ProfessionalLiabilityInfoHistoryID = updateProfessionalLiabilityInfoHistory.ProfessionalLiabilityInfoHistoryID;
                        updateProfessionalLiabilityInfoHistory = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoHistory, ProfessionalLiabilityInfoHistory>(professionalLiabilityInfoHistory, updateProfessionalLiabilityInfoHistory);
                        Update(profile);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_LIABILITY_HISTORY_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Affiliation

        public void AddProfessionalAffiliation(int profileId, ProfessionalAffiliationInfo professionalAffiliation)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.ProfessionalAffiliationInfos == null)
                        profile.ProfessionalAffiliationInfos = new List<ProfessionalAffiliationInfo>();

                    profile.ProfessionalAffiliationInfos.Add(professionalAffiliation);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_AFFILIATION_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateProfessionalAffiliation(int profileId, ProfessionalAffiliationInfo professionalAffiliation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfessionalAffiliationInfo = profile.ProfessionalAffiliationInfos.FirstOrDefault(d => d.ProfessionalAffiliationInfoID == professionalAffiliation.ProfessionalAffiliationInfoID);
                    updateProfessionalAffiliationInfo = AutoMapper.Mapper.Map<ProfessionalAffiliationInfo, ProfessionalAffiliationInfo>(professionalAffiliation, updateProfessionalAffiliationInfo);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_AFFILIATION_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Professional Reference

        public void AddProfessionalReference(int profileId, Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo professionalReference)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.ProfessionalReferenceInfos == null)
                        profile.ProfessionalReferenceInfos = new List<ProfessionalReferenceInfo>();

                    profile.ProfessionalReferenceInfos.Add(professionalReference);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_REFERENCE_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateProfessionalReference(int profileId, Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo professionalReference)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfessionalReferenceInfo = profile.ProfessionalReferenceInfos.FirstOrDefault(d => d.ProfessionalReferenceInfoID == professionalReference.ProfessionalReferenceInfoID);
                    updateProfessionalReferenceInfo = AutoMapper.Mapper.Map<ProfessionalReferenceInfo, ProfessionalReferenceInfo>(professionalReference, updateProfessionalReferenceInfo);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_REFERENCE_UPDATE_EXCEPTION, ex);
            }
        }

        public void SetProfessionalReferenceStatus(int profileId, int professionalReferenceId, StatusType status)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfessionalReferenceInfo = profile.ProfessionalReferenceInfos.FirstOrDefault(d => d.ProfessionalReferenceInfoID == professionalReferenceId);
                    updateProfessionalReferenceInfo.StatusType = status;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_REFERENCE_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Work History

        public void AddProfessionalWorkExperience(int profileId, ProfessionalWorkExperience professionalWorkExperience)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.ProfessionalWorkExperiences == null)
                        profile.ProfessionalWorkExperiences = new List<ProfessionalWorkExperience>();

                    profile.ProfessionalWorkExperiences.Add(professionalWorkExperience);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateProfessionalWorkExperience(int profileId, ProfessionalWorkExperience professionalWorkExperience)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfessionalWorkExperience = profile.ProfessionalWorkExperiences.FirstOrDefault(d => d.ProfessionalWorkExperienceID == professionalWorkExperience.ProfessionalWorkExperienceID);
                    updateProfessionalWorkExperience = AutoMapper.Mapper.Map<ProfessionalWorkExperience, ProfessionalWorkExperience>(professionalWorkExperience, updateProfessionalWorkExperience);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddMilitaryServiceInformation(int profileId, MilitaryServiceInformation militaryServiceInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.MilitaryServiceInformations == null)
                        profile.MilitaryServiceInformations = new List<MilitaryServiceInformation>();

                    profile.MilitaryServiceInformations.Add(militaryServiceInformation);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateMilitaryServiceInformation(int profileId, MilitaryServiceInformation militaryServiceInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateMilitaryServiceInformation = profile.MilitaryServiceInformations.FirstOrDefault(d => d.MilitaryServiceInformationID == militaryServiceInformation.MilitaryServiceInformationID);
                    updateMilitaryServiceInformation = AutoMapper.Mapper.Map<MilitaryServiceInformation, MilitaryServiceInformation>(militaryServiceInformation, updateMilitaryServiceInformation);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddPublicHealthService(int profileId, PublicHealthService publicHealthService)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.PublicHealthServices == null)
                        profile.PublicHealthServices = new List<PublicHealthService>();

                    profile.PublicHealthServices.Add(publicHealthService);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdatePublicHealthService(int profileId, PublicHealthService publicHealthService)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updatePublicHealthService = profile.PublicHealthServices.FirstOrDefault(d => d.PublicHealthServiceID == publicHealthService.PublicHealthServiceID);
                    updatePublicHealthService = AutoMapper.Mapper.Map<PublicHealthService, PublicHealthService>(publicHealthService, updatePublicHealthService);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddWorkGap(int profileId, WorkGap workGap)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.WorkGaps == null)
                        profile.WorkGaps = new List<WorkGap>();

                    profile.WorkGaps.Add(workGap);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.WORK_GAP_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateWorkGap(int profileId, WorkGap workGap)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateWorkGap = profile.WorkGaps.FirstOrDefault(d => d.WorkGapID == workGap.WorkGapID);
                    updateWorkGap = AutoMapper.Mapper.Map<WorkGap, WorkGap>(workGap, updateWorkGap);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.WORK_GAP_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Education History

        public void AddEducationDetail(int profileId, Entities.MasterProfile.EducationHistory.EducationDetail educationDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.EducationDetails == null)
                        profile.EducationDetails = new List<EducationDetail>();

                    profile.EducationDetails.Add(educationDetail);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.EDUCATION_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateEducationDetail(int profileId, Entities.MasterProfile.EducationHistory.EducationDetail educationDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateEducationDetail = profile.EducationDetails.FirstOrDefault(d => d.EducationDetailID == educationDetail.EducationDetailID);
                    updateEducationDetail = AutoMapper.Mapper.Map<EducationDetail, EducationDetail>(educationDetail, updateEducationDetail);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.EDUCATION_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddTrainingDetail(int profileId, Entities.MasterProfile.EducationHistory.TrainingDetail trainingDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.TrainingDetails == null)
                        profile.TrainingDetails = new List<TrainingDetail>();

                    profile.TrainingDetails.Add(trainingDetail);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.TRAINING_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateTrainingDetail(int profileId, Entities.MasterProfile.EducationHistory.TrainingDetail trainingDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateTrainingDetail = profile.TrainingDetails.FirstOrDefault(d => d.TrainingDetailID == trainingDetail.TrainingDetailID);
                    trainingDetail.ResidencyInternshipDetails = updateTrainingDetail.ResidencyInternshipDetails;
                    updateTrainingDetail = AutoMapper.Mapper.Map<TrainingDetail, TrainingDetail>(trainingDetail, updateTrainingDetail);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.TRAINING_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddResidencyInternshipDetail(int profileId, int trainingId, Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail residencyInternshipDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {

                    var trainingDetails = profile.TrainingDetails.FirstOrDefault(t => t.TrainingDetailID == trainingId);

                    if (trainingDetails.ResidencyInternshipDetails == null)
                        trainingDetails.ResidencyInternshipDetails = new List<ResidencyInternshipDetail>();

                    trainingDetails.ResidencyInternshipDetails.Add(residencyInternshipDetail);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.RESIDENCY_INTERNSHIP_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateResidencyInternshipDetail(int profileId, int trainingId, Entities.MasterProfile.EducationHistory.ResidencyInternshipDetail residencyInternshipDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateResidencyInternshipDetail = profile.TrainingDetails.FirstOrDefault(d => d.TrainingDetailID == trainingId)
                                                            .ResidencyInternshipDetails.FirstOrDefault(r => r.ResidencyInternshipDetailID == residencyInternshipDetail.ResidencyInternshipDetailID);
                    updateResidencyInternshipDetail = AutoMapper.Mapper.Map<ResidencyInternshipDetail, ResidencyInternshipDetail>(residencyInternshipDetail, updateResidencyInternshipDetail);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.RESIDENCY_INTERNSHIP_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public void SetAllResidencyInternshipAsSecondary(int profileId, int trainingId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var primaryResindencyInternship = profile.TrainingDetails.Select(h => h.ResidencyInternshipDetails.FirstOrDefault(r => r.Preference.Equals(PreferenceType.Primary.ToString()))).ToList();

                    foreach (var item in primaryResindencyInternship)
                    {
                        if (item != null && item.PreferenceType == PreferenceType.Primary)
                        {
                            item.PreferenceType = PreferenceType.Secondary;
                            break;
                        }
                    }

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.RESIDENCY_INTERNSHIP_PREFERENCE_SET_EXCEPTION, ex);
            }
        }

        public void AddCMECertification(int profileId, Entities.MasterProfile.EducationHistory.CMECertification cmeCertification)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.CMECertifications == null)
                        profile.CMECertifications = new List<CMECertification>();

                    profile.CMECertifications.Add(cmeCertification);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CME_CERTIFICATION_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateCMECertification(int profileId, Entities.MasterProfile.EducationHistory.CMECertification cmeCertification)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateCMECertification = profile.CMECertifications.FirstOrDefault(d => d.CMECertificationID == cmeCertification.CMECertificationID);
                    updateCMECertification = AutoMapper.Mapper.Map<CMECertification, CMECertification>(cmeCertification, updateCMECertification);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CME_CERTIFICATION_UPDATE_EXCEPTION, ex);
            }
        }

        public void UpdateECFMGDetail(int profileId, ECFMGDetail ecfmgDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateECFMGDetail = profile.ECFMGDetail;

                    if (updateECFMGDetail == null)
                        profile.ECFMGDetail = ecfmgDetail;
                    else
                        updateECFMGDetail = AutoMapper.Mapper.Map<ECFMGDetail, ECFMGDetail>(ecfmgDetail, updateECFMGDetail);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.ECFMG_CERTIFICATION_UPDATE_EXCEPTION, ex);
            }
        }


        public void AddProgramDetail(int profileId, ProgramDetail programDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.ProgramDetails == null)
                        profile.ProgramDetails = new List<ProgramDetail>();

                    profile.ProgramDetails.Add(programDetail);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROGRAM_DETAIL_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateProgramDetail(int profileId, ProgramDetail programDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProgramDetail = profile.ProgramDetails.FirstOrDefault(d => d.ProgramDetailID == programDetail.ProgramDetailID);
                    //programDetail.SchoolInformation = updateProgramDetail.SchoolInformation;
                    updateProgramDetail = AutoMapper.Mapper.Map<ProgramDetail, ProgramDetail>(programDetail, updateProgramDetail);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROGRAM_DETAIL_UPDATE_EXCEPTION, ex);
            }
        }

        public void SetAllProgramAsSecondary(int profileId, int programDetailId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var primaryResindencyInternship = profile.ProgramDetails.ToList();

                    foreach (var item in primaryResindencyInternship)
                    {
                        if (item != null && item.PreferenceType == PreferenceType.Primary)
                        {
                            item.PreferenceType = PreferenceType.Secondary;
                            break;
                        }
                    }

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.RESIDENCY_INTERNSHIP_PREFERENCE_SET_EXCEPTION, ex);
            }
        }

        #endregion

        #region Practice Location

        public void AddPracticeLocation(int profileId, PracticeLocationDetail practiceLocationDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.PracticeLocationDetails == null)
                        profile.PracticeLocationDetails = new List<PracticeLocationDetail>();

                    profile.PracticeLocationDetails.Add(practiceLocationDetail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Disclosure Questions

        public void AddEditDisclosureQuestionAnswers(int profileId, Entities.MasterProfile.DisclosureQuestions.ProfileDisclosure disclosureQuestionAnswers)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    var updateProfileDisclosure = profile.ProfileDisclosure;

                    if (updateProfileDisclosure == null)
                        profile.ProfileDisclosure = disclosureQuestionAnswers;
                    else
                    {
                        updateProfileDisclosure = AutoMapper.Mapper.Map<ProfileDisclosure, ProfileDisclosure>(disclosureQuestionAnswers, updateProfileDisclosure);

                        foreach (var profileDisclosureQuestionAnswer in disclosureQuestionAnswers.ProfileDisclosureQuestionAnswers)
                        {
                            if (profileDisclosureQuestionAnswer.ProfileDisclosureQuestionAnswerID != 0)
                            {
                                var answer = updateProfileDisclosure.ProfileDisclosureQuestionAnswers.FirstOrDefault(t => t.ProfileDisclosureQuestionAnswerID == profileDisclosureQuestionAnswer.ProfileDisclosureQuestionAnswerID);
                                if (answer != null)
                                {
                                    answer = AutoMapper.Mapper.Map<ProfileDisclosureQuestionAnswer, ProfileDisclosureQuestionAnswer>(profileDisclosureQuestionAnswer, answer);
                                }
                                else
                                    updateProfileDisclosure.ProfileDisclosureQuestionAnswers.Add(profileDisclosureQuestionAnswer);
                            }
                            else
                                updateProfileDisclosure.ProfileDisclosureQuestionAnswers.Add(profileDisclosureQuestionAnswer);
                        }
                    }
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.DISCLOSURE_QUESTIONS_CREATE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Profile Document

        public void AddDocument(int profileId, Entities.MasterProfile.ProfileDocument profileDocument)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    if (profile.ProfileDocuments == null)
                        profile.ProfileDocuments = new List<ProfileDocument>();

                    profile.ProfileDocuments.Add(profileDocument);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFILE_DOCUMENT_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateDocument(int profileId, string previousDocPath, Entities.MasterProfile.ProfileDocument profileDocument)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfileDocument = profile.ProfileDocuments.FirstOrDefault(d => d.DocPath.Equals(previousDocPath));

                    if (updateProfileDocument == null)
                        throw new ProfileDocumentNotFoundException(ExceptionMessage.PROFILE_DOCUMENT_CREATE_EXCEPTION);

                    updateProfileDocument.DocPath = profileDocument.DocPath;
                    updateProfileDocument.ExpiryDate = profileDocument.ExpiryDate;

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFILE_DOCUMENT_UPDATE_EXCEPTION, ex);
            }
        }

        public void RemoveDocument(int profileId, string previousDocPath)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateProfileDocument = profile.ProfileDocuments.FirstOrDefault(d => d.DocPath.Equals(previousDocPath));

                    if (updateProfileDocument == null)
                        throw new ProfileDocumentNotFoundException(ExceptionMessage.PROFILE_DOCUMENT_CREATE_EXCEPTION);

                    profile.ProfileDocuments.Remove(updateProfileDocument);

                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFILE_DOCUMENT_UPDATE_EXCEPTION, ex);
            }
        }

        #endregion

       #region ContractInformation


        public void AddContractInformation(int profileId, ContractInfo contractInfo)
        {
            try {

                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.ContractInfoes == null)
                        profile.ContractInfoes = new List<ContractInfo>();

                    profile.ContractInfoes.Add(contractInfo);

                }
            }


            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_INFORMATION_ADD_EXCEPTION, ex);
            }
      
        }

        public void AddGroupInformation(int profileId, int contractInfoId, ContractGroupInfo contractGroupInfo)
        {
            try
            {

                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var contractInfo = profile.ContractInfoes.FirstOrDefault(x => x.ContractInfoID == contractInfoId);

                    if (contractInfo.ContractGroupInfoes == null) {

                        contractInfo.ContractGroupInfoes = new List<ContractGroupInfo>();
                    
                    }

                    contractInfo.ContractGroupInfoes.Add(contractGroupInfo);
                
                }

            }

            catch (Exception ex) {

                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_ADD_EXCEPTION, ex);
            }
        }


        public void UpdateContractInformation(int profileId, Entities.MasterProfile.Contract.ContractInfo contractInfo)
        {
            try {

                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    var updateContractInformation = profile.ContractInfoes.FirstOrDefault(x => x.ContractInfoID == contractInfo.ContractInfoID);
                    updateContractInformation = AutoMapper.Mapper.Map<AHC.CD.Entities.MasterProfile.Contract.ContractInfo, AHC.CD.Entities.MasterProfile.Contract.ContractInfo>(contractInfo, updateContractInformation);
                    
                    Update(profile);
                }
            
            }

            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        public void UpdateGroupInformation(int profileId, int contractInfoId, Entities.MasterProfile.Contract.ContractGroupInfo contractGroupInfo)
        {
            try {

                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var contractInfo = profile.ContractInfoes.FirstOrDefault(x=>x.ContractInfoID == contractInfoId);
                    var updateContractGroupInfo = contractInfo.ContractGroupInfoes.FirstOrDefault(x => x.ContractGroupInfoId == contractGroupInfo.ContractGroupInfoId);
                    updateContractGroupInfo = AutoMapper.Mapper.Map<ContractGroupInfo, ContractGroupInfo>(contractGroupInfo,updateContractGroupInfo);
                    Update(profile);
                }

            }

            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_UPDATE_EXCEPTION, ex);
            }


        }
   #endregion


        #region Private Methods

        /// <summary>
        /// Finds Individual Provider
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="hospitalPrivilegeInformation"></param>
        /// <returns>Provider</returns>
        private Profile FindProfile(int profileId)
        {
            return DbSet.Find(profileId);
        }


        /// <summary>
        /// Verify whether profile is exists
        /// </summary>
        /// <param name="profileId"></param>
        /// <param name="profile"></param>
        /// <returns>true or false</returns>
        private static bool IsProviderExists(int profileId, Profile profile)
        {
            if (profile == null)
            {
                throw new ProfileNotFoundException(ExceptionMessage.PROFILE_NOT_FOUND + " " + profileId);
            }
            return true;
        }

        private bool IsDuplicatePracticeLocation(int profileId, int facilityId){

            var isDuplicate = true;

            Profile profile = Find(profileId, "PracticeLocationDetails.Facility");

            // Has to be replaced with LINQ

            foreach (var location in profile.PracticeLocationDetails)
            {
                if (location.Facility.FacilityID == facilityId)
                {
                    isDuplicate = true;
                    break;
                }
            }

            return isDuplicate;
        }

        #endregion








       
    }
}

