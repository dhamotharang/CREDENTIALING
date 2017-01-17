using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.Profile
{
    public interface IProfileRepository : IGenericRepository<IndividualProvider>
    {
        #region Hospital Privileges

        Task<int> AddHospitalPrivilegeAsync(int individualProviderID, HospitalPrivilegeInformation hospitalPrivilegeInformation);
        Task UpdateHospitalPrivilegeAsync(int individualProviderID, HospitalPrivilegeInformation hospitalPrivilegeInformation);

        #endregion

        #region Demographics

        Task<int> AddPersonalDetailsAsync(int individualProviderID, PersonalDetail personalDetail);
        Task UpdatePersonalDetailsAsync(int individualProviderID, PersonalDetail personalDetail);

        Task<int> AddOtherLegalNamesAsync(int individualProviderID, OtherLegalName otherLegalName);
        Task UpdateOtherLegalNamesAsync(int individualProviderID, OtherLegalName otherLegalName);


        Task<int> AddHomeAddressAsync(int individualProviderID, HomeAddress homeAddress);
        Task UpdateHomeAddressAsync(int individualProviderID, HomeAddress homeAddress);

        Task<int> AddContactDetailsAsync(int individualProviderID, ContactDetail contactDetail);
        Task UpdateContactDetailsAsync(int individualProviderID, ContactDetail contactDetail);

        Task<int> AddPersonalIdentificationAsync(int individualProviderID, PersonalIdentification personalIdentification);
        Task UpdatePersonalIdentificationAsync(int individualProviderID, PersonalIdentification personalIdentification);

        #endregion

        #region Ethnicity
        Task<int> AddBirthInformationAsync(int individualProviderID, BirthInformation birthInformation);
        Task UpdateBirthInformationAsync(int individualProviderID,      BirthInformation  birthInformation);


        Task<int> AddVisaInformationAsync(int individualProviderID, VisaDetail visaInformation);
        Task UpdateVisaInformationAsync(int individualProviderID,  VisaDetail visaInformation);

        Task<int> AddLanguageInformationAsync(int individualProviderID, LanguageInfo languageInformation);
        Task UpdateLanguageInformationAsync(int individualProviderID,   LanguageInfo languageInformation);


         #endregion

        #region ProfessionalAffiliation

        Task<int> AddProfessionalAffiliationAsync(int individualProviderID, ProfessionalAffiliationInfo professionalAffiliation);
        Task UpdateProfessionalAffiliationAsync(int individualProviderID, ProfessionalAffiliationInfo professionalAffiliation);

        #endregion

        #region ProfessionalReferences

        Task<int> AddProfessionalReferenceAsync(int individualProviderID, ProfessionalReferenceInfo professionalReference);
        Task UpdateProfessionalReferencesAsync(int individualProviderID, ProfessionalReferenceInfo professionalReference);

        #endregion

        #region Work History

        Task<int> AddProfessionalWorkExperienceAsync(int individualProviderID, ProfessionalWorkExperience professionalWorkExperience);
        Task UpdateProfessionalWorkExperienceAsync(int individualProviderID, ProfessionalWorkExperience professionalWorkExperience);

        Task<int> AddWorkGapAsync(int individualProviderID, WorkGap workGap);
        Task UpdateWorkGapAsync(int individualProviderID, WorkGap workGap);

        #endregion

    }
}
