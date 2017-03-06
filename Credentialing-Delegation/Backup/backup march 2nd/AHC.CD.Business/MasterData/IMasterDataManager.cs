﻿using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfileReviewSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.MasterData
{
    public interface IMasterDataManager
    {
        Task<IEnumerable<LOB>> GetAllLOBsOfPlanByPlanIDAsync(int planID);

        Task<IEnumerable<MasterPracticePaymentRemittancePerson>> GetAllMasterPaymentRemittancePersonAsync();

        Task<IEnumerable<MasterEmployee>> GetAllMasterBillingContactPersonAsync();

        Task<IEnumerable<MasterEmployee>> GetAllMasterCredentialingContactPersonAsync();

        Task<IEnumerable<MasterEmployee>> GetAllMasterBusinessContactPersonAsync();

        Task<List<int?>> GetAllProfileIDsFromCredentialingInfoIDsAsync(int[] ProviderIDArray);

        Task<IEnumerable<EmailTemplate>> GetAllEmailTemplatesAsync();

        Task<IEnumerable<Group>> GetAllGroupsAsync();

        Task<IEnumerable<StateLicenseStatus>> GetAllLicenseStatusAsync();

        Task<IEnumerable<LOB>> GetAllLOBsOfPlanContractByPlanIDAsync(int planID);

        Task<IEnumerable<StaffCategory>> GetAllStaffCategoryAsync();

        Task<IEnumerable<SpecialtyBoard>> GetAllspecialtyBoardAsync();

        Task<IEnumerable<Specialty>> GetAllSpecialtyAsync();

        Task<IEnumerable<School>> GetAllSchoolAsync();

        Task<IEnumerable<QualificationDegree>> GetAllQualificationDegreeAsync();

        Task<IEnumerable<ProviderType>> GetAllProviderTypeAsync();

        Task<IEnumerable<ProfileDisclosureQuestion>> GetAllProfileDisclosureQuestionAsync();

        Task<IEnumerable<FacilityPracticeType>> GetAllLocationPracticeTypeAsync();

        Task<IEnumerable<FacilityServiceQuestion>> GetAllPracticeServiceQuestionAsync();

        Task<IEnumerable<PracticeOpenStatusQuestion>> GetAllPracticeOpenStatusQuestionAsync();

        Task<IEnumerable<FacilityAccessibilityQuestion>> GetAllPracticeAccessibilityQuestionAsync();

        Task<IEnumerable<MilitaryRank>> GetAllMilitaryRankAsync();

        Task<IEnumerable<MilitaryPresentDuty>> GetAllMilitaryPresentDutyAsync();

        Task<IEnumerable<MilitaryDischarge>> GetAllMilitaryDischargeAsync();

        Task<IEnumerable<MilitaryBranch>> GetAllMilitaryBranchAsync();

        Task<IEnumerable<MilitaryRankBusinessModel>> GetAllMilitaryRanks();

        Task<IEnumerable<InsuranceCarrier>> GetAllInsuranceCarrierAsync();

        Task<IEnumerable<InsuranceCarrierAddress>> GetAllInsuranceCarrierAddressesAsync();

        Task<IEnumerable<HospitalContactPerson>> GetAllHospitalContactPersonAsync();

        Task<IEnumerable<HospitalContactInfo>> GetAllHospitalContactInfoAsync();

        Task<IEnumerable<Hospital>> GetAllHospitalAsync();

        Task<IEnumerable<DEASchedule>> GetAllDEAScheduleAsync();

        Task<IEnumerable<Certification>> GetAllCertificationAsync();

        Task<IEnumerable<AdmittingPrivilege>> GetAllAdmittingPrivilegeAsync();

        Task<IEnumerable<VisaType>> GetAllVisaTypeAsync();

        Task<IEnumerable<InsuaranceCompanyName>> GetAllInsuaranceCompanyNameAsync();

        Task<IEnumerable<VisaStatus>> GetAllVisaStatusAsync();

        Task<IEnumerable<LocationDetail>> GetLocationsByCityAsync(string city);

        Task<IEnumerable<string>> GetAllStatesAsync();

        Task<IEnumerable<Country>> GetAllCountriesAsync();

        Task<IEnumerable<LocationDetail>> GetCitiesAsync(string city);

        Task<IEnumerable<LocationDetail>> GetCitiesAllAsync();


        Task<IEnumerable<Question>> GetAllQuestionsAsync();

        Task<IEnumerable<QuestionCategory>> GetAllQuestionCategoriesAsync();

        Task<IEnumerable<OrganizationGroup>> GetAllOrganizationGroupAsync();

        Task<IEnumerable<FacilityAccessibilityQuestion>> GetAllAccessibilityQuestionsAsync();

        Task<IEnumerable<FacilityPracticeType>> GetAllPracticeTypesAsync();

        Task<IEnumerable<FacilityServiceQuestion>> GetAllServiceQuestionsAsync();

        Task<IEnumerable<PracticeOpenStatusQuestion>> GetAllOpenPracticeStatusQuestionsAsync();

        Task<IEnumerable<Employee>> GetAllBusinessContactPersonAsync();

        Task<IEnumerable<Employee>> GetAllBillingContactAsync();

        Task<IEnumerable<PracticePaymentAndRemittance>> GetAllPaymentAndRemittance();

        Task<IEnumerable<Employee>> GetAllCredentialingContactAsync();

        Task<IEnumerable<ProviderLevel>> GetAllProviderLevelsAsync();

        Task<IEnumerable<OrganizationGroup>> GetAllOrganizationGroupsAsync();

        Task InitData();

        Task<IEnumerable<Plan>> GetAllPlanNamesAsync();

        Task<IEnumerable<Plan>> GetAllPlanAsync();

        Task<IEnumerable<Plan>> GetAllInactivePlansAsync();

        Task<IEnumerable<LOB>> GetAllLOBAsync();

        Task<IEnumerable<Plan>> GetAllPlans_ForBEAsync(List<string> Groups);

        Task<IEnumerable<ProfileVerificationParameter>> GetAllProfileVerificationParameter();

        Task<IEnumerable<VerificationLink>> GetAllVerificationLinks();

        Task<IEnumerable<CredentialingRequest>> GetAllCredentialingRequestAsync();

        Task<IEnumerable<ProfileSection>> GetAllProfileSectionsAsync();

        Task<IEnumerable<ProfileReviewSection>> GetAllNotApplicableSectoinAsync(int profileId);

        //Task<IEnumerable<Group>> GetAllOrganizationGroupsAsync();

        Task<IEnumerable<ProfileSubSection>> GetAllProfileSubSectionsAsync();

        Task<IEnumerable<VerificationLink>> GetAllVerificationLinksAsync();

        Task<IEnumerable<DecredentialingReason>> GetAllDecredentialingReasonsAsync();

        Task<int> InactivateVerificationLink(int verificationLinkID);

        List<NotesTemplate> GetNotesTemplateByCode(string Code);
        List<NotesTemplate> GetAllNotesTemplates();

        Task<int> InactivateDecredentialingReason(int DecredentialingReasonID);

        Task<int> InactivateNotesTemplte(int notesTemplateID);

        Task<IEnumerable<Facility>> GetAllMasterFacilityInformationAsync();

        Task AddFacilityAsync(int organizationId, Facility facility);

        Task UpdateFacilityAsync(int organizationId, Facility facility);

        Task<IEnumerable<Profile>> GetAllMidLevelPractitionersAsync();
    }
}