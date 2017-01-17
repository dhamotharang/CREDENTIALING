using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profile;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
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
        IContactDetailRepository contactDetailRepositry = null;
       
        public ProfileManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.contactDetailRepositry = uow.GetContactDetailRepository();
           
        }

        #region Business Rules

        private bool IsHospitalExists(int providerID, string hospitalName)
        {
            //return profileRepository.Find(providerID).HospitalPrivilegeInformations.Any(h => h.HospitalName.ToLower().Equals(hospitalName.ToLower()));

            return true;
        }


        /// <summary>
        /// Checks for duplicacy of emailId for an individual provider
        /// </summary>
        /// <param name="providerID"></param>
        /// <param name="emailId"></param>
        /// <returns>true or false</returns>
        //private bool IsEmailExist(int providerID, List<string> emailIds)
        //{

        //    //return profileRepository.GetAll("ContactDetail").Any(p => p.ContactDetail.EmailIDs.Intersect(emailIds).Count() != 0);

        //    contactDetailRepositry.GetAll().Where(p => p.EmailIDs.Where(q => emailIds.Any(m => m.Equals(q))));
        //}

       

        #endregion


        #region Hospital Privilege

        /// <summary>
        /// Update Hospital Privilege information for a given Individual Provider
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="hospitalPrivilegeInformation"></param>
        public async Task UpdateHospitalPrivilegeAsync(int individualProviderID, Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation hospitalPrivilegeInformation)
        {
            try
            {
                await profileRepository.UpdateHospitalPrivilegeAsync(individualProviderID, hospitalPrivilegeInformation);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Add Hospital Privilege to existing Individual Provider
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="hospitalPrivilegeInformation"></param>
        /// <returns>HospitalPrivilege ID</returns>
       
        public async Task<int> AddHospitalPrivilegeAsync(int individualProviderID, Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation hospitalPrivilegeInformation)
        {
            try
            {
                // Hospital Names should not be duplicated
                //if (!IsHospitalExists(individualProviderID, hospitalPrivilegeInformation.HospitalName))
                //    return await profileRepository.AddHospitalPrivilegeAsync(individualProviderID, hospitalPrivilegeInformation);
                //else
                //    throw new HospitalAlreadyAddedException(hospitalPrivilegeInformation.HospitalName + " " + ExceptionMessage.HOSPITAL_EXISTS);

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

     

        #endregion

        #region Demographics


        /// <summary>
        /// Add Personal Details for a given Individual Provider
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="personalDetail"></param>
       /// <returns>Personal Detail ID</returns>
        public async Task<int> AddPersonalDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalDetail personalDetail)
        {
            return await profileRepository.AddPersonalDetailsAsync(individualProviderID, personalDetail);
        }



        public  async Task UpdatePersonalDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalDetail personalDetail)
        {
             await profileRepository.UpdatePersonalDetailsAsync(individualProviderID, personalDetail);
        }

        public async Task<int> AddOtherLegalNamesAsync(int individualProviderID, Entities.MasterProfile.Demographics.OtherLegalName otherLegalName)
        {
            return await profileRepository.AddOtherLegalNamesAsync(individualProviderID, otherLegalName);
        }

        public async Task UpdateOtherLegalNamesAsync(int individualProviderID, Entities.MasterProfile.Demographics.OtherLegalName otherLegalName)
        {
             await profileRepository.UpdateOtherLegalNamesAsync(individualProviderID, otherLegalName);
        }

        public async Task<int> AddHomeAddressAsync(int individualProviderID, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {
            return await profileRepository.AddHomeAddressAsync(individualProviderID, homeAddress);
        }



        public async Task UpdateHomeAddressAsync(int individualProviderID, Entities.MasterProfile.Demographics.HomeAddress homeAddress)
        {
            await profileRepository.UpdateHomeAddressAsync(individualProviderID, homeAddress);
        }

       


        /// <summary>
        /// Add's Contact details of a provider
        /// </summary>
        /// <param name="individualProviderID"></param>
        /// <param name="contactDetail"></param>
        /// <returns>ContactDetailId</returns>
        public async Task<int> AddContactDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.ContactDetail contactDetail)
        {
            return await profileRepository.AddContactDetailsAsync(individualProviderID, contactDetail);
        }


        public async Task UpdateContactDetailsAsync(int individualProviderID, Entities.MasterProfile.Demographics.ContactDetail contactDetail)
        {
           await profileRepository.UpdateContactDetailsAsync(individualProviderID, contactDetail);
        }



        public async Task<int> AddPersonalIdentificationAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalIdentification personalIdentification)
        {
            try
            {
                return await profileRepository.AddPersonalIdentificationAsync(individualProviderID, personalIdentification);
            }

            catch (Exception) {

                throw new SSNAlreadyExistsException(personalIdentification.SSN + " " + ExceptionMessage.SSN_EXISTS);
            }
        }

        public async Task UpdatePersonalIdentificationAsync(int individualProviderID, Entities.MasterProfile.Demographics.PersonalIdentification personalIdentification)
        {
           await profileRepository.UpdatePersonalIdentificationAsync(individualProviderID, personalIdentification);
        }

        #endregion

        #region Ethnicity




        public async Task<int> AddBirthInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.BirthInformation birthInformation)
        {
            return await profileRepository.AddBirthInformationAsync(individualProviderID, birthInformation);
        }

        //public bool SaveBirthCertificate(int individualProviderID, DocumentDTO documentDTO)
        //{
        //    documentDTO.DocumentFolder = documentRootLocator.GetDocumentRootFolder() + "\\" + "Documents\\BirthCertificates\\";

        //    return documentManager.SaveDocument(documentDTO);
        //}

        public async Task UpdateBirthInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.BirthInformation birthInformation)
        {
             await profileRepository.UpdateBirthInformationAsync(individualProviderID, birthInformation);
        }

        public async Task<int> AddVisaInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.VisaDetail visaDetail)
        {
            return await profileRepository.AddVisaInformationAsync(individualProviderID, visaDetail);
        }

        public async Task UpdateVisaInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.VisaDetail visaDetail)
        {
             await profileRepository.UpdateVisaInformationAsync(individualProviderID,visaDetail);
        }

        public async Task<int> AddLanguageInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.LanguageInfo languageInformation)
        {
            return await profileRepository.AddLanguageInformationAsync(individualProviderID,languageInformation);
        }

        public async Task UpdateLanguageInformationAsync(int individualProviderID, Entities.MasterProfile.Demographics.LanguageInfo languageInformation)
        {
             await profileRepository.UpdateLanguageInformationAsync(individualProviderID, languageInformation);
        }

        #endregion


        #region Professional Affilaition
        public async Task<int> AddProfessionalAffiliationAsync(int individualProviderID, Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo professionalAffiliation)
        {
            return await profileRepository.AddProfessionalAffiliationAsync(individualProviderID, professionalAffiliation);
        }

        public async Task UpdateProfessionalAffiliationAsync(int individualProviderID, Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo professionalAffiliation)
        {
             await profileRepository.UpdateProfessionalAffiliationAsync(individualProviderID, professionalAffiliation);
        }

        #endregion

        #region Work History

        public  async Task<int> AddProfessionalWorkExperienceAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience professionalWorkExperience)
        {
            return await profileRepository.AddProfessionalWorkExperienceAsync(individualProviderID, professionalWorkExperience);
        }

        public async Task UpdateProfessionalWorkExperienceAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.ProfessionalWorkExperience professionalWorkExperience)
        {
             await profileRepository.UpdateProfessionalWorkExperienceAsync(individualProviderID, professionalWorkExperience);
        }


        public async Task<int> AddWorkGapAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.WorkGap workGap)
        {
            return await profileRepository.AddWorkGapAsync(individualProviderID, workGap);
        }

        public async Task UpdateWorkGapAsync(int individualProviderID, Entities.MasterProfile.WorkHistory.WorkGap workGap)
        {
           await profileRepository.UpdateWorkGapAsync(individualProviderID, workGap);
        }

        #endregion


        public async Task<IndividualProvider> GetProviderByIdAsync(int individualProviderID)
        {
            return await profileRepository.FindAsync(individualProviderID);
        }


        public Task<IndividualProvider> GetProviderById(int individualProviderID)
        {
            throw new NotImplementedException();
        }
    }
}
