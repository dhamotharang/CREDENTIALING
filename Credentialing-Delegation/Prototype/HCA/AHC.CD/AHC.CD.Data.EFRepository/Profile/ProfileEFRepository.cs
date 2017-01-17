using AHC.CD.Data.Repository.Profile;
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

namespace AHC.CD.Data.EFRepository.Profile
{
    internal class ProfileEFRepository : EFGenericRepository<IndividualProvider>,  IProfileRepository
    {
        /// <summary>
        /// Finds Individual Provider
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="hospitalPrivilegeInformation"></param>
        /// <returns>Provider</returns>

        private IndividualProvider FindProvider(int individualProviderID)
        {
            IndividualProvider provider = DbSet.Find(individualProviderID);
            return provider;
        }


        /// <summary>
        /// Verify whether provider is exists
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="provider"></param>
        /// <returns>true or flase</returns>
        private static bool IsProviderExists(int individualProviderID, IndividualProvider provider)
        {
            if (provider == null)
            {
                throw new IndividualProviderNotFoundException(ExceptionMessage.PROVIDER_NOT_FOUND + " " + individualProviderID);
            }
            return true;
        }

        #region Hospital Privilege

        /// <summary>
        /// Add Hospital Privilege to existing Individual Provider
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="hospitalPrivilegeInformation"></param>
        /// <returns>HospitalPrivilege ID</returns>
        public async Task<int> AddHospitalPrivilegeAsync(int individualProviderID, Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation hospitalPrivilegeInformation)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                if (provider.HospitalPrivilegeInformations == null)
                    provider.HospitalPrivilegeInformations = new List<HospitalPrivilegeInformation>();
                provider.HospitalPrivilegeInformations.Add(hospitalPrivilegeInformation);
                await SaveAsync();
            }
                return hospitalPrivilegeInformation.HospitalPrivilegeInformationID;
            
        }


        /// <summary>
        /// Update Hospital Privilege information for a given Individual Provider
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="hospitalPrivilegeInformation"></param>
        public async Task UpdateHospitalPrivilegeAsync(int individualProviderID, HospitalPrivilegeInformation hospitalPrivilegeInformation)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID,provider))
            {
                var updatableHospitalPrivilegeInfo = provider.HospitalPrivilegeInformations.Where(h => h.HospitalPrivilegeInformationID == hospitalPrivilegeInformation.HospitalPrivilegeInformationID).FirstOrDefault();
                
                updatableHospitalPrivilegeInfo = AutoMapper.Mapper.Map<HospitalPrivilegeInformation>(hospitalPrivilegeInformation);
              
                Update(provider);
                await SaveAsync();
                
            }
        }

        #endregion

        # region Ethnicity

        /// <summary>
        /// Add's Birth Information
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="birthInformation"></param>
        /// <returns>BirthInformationId</returns>

        public async Task<int> AddBirthInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.BirthInformation birthInformation)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                //if (provider.BirthInformation == null)
                //    provider.BirthInformation = new BirthInformation();
                 
                provider.BirthInformation = birthInformation;
                await SaveAsync();
            }
            return birthInformation.BirthInformationID;
        }


        /// <summary>
        /// Updates BirthInformation
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="birthInformation"></param>
        /// <returns>void</returns>

        public async Task UpdateBirthInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.BirthInformation birthInformation)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateBirthInformation = provider.BirthInformation;

                updateBirthInformation = AutoMapper.Mapper.Map<BirthInformation>(birthInformation);

                Update(provider);
                await SaveAsync();

            }
        }

        /// <summary>
        /// Add's Visa Information
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="visaInformation"></param>
        /// <returns>VisaDetailId</returns>

        public async Task<int> AddVisaInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.VisaDetail visaInformation)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                //if (provider.VisaDetail == null)

                    provider.VisaDetail = visaInformation;
                await SaveAsync();
            }
            return visaInformation.VisaDetailID;
        }

        /// <summary>
        /// Updates Visa Information
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="visaInformation"></param>
        /// <returns>void</returns>

        public async Task UpdateVisaInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.VisaDetail visaInformation)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateVisaInformation = provider.VisaDetail;

                updateVisaInformation = AutoMapper.Mapper.Map<VisaDetail>(visaInformation);

                Update(provider);
                await SaveAsync();
            }
        }

        /// <summary>
        /// Add's Language Information
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="LanguageInformation"></param>
        /// <returns>LanguageInformationId</returns>
        public async Task<int> AddLanguageInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.LanguageInfo languageInformation)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                //if (provider.LanguageInfo == null)

                    provider.LanguageInfo = languageInformation;
                await SaveAsync();
            }
            return languageInformation.LanguageInfoID;
        }

        /// <summary>
        /// Updates Language Information
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="languageInformation"></param>
        /// <returns>void</returns>

        public async Task UpdateLanguageInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.LanguageInfo languageInformation)
        {


            IndividualProvider provider = FindProvider(individualProviderID);

            if (IsProviderExists(individualProviderID, provider))
            {
                var updateLanguageInformation = provider.LanguageInfo;

                updateLanguageInformation = AutoMapper.Mapper.Map<LanguageInfo>(languageInformation);

                Update(provider);
                await SaveAsync();
            }

        }


      #endregion


        #region Demographics

        /// <summary>
        /// Add's Personal Details
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="personalDetail"></param>
        /// <returns>PersonalDetailId</returns>

        public async Task<int> AddPersonalDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalDetail personalDetail)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                //if (provider.PersonalDetail == null)

                    provider.PersonalDetail = personalDetail;
                await SaveAsync();
            }
            return personalDetail.PersonalDetailID;
        }

        /// <summary>
        /// Updates Personal Detail
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="personalDetail"></param>
        /// <returns>void</returns>

        public async Task UpdatePersonalDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalDetail personalDetail)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updatePersonalDetails = provider.PersonalDetail;

                updatePersonalDetails = AutoMapper.Mapper.Map<PersonalDetail>(personalDetail);

                Update(provider);
                await SaveAsync();

            }
        }

        /// <summary>
        /// Add's Contact Details
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="contactDetail"></param>
        /// <returns>ContactDetailId</returns>
        public async Task<int> AddContactDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.ContactDetail contactDetail)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                //if (provider.ContactDetail == null)

                    provider.ContactDetail = contactDetail;
                await SaveAsync();
            }
            return contactDetail.ContactDetailID;
        }

        /// <summary>
        /// Updates Contact Detail
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="hospitalPrivil"></param>
        /// <returns>Provider</returns>
        public async Task UpdateContactDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.ContactDetail contactDetail)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateContactDetails = provider.ContactDetail;

                updateContactDetails = AutoMapper.Mapper.Map<ContactDetail>(contactDetail);

                Update(provider);
                await SaveAsync();

            }
        }

        public async Task<int> AddPersonalIdentificationAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalIdentification personalIdentification)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                //if (provider.PersonalIdentification == null)

                    provider.PersonalIdentification = personalIdentification;
                await SaveAsync();
            }
            return personalIdentification.PersonalIdentificationID;
        }

        public async Task UpdatePersonalIdentificationAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalIdentification personalIdentification)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updatePersonalIdentification = provider.PersonalIdentification;

                updatePersonalIdentification = AutoMapper.Mapper.Map<PersonalIdentification>(personalIdentification);

                Update(provider);
                await SaveAsync();

            }
        }


        public async Task<int> AddOtherLegalNamesAsync(int individualProviderID, Entities.MasterProfile.Demographics.OtherLegalName otherLegalName)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                if (provider.OtherLegalNames == null)
                    provider.OtherLegalNames = new List<OtherLegalName>();
                provider.OtherLegalNames.Add(otherLegalName);
                await SaveAsync();
            }
            return otherLegalName.OtherLegalNameID;
        }

        public async Task UpdateOtherLegalNamesAsync(int individualProviderID, Entities.MasterProfile.Demographics.OtherLegalName otherLegalName)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateOtherLegalNames = provider.OtherLegalNames.Where(h => h.OtherLegalNameID == otherLegalName.OtherLegalNameID).FirstOrDefault();

                updateOtherLegalNames = AutoMapper.Mapper.Map<OtherLegalName>(otherLegalName);

                Update(provider);
                await SaveAsync();

            }
        }

        public async Task<int> AddHomeAddressAsync(int individualProviderID, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                if (provider.HomeAddresses == null)
                    provider.HomeAddresses = new List<HomeAddress>();
                provider.HomeAddresses.Add(homeAddress);
                await SaveAsync();
            }
            return homeAddress.HomeAddressID;
        }

        public async Task UpdateHomeAddressAsync(int individualProviderID, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {

            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateHomeAddress = provider.HomeAddresses.Where(h => h.HomeAddressID == homeAddress.HomeAddressID).FirstOrDefault();

                updateHomeAddress = AutoMapper.Mapper.Map<HomeAddress>(homeAddress);

                Update(provider);
                await SaveAsync();

            }
        }
        #endregion

        #region Professional Affiliation

        public async Task<int> AddProfessionalAffiliationAsync(int individualProviderID, Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo professionalAffiliation)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                if (provider.ProfessionalAffiliationInfos == null)
                    provider.ProfessionalAffiliationInfos = new List<ProfessionalAffiliationInfo>();
                provider.ProfessionalAffiliationInfos.Add(professionalAffiliation);
                await SaveAsync();
            }
            return professionalAffiliation.ProfessionalAffiliationInfoID;
        }

        public async Task UpdateProfessionalAffiliationAsync(int individualProviderID, Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo professionalAffiliation)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateProfessionalAffiliation = provider.ProfessionalAffiliationInfos.Where(h => h.ProfessionalAffiliationInfoID == professionalAffiliation.ProfessionalAffiliationInfoID).FirstOrDefault();

                updateProfessionalAffiliation = AutoMapper.Mapper.Map<ProfessionalAffiliationInfo>(professionalAffiliation);

                Update(provider);
                await SaveAsync();

            }
        }

        #endregion

        #region Work History

        public async Task<int> AddProfessionalWorkExperienceAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience professionalWorkExperience)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                if (provider.ProfessionalWorkExperiences == null)
                    provider.ProfessionalWorkExperiences = new List<ProfessionalWorkExperience>();
                provider.ProfessionalWorkExperiences.Add(professionalWorkExperience);
                await SaveAsync();
            }
            return professionalWorkExperience.ProfessionalWorkExperienceID;   
        }

        public async Task UpdateProfessionalWorkExperienceAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience professionalWorkExperience)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateProfessionalWorkExperience = provider.ProfessionalWorkExperiences.Where(h => h.ProfessionalWorkExperienceID == professionalWorkExperience.ProfessionalWorkExperienceID).FirstOrDefault();

                updateProfessionalWorkExperience = AutoMapper.Mapper.Map<ProfessionalWorkExperience>(professionalWorkExperience);

                Update(provider);
                await SaveAsync();

            }
        }

        public async Task<int> AddWorkGapAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.WorkGap workGap)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                if (provider.WorkGaps == null)
                    provider.WorkGaps = new List<WorkGap>();
                provider.WorkGaps.Add(workGap);
                await SaveAsync();
            }
            return   workGap.WorkGapID; 
        }

        public async Task UpdateWorkGapAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.WorkGap workGap)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                var updateWorkGap = provider.WorkGaps.Where(h => h.WorkGapID == workGap.WorkGapID).FirstOrDefault();

                updateWorkGap = AutoMapper.Mapper.Map<WorkGap>(workGap);

                Update(provider);
                await SaveAsync();

            }
        }

        #endregion


        public async Task<int> AddProfessionalReferenceAsync(int individualProviderID, Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo professionalReference)
        {
            IndividualProvider provider = FindProvider(individualProviderID);
            if (IsProviderExists(individualProviderID, provider))
            {
                if (provider.ProfessionalRefereneInfos == null)
                    provider.ProfessionalRefereneInfos = new List<ProfessionalReferenceInfo>();
                provider.ProfessionalRefereneInfos.Add(professionalReference);
                await SaveAsync();
            }
            return professionalReference.ProfessionalReferenceInfoID;
        }

        public async Task UpdateProfessionalReferencesAsync(int individualProviderID, Entities.MasterProfile.ProfessionalReference.ProfessionalReferenceInfo professionalReference)
        {
            IndividualProvider provider = FindProvider(individualProviderID);

            var updateProfessionalReference = provider.ProfessionalRefereneInfos.Where(h => h.ProfessionalReferenceInfoID == professionalReference.ProfessionalReferenceInfoID).FirstOrDefault();

            updateProfessionalReference = AutoMapper.Mapper.Map<ProfessionalReferenceInfo>(professionalReference);

            Update(provider);
            await SaveAsync();
        }
    }
}

        