using AHC.CD.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using AHC.CD.Exceptions.MasterData;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.Business.MasterData
{
    public class MasterDataAddEdit : IMasterDataAddEdit
    {
        private IUnitOfWork uof = null;

        public MasterDataAddEdit(IUnitOfWork uof)
        {
            this.uof = uof;
        }

        public int AddProviderType(Entities.MasterData.Tables.ProviderType providerType)
        {
            try
            {
                var providerTypeRepo = uof.GetGenericRepository<ProviderType>();
                providerTypeRepo.Create(providerType);
                providerTypeRepo.Save();
                return providerType.ProviderTypeID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.PROVIDERTYPE_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateProviderType(Entities.MasterData.Tables.ProviderType providerType)
        {
            try
            {
                var providerTypeRepo = uof.GetGenericRepository<ProviderType>();
                providerTypeRepo.Update(providerType);
                await providerTypeRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.PROVIDERTYPE_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddSchool(Entities.MasterData.Tables.School school)
        {
            try
            {
                var schoolRepo = uof.GetGenericRepository<School>();
                schoolRepo.Create(school);
                schoolRepo.Save();
                return school.SchoolID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.SCHOOL_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateSchool(Entities.MasterData.Tables.School school)
        {
            try
            {
                var schoolRepo = uof.GetGenericRepository<School>();
                schoolRepo.Update(school);
                await schoolRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.SCHOOL_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddAdmittingPrivileges(Entities.MasterData.Tables.AdmittingPrivilege admittingPrivilege)
        {
            try
            {
                var admittingPrivilegeRepo = uof.GetGenericRepository<AdmittingPrivilege>();
                admittingPrivilegeRepo.Create(admittingPrivilege);
                admittingPrivilegeRepo.Save();
                return admittingPrivilege.AdmittingPrivilegeID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Admitting_Privileges_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateAdmittingPrivilege(Entities.MasterData.Tables.AdmittingPrivilege admittingPrivilege)
        {
            try
            {
                var admittingPrivilegeRepo = uof.GetGenericRepository<AdmittingPrivilege>();
                admittingPrivilegeRepo.Update(admittingPrivilege);
                await admittingPrivilegeRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Admitting_Privileges_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddCertification(Entities.MasterData.Tables.Certification certification)
        {
            try
            {
                var certificationRepo = uof.GetGenericRepository<Certification>();
                certificationRepo.Create(certification);
                certificationRepo.Save();
                return certification.CertificationID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Certification_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateCertification(Entities.MasterData.Tables.Certification certification)
        {
            try
            {
                var certificationRepo = uof.GetGenericRepository<Certification>();
                certificationRepo.Update(certification);
                await certificationRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Certification_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddPracticeAccessibilityQuestions(Entities.MasterData.Account.Accessibility.FacilityAccessibilityQuestion practiceAccessibilityQuestion)
        {
            try
            {
                var practiceAccessibilityQuestionRepo = uof.GetGenericRepository<FacilityAccessibilityQuestion>();
                practiceAccessibilityQuestionRepo.Create(practiceAccessibilityQuestion);
                practiceAccessibilityQuestionRepo.Save();
                return practiceAccessibilityQuestion.FacilityAccessibilityQuestionId;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Practice_Accessibility_Questions_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdatePracticeAccessibilityQuestions(Entities.MasterData.Account.Accessibility.FacilityAccessibilityQuestion practiceAccessibilityQuestion)
        {
            try
            {
                var practiceAccessibilityQuestionRepo = uof.GetGenericRepository<FacilityAccessibilityQuestion>();
                practiceAccessibilityQuestionRepo.Update(practiceAccessibilityQuestion);
                await practiceAccessibilityQuestionRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Practice_Accessibility_Questions_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddPracticeOpenStatusQuestion(Entities.MasterData.Account.PracticeOpenStatusQuestion practiceOpenStatusQuestion)
        {
            try
            {
                var practiceOpenStatusQuestionRepo = uof.GetGenericRepository<PracticeOpenStatusQuestion>();
                practiceOpenStatusQuestionRepo.Create(practiceOpenStatusQuestion);
                practiceOpenStatusQuestionRepo.Save();
                return practiceOpenStatusQuestion.PracticeOpenStatusQuestionID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Practice_OpenStatus_Question_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdatePracticeOpenStatusQuestion(Entities.MasterData.Account.PracticeOpenStatusQuestion practiceOpenStatusQuestion)
        {
            try
            {
                var practiceOpenStatusQuestionRepo = uof.GetGenericRepository<PracticeOpenStatusQuestion>();
                practiceOpenStatusQuestionRepo.Update(practiceOpenStatusQuestion);
                await practiceOpenStatusQuestionRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.PROFESSIONAL_AFFILIATION_CREATE_EXCEPTION, ex);
            }
        }

        public int AddPracticeServiceQuestion(Entities.MasterData.Account.Service.FacilityServiceQuestion practiceServiceQuestion)
        {
            try
            {
                var practiceServiceQuestionRepo = uof.GetGenericRepository<FacilityServiceQuestion>();
                practiceServiceQuestionRepo.Create(practiceServiceQuestion);
                practiceServiceQuestionRepo.Save();
                return practiceServiceQuestion.FacilityServiceQuestionID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Practice_Service_Question_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdatePracticeServiceQuestion(Entities.MasterData.Account.Service.FacilityServiceQuestion practiceServiceQuestion)
        {
            try
            {
                var practiceServiceQuestionRepo = uof.GetGenericRepository<FacilityServiceQuestion>();
                practiceServiceQuestionRepo.Update(practiceServiceQuestion);
                await practiceServiceQuestionRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Practice_Service_Question_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddQualificationDegree(Entities.MasterData.Tables.QualificationDegree qualificationDegree)
        {
            try
            {
                var qualificationDegreeRepo = uof.GetGenericRepository<QualificationDegree>();
                qualificationDegreeRepo.Create(qualificationDegree);
                qualificationDegreeRepo.Save();
                return qualificationDegree.QualificationDegreeID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Qualification_Degree_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateQualificationDegree(Entities.MasterData.Tables.QualificationDegree qualificationDegree)
        {
            try
            {
                var qualificationDegreeRepo = uof.GetGenericRepository<QualificationDegree>();
                qualificationDegreeRepo.Update(qualificationDegree);
                await qualificationDegreeRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Qualification_Degree_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddSpeciality(Entities.MasterData.Tables.Specialty specialty)
        {
            try
            {
                var specialityRepo = uof.GetGenericRepository<Specialty>();
                specialityRepo.Create(specialty);
                specialityRepo.Save();
                return specialty.SpecialtyID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Speciality_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateSpeciality(Entities.MasterData.Tables.Specialty specialty)
        {
            try
            {
                var specialityRepo = uof.GetGenericRepository<Specialty>();
                specialityRepo.Update(specialty);
                await specialityRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Speciality_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddSpecialityBoard(Entities.MasterData.Tables.SpecialtyBoard specialtyBoard)
        {
            try
            {
                var specialityBoardRepo = uof.GetGenericRepository<SpecialtyBoard>();
                specialityBoardRepo.Create(specialtyBoard);
                specialityBoardRepo.Save();
                return specialtyBoard.SpecialtyBoardID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Speciality_Board_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateSpecialityBoard(Entities.MasterData.Tables.SpecialtyBoard specialtyBoard)
        {
            try
            {
                var specialityBoardRepo = uof.GetGenericRepository<SpecialtyBoard>();
                specialityBoardRepo.Update(specialtyBoard);
                await specialityBoardRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Speciality_Board_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddStaffCategory(Entities.MasterData.Tables.StaffCategory staffCategory)
        {
            try
            {
                var staffCategoryRepo = uof.GetGenericRepository<StaffCategory>();
                staffCategoryRepo.Create(staffCategory);
                staffCategoryRepo.Save();
                return staffCategory.StaffCategoryID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Staff_Category_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateStaffCategory(Entities.MasterData.Tables.StaffCategory staffCategory)
        {
            try
            {
                var staffCategoryRepo = uof.GetGenericRepository<StaffCategory>();
                staffCategoryRepo.Update(staffCategory);
                await staffCategoryRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Staff_Category_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddStateLicenseStatus(Entities.MasterData.Tables.StateLicenseStatus stateLicenseStatus)
        {
            try
            {
                var stateLicenseStatusRepo = uof.GetGenericRepository<StateLicenseStatus>();
                stateLicenseStatusRepo.Create(stateLicenseStatus);
                stateLicenseStatusRepo.Save();
                return stateLicenseStatus.StateLicenseStatusID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.State_License_Status_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateStateLicenseStatus(Entities.MasterData.Tables.StateLicenseStatus stateLicenseStatus)
        {
            try
            {
                var stateLicenseStatusRepo = uof.GetGenericRepository<StateLicenseStatus>();
                stateLicenseStatusRepo.Update(stateLicenseStatus);
                await stateLicenseStatusRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.State_License_Status_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddVisaStatus(Entities.MasterData.Tables.VisaStatus visaStatus)
        {
            try
            {
                var visaStatusRepo = uof.GetGenericRepository<VisaStatus>();
                visaStatusRepo.Create(visaStatus);
                visaStatusRepo.Save();
                return visaStatus.VisaStatusID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Visa_Status_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateVisaStatus(Entities.MasterData.Tables.VisaStatus visaStatus)
        {
            try
            {
                var visaStatusRepo = uof.GetGenericRepository<VisaStatus>();
                visaStatusRepo.Update(visaStatus);
                await visaStatusRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Visa_Status_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddVisaType(Entities.MasterData.Tables.VisaType visaType)
        {
            try
            {
                var visaTypeRepo = uof.GetGenericRepository<VisaType>();
                visaTypeRepo.Create(visaType);
                visaTypeRepo.Save();
                return visaType.VisaTypeID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Visa_Type_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateVisaType(Entities.MasterData.Tables.VisaType visaType)
        {
            try
            {
                var visaTypeRepo = uof.GetGenericRepository<VisaType>();
                visaTypeRepo.Update(visaType);
                await visaTypeRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Visa_Type_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddProviderLevel(Entities.MasterData.Tables.ProviderLevel providerLevel)
        {
            try
            {
                var providerLevelRepo = uof.GetGenericRepository<ProviderLevel>();
                providerLevelRepo.Create(providerLevel);
                providerLevelRepo.Save();
                return providerLevel.ProviderLevelID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Provider_Level_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateProviderLevel(Entities.MasterData.Tables.ProviderLevel providerLevel)
        {
            try
            {
                var providerLevelRepo = uof.GetGenericRepository<ProviderLevel>();
                providerLevelRepo.Update(providerLevel);
                await providerLevelRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Provider_Level_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddGroup(Group group)
        {
            try
            {
                var organizationRepo = uof.GetGenericRepository<Organization>();
                var organization = organizationRepo.Find(x => x.OrganizationID == 1, "PracticingGroups, PracticingGroups.Group");
                
                PracticingGroup addPracticingGroup = new PracticingGroup();
                var practicingGroups = organization.PracticingGroups;
                addPracticingGroup.TaxId = "1234567890";
                addPracticingGroup.Group = group;
                addPracticingGroup.StatusType = StatusType.Active;
                practicingGroups.Add(addPracticingGroup);
                organizationRepo.Update(organization);
                organizationRepo.Save();

                //var groupRepo = uof.GetGenericRepository<Group>();
                //groupRepo.Create(group);
                ////groupRepo.Save();
                //var practicingGroupRepo = uof.GetGenericRepository<PracticingGroup>();
                //practicingGroupRepo.Create(new PracticingGroup() { Group = group, StatusType = StatusType.Active, TaxId = "1234567890" });
                //practicingGroups = practicingGroupRepo.GetAll().ToList();
                //addPracticingGroup.Group = group;
                //addPracticingGroup.TaxId = "1234567890";
                //addPracticingGroup.StatusType = StatusType.Active;
                //practicingGroups.Add(addPracticingGroup);
                //practicingGroupRepo.Save();
                //organization.PracticingGroups = practicingGroups;

                return group.GroupID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Group_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateGroup(Group group)
        {
            try
            {
                var groupRepo = uof.GetGenericRepository<Group>();
                groupRepo.Update(group);
                await groupRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Group_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddMilitaryDischarge(MilitaryDischarge militaryDischarge)
        {
            try
            {
                var militaryDischargeRepo = uof.GetGenericRepository<MilitaryDischarge>();
                militaryDischargeRepo.Create(militaryDischarge);
                militaryDischargeRepo.Save();
                return militaryDischarge.MilitaryDischargeID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Military_Discharge_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateMilitaryDischarge(MilitaryDischarge militaryDischarge)
        {
            try
            {
                var militaryDischargeRepo = uof.GetGenericRepository<MilitaryDischarge>();
                militaryDischargeRepo.Update(militaryDischarge);
                await militaryDischargeRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Military_Discharge_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddMilitaryPresentDuty(MilitaryPresentDuty militaryPresentDuty)
        {
            try
            {
                var militaryPresentDutyRepo = uof.GetGenericRepository<MilitaryPresentDuty>();
                militaryPresentDutyRepo.Create(militaryPresentDuty);
                militaryPresentDutyRepo.Save();
                return militaryPresentDuty.MilitaryPresentDutyID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Military_Present_Duty_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateMilitaryPresentDuty(MilitaryPresentDuty militaryPresentDuty)
        {
            try
            {
                var militaryPresentDutyRepo = uof.GetGenericRepository<MilitaryPresentDuty>();
                militaryPresentDutyRepo.Update(militaryPresentDuty);
                await militaryPresentDutyRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Military_Present_Duty_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddDEASchedule(DEASchedule schedule)
        {
            try
            {
                var dEAScheduleRepo = uof.GetGenericRepository<DEASchedule>();
                dEAScheduleRepo.Create(schedule);
                dEAScheduleRepo.Save();
                return schedule.DEAScheduleID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.DEASchedule_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateDEASchedule(DEASchedule schedule)
        {
            try
            {
                var dEAScheduleRepo = uof.GetGenericRepository<DEASchedule>();
                dEAScheduleRepo.Update(schedule);
                await dEAScheduleRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.DEASchedule_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddHospital(Hospital hospital)
        {
            try
            {
                var hospitalRepo = uof.GetGenericRepository<Hospital>();
                hospitalRepo.Create(hospital);
                hospitalRepo.Save();
                return hospital.HospitalID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Hospital_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateHospital(Hospital hospital)
        {
            try
            {
                var hospitalRepo = uof.GetGenericRepository<Hospital>();
                hospitalRepo.Update(hospital);
                await hospitalRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Hospital_UPDATE_EXCEPTION, ex);
            }
        }

        # region Private Methods

        public bool IsSchoolExist(School school)
        {
            return uof.GetGenericRepository<School>().Any(s => s.Name.Equals(school.Name));

        }

        public bool IsProviderTypeExist(ProviderType providerType)
        {
            return uof.GetGenericRepository<ProviderType>().Any(s => s.Title.Equals(providerType.Title));

        }

        public bool IsAdmittingPrivilegeExist(AdmittingPrivilege admittingPrivilege)
        {
            return uof.GetGenericRepository<AdmittingPrivilege>().Any(s => s.Title.Equals(admittingPrivilege.Title));

        }

        public bool IsCertificationExist(Certification certification)
        {
            return uof.GetGenericRepository<Certification>().Any(s => s.Name.Equals(certification.Name));

        }


        public bool IsPracticeAccessibilityQuestionExist(PracticeAccessibilityQuestion practiceAccessibilityQuestion)
        {
            return uof.GetGenericRepository<PracticeAccessibilityQuestion>().Any(s => s.Title.Equals(practiceAccessibilityQuestion.Title));

        }


        public bool IsPracticeOpenStatusQuestionExist(PracticeOpenStatusQuestion practiceOpenStatusQuestion)
        {
            return uof.GetGenericRepository<PracticeOpenStatusQuestion>().Any(s => s.Title.Equals(practiceOpenStatusQuestion.Title));

        }


        public bool IsPracticeServiceQuestionExist(PracticeServiceQuestion practiceServiceQuestion)
        {
            return uof.GetGenericRepository<PracticeServiceQuestion>().Any(s => s.Title.Equals(practiceServiceQuestion.Title));

        }


        public bool IsQualificationDegreeExist(QualificationDegree qualificationDegree)
        {
            return uof.GetGenericRepository<QualificationDegree>().Any(s => s.Title.Equals(qualificationDegree.Title));

        }


        public bool IsSpecialityExist(Specialty speciality)
        {
            return uof.GetGenericRepository<Specialty>().Any(s => s.Name.Equals(speciality.Name));

        }


        public bool IsSpecialityExist(SpecialtyBoard specialtyBoard)
        {
            return uof.GetGenericRepository<SpecialtyBoard>().Any(s => s.Name.Equals(specialtyBoard.Name));

        }



        #endregion

        public async Task UpdateHospitalContact(HospitalContactInfo hospitalContactInfo)
        {
            try
            {
                var hospitalContactRepo = uof.GetGenericRepository<HospitalContactInfo>();
                hospitalContactRepo.Update(hospitalContactInfo);
                 hospitalContactRepo.Save();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Hospital_Contact_UPDATE_EXCEPTION, ex);
            }
        }


        public async Task UpdateHospitalContactPerson(HospitalContactPerson hospitalContactPerson)
        {
            try
            {
                var hospitalContactPersonRepo = uof.GetGenericRepository<HospitalContactPerson>();
                hospitalContactPersonRepo.Update(hospitalContactPerson);
                 hospitalContactPersonRepo.Save();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Hospital_Contact_Person_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddHospitalContactInfo(int hospitalId, HospitalContactInfo hospitalContactInfo)
        {
            try
            {
                var hospitalRepo = uof.GetGenericRepository<Hospital>();
                var hospital = hospitalRepo.Find(hospitalId);
                hospital.HospitalContactInfoes.Add(hospitalContactInfo);
                hospitalRepo.Save();
                return hospital.HospitalID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Hospital_Contact_ADD_EXCEPTION, ex);
            }
        }

        public int AddHospitalContactPerson(int hospitalId, int hospitalContactInfoId, HospitalContactPerson hospitalContactPerson)
        {
            try
            {
                var hospitalRepo = uof.GetGenericRepository<Hospital>();
                var hospital = hospitalRepo.Find(hospitalId);
                var hospitalContactInfo = hospital.HospitalContactInfoes.FirstOrDefault(x => x.HospitalContactInfoID == hospitalContactInfoId);
                hospitalContactInfo.HospitalContactPersons.Add(hospitalContactPerson);
                hospitalRepo.Save();
                return hospitalContactPerson.HospitalContactPersonID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Hospital_Contact_Person_ADD_EXCEPTION, ex);
            }
        }

        public int AddInsuranceCarrier(InsuranceCarrier insuranceCarrier)
        {
            try
            {
                var insuranceCarrierRepo = uof.GetGenericRepository<InsuranceCarrier>();
                insuranceCarrierRepo.Create(insuranceCarrier);
                insuranceCarrierRepo.Save();
                return insuranceCarrier.InsuranceCarrierID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Insurance_Carrier_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateInsuranceCarrier(InsuranceCarrier insuranceCarrier)
        {
            try
            {
                var insuranceCarrierRepo = uof.GetGenericRepository<InsuranceCarrier>();
                insuranceCarrierRepo.Update(insuranceCarrier);
                await insuranceCarrierRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Insurance_Carrier_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddInsuranceCarrierAddress(int InsuranceCarrierId, InsuranceCarrierAddress insuranceCarrierAddress)
        {
            try
            {
                var insuranceCarrierRepo = uof.GetGenericRepository<InsuranceCarrier>();
                var insuranceCarrier = insuranceCarrierRepo.Find(InsuranceCarrierId);
                insuranceCarrier.InsuranceCarrierAddresses.Add(insuranceCarrierAddress);
                insuranceCarrierRepo.Save();
                return insuranceCarrierAddress.InsuranceCarrierAddressID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Insurance_Carrier_Address_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateInsuranceCarrierAddress(InsuranceCarrierAddress insuranceCarrierAddress)
        {
            try
            {
                var insuranceCarrierAddressRepo = uof.GetGenericRepository<InsuranceCarrierAddress>();
                insuranceCarrierAddressRepo.Update(insuranceCarrierAddress);
                await insuranceCarrierAddressRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Insurance_Carrier_Address_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddMilitaryBranch(MilitaryBranch militaryBranch)
        {
            try
            {
                var militaryBranchRepo = uof.GetGenericRepository<MilitaryBranch>();
                militaryBranchRepo.Create(militaryBranch);
                militaryBranchRepo.Save();
                return militaryBranch.MilitaryBranchID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Military_Branch_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateMilitaryBranch(MilitaryBranch militaryBranch)
        {
            try
            {
                var militaryBranchRepo = uof.GetGenericRepository<MilitaryBranch>();
                militaryBranchRepo.Update(militaryBranch);
                await militaryBranchRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Military_Branch_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddMilitaryRank(MilitaryRank militaryRank)
        {
            try
            {
                var militaryRankRepo = uof.GetMilitaryRankRepository();
                militaryRankRepo.AddMilitaryRank(militaryRank);
                var allRanks = militaryRankRepo.GetAll().ToList();
                var rank = allRanks.FirstOrDefault(x => x.Title.Equals(militaryRank.Title));
                return rank.MilitaryRankID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Military_Rank_ADD_EXCEPTION, ex);
            }
        }

        public int UpdateMilitaryRank(MilitaryRank militaryRank)
        {
            try
            {
                var militaryRankRepo = uof.GetMilitaryRankRepository();
                militaryRankRepo.UpdateMilitaryRank(militaryRank);
                return militaryRank.MilitaryRankID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Military_Rank_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddQuestionCategory(QuestionCategory questionCategory)
        {
            try
            {
                var questionCategoryRepo = uof.GetGenericRepository<QuestionCategory>();
                questionCategoryRepo.Create(questionCategory);
                questionCategoryRepo.Save();
                return questionCategory.QuestionCategoryID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Question_Category_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateQuestionCategory(QuestionCategory questionCategory)
        {
            try
            {
                var questionCategoryRepo = uof.GetGenericRepository<QuestionCategory>();
                questionCategoryRepo.Update(questionCategory);
                await questionCategoryRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Question_Category_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddQuestion(Question question)
        {
            try
            {
                var questionRepo = uof.GetGenericRepository<Question>();
                questionRepo.Create(question);
                questionRepo.Save();
                return question.QuestionID;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Question_ADD_EXCEPTION, ex);
            }
        }

        public async Task UpdateQuestion(Question question)
        {
            try
            {
                var questionRepo = uof.GetGenericRepository<Question>();
                questionRepo.Update(question);
                await questionRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Question_UPDATE_EXCEPTION, ex);
            }
        }

        public int AddCity(int stateId, Entities.Location.City city)
        {
            var stateRepo = uof.GetGenericRepository<State>();
            var state = stateRepo.Find(x => x.StateID == stateId);
            state.Cities.Add(city);
            stateRepo.Save();
            return city.CityID;
        }
    }
}