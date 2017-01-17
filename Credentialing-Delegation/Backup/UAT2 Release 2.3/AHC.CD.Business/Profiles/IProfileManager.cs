using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.DTO;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    public interface IProfileManager
    {
        string GetProfileStatus(int profileID);

        #region Get Profiles

        Task<IEnumerable<Profile>> GetAllActiveAsync();
        Task<IEnumerable<Profile>> GetAllAync();
        Task<Profile> GetByIdAsync(int profileId);
        Task<IEnumerable<Object>> GetAllProfileForOperationAsync();
        Task<bool> ProfileExistAsync(int profileId);
        Task<IEnumerable<Profile>> GetAllProfileByProviderLevel(string providerLevel, int profileId);
        Task<string> GetAllProviderLevelByProfileId(int profileID);

        #endregion

        #region Deactivate Profile

        Task DeactivateProfile(int profileID);

        #endregion

        #region Reactivate Profile

        Task ReactivateProfile(int profileID);

        #endregion

        #region Demographics

        Task<string> UpdateProfileImageAsync(int profileId, DocumentDTO document);
        Task RemoveProfileImageAsync(int profileId, string imagePath);
        Task UpdatePersonalDetailAsync(int profileId, PersonalDetail personalDetail);
        Task<int> AddOtherLegalNamesAsync(int profileId, OtherLegalName otherLegalName, DocumentDTO document);
        Task UpdateOtherLegalNamesAsync(int profileId, OtherLegalName otherLegalName, DocumentDTO document);
        Task RemoveOtherLegalNameAsync(int profileId, OtherLegalName otherLegalName);
        Task<int> AddHomeAddressAsync(int profileId, HomeAddress homeAddress);
        Task UpdateHomeAddressAsync(int profileId, HomeAddress homeAddress);
        Task RemoveHomeAddressAsync(int profileId, HomeAddress homeAddress);
        Task UpdateContactDetailAsync(int profileId, ContactDetail contactDetail);
        Task UpdatePersonalIdentificationAsync(int profileId, PersonalIdentification personalIdentification, DocumentDTO dlDocument, DocumentDTO ssnDocument);
        Task UpdateBirthInformationAsync(int profileId, BirthInformation birthInformation, DocumentDTO document);
        Task UpdateVisaInformationAsync(int profileId, VisaDetail visaDetail, DocumentDTO visaDocument, DocumentDTO greenCarddocument, DocumentDTO nationalIDdocument);
        Task UpdateLanguageInformationAsync(int profileId, LanguageInfo languageInformation);
        //Task RemoveOtherLegalNameDocument(int otherLegalNameID, string documentFullPath);



        #endregion

        #region Identification & License

        Task<int> AddStateLicenseAsync(int profileId, StateLicenseInformation stateLicense, DocumentDTO document);
        Task UpdateStateLicenseAsync(int profileId, StateLicenseInformation stateLicense, DocumentDTO document);
        Task RenewStateLicenseAsync(int profileId, StateLicenseInformation stateLicense, DocumentDTO document);
        Task RemoveStateLicenseAsync(int profileId, StateLicenseInformation stateLicense);

        Task<int> AddFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense, DocumentDTO document);
        Task UpdateFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense, DocumentDTO document);
        Task RenewFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense, DocumentDTO document);
        Task<FederalDEAInformation> RemoveFederalDEALicenseAsync(int profileId, FederalDEAInformation federalDEALicense);

        Task<int> AddCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense, DocumentDTO document);
        Task UpdateCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense, DocumentDTO document);
        Task RenewCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense, DocumentDTO document);
        Task RemoveCDSCLicenseAsync(int profileId, CDSCInformation cdscLicense);

        Task<int> AddMedicareInformationAsync(int profileId, MedicareInformation medicareInformation, DocumentDTO document);
        Task UpdateMedicareInformationAsync(int profileId, MedicareInformation medicareInformation, DocumentDTO document);
        Task RemoveMedicareInformationAsync(int profileId, MedicareInformation medicareInformation);

        Task<int> AddMedicaidInformationAsync(int profileId, MedicaidInformation medicaidInformation, DocumentDTO document);
        Task UpdateMedicaidInformationAsync(int profileId, MedicaidInformation medicaidInformation, DocumentDTO document);
        Task RemoveMedicaidInformationAsync(int profileId, MedicaidInformation medicaidInformation);

        Task UpdateOtherIdentificationNumberAsync(int profileId, OtherIdentificationNumber otherIdentificationNumber);

        #endregion

        #region Specialty/Board

        Task<int> AddSpecialtyDetailAsync(int profileId, SpecialtyDetail specialtyDetail, DocumentDTO document);
        Task<SpecialtyDetail> UpdateSpecialtyDetailAsync(int profileId, SpecialtyDetail specialtyDetail, DocumentDTO document);
        Task RenewSpecialtyDetailAsync(int profileId, SpecialtyDetail specialtyDetail, DocumentDTO document);
        Task RemoveSpecialityDetailAsync(int profileId, SpecialtyDetail specialtyDetail);

        Task UpdatePracticeInterestAsync(int profileId, PracticeInterest practiceInterest);

        #endregion

        #region Hospital Privilege

        Task UpdateHospitalPrivilegeInformationAsync(int profileId, HospitalPrivilegeInformation hospitalPrivilegeInformation);
        Task<int> AddHospitalPrivilegeDetailAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail, DocumentDTO document);
        Task UpdateHospitalPrivilegeDetailAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail, DocumentDTO document);
        Task RenewHospitalPrivilegeDetailAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail, DocumentDTO document);
        Task RemoveHospitalPrivilegeAsync(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail);

        #endregion

        #region Professional Liability

        Task<int> AddProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo, DocumentDTO document);
        Task UpdateProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo, DocumentDTO document);
        Task RenewProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo, DocumentDTO document);
        Task RemoveProfessionalLiabilityAsync(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo);

        #endregion

        #region Professional Affiliation

        Task<int> AddProfessionalAffiliationAsync(int profileId, ProfessionalAffiliationInfo professionalAffiliation);
        Task UpdateProfessionalAffiliationAsync(int profileId, ProfessionalAffiliationInfo professionalAffiliation);
        Task RemoveProfessionalAffiliationAsync(int profileId, ProfessionalAffiliationInfo professionalAffiliation);

        #endregion

        #region Professional Reference

        Task<int> AddProfessionalReferenceAsync(int profileId, ProfessionalReferenceInfo professionalReference);
        Task UpdateProfessionalReferenceAsync(int profileId, ProfessionalReferenceInfo professionalReference);
        Task SetProfessionalReferenceStatusAsync(int profileId, int professionalReferenceId, StatusType status);
        Task RemoveProfessionalReferenceAsync(int profileId, ProfessionalReferenceInfo professionalReference);

        #endregion

        #region CV Upload

        Task<int> AddCVAsync(int profileId, CVInformation cvInformation, DocumentDTO document);
        Task UpdateCVAsync(int profileId, CVInformation cvInformation, DocumentDTO document);
        Task RemoveCVAsync(int profileId, CVInformation cvInformation);

        #endregion

        #region Work History

        Task<int> AddProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperience professionalWorkExperience, DocumentDTO document);
        Task UpdateProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperience professionalWorkExperience, DocumentDTO document);
        Task RemoveProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperience professionalWorkExperience);

        Task<int> AddMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformation militaryServiceInformation);
        Task UpdateMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformation militaryServiceInformation);
        Task RemoveMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformation militaryServiceInformation);

        Task<int> AddPublicHealthServiceAsync(int profileId, PublicHealthService publicHealthService);
        Task UpdatePublicHealthServiceAsync(int profileId, PublicHealthService publicHealthService);
        Task RemovePublicHealthServiceAsync(int profileId, PublicHealthService publicHealthService);

        Task<int> AddWorkGapAsync(int profileId, WorkGap workGap);
        Task UpdateWorkGapAsync(int profileId, WorkGap workGap);
        Task RemoveWorkGapAsync(int profileId, WorkGap workGap);

        #endregion

        #region Education Hsitory

        Task<int> AddEducationDetailAsync(int profileId, EducationDetail educationDetail, DocumentDTO graduateDocument);
        Task UpdateEducationDetailAsync(int profileId, EducationDetail educationDetail, DocumentDTO graduateDocument);
        Task RemoveEducationDetailAsync(int profileId, EducationDetail educationDetail);

        Task<int> AddTrainingDetailAsync(int profileId, TrainingDetail trainingDetail, IList<DocumentDTO> documents);
        Task UpdateTrainingDetailAsync(int profileId, TrainingDetail trainingDetail);

        Task<int> AddResidencyInternshipDetailAsync(int profileId, int trainingId, ResidencyInternshipDetail residencyInternshipDetail, DocumentDTO document);
        Task UpdateResidencyInternshipDetailAsync(int profileId, int trainingId, ResidencyInternshipDetail residencyInternshipDetail, DocumentDTO document);

        Task<int> AddProgramDetailAsync(int profileId, ProgramDetail programDetail, DocumentDTO document);
        Task UpdateProgramDetailAsync(int profileId, ProgramDetail programDetail, DocumentDTO document);
        Task RemoveProgramDetailAsync(int profileId, ProgramDetail programDetail);

        Task<int> AddCMECertificationAsync(int profileId, CMECertification cmeCertification, DocumentDTO document);
        Task UpdateCMECertificationAsync(int profileId, CMECertification cmeCertification, DocumentDTO document);
        Task RemoveCertificationDetailAsync(int profileId, CMECertification cmeCertification);

        Task UpdateECFMGDetailAsync(int profileId, ECFMGDetail ecfmgDetail, DocumentDTO document);

        #endregion

        #region Disclosure Questions Answers

        Task<int> AddEditDisclosureQuestionAnswersAsync(int profileId, AHC.CD.Entities.MasterProfile.DisclosureQuestions.ProfileDisclosure disclosureQuestionAnswers);

        #endregion

        #region Contract Information

        Task<int> AddContractInformationAsync(int profileId, ContractInfo contractInfo, DocumentDTO document);
        Task UpdateContractInformationAsync(int profileId, ContractInfo contractInfo, DocumentDTO document);
        Task UpdateContractGroupInformationAsync(int profileId, int contractInfoId, ContractGroupInfo contractGroupInfo);
        Task AddContractGroupInformationAsync(int profileId, int contractInfoId, ContractGroupInfo contractGroupInfo);
        Task RemoveContractGroupInformationAsync(int profileId, int contractInfoId, ContractGroupInfo contractGroupInfo);

        #endregion

        #region DocumentRepository

        Task<int> AddOtherDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument dataModelOtherDocument, DocumentDTO document);

        Task UpdateOtherDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument dataModelOtherDocument, DocumentDTO document);

        Task RemoveOtherDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument dataModelOtherDocument);

        int GetCredentialingUserId(string UserAuthId);

        #endregion

        int? GetProfileIdFromAuthId(string UserAuthId);

        int GetCDUserIdFromAuthId(string UserAuthId);

        //#region Practice Location

        //Task<int> AddPracticeLocationAsync(int profileId, PracticeLocationDetail practiceLocationDetail);

        //Task UpdateWorkersCompensationInformationAsync(int profileId, WorkersCompensationInformation workersCompensationInformation);

        //Task UpdateOfficeManagerAsync(int profileId, BusinessOfficeContactPerson officemanager);

        //Task UpdateOpenPracticeStatusAsync(int profileId, OpenPracticeStatus openPracticeStatus);

        //Task UpdateAccessibilitiesAsync(int profileId, PracticeAccessibility practiceAccessibility);

        //#endregion

        // temp action . should be removed while production

        Task SaveProfileTemp(Profile profile);

        #region Get Profiles as Section Wise

        Task<object> GetDemographicsProfileDataAsync(int profileId);

        Task<object> GetIdentificationAndLicensesProfileDataAsync(int profileId);

        Task<object> GetBoardSpecialtiesProfileDataAsync(int profileId);

        Task<object> GetHospitalPrivilegesProfileDataAsync(int profileId);

        Task<object> GetProfessionalLiabilitiesProfileDataAsync(int profileId);

        Task<object> GetProfessionalReferencesProfileDataAsync(int profileId);

        Task<object> GetEducationHistoriesProfileDataAsync(int profileId);

        Task<object> GetWorkHistoriesProfileDataAsync(int profileId);

        Task<object> GetProfessionalAffiliationsProfileDataAsync(int profileId);

        Task<object> GetPracticeLocationsProfileDataAsync(int profileId);

        Task<object> GetDisclosureQuestionsProfileDataAsync(int profileId);

        Task<object> GetContractInfoProfileDataAsync(int profileId);

        Task<object> GetDocumentRepositoryDataAsync(int profileId, int CDUserId, bool isCCO);

        #endregion

        Task<object> GetMyNotification(int cdUserId);

        Task<object> AddProfileDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument dataModelOtherDocument, DocumentDTO document);

        Task<object> AddPlanDocumentAsync(int profileId, Entities.DocumentRepository.OtherDocument dataModelOtherDocument, DocumentDTO document);

    }
}
