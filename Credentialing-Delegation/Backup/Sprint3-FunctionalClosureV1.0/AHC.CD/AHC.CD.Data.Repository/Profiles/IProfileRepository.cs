using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
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

namespace AHC.CD.Data.Repository.Profiles
{
    public interface IProfileRepository : IGenericRepository<Profile>
    {
        #region Demographics

        string UpdateProfileImage(int profileId, string imagePath);
        void UpdatePersonalDetail(int profileId, PersonalDetail personalDetail);

        void AddOtherLegalNames(int profileId, OtherLegalName otherLegalName);
        void UpdateOtherLegalNames(int profileId, OtherLegalName otherLegalName);

        void AddHomeAddress(int profileId, HomeAddress homeAddress);
        void UpdateHomeAddress(int profileId, HomeAddress homeAddress);
        void SetAllHomeAddressAsSecondary(int profileId);

        void UpdateContactDetail(int profileId, ContactDetail contactDetail);
        void UpdatePersonalIdentification(int profileId, PersonalIdentification personalIdentification);

        void UpdateBirthInformation(int profileId, BirthInformation birthInformation);
        void UpdateVisaInformation(int profileId, VisaDetail visaInformation);
        void AddVisaInformationHistory(int profileId);

        void UpdateLanguageInformation(int profileId, LanguageInfo languageInformation);

        #endregion

        #region Identification and License

        void AddStateLicense(int profileId, StateLicenseInformation stateLicense);
        void UpdateStateLicense(int profileId, StateLicenseInformation stateLicense);
        void AddStateLicenseHistory(int profileId, int stateLicenseId);
        void UpdateStateLicenseHistory(int profileId, int stateLicenseId, StateLicenseInfoHistory stateLicenseHistory);

        void AddFederalDEALicense(int profileId, FederalDEAInformation federalDEALicense);
        void UpdateFederalDEALicense(int profileId, FederalDEAInformation federalDEALicense);
        void AddFederalDEALicenseHistory(int profileId, int federalDEALicenseId);
        void UpdateFederalDEALicenseHistory(int profileId, int federalDEALicenseId, FederalDEAInfoHistory federalDEALicenseHistory);

        void AddCDSCLicense(int profileId, CDSCInformation cdscLicense);
        void UpdateCDSCLicense(int profileId, CDSCInformation cdscLicense);
        void AddCDSCLicenseHistory(int profileId, int cdscLicenseId);
        void UpdateCDSCLicenseHistory(int profileId, int cdscLicenseId, CDSCInfoHistory cdscLicenseHistory);

        void AddMedicareInformation(int profileId, MedicareInformation medicareInformation);
        void UpdateMedicareInformation(int profileId, MedicareInformation medicareInformation);

        void AddMedicaidInformation(int profileId, MedicaidInformation medicaidInformation);
        void UpdateMedicaidInformation(int profileId, MedicaidInformation medicaidInformation);

        void UpdateOtherIdentificationNumber(int profileId, OtherIdentificationNumber otherIdentificationNumber);
        
        #endregion

        #region Specialty/Board

        void AddSpecialtyDetail(int profileId, SpecialtyDetail specialtyDetail);
        void UpdateSpecialtyDetail(int profileId, SpecialtyDetail specialtyDetail);
        void AddSpecialtyBoardCertifiedDetailHistory(int profileId, int specialtyBoardCertifiedDetailId);
        void UpdateSpecialtyBoardCertifiedDetailHistory(int profileId, int specialtyBoardCertifiedDetailId, SpecialtyBoardCertifiedDetailHistory specialtyBoardCertifiedDetailHistory);

        void SetAllSpecialityAsSecondary(int profileId);
        void UpdatePracticeInterest(int profileId, PracticeInterest practiceInterest);

        #endregion

        #region Hospital Privileges

        void UpdateHospitalPrivilegeInformation(int profileId, HospitalPrivilegeInformation hospitalPrivilegeInformation);
        void AddHospitalPrivilegeDetail(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail);
        void UpdateHospitalPrivilegeDetail(int profileId, HospitalPrivilegeDetail hospitalPrivilegeDetail);
        void AddHospitalPrivilegeDetailHistory(int profileId, int hospitalPrivilegeDetailId);
        void UpdateHospitalPrivilegeDetailHistory(int profileId, int hospitalPrivilegeDetailId, HospitalPrivilegeDetailHistory hospitalPrivilegeDetailHistory);
        void AddHospitalPrivilegeInformationHistory(int profileId);

        void SetAllHospitalPrivilegeAsSecondary(int profileId);

        #endregion

        #region Professional Liability

        void AddProfessionalLiability(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo);
        void UpdateProfessionalLiability(int profileId, ProfessionalLiabilityInfo professionalLiabilityInfo);
        void AddProfessionalLiabilityHistory(int profileId, int professionalLiabilityInfoId);
        void UpdateProfessionalLiabilityHistory(int profileId, int professionalLiabilityInfoId, ProfessionalLiabilityInfoHistory professionalLiabilityInfoHistory);

        #endregion        

        #region ProfessionalAffiliation

        void AddProfessionalAffiliation(int profileId, ProfessionalAffiliationInfo professionalAffiliation);
        void UpdateProfessionalAffiliation(int profileId, ProfessionalAffiliationInfo professionalAffiliation);

        #endregion

        #region ProfessionalReferences

        void AddProfessionalReference(int profileId, ProfessionalReferenceInfo professionalReference);
        void UpdateProfessionalReference(int profileId, ProfessionalReferenceInfo professionalReference);
        void SetProfessionalReferenceStatus(int profileId, int professionalReferenceId, StatusType status);

        #endregion

        #region Work History

        void AddProfessionalWorkExperience(int profileId, ProfessionalWorkExperience professionalWorkExperience);
        void UpdateProfessionalWorkExperience(int profileId, ProfessionalWorkExperience professionalWorkExperience);

        void AddMilitaryServiceInformation(int profileId, MilitaryServiceInformation militaryServiceInformation);
        void UpdateMilitaryServiceInformation(int profileId, MilitaryServiceInformation militaryServiceInformation);

        void AddPublicHealthService(int profileId, PublicHealthService publicHealthService);
        void UpdatePublicHealthService(int profileId, PublicHealthService publicHealthService);

        void AddWorkGap(int profileId, WorkGap workGap);
        void UpdateWorkGap(int profileId, WorkGap workGap);

        #endregion

        #region Profile Document

        void AddDocument(int profileId, Entities.MasterProfile.ProfileDocument profileDocument);
        void UpdateDocument(int profileId, string previousDocPath, Entities.MasterProfile.ProfileDocument profileDocument);

        #endregion

        #region Education Hsitory

        void AddEducationDetail(int profileId, EducationDetail educationDetail);
        void UpdateEducationDetail(int profileId, EducationDetail educationDetail);

        void AddTrainingDetail(int profileId, TrainingDetail trainingDetail);
        void UpdateTrainingDetail(int profileId, TrainingDetail trainingDetail);

        void AddResidencyInternshipDetail(int profileId, int trainingId, ResidencyInternshipDetail residencyInternshipDetail);
        void UpdateResidencyInternshipDetail(int profileId, int trainingId, ResidencyInternshipDetail residencyInternshipDetail);
        void SetAllResidencyInternshipAsSecondary(int profileId, int trainingId);

        void AddCMECertification(int profileId, CMECertification cmeCertification);
        void UpdateCMECertification(int profileId, CMECertification cmeCertification);

        void UpdateECFMGDetail(int profileId, ECFMGDetail ecfmgDetail);

        #endregion   
        
    }
}
