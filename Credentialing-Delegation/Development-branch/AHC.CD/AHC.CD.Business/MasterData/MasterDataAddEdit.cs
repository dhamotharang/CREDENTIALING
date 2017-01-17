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
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Resources.Rules;
using AHC.CD.Entities.MasterProfile.PracticeLocation;

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

        public async Task UpdateInsuaranceCompany(Entities.MasterData.Tables.InsuaranceCompanyName insuaranceCompanyName)
        {
            try
            {
                var insuaranceCompanyNameRepo = uof.GetGenericRepository<InsuaranceCompanyName>();
                insuaranceCompanyNameRepo.Update(insuaranceCompanyName);
                await insuaranceCompanyNameRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MasterDataUpdateException(ExceptionMessage.Insuarance_Company_Name_UPDATE_EXCEPTION, ex);
            }
        }
        public int AddInsuaranceCompany(Entities.MasterData.Tables.InsuaranceCompanyName InsuaranceCompanyName)
        {
            try
            {
                var InsuaranceCompanyRepo = uof.GetGenericRepository<InsuaranceCompanyName>();
                InsuaranceCompanyRepo.Create(InsuaranceCompanyName);
                InsuaranceCompanyRepo.Save();
                return InsuaranceCompanyName.InsuaranceCompanyNameID;
            }
            catch(Exception ex)
            {
                throw new MasterDataAddException(ExceptionMessage.Insuarance_Company_Name_ADD_EXCEPTION, ex);
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

                AddOrganizationGroup(group);

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


        public int AddOrganizationGroup(OrganizationGroup group)
        {
            try
            {
                var organizationgroupRepo = uof.GetGenericRepository<OrganizationGroup>();
                organizationgroupRepo.Create(group);
                organizationgroupRepo.Save();

                return group.OrganizationGroupID;
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

        public async Task UpdateOrganizationGroup(OrganizationGroup group)
        {
            try
            {
                var groupRepo = uof.GetGenericRepository<OrganizationGroup>();
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

        private void AddOrganizationGroup(Group group)
        {
            try
            {
                if(group != null)
                {
                    OrganizationGroup orgGroup = new OrganizationGroup();
                    orgGroup.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    orgGroup.GroupName = group.Name;
                    orgGroup.GroupDescription = group.Description;
                    var organizationgroupRepo = uof.GetGenericRepository<OrganizationGroup>();
                    organizationgroupRepo.Create(orgGroup);
                    organizationgroupRepo.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        /// <summary>
        /// Method to save Email Template
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        public async Task<EmailTemplate> SaveEmailTemplateAsync(EmailTemplate emailTemplate)
        {
            try
            {
                var emailTemplateRepo = uof.GetGenericRepository<EmailTemplate>();
                if (emailTemplate.EmailTemplateID != 0)
                {
                    EmailTemplate updateEmailTemplate = await emailTemplateRepo.FindAsync(e => e.EmailTemplateID == emailTemplate.EmailTemplateID);
                    updateEmailTemplate = AutoMapper.Mapper.Map<EmailTemplate, EmailTemplate>(emailTemplate,updateEmailTemplate);
                    emailTemplateRepo.Update(updateEmailTemplate);
                }
                else
                {
                    emailTemplateRepo.Create(emailTemplate);
                }
                await emailTemplateRepo.SaveAsync();
                return emailTemplate;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Method to save Office Manager
        /// </summary>
        /// <param name="officeManager"></param>
        /// <returns></returns>
        public async Task<MasterEmployee> SaveOfficeManager(MasterEmployee officeManager)
        {
            var departmentRepository = uof.GetGenericRepository<Department>();
            var officeManagerRepo = uof.GetGenericRepository<MasterEmployee>();
            if (officeManager.MasterEmployeeID != 0)
            {
                MasterEmployee updateOfficeManager = await officeManagerRepo.FindAsync(o => o.MasterEmployeeID == officeManager.MasterEmployeeID, "Departments.Department, Designations.Designation");
                List<EmployeeDepartment> updateEmployeeDepartments = updateOfficeManager.Departments.ToList();
                List<EmployeeDesignation> updateEmployeeDesignations = updateOfficeManager.Designations.ToList();
                updateOfficeManager = AutoMapper.Mapper.Map<MasterEmployee, MasterEmployee>(officeManager, updateOfficeManager);
                updateOfficeManager.Designations = updateEmployeeDesignations;
                updateOfficeManager.Departments = updateEmployeeDepartments;
                officeManagerRepo.Update(updateOfficeManager);
            }
            else
            {
                if (officeManager.Departments == null)
                    officeManager.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                EmployeeDepartment ed = new EmployeeDepartment();
                var dr = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BUSINESS).FirstOrDefault();
                ed.DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BUSINESS).FirstOrDefault().DepartmentID;
                officeManager.Departments.Add(ed);

                if (officeManager.Designations == null)
                    officeManager.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                officeManager.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.OfficeManagerDesignationId });
                officeManagerRepo.Create(officeManager);
            }
            await officeManagerRepo.SaveAsync();
            return officeManager;
        }

        /// <summary>
        /// Method to save Billing Contact
        /// </summary>
        /// <param name="billingContact"></param>
        /// <returns></returns>
        public async Task<MasterEmployee> SaveBillingContact(MasterEmployee billingContact)
        {
            var departmentRepository = uof.GetGenericRepository<Department>();
            var billingContactRepo = uof.GetGenericRepository<MasterEmployee>();
            if (billingContact.MasterEmployeeID != 0)
            {
                MasterEmployee updateBillingContact = await billingContactRepo.FindAsync(o => o.MasterEmployeeID == billingContact.MasterEmployeeID, "Departments.Department, Designations.Designation");
                List<EmployeeDepartment> updateEmployeeDepartments = updateBillingContact.Departments.ToList();
                List<EmployeeDesignation> updateEmployeeDesignations = updateBillingContact.Designations.ToList();
                updateBillingContact = AutoMapper.Mapper.Map<MasterEmployee, MasterEmployee>(billingContact, updateBillingContact);
                updateBillingContact.Designations = updateEmployeeDesignations;
                updateBillingContact.Departments = updateEmployeeDepartments;
                billingContactRepo.Update(updateBillingContact);   
            }
            else
            {
                if (billingContact.Departments == null)
                    billingContact.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                EmployeeDepartment ed = new EmployeeDepartment();
                var dr = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BILLING).FirstOrDefault();
                ed.DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BILLING).FirstOrDefault().DepartmentID;
                billingContact.Departments.Add(ed);

                if (billingContact.Designations == null)
                    billingContact.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                billingContact.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.BillingContactDesignationId });
                billingContactRepo.Create(billingContact);
            }
            await billingContactRepo.SaveAsync();
            return billingContact;
        }

        /// <summary>
        /// Method to save Payment and Remittance
        /// </summary>
        /// <param name="paymentAndRemittance"></param>
        /// <returns></returns>
        public async Task<MasterPracticePaymentRemittancePerson> SavePaymentAndRemittance(MasterPracticePaymentRemittancePerson paymentAndRemittance)
        {
            var departmentRepository = uof.GetGenericRepository<Department>();
            var paymentAndRemittanceRepo = uof.GetGenericRepository<MasterPracticePaymentRemittancePerson>();
            if (paymentAndRemittance.MasterPracticePaymentRemittancePersonID != 0)
            {
                MasterPracticePaymentRemittancePerson updatePaymentAndRemittance = await paymentAndRemittanceRepo.FindAsync(p => p.MasterPracticePaymentRemittancePersonID == paymentAndRemittance.MasterPracticePaymentRemittancePersonID, "PaymentAndRemittancePerson.Departments.Department, PaymentAndRemittancePerson.Designations.Designation");
                List<EmployeeDepartment> updateEmployeeDepartments = updatePaymentAndRemittance.PaymentAndRemittancePerson.Departments.ToList();
                List<EmployeeDesignation> updateEmployeeDesignations = updatePaymentAndRemittance.PaymentAndRemittancePerson.Designations.ToList();
                updatePaymentAndRemittance.PaymentAndRemittancePerson = AutoMapper.Mapper.Map<MasterEmployee, MasterEmployee>(paymentAndRemittance.PaymentAndRemittancePerson, updatePaymentAndRemittance.PaymentAndRemittancePerson);
                updatePaymentAndRemittance = AutoMapper.Mapper.Map<MasterPracticePaymentRemittancePerson, MasterPracticePaymentRemittancePerson>(paymentAndRemittance, updatePaymentAndRemittance);
                updatePaymentAndRemittance.PaymentAndRemittancePerson.Designations = updateEmployeeDesignations;
                updatePaymentAndRemittance.PaymentAndRemittancePerson.Departments = updateEmployeeDepartments;
                paymentAndRemittanceRepo.Update(updatePaymentAndRemittance);
            }
            else
            {
                if (paymentAndRemittance.PaymentAndRemittancePerson.Departments == null)
                    paymentAndRemittance.PaymentAndRemittancePerson.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                paymentAndRemittance.PaymentAndRemittancePerson.Departments.Add(new EmployeeDepartment()
                {
                    DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.PAYMENT).FirstOrDefault().DepartmentID
                });

                if (paymentAndRemittance.PaymentAndRemittancePerson.Designations == null)
                    paymentAndRemittance.PaymentAndRemittancePerson.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                paymentAndRemittance.PaymentAndRemittancePerson.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.PaymentAndRemittanceContactDesignationId });

                paymentAndRemittanceRepo.Create(paymentAndRemittance);
            }
            await paymentAndRemittanceRepo.SaveAsync();
            return paymentAndRemittance;            
        }

        /// <summary>
        /// Method to save Covering Colleague
        /// </summary>
        /// <param name="practiceProvider"></param>
        /// <returns></returns>
        public async Task<PracticeProvider> SaveCoveringColleague(PracticeProvider practiceProvider)
        {
            var practiceProviderRepo = uof.GetGenericRepository<PracticeProvider>();
            if (practiceProvider.PracticeProviderID != 0)
            {
                PracticeProvider updatePracticeProvider = await practiceProviderRepo.FindAsync(p => p.PracticeProviderID == practiceProvider.PracticeProviderID, "PracticeProviderTypes.ProviderType, PracticeProviderSpecialties.Specialty");
                updatePracticeProvider = AutoMapper.Mapper.Map<PracticeProvider, PracticeProvider>(practiceProvider, updatePracticeProvider);

                if (practiceProvider.PracticeType == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague)
                {
                    foreach (var providerType in practiceProvider.PracticeProviderTypes)
                    {
                        if (providerType.PracticeProviderTypeId != 0)
                        {
                            var type = updatePracticeProvider.PracticeProviderTypes.FirstOrDefault(t => t.PracticeProviderTypeId == providerType.PracticeProviderTypeId);
                            if (type != null)
                            {
                                type = AutoMapper.Mapper.Map<PracticeProviderType, PracticeProviderType>(providerType, type);
                            }
                            else
                                updatePracticeProvider.PracticeProviderTypes.Add(providerType);
                        }
                        else
                        {
                            updatePracticeProvider.PracticeProviderTypes.Add(providerType);
                        }
                    }

                    foreach (var providerSpecialty in practiceProvider.PracticeProviderSpecialties)
                    {
                        if (providerSpecialty.PracticeProviderSpecialtyId != 0)
                        {
                            var specialty = updatePracticeProvider.PracticeProviderSpecialties.FirstOrDefault(t => t.PracticeProviderSpecialtyId == providerSpecialty.PracticeProviderSpecialtyId);
                            if (specialty != null)
                            {
                                specialty = AutoMapper.Mapper.Map<PracticeProviderSpecialty, PracticeProviderSpecialty>(providerSpecialty, specialty);
                            }
                            else
                                updatePracticeProvider.PracticeProviderSpecialties.Add(providerSpecialty);
                        }
                        else
                        {
                            updatePracticeProvider.PracticeProviderSpecialties.Add(providerSpecialty);
                        }
                    }

                }
                practiceProviderRepo.Update(updatePracticeProvider);
            }
            else
            {
                practiceProviderRepo.Create(practiceProvider);
            }
            await practiceProviderRepo.SaveAsync();
            return practiceProvider;
        }

        /// <summary>
        /// Method to save Credentialing contact
        /// </summary>
        /// <param name="credentialingContact"></param>
        /// <returns></returns>
        public async Task<MasterEmployee> SaveCredentialingContact(MasterEmployee credentialingContact)
        {
            var departmentRepository = uof.GetGenericRepository<Department>();
            var credentialingContactRepo = uof.GetGenericRepository<MasterEmployee>();
            if (credentialingContact.MasterEmployeeID != 0)
            {
                MasterEmployee updateCredentialingContact = await credentialingContactRepo.FindAsync(o => o.MasterEmployeeID == credentialingContact.MasterEmployeeID, "Departments.Department, Designations.Designation");
                List<EmployeeDepartment> updateEmployeeDepartments = updateCredentialingContact.Departments.ToList();
                List<EmployeeDesignation> updateEmployeeDesignations = updateCredentialingContact.Designations.ToList();
                updateCredentialingContact = AutoMapper.Mapper.Map<MasterEmployee, MasterEmployee>(credentialingContact, updateCredentialingContact);
                updateCredentialingContact.Designations = updateEmployeeDesignations;
                updateCredentialingContact.Departments = updateEmployeeDepartments;
                credentialingContactRepo.Update(updateCredentialingContact);
            }
            else
            {
                if (credentialingContact.Departments == null)
                    credentialingContact.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                EmployeeDepartment ed = new EmployeeDepartment();
                var dr = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.CredentialingContact).FirstOrDefault();
                ed.DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.CredentialingContact).FirstOrDefault().DepartmentID;
                credentialingContact.Departments.Add(ed);

                if (credentialingContact.Designations == null)
                    credentialingContact.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                credentialingContact.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.PrimaryCredentialingContactDesignationId });
                credentialingContactRepo.Create(credentialingContact);
            }
            await credentialingContactRepo.SaveAsync();
            return credentialingContact;
        }

        public async Task<ProfileSubSection> SaveProfileSubSection(ProfileSubSection profileSubSection)
        {
            try
            {
                ProfileSubSection subsection = new ProfileSubSection();
                subsection.Status = AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString();
                subsection.SubSectionName = profileSubSection.SubSectionName;
                var profilesubsectionrepo = uof.GetGenericRepository<ProfileSubSection>();
                profilesubsectionrepo.Create(subsection);
                await profilesubsectionrepo.SaveAsync();
                return profileSubSection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VerificationLink> SaveVerificationLink(VerificationLink verificationlink)
        {
            try
            {
                ProfileSubSection subsection = new ProfileSubSection();
                subsection.Status = AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString();
                subsection.SubSectionName = verificationlink.Name;
                var verificationlinkrepo = uof.GetGenericRepository<VerificationLink>();
                verificationlinkrepo.Create(verificationlink);
                await verificationlinkrepo.SaveAsync();
                return verificationlink;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}