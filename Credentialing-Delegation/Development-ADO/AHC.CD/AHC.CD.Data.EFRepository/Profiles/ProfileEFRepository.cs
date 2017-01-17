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
using AHC.CD.Entities.DocumentRepository;

namespace AHC.CD.Data.EFRepository.Profiles
{
    internal class ProfileEFRepository : EFGenericRepository<Profile>, IProfileRepository
    {
        //public ProfileEFRepository()
        //{
        //    this.context = new EFEntityContext(false);
        //}


        #region Deactivate Profile

        public void DeactivateProfile(int profileID, StatusType status)
        {
            try
            {
                Profile profile = FindProfile(profileID);
                if (IsProviderExists(profileID, profile))
                {
                    profile.StatusType = status;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.UNABLE_TO_DEACTIVATE_PROFILE, ex);
            }
        }

        #endregion

        #region Reactivate Profile

        public void ReactivateProfile(int profileID, StatusType status)
        {
            try
            {
                Profile profile = FindProfile(profileID);
                if (IsProviderExists(profileID, profile))
                {
                    profile.StatusType = status;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.UNABLE_TO_REACTIVATE_PROFILE, ex);
            }
        }

        #endregion

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

        public void AddOtherLegalNameHistory(int profileId, int OtherLegalNameId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    OtherLegalNameHistory addOtherLegalNameHistory = new OtherLegalNameHistory();
                    var updateOtherLegalName = profile.OtherLegalNames.FirstOrDefault(a => a.OtherLegalNameID == OtherLegalNameId);
                    addOtherLegalNameHistory = AutoMapper.Mapper.Map<OtherLegalName, OtherLegalNameHistory>(updateOtherLegalName, addOtherLegalNameHistory);
                    addOtherLegalNameHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateOtherLegalName.OtherLegalNameHistory == null)
                    {
                        updateOtherLegalName.OtherLegalNameHistory = new List<OtherLegalNameHistory>();
                    }
                    updateOtherLegalName.OtherLegalNameHistory.Add(addOtherLegalNameHistory);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_LEGAL_NAME_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void RemoveOtherLegalName(int profileId, OtherLegalName otherLegalName)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeOtherLegalNameInfo = profile.OtherLegalNames.FirstOrDefault(d => d.OtherLegalNameID == otherLegalName.OtherLegalNameID);
                    removeOtherLegalNameInfo.StatusType = otherLegalName.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_LEGAL_NAME_REMOVE_EXCEPTION, ex);
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

        public void AddHomeAddressHistory(int profileId, int HomeAddressId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    HomeAddressHistory addHomeAddressHistory = new HomeAddressHistory();
                    var updateHomeAddress = profile.HomeAddresses.FirstOrDefault(a => a.HomeAddressID == HomeAddressId);
                    addHomeAddressHistory = AutoMapper.Mapper.Map<HomeAddress, HomeAddressHistory>(updateHomeAddress, addHomeAddressHistory);
                    addHomeAddressHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateHomeAddress.HomeAddressHistory == null)
                    {
                        updateHomeAddress.HomeAddressHistory = new List<HomeAddressHistory>();
                    }
                    updateHomeAddress.HomeAddressHistory.Add(addHomeAddressHistory);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOME_ADDRESS_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void RemoveHomeAddress(int profileId, HomeAddress homeAddress)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeHomeAddress = profile.HomeAddresses.FirstOrDefault(d => d.HomeAddressID == homeAddress.HomeAddressID);
                    removeHomeAddress.StatusType = homeAddress.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOME_ADDRESS_REMOVE_EXCEPTION, ex);
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
                        CurrentIssueDate = updateStateLicense.CurrentIssueDate,
                        ExpiryDate = updateStateLicense.ExpiryDate,
                        StateLicenseDocumentPath = updateStateLicense.StateLicenseDocumentPath,
                        HistoryStatusType = HistoryStatusType.Renewed
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
                                                            h.ExpiryDate.Value.Equals(updateStateLicense.ExpiryDate.Value.Date) &&
                                                            h.CurrentIssueDate.Value.Equals(updateStateLicense.CurrentIssueDate.Value.Date));

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

        public void AddStateLicenseHistoryForRemoval(int profileId, int stateLicenseInformationID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    StateLicenseInfoHistory addStateLicenseInformationHistory = new StateLicenseInfoHistory();
                    var updateStateLicenseInformation = profile.StateLicenses.FirstOrDefault(a => a.StateLicenseInformationID == stateLicenseInformationID);
                    addStateLicenseInformationHistory = AutoMapper.Mapper.Map<StateLicenseInformation, StateLicenseInfoHistory>(updateStateLicenseInformation, addStateLicenseInformationHistory);
                    addStateLicenseInformationHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateStateLicenseInformation.StateLicenseInfoHistory == null)
                    {
                        updateStateLicenseInformation.StateLicenseInfoHistory = new List<StateLicenseInfoHistory>();
                    }
                    updateStateLicenseInformation.StateLicenseInfoHistory.Add(addStateLicenseInformationHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.STATE_LICENSE_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void RemoveStateLicense(int profileId, StateLicenseInformation stateLicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeStateLicense = profile.StateLicenses.FirstOrDefault(d => d.StateLicenseInformationID == stateLicense.StateLicenseInformationID);
                    removeStateLicense.StatusType = stateLicense.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.STATE_LICENSE_REMOVE_EXCEPTION, ex);
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
                        ExpiryDate = updateFederalDEALicense.ExpiryDate,
                        IssueDate = updateFederalDEALicense.IssueDate,
                        FederalDEADocumentPath = updateFederalDEALicense.DEALicenceCertPath,
                        DEAScheduleInfoHistory = scheduleHistory,
                        HistoryStatusType = HistoryStatusType.Renewed
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
                                                                            h.ExpiryDate.Value.Equals(federalDEALicenseHistory.ExpiryDate.Value) &&
                                                                            h.IssueDate.Value.Equals(federalDEALicenseHistory.IssueDate.Value));

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

        public void AddFederalDEALicenseHistoryForRemoval(int profileId, int federalDEAInformationID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    FederalDEAInfoHistory addFederalDEAInformationHistory = new FederalDEAInfoHistory();
                    var updateFederalDEAInformation = profile.FederalDEAInformations.FirstOrDefault(a => a.FederalDEAInformationID == federalDEAInformationID);

                    List<DEAScheduleInfoHistory> scheduleHistory = new List<DEAScheduleInfoHistory>();
                    foreach (var item in updateFederalDEAInformation.DEAScheduleInfoes)
                    {
                        scheduleHistory.Add(new DEAScheduleInfoHistory() { DEAScheduleID = item.DEAScheduleID, YesNoOption = item.YesNoOption });
                    } 
                    
                    addFederalDEAInformationHistory = AutoMapper.Mapper.Map<FederalDEAInformation, FederalDEAInfoHistory>(updateFederalDEAInformation, addFederalDEAInformationHistory);
                    addFederalDEAInformationHistory.DEAScheduleInfoHistory = scheduleHistory;
                    addFederalDEAInformationHistory.FederalDEADocumentPath = updateFederalDEAInformation.DEALicenceCertPath;
                    addFederalDEAInformationHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateFederalDEAInformation.FederalDEAInfoHistory == null)
                    {
                        updateFederalDEAInformation.FederalDEAInfoHistory = new List<FederalDEAInfoHistory>();
                    }
                    updateFederalDEAInformation.FederalDEAInfoHistory.Add(addFederalDEAInformationHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.FEDERAL_DEA_LICENSE_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public FederalDEAInformation RemoveFederalDEALicense(int profileId, FederalDEAInformation federalDEALicense)
        {
            try {

                Profile profile = FindProfile(profileId);
                FederalDEAInformation removeFederalDeaLicense = null;
                if(IsProviderExists(profileId, profile))
                {
                    removeFederalDeaLicense = profile.FederalDEAInformations.FirstOrDefault(d => d.FederalDEAInformationID == federalDEALicense.FederalDEAInformationID);
                    removeFederalDeaLicense.StatusType = federalDEALicense.StatusType;
                    Update(profile);
                }
                return removeFederalDeaLicense;
            }

            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_REFRENCE_REMOVE_EXCEPTION, ex);
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
                        ExpiryDate = updateCDSCLicense.ExpiryDate,
                        IssueDate = updateCDSCLicense.IssueDate,
                        CDSCCerificatePath = updateCDSCLicense.CDSCCerificatePath,
                        HistoryStatusType = HistoryStatusType.Renewed
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
                                                            h.ExpiryDate.Value.Equals(cdscLicenseHistory.ExpiryDate.Value) &&
                                                            h.IssueDate.Value.Equals(cdscLicenseHistory.IssueDate.Value));

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

        public void AddCDSCLicenseHistoryForRemoval(int profileId, int cDSCInformationID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    CDSCInfoHistory addCDSCInformationHistory = new CDSCInfoHistory();
                    var updateCDSCInformation = profile.CDSCInformations.FirstOrDefault(a => a.CDSCInformationID == cDSCInformationID);
                    addCDSCInformationHistory = AutoMapper.Mapper.Map<CDSCInformation, CDSCInfoHistory>(updateCDSCInformation, addCDSCInformationHistory);
                    addCDSCInformationHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateCDSCInformation.CDSCInfoHistory == null)
                    {
                        updateCDSCInformation.CDSCInfoHistory = new List<CDSCInfoHistory>();
                    }
                    updateCDSCInformation.CDSCInfoHistory.Add(addCDSCInformationHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CDSC_LICENSE_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void RemoveCDSCLicense(int profileId, CDSCInformation cdscLicense)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeCDSCInformation = profile.CDSCInformations.FirstOrDefault(d => d.CDSCInformationID == cdscLicense.CDSCInformationID);
                    removeCDSCInformation.StatusType = cdscLicense.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CDSC_LICENSE_REMOVE_EXCEPTION, ex);
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

        public void AddMedicareInformationHistory(int profileId, int medicareInformationID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    MedicareInformationHistory addMedicareInformationHistory = new MedicareInformationHistory();
                    var updateMedicareInformation = profile.MedicareInformations.FirstOrDefault(a => a.MedicareInformationID == medicareInformationID);
                    addMedicareInformationHistory = AutoMapper.Mapper.Map<MedicareInformation, MedicareInformationHistory>(updateMedicareInformation, addMedicareInformationHistory);
                    addMedicareInformationHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateMedicareInformation.MedicareInformationHistory == null)
                    {
                        updateMedicareInformation.MedicareInformationHistory = new List<MedicareInformationHistory>();
                    }
                    updateMedicareInformation.MedicareInformationHistory.Add(addMedicareInformationHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICARE_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveMedicareInformation(int profileId, MedicareInformation medicareInformation)
        {
            try
            {

                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removemedicareInformation = profile.MedicareInformations.FirstOrDefault(d => d.MedicareInformationID == medicareInformation.MedicareInformationID);
                    removemedicareInformation.StatusType = medicareInformation.StatusType;
                    Update(profile);
                }
            }

            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICARE_NUMBER_REMOVE_EXCEPTION, ex);
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

        public void AddMedicaidInformationHistory(int profileId, int medicaidInformationID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    MedicaidInformationHistory addMedicaidInformationHistory = new MedicaidInformationHistory();
                    var updateMedicaidInformation = profile.MedicaidInformations.FirstOrDefault(a => a.MedicaidInformationID == medicaidInformationID);
                    addMedicaidInformationHistory = AutoMapper.Mapper.Map<MedicaidInformation, MedicaidInformationHistory>(updateMedicaidInformation, addMedicaidInformationHistory);
                    addMedicaidInformationHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateMedicaidInformation.MedicaidInformationHistory == null)
                    {
                        updateMedicaidInformation.MedicaidInformationHistory = new List<MedicaidInformationHistory>();
                    }
                    updateMedicaidInformation.MedicaidInformationHistory.Add(addMedicaidInformationHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICAID_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveMedicaidInformation(int profileId, MedicaidInformation medicaidInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removemedicaidInformation = profile.MedicaidInformations.FirstOrDefault(d => d.MedicaidInformationID == medicaidInformation.MedicaidInformationID);
                    removemedicaidInformation.StatusType = medicaidInformation.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MEDICAID_NUMBER_REMOVE_EXCEPTION, ex);
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

        public SpecialtyDetail UpdateSpecialtyDetail(int profileId, Entities.MasterProfile.BoardSpecialty.SpecialtyDetail specialtyDetail)
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
                    return updateSpecialtyDetail;
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.SPECIALITY_BOARD_UPDATE_EXCEPTION, ex);
            }

            return null;
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
                        IssueDate = updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.LastReCerificationDate,
                        ExpiryDate = updateSpecialtyDetail.SpecialtyBoardCertifiedDetail.ExpirationDate,
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

        public void RemoveSpecialityDetail(int profileId, SpecialtyDetail specialtyDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeSpecialtyDetail = profile.SpecialtyDetails.FirstOrDefault(d => d.SpecialtyDetailID == specialtyDetail.SpecialtyDetailID);
                    removeSpecialtyDetail.StatusType = specialtyDetail.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.SPECIALITY_BOARD_REMOVE_EXCEPTION, ex);
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
                        IssueDate = updateHospitalPrivilegeDetail.AffilicationStartDate,
                        ExpiryDate = updateHospitalPrivilegeDetail.AffiliationEndDate,
                        HospitalPrevilegeLetterPath = updateHospitalPrivilegeDetail.HospitalPrevilegeLetterPath,
                        HistoryStatusType = HistoryStatusType.Renewed
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
                                                            h.ExpiryDate.Value.Equals(updateHospitalPrivilegeDetail.AffiliationEndDate.Value) &&
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

        public void AddHospitalPrivilegeHistory(int profileId, int hospitalPrivilegeDetailId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    HospitalPrivilegeDetailHistory addHospitalPrivilegeDetailHistory = new HospitalPrivilegeDetailHistory();
                    var updateHospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(h => h.HospitalPrivilegeDetailID == hospitalPrivilegeDetailId);
                    addHospitalPrivilegeDetailHistory = AutoMapper.Mapper.Map<HospitalPrivilegeDetail, HospitalPrivilegeDetailHistory>(updateHospitalPrivilegeDetail, addHospitalPrivilegeDetailHistory);
                    addHospitalPrivilegeDetailHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateHospitalPrivilegeDetail.HospitalPrivilegeDetailHistory == null)
                    {
                        updateHospitalPrivilegeDetail.HospitalPrivilegeDetailHistory = new List<HospitalPrivilegeDetailHistory>();
                    }
                    updateHospitalPrivilegeDetail.HospitalPrivilegeDetailHistory.Add(addHospitalPrivilegeDetailHistory);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void RemoveHospitalPrivilege(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeHospitalPrivilegeDetail = profile.HospitalPrivilegeInformation.HospitalPrivilegeDetails.FirstOrDefault(h => h.HospitalPrivilegeDetailID == hospitalPrivilegeDetail.HospitalPrivilegeDetailID);
                    removeHospitalPrivilegeDetail.StatusType = hospitalPrivilegeDetail.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_REMOVE_EXCEPTION, ex);
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
                        IssueDate = updateProfessionalLiabilityInsurance.EffectiveDate,
                        ExpiryDate = updateProfessionalLiabilityInsurance.ExpirationDate,
                        InsuranceCertificatePath = updateProfessionalLiabilityInsurance.InsuranceCertificatePath,
                        HistoryStatusType = HistoryStatusType.Renewed
                    });
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_LIABILITY_HISTORY_CREATE_EXCEPTION, ex);
            }
        }

        public void AddProfessionalLiabilityHistoryOnRemoval(int profileId, int professionalLiabilityInfoId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    ProfessionalLiabilityInfoHistory addProfessionalLiabilityInfoHistory = new ProfessionalLiabilityInfoHistory();
                    var updateProfessionalLiabilityInfo = profile.ProfessionalLiabilityInfoes.FirstOrDefault(a => a.ProfessionalLiabilityInfoID == professionalLiabilityInfoId);
                    addProfessionalLiabilityInfoHistory = AutoMapper.Mapper.Map<ProfessionalLiabilityInfo, ProfessionalLiabilityInfoHistory>(updateProfessionalLiabilityInfo, addProfessionalLiabilityInfoHistory);
                    addProfessionalLiabilityInfoHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateProfessionalLiabilityInfo.ProfessionalLiabilityInfoHistory == null)
                    {
                        updateProfessionalLiabilityInfo.ProfessionalLiabilityInfoHistory = new List<ProfessionalLiabilityInfoHistory>();
                    }
                    updateProfessionalLiabilityInfo.ProfessionalLiabilityInfoHistory.Add(addProfessionalLiabilityInfoHistory);
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
                                                            h.ExpiryDate.Value.Equals(updateprofessionalLiabilityInfo.EffectiveDate) &&
                                                            h.IssueDate.Value.Equals(updateprofessionalLiabilityInfo.ExpirationDate));

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

        public void RemoveProfessionalLiability(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeProfessionalLiability = profile.ProfessionalLiabilityInfoes.FirstOrDefault(d => d.ProfessionalLiabilityInfoID == professionalLiabilityInfo.ProfessionalLiabilityInfoID);
                    removeProfessionalLiability.StatusType = professionalLiabilityInfo.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.HOME_ADDRESS_REMOVE_EXCEPTION, ex);
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

        public void AddProfessionalAffiliationHistory(int profileId, int professionalAffiliationInfoID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    ProfessionalAffiliationInfoHistory addProfessionalAffiliationInfoHistory = new ProfessionalAffiliationInfoHistory();
                    var updateProfessionalAffiliationInfo = profile.ProfessionalAffiliationInfos.FirstOrDefault(a => a.ProfessionalAffiliationInfoID == professionalAffiliationInfoID);
                    addProfessionalAffiliationInfoHistory = AutoMapper.Mapper.Map<ProfessionalAffiliationInfo, ProfessionalAffiliationInfoHistory>(updateProfessionalAffiliationInfo, addProfessionalAffiliationInfoHistory);
                    addProfessionalAffiliationInfoHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateProfessionalAffiliationInfo.ProfessionalAffiliationInfoHistory == null)
                    {
                        updateProfessionalAffiliationInfo.ProfessionalAffiliationInfoHistory = new List<ProfessionalAffiliationInfoHistory>();
                    }
                    updateProfessionalAffiliationInfo.ProfessionalAffiliationInfoHistory.Add(addProfessionalAffiliationInfoHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_AFFILIATION_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveProfessionalAffiliation(int profileId, ProfessionalAffiliationInfo professionalAffiliation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeProfessionalAffiliationInfo = profile.ProfessionalAffiliationInfos.FirstOrDefault(d => d.ProfessionalAffiliationInfoID == professionalAffiliation.ProfessionalAffiliationInfoID);
                    removeProfessionalAffiliationInfo.StatusType = professionalAffiliation.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_AFFILIATION_REMOVE_EXCEPTION, ex);
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

        public void AddProfessionalReferenceHistory(int profileId, int professionalReferenceInfoID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    ProfessionalReferenceInfoHistory addProfessionalReferenceInfoHistory = new ProfessionalReferenceInfoHistory();
                    var updateProfessionalReferenceInfo = profile.ProfessionalReferenceInfos.FirstOrDefault(a => a.ProfessionalReferenceInfoID == professionalReferenceInfoID);
                    addProfessionalReferenceInfoHistory = AutoMapper.Mapper.Map<ProfessionalReferenceInfo, ProfessionalReferenceInfoHistory>(updateProfessionalReferenceInfo, addProfessionalReferenceInfoHistory);
                    addProfessionalReferenceInfoHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateProfessionalReferenceInfo.ProfessionalReferenceInfoHistory == null)
                    {
                        updateProfessionalReferenceInfo.ProfessionalReferenceInfoHistory = new List<ProfessionalReferenceInfoHistory>();
                    }
                    updateProfessionalReferenceInfo.ProfessionalReferenceInfoHistory.Add(addProfessionalReferenceInfoHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_REFERENCE_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveProfessionalReference(int profileId, ProfessionalReferenceInfo professionalReference)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeProfessionalReferenceInfo = profile.ProfessionalReferenceInfos.FirstOrDefault(d => d.ProfessionalReferenceInfoID == professionalReference.ProfessionalReferenceInfoID);
                    removeProfessionalReferenceInfo.StatusType = professionalReference.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_REFRENCE_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region CV Upload

        public void AddCVAsync(int profileId, CVInformation cvInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                profile.CVInformation = cvInformation;       
                Update(profile);
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CV_UPLOADED_EXCEPTION, ex);
            }
        }

        public void UpdateCVAsync(int profileId, CVInformation cvInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                profile.CVInformation = cvInformation;
                Update(profile);
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CV_UPLOADED_EXCEPTION, ex);
            }
        }

        public void RemoveCVAsync(int profileId, CVInformation cvInformation)
        {
            throw new NotImplementedException();
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

        public void AddProfessionalWorkExperienceHistory(int profileId, int professionalWorkExperienceID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    ProfessionalWorkExperienceHistory addProfessionalWorkExperienceHistory = new ProfessionalWorkExperienceHistory();
                    var updateProfessionalWorkExperience = profile.ProfessionalWorkExperiences.FirstOrDefault(a => a.ProfessionalWorkExperienceID == professionalWorkExperienceID);
                    addProfessionalWorkExperienceHistory = AutoMapper.Mapper.Map<ProfessionalWorkExperience, ProfessionalWorkExperienceHistory>(updateProfessionalWorkExperience, addProfessionalWorkExperienceHistory);
                    addProfessionalWorkExperienceHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateProfessionalWorkExperience.ProfessionalWorkExperienceHistory == null)
                    {
                        updateProfessionalWorkExperience.ProfessionalWorkExperienceHistory = new List<ProfessionalWorkExperienceHistory>();
                    }
                    updateProfessionalWorkExperience.ProfessionalWorkExperienceHistory.Add(addProfessionalWorkExperienceHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveProfessionalWorkExperience(int profileId, ProfessionalWorkExperience professionalWorkExperience)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeProfessionalWorkExperience = profile.ProfessionalWorkExperiences.FirstOrDefault(w => w.ProfessionalWorkExperienceID == professionalWorkExperience.ProfessionalWorkExperienceID);
                    removeProfessionalWorkExperience.StatusType = professionalWorkExperience.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_REMOVE_EXCEPTION, ex);
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

        public void AddMilitaryServiceInformationHistory(int profileId, int militaryServiceInformationID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    MilitaryServiceInformationHistory addMilitaryServiceInformationHistory = new MilitaryServiceInformationHistory();
                    var updateMilitaryServiceInformation = profile.MilitaryServiceInformations.FirstOrDefault(a => a.MilitaryServiceInformationID == militaryServiceInformationID);
                    addMilitaryServiceInformationHistory = AutoMapper.Mapper.Map<MilitaryServiceInformation, MilitaryServiceInformationHistory>(updateMilitaryServiceInformation, addMilitaryServiceInformationHistory);
                    addMilitaryServiceInformationHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateMilitaryServiceInformation.MilitaryServiceInformationHistory == null)
                    {
                        updateMilitaryServiceInformation.MilitaryServiceInformationHistory = new List<MilitaryServiceInformationHistory>();
                    }
                    updateMilitaryServiceInformation.MilitaryServiceInformationHistory.Add(addMilitaryServiceInformationHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveMilitaryServiceInformation(int profileId, MilitaryServiceInformation militaryServiceInformation)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeMilitaryServiceInformation = profile.MilitaryServiceInformations.FirstOrDefault(w => w.MilitaryServiceInformationID == militaryServiceInformation.MilitaryServiceInformationID);
                    removeMilitaryServiceInformation.StatusType = militaryServiceInformation.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.MILITARY_SERVICE_INFORMATION_REMOVE_EXCEPTION, ex);
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

        public void AddPublicHealthServiceHistory(int profileId, int publicHealthServiceID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    PublicHealthServiceHistory addPublicHealthServiceHistory = new PublicHealthServiceHistory();
                    var updatePublicHealthService = profile.PublicHealthServices.FirstOrDefault(a => a.PublicHealthServiceID == publicHealthServiceID);
                    addPublicHealthServiceHistory = AutoMapper.Mapper.Map<PublicHealthService, PublicHealthServiceHistory>(updatePublicHealthService, addPublicHealthServiceHistory);
                    addPublicHealthServiceHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updatePublicHealthService.PublicHealthServiceHistory == null)
                    {
                        updatePublicHealthService.PublicHealthServiceHistory = new List<PublicHealthServiceHistory>();
                    }
                    updatePublicHealthService.PublicHealthServiceHistory.Add(addPublicHealthServiceHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemovePublicHealthService(int profileId, PublicHealthService publicHealthService)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removePublicHealthService = profile.PublicHealthServices.FirstOrDefault(w => w.PublicHealthServiceID == publicHealthService.PublicHealthServiceID);
                    removePublicHealthService.StatusType = publicHealthService.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PUBLIC_HEALTH_SERVICE_REMOVE_EXCEPTION, ex);
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

        public void AddWorkGapHistory(int profileId, int workGapID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    WorkGapHistory addWorkGapHistory = new WorkGapHistory();
                    var updateWorkGap = profile.WorkGaps.FirstOrDefault(a => a.WorkGapID == workGapID);
                    addWorkGapHistory = AutoMapper.Mapper.Map<WorkGap, WorkGapHistory>(updateWorkGap, addWorkGapHistory);
                    addWorkGapHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateWorkGap.WorkGapHistory == null)
                    {
                        updateWorkGap.WorkGapHistory = new List<WorkGapHistory>();
                    }
                    updateWorkGap.WorkGapHistory.Add(addWorkGapHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.WORK_GAP_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveWorkGap(int profileId, WorkGap workGap)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeWorkGap = profile.WorkGaps.FirstOrDefault(w => w.WorkGapID == workGap.WorkGapID);
                    removeWorkGap.StatusType = workGap.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.WORK_GAP_REMOVE_EXCEPTION, ex);
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

        public void AddEducationDetailHistory(int profileId, int educationDetailID)
        {
            try
            {
                Profile profile = this.Get(p => p.ProfileID == profileId, "EducationDetails.SchoolInformation").FirstOrDefault();
                if (IsProviderExists(profileId, profile))
                {
                    EducationDetailHistory addEducationDetailHistory = new EducationDetailHistory();
                    var updateEducationDetail = profile.EducationDetails.FirstOrDefault(a => a.EducationDetailID == educationDetailID);
                    addEducationDetailHistory = AutoMapper.Mapper.Map<EducationDetail, EducationDetailHistory>(updateEducationDetail, addEducationDetailHistory);
                    addEducationDetailHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateEducationDetail.EducationDetailHistory == null)
                    {
                        updateEducationDetail.EducationDetailHistory = new List<EducationDetailHistory>();
                    }
                    updateEducationDetail.EducationDetailHistory.Add(addEducationDetailHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.EDUCATION_DETAIL_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public EducationDetail RemoveEducationDetail(int profileId, EducationDetail educationDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeEducationDetail = profile.EducationDetails.FirstOrDefault(e => e.EducationDetailID == educationDetail.EducationDetailID);
                    removeEducationDetail.StatusType = educationDetail.StatusType;
                    Update(profile);
                    return removeEducationDetail;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.EDUCATION_DETAIL_REMOVE_EXCEPTION, ex);
            }
            return null;
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

        public void AddCertificationDetailHistory(int profileId, int cmeCertificationID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    CMECertificationHistory addCertificationDetailHistory = new CMECertificationHistory();
                    var updateCertificationDetail = profile.CMECertifications.FirstOrDefault(a => a.CMECertificationID == cmeCertificationID);
                    addCertificationDetailHistory = AutoMapper.Mapper.Map<CMECertification, CMECertificationHistory>(updateCertificationDetail, addCertificationDetailHistory);
                    addCertificationDetailHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateCertificationDetail.CMECertificationHistory == null)
                    {
                        updateCertificationDetail.CMECertificationHistory = new List<CMECertificationHistory>();
                    }
                    updateCertificationDetail.CMECertificationHistory.Add(addCertificationDetailHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CME_CERTIFICATION_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveCertificationDetail(int profileId, CMECertification cmeCertification)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeCMECertification = profile.CMECertifications.FirstOrDefault(d => d.CMECertificationID == cmeCertification.CMECertificationID);
                    removeCMECertification.StatusType = cmeCertification.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CME_CERTIFICATION_REMOVE_EXCEPTION, ex);
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

        public void AddProgramDetailHistory(int profileId, int programDetailID)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    ProgramDetailHistory addProgramDetailHistory = new ProgramDetailHistory();
                    var updateProgramDetail = profile.ProgramDetails.FirstOrDefault(a => a.ProgramDetailID == programDetailID);
                    addProgramDetailHistory = AutoMapper.Mapper.Map<ProgramDetail, ProgramDetailHistory>(updateProgramDetail, addProgramDetailHistory);
                    addProgramDetailHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateProgramDetail.ProgramDetailHistory == null)
                    {
                        updateProgramDetail.ProgramDetailHistory = new List<ProgramDetailHistory>();
                    }
                    updateProgramDetail.ProgramDetailHistory.Add(addProgramDetailHistory);
                    Update(profile);
                }

            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROGRAM_DETAIL_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveProgramDetail(int profileId, ProgramDetail programDetail)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeProgramDetail = profile.ProgramDetails.FirstOrDefault(p => p.ProgramDetailID == programDetail.ProgramDetailID);
                    removeProgramDetail.StatusType = programDetail.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.PROGRAM_DETAIL_REMOVE_EXCEPTION, ex);
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
            try
            {

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
                    if (contractInfo.ContractGroupInfoes == null)
                    {
                        contractInfo.ContractGroupInfoes = new List<ContractGroupInfo>();
                    }
                    contractInfo.ContractGroupInfoes.Add(contractGroupInfo);
                }
            }

            catch (Exception ex)
            {

                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_ADD_EXCEPTION, ex);
            }
        }

        public void UpdateContractInformation(int profileId, Entities.MasterProfile.Contract.ContractInfo contractInfo)
        {
            try
            {

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
            try
            {

                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var contractInfo = profile.ContractInfoes.FirstOrDefault(x => x.ContractInfoID == contractInfoId);
                    var updateContractGroupInfo = contractInfo.ContractGroupInfoes.FirstOrDefault(x => x.ContractGroupInfoId == contractGroupInfo.ContractGroupInfoId);
                    updateContractGroupInfo = AutoMapper.Mapper.Map<ContractGroupInfo, ContractGroupInfo>(contractGroupInfo, updateContractGroupInfo);
                    Update(profile);
                }

            }

            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_UPDATE_EXCEPTION, ex);
            }
        }

        public void AddContractGroupInformationHistory(int profileId, int contractInfoId, int contractGroupInfoId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    ContractGroupInfoHistory addContractGroupInfoHistory = new ContractGroupInfoHistory();
                    var contractInfo = profile.ContractInfoes.FirstOrDefault(x => x.ContractInfoID == contractInfoId);
                    var updateContractGroupInfo = contractInfo.ContractGroupInfoes.FirstOrDefault(h => h.ContractGroupInfoId == contractGroupInfoId);
                    addContractGroupInfoHistory = AutoMapper.Mapper.Map<ContractGroupInfo, ContractGroupInfoHistory>(updateContractGroupInfo, addContractGroupInfoHistory);
                    addContractGroupInfoHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updateContractGroupInfo.ContractGroupInfoHistory == null)
                    {
                        updateContractGroupInfo.ContractGroupInfoHistory = new List<ContractGroupInfoHistory>();
                    }
                    updateContractGroupInfo.ContractGroupInfoHistory.Add(addContractGroupInfoHistory);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_HISTORY_ADD_EXCEPTION, ex);
            }
        }

        public void RemoveContractGroupInformation(int profileId, int contractInfoId, ContractGroupInfo contractGroupInfo)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var contractInfo = profile.ContractInfoes.FirstOrDefault(x => x.ContractInfoID == contractInfoId);
                    var removeContractGroupInfo = contractInfo.ContractGroupInfoes.FirstOrDefault(h => h.ContractGroupInfoId == contractGroupInfo.ContractGroupInfoId);
                    removeContractGroupInfo.StatusType = contractGroupInfo.StatusType;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.CONTRACT_GROUP_INFORMATION_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Document Repository

        public void AddOtherDocument(int profileId, Entities.DocumentRepository.OtherDocument otherDocument)
        {
            try
            {
                Profile profile = FindProfile(profileId);

                if (IsProviderExists(profileId, profile))
                {
                    if (profile.OtherDocuments == null)
                        profile.OtherDocuments = new List<OtherDocument>();

                    profile.OtherDocuments.Add(otherDocument);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_DOCUMENT_CREATE_EXCEPTION, ex);
            }
        }

        public void UpdateOtherDocument(int profileId, OtherDocument otherDocument)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var updateOtherDocument = profile.OtherDocuments.Where(d => d.OtherDocumentID == otherDocument.OtherDocumentID).FirstOrDefault();
                    updateOtherDocument = AutoMapper.Mapper.Map<OtherDocument, OtherDocument>(otherDocument, updateOtherDocument);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_DOCUMENT_UPDATE_EXCEPTION, ex);
            }
        }

        public void RemoveOtherDocument(int profileId, OtherDocument otherDocument)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    var removeOtherDocummentInfo = profile.OtherDocuments.FirstOrDefault(d => d.OtherDocumentID == otherDocument.OtherDocumentID);
                    removeOtherDocummentInfo.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_DOUCUMENT_REMOVE_EXCEPTION, ex);
            }
        }

        public void AddOtherDocumentHistory(int profileId, int OtherDocumentId)
        {
            try
            {
                Profile profile = FindProfile(profileId);
                if (IsProviderExists(profileId, profile))
                {
                    OtherDocumentHistory addOtherDocumentHistory = new OtherDocumentHistory();
                     
                    var updateOtherDocument = profile.OtherDocuments.FirstOrDefault(a => a.OtherDocumentID == OtherDocumentId);
                    addOtherDocumentHistory.Title = updateOtherDocument.Title;
                    addOtherDocumentHistory.DocumentPath = updateOtherDocument.DocumentPath;
                    addOtherDocumentHistory.ModifiedBy = updateOtherDocument.ModifiedBy;
                    addOtherDocumentHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    updateOtherDocument.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    //addOtherDocumentHistory = AutoMapper.Mapper.Map<OtherDocument, OtherDocumentHistory>(updateOtherDocument, addOtherDocumentHistory);
                    
                    if (updateOtherDocument.OtherDocumentHistory == null)
                    {
                        updateOtherDocument.OtherDocumentHistory = new List<OtherDocumentHistory>();
                    }
                    updateOtherDocument.OtherDocumentHistory.Add(addOtherDocumentHistory);
                    Update(profile);
                }
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.OTHER_DOCUMENT_HISTORY_CREATE_EXCEPTION, ex);
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

