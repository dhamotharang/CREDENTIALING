using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.Credentialing;

namespace AHC.CD.Business.MasterData
{
   public interface IMasterDataAddEdit
   {
       Task<ProfileSubSection> SaveProfileSubSection(ProfileSubSection profileSubSecton);

       Task UpdateProfileSubSection(ProfileSubSection profileSubSection);

       Task<VerificationLink> SaveVerificationLink(VerificationLink verificationlink);

       Task<DecredentialingReason> SaveDecredentialingReason(DecredentialingReason decredentialingreason);

       Task<DecredentialingReason> UpdateDecredentialingReason(DecredentialingReason decredentialingreason); 

       Task<MasterEmployee> SaveCredentialingContact(MasterEmployee credentialingContact);

       Task<PracticeProvider> SaveCoveringColleague(PracticeProvider practiceProvider);

       Task<MasterPracticePaymentRemittancePerson> SavePaymentAndRemittance(MasterPracticePaymentRemittancePerson paymentAndRemittance);

       Task<MasterEmployee> SaveBillingContact(MasterEmployee billingContact);

       Task<MasterEmployee> SaveOfficeManager(MasterEmployee officeManager);

       Task<EmailTemplate> SaveEmailTemplateAsync(EmailTemplate emailTemplate);

       int AddProviderType(ProviderType providerType);

       Task UpdateProviderType(ProviderType providerType);

       int AddSchool(School school);
       int AddSchoolContactInfo(int SchoolID, SchoolContactInfo ContactInfo);

       Task UpdateSchool(School school);

       void UpdateSchoolContactInfo(SchoolContactInfo ContactInfo);

       int AddAdmittingPrivileges(AdmittingPrivilege admittingPrivilege);

       Task UpdateAdmittingPrivilege(AdmittingPrivilege admittingPrivilege);

       int AddCertification(Certification certification);

       Task UpdateCertification(Certification certification);

       int AddPracticeAccessibilityQuestions(FacilityAccessibilityQuestion practiceAccessibilityQuestion);

       Task UpdatePracticeAccessibilityQuestions(FacilityAccessibilityQuestion practiceAccessibilityQuestion);

       int AddPracticeOpenStatusQuestion(PracticeOpenStatusQuestion practiceOpenStatusQuestion);

       Task UpdatePracticeOpenStatusQuestion(PracticeOpenStatusQuestion practiceOpenStatusQuestion);

       int AddPracticeServiceQuestion(FacilityServiceQuestion practiceServiceQuestion);

       Task UpdatePracticeServiceQuestion(FacilityServiceQuestion practiceServiceQuestion);

       int AddQualificationDegree(QualificationDegree qualificationDegree);

       Task UpdateQualificationDegree(QualificationDegree qualificationDegree);

       int AddSpeciality(Specialty specialty);
       Task UpdateSpeciality(Specialty specialty);

       int AddSpecialityBoard(SpecialtyBoard specialtyBoard);
       Task UpdateSpecialityBoard(SpecialtyBoard specialtyBoard);

       int AddStaffCategory(StaffCategory staffCategory);

       Task UpdateStaffCategory(StaffCategory staffCategory);

       int AddStateLicenseStatus(StateLicenseStatus stateLicenseStatus);

       Task UpdateStateLicenseStatus(StateLicenseStatus stateLicenseStatus);

       int AddVisaStatus(VisaStatus visaStatus);

       Task UpdateVisaStatus(VisaStatus visaStatus);

       int AddVisaType(VisaType visaType);

       Task UpdateInsuaranceCompany(InsuaranceCompanyName insuaranceCompanyName);

       int AddInsuaranceCompany(InsuaranceCompanyName insuaranceCompanyName);

       Task UpdateVisaType(VisaType visaType);

       int AddProviderLevel(ProviderLevel providerLevel);

       Task UpdateProviderLevel(ProviderLevel providerLevel);

       int AddGroup(Group group);

       Task UpdateGroup(Group group);

       int AddDEASchedule(DEASchedule schedule);

       Task UpdateDEASchedule(DEASchedule schedule);

       int AddMilitaryDischarge(MilitaryDischarge militaryDischarge);

       Task UpdateMilitaryDischarge(MilitaryDischarge militaryDischarge);

       int AddMilitaryPresentDuty(MilitaryPresentDuty militaryPresentDuty);

       Task UpdateMilitaryPresentDuty(MilitaryPresentDuty militaryPresentDuty);

       int AddHospital(Hospital hospital);

       Task UpdateHospital(Hospital hospital);

       void UpdateHospitalContact(HospitalContactInfo hospitalContactInfo);
       void UpdateHospitalContactPerson(HospitalContactPerson hospitalContactPerson);


       int AddInsuranceCarrier(InsuranceCarrier insuranceCarrier);

       Task UpdateInsuranceCarrier(InsuranceCarrier insuranceCarrier);

       int AddInsuranceCarrierAddress( int InsuranceCarrierId, InsuranceCarrierAddress insuranceCarrierAddress);

       Task UpdateInsuranceCarrierAddress(InsuranceCarrierAddress insuranceCarrierAddress);
       

       int AddMilitaryBranch(MilitaryBranch militaryBranch);

       Task UpdateMilitaryBranch(MilitaryBranch militaryBranch);

       int AddMilitaryRank(MilitaryRank militaryRank);

       int UpdateMilitaryRank(MilitaryRank militaryRank);

       int AddHospitalContactInfo(int hospitalId, HospitalContactInfo hospitalContactInfo);

       int AddHospitalContactPerson(int hospitalId, int hospitalContactInfoId, HospitalContactPerson hospitalContactPerson);

       int AddQuestionCategory(QuestionCategory questionCategory);

       Task UpdateQuestionCategory(QuestionCategory questionCategory);

       int AddQuestion(Question question);

       Task UpdateQuestion(Question question);

       int AddCity(int stateId, City city);

       void UpdateCity(Entities.Location.City city);

       Task UpdateOrganizationGroup(OrganizationGroup group);

       int AddOrganizationGroup(OrganizationGroup group);

       int AddNotestemplate(NotesTemplate Template);

       bool UpdateNotestemplate(NotesTemplate Template);

    }
 }
