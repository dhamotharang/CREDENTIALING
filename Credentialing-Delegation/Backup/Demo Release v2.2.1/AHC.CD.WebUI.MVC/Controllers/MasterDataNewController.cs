using AHC.CD.Business.MasterData;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class MasterDataNewController : Controller
    {
        private IErrorLogger ErrorLogger { get; set; }
        private IMasterDataManager masterDataManager = null;
        private IMasterDataAddEdit masterDataAddEdit = null;

        public MasterDataNewController(IErrorLogger errorLogger, IMasterDataManager IMasterDataManager, IMasterDataAddEdit masterDataAddEdit)
        {
            this.ErrorLogger = errorLogger;
            this.masterDataManager = IMasterDataManager;
            this.masterDataAddEdit = masterDataAddEdit;
        }

        #region Get All Master Data

        //get all AdmittingPrivileges data
        public async Task<JsonResult> GetAllAdmittingPrivileges()
        {
            var data = await masterDataManager.GetAllAdmittingPrivilegeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryPresentDuties()
        {
            var data = await masterDataManager.GetAllMilitaryPresentDutyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryDischarges()
        {
            var data = await masterDataManager.GetAllMilitaryDischargeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllCertificates()
        {
            var data = await masterDataManager.GetAllCertificationAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllQualificationDegrees()
        {
            var data = await masterDataManager.GetAllQualificationDegreeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllSpecialities()
        {
            var data = await masterDataManager.GetAllSpecialtyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllSchools()
        {
            var data = await masterDataManager.GetAllSchoolAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllspecialtyBoards()
        {
            var data = await masterDataManager.GetAllspecialtyBoardAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllVisaTypes()
        {
            var data = await masterDataManager.GetAllVisaTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllVisaStatus()
        {
            var data = await masterDataManager.GetAllVisaStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllLicenseStatus()
        {
            var data = await masterDataManager.GetAllLicenseStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllStaffCategories()
        {
            var data = await masterDataManager.GetAllStaffCategoryAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllPracticeServiceQuestions()
        {
            var data = await masterDataManager.GetAllPracticeServiceQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllPracticeOpenStatusQuestions()
        {
            var data = await masterDataManager.GetAllPracticeOpenStatusQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllPracticeAccessibilityQuestions()
        {
            var data = await masterDataManager.GetAllPracticeAccessibilityQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllProviderTypes()
        {
            var data = await masterDataManager.GetAllProviderTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllProviderLevels()
        {
            var data = await masterDataManager.GetAllProviderLevelsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllOrganizationGroupAsync()
        {
            var data = await masterDataManager.GetAllOrganizationGroupAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllDEASchedules()
        {
            var data = await masterDataManager.GetAllDEAScheduleAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllHospitals()
        {
            var data = await masterDataManager.GetAllHospitalAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllInsuranceCarriers()
        {
            var data = await masterDataManager.GetAllInsuranceCarrierAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllQuestions()
        {
            var data = await masterDataManager.GetAllQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllQuestionCategories()
        {
            var data = await masterDataManager.GetAllQuestionCategoriesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryBranches()
        {
            var data = await masterDataManager.GetAllMilitaryBranchAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryRanks()
        {
            var data = await masterDataManager.GetAllMilitaryRanks();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<string> GetAllCities()
        {
            var data = await masterDataManager.GetCitiesAllAsync();
            return JsonConvert.SerializeObject(data);
        }

        public async Task<string> GetAllGroups()
        {
            var data = await masterDataManager.GetAllGroupsAsync();
            return JsonConvert.SerializeObject(data);
        }

        #endregion

        #region Add/Edit Master Data

        public ActionResult Index()
        {
            return View();
        }

        #region Groups/IPA

        public ActionResult Groups()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGroups(OrganizationGroupViewModel organizationGroupDetails)
        {
            string status = "true";
            Group organizationGroup = null;

            try
            {
                if (ModelState.IsValid)
                {
                    organizationGroup = AutoMapper.Mapper.Map<OrganizationGroupViewModel, Group>(organizationGroupDetails);

                    organizationGroup.GroupID = masterDataAddEdit.AddGroup(organizationGroup);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Group_ADD_EXCEPTION;
            }

            return Json(new { status = status, organizationGroupDetails = organizationGroup }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateGroups(OrganizationGroupViewModel organizationGroupDetails)
        {
            string status = "true";
            Group organizationGroup = null;

            try
            {
                if (ModelState.IsValid)
                {
                    organizationGroup = AutoMapper.Mapper.Map<OrganizationGroupViewModel, Group>(organizationGroupDetails);

                    await masterDataAddEdit.UpdateGroup(organizationGroup);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Group_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, organizationGroupDetails = organizationGroup }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProviderLevels

        public ActionResult ProviderLevels()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProviderLevels(ProviderLevelViewModel providerLevelDetails)
        {
            string status = "true";
            ProviderLevel providerLevel = null;

            try
            {
                if (ModelState.IsValid)
                {
                    providerLevel = AutoMapper.Mapper.Map<ProviderLevelViewModel, ProviderLevel>(providerLevelDetails);

                    providerLevel.ProviderLevelID = masterDataAddEdit.AddProviderLevel(providerLevel);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Provider_Level_ADD_EXCEPTION;
            }

            return Json(new { status = status, providerLevelDetails = providerLevel }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProviderLevels(ProviderLevelViewModel providerLevelDetails)
        {
            string status = "true";
            ProviderLevel providerLevel = null;

            try
            {
                if (ModelState.IsValid)
                {
                    providerLevel = AutoMapper.Mapper.Map<ProviderLevelViewModel, ProviderLevel>(providerLevelDetails);

                    await masterDataAddEdit.UpdateProviderLevel(providerLevel);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Provider_Level_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, providerLevelDetails = providerLevel }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region VisaType

        public ActionResult VisaType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVisaType(VisaTypeViewModel visaTypeDetails)
        {
            string status = "true";
            VisaType visaType = null;

            try
            {
                if (ModelState.IsValid)
                {
                    visaType = AutoMapper.Mapper.Map<VisaTypeViewModel, VisaType>(visaTypeDetails);

                    visaType.VisaTypeID = masterDataAddEdit.AddVisaType(visaType);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Visa_Type_ADD_EXCEPTION;
            }

            return Json(new { status = status, visaTypeDetails = visaType }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateVisaType(VisaTypeViewModel visaTypeDetails)
        {
            string status = "true";
            VisaType visaType = null;

            try
            {
                if (ModelState.IsValid)
                {
                    visaType = AutoMapper.Mapper.Map<VisaTypeViewModel, VisaType>(visaTypeDetails);

                    await masterDataAddEdit.UpdateVisaType(visaType);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Visa_Type_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, visaTypeDetails = visaType }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region VisaStatus

        public ActionResult VisaStatus()
        {
            return View();

        }

        [HttpPost]
        public ActionResult AddVisaStatus(VisaStatusViewModel visaStatusDetails)
        {
            string status = "true";
            VisaStatus visaStatus = null;

            try
            {
                if (ModelState.IsValid)
                {
                    visaStatus = AutoMapper.Mapper.Map<VisaStatusViewModel, VisaStatus>(visaStatusDetails);

                    visaStatus.VisaStatusID = masterDataAddEdit.AddVisaStatus(visaStatus);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Visa_Status_ADD_EXCEPTION;
            }

            return Json(new { status = status, visaStatusDetails = visaStatus }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateVisaStatus(VisaStatusViewModel visaStatusDetails)
        {
            string status = "true";
            VisaStatus visaStatus = null;

            try
            {
                if (ModelState.IsValid)
                {
                    visaStatus = AutoMapper.Mapper.Map<VisaStatusViewModel, VisaStatus>(visaStatusDetails);

                    await masterDataAddEdit.UpdateVisaStatus(visaStatus);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Visa_Status_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, visaStatusDetails = visaStatus }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region LicenseStatus

        public ActionResult LicenseStatus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLicenseStatus(StateLicenseStatusViewModel stateLicenseStatusDetails)
        {
            string status = "true";
            StateLicenseStatus stateLicenseStatus = null;

            try
            {
                if (ModelState.IsValid)
                {
                    stateLicenseStatus = AutoMapper.Mapper.Map<StateLicenseStatusViewModel, StateLicenseStatus>(stateLicenseStatusDetails);

                    stateLicenseStatus.StateLicenseStatusID = masterDataAddEdit.AddStateLicenseStatus(stateLicenseStatus);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.State_License_Status_ADD_EXCEPTION;
            }

            return Json(new { status = status, stateLicenseStatusDetails = stateLicenseStatus }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateLicenseStatus(StateLicenseStatusViewModel stateLicenseStatusDetails)
        {
            string status = "true";
            StateLicenseStatus stateLicenseStatus = null;

            try
            {
                if (ModelState.IsValid)
                {
                    stateLicenseStatus = AutoMapper.Mapper.Map<StateLicenseStatusViewModel, StateLicenseStatus>(stateLicenseStatusDetails);

                    await masterDataAddEdit.UpdateStateLicenseStatus(stateLicenseStatus);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.State_License_Status_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, stateLicenseStatusDetails = stateLicenseStatus }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region StaffCategory

        public ActionResult StaffCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStaffCategory(StaffCategoryViewModel staffCategoryDetails)
        {
            string status = "true";
            StaffCategory staffCategory = null;

            try
            {
                if (ModelState.IsValid)
                {
                    staffCategory = AutoMapper.Mapper.Map<StaffCategoryViewModel, StaffCategory>(staffCategoryDetails);

                    staffCategory.StaffCategoryID = masterDataAddEdit.AddStaffCategory(staffCategory);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Staff_Category_ADD_EXCEPTION;
            }

            return Json(new { status = status, staffCategoryDetails = staffCategory }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStaffCategory(StaffCategoryViewModel staffCategoryDetails)
        {
            string status = "true";
            StaffCategory staffCategory = null;

            try
            {
                if (ModelState.IsValid)
                {
                    staffCategory = AutoMapper.Mapper.Map<StaffCategoryViewModel, StaffCategory>(staffCategoryDetails);

                    await masterDataAddEdit.UpdateStaffCategory(staffCategory);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Staff_Category_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, staffCategoryDetails = staffCategory }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult SpecialtyBoard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSpecialtyBoard(SpecialtyBoardViewModel specialtyBoardDetails)
        {
            string status = "true";
            SpecialtyBoard specialtyBoard = null;
            try
            {
                if (ModelState.IsValid)
                {
                    specialtyBoard = AutoMapper.Mapper.Map<SpecialtyBoardViewModel, SpecialtyBoard>(specialtyBoardDetails);
                    specialtyBoard.SpecialtyBoardID = masterDataAddEdit.AddSpecialityBoard(specialtyBoard);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Speciality_Board_ADD_EXCEPTION;
            }
            return Json(new { status = status, specialtyBoardDetails = specialtyBoard }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSpecialtyBoard(SpecialtyBoardViewModel specialtyBoardDetails)
        {
            string status = "true";
            SpecialtyBoard specialtyBoard = null;
            try
            {
                if (ModelState.IsValid)
                {
                    specialtyBoard = AutoMapper.Mapper.Map<SpecialtyBoardViewModel, SpecialtyBoard>(specialtyBoardDetails);
                    await masterDataAddEdit.UpdateSpecialityBoard(specialtyBoard);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Speciality_Board_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, specialtyBoardDetails = specialtyBoard }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Specialty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSpecialty(SpecialtyViewModel specialtyDetails)
        {
            string status = "true";
            Specialty specialty = null;
            try
            {
                if (ModelState.IsValid)
                {
                    specialty = AutoMapper.Mapper.Map<SpecialtyViewModel, Specialty>(specialtyDetails);
                    specialty.SpecialtyID = masterDataAddEdit.AddSpeciality(specialty);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Speciality_ADD_EXCEPTION;
            }
            return Json(new { status = status, specialtyDetails = specialty }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSpecialty(SpecialtyViewModel specialtyDetails)
        {
            string status = "true";
            Specialty specialty = null;

            try
            {
                if (ModelState.IsValid)
                {
                    specialty = AutoMapper.Mapper.Map<SpecialtyViewModel, Specialty>(specialtyDetails);

                    await masterDataAddEdit.UpdateSpeciality(specialty);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Speciality_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, specialtyDetails = specialty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult School()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSchool(SchoolViewModel schoolDetails)
        {
            string status = "true";
            School school = null;

            try
            {
                if (ModelState.IsValid)
                {
                    school = AutoMapper.Mapper.Map<SchoolViewModel, School>(schoolDetails);

                    school.SchoolID = masterDataAddEdit.AddSchool(school);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.SCHOOL_ADD_EXCEPTION;
            }

            return Json(new { status = status, schoolDetails = school }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateSchool(SchoolViewModel schoolDetails)
        {
            string status = "true";
            School school = null;

            try
            {
                if (ModelState.IsValid)
                {
                    school = AutoMapper.Mapper.Map<SchoolViewModel, School>(schoolDetails);

                    await masterDataAddEdit.UpdateSchool(school);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.SCHOOL_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, schoolDetails = school }, JsonRequestBehavior.AllowGet);
        }

        #region DEASchedule

        public ActionResult DEASchedule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDEASchedule(DEAScheduleViewModel dEAScheduleDetails)
        {
            string status = "true";
            DEASchedule dEASchedule = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dEASchedule = AutoMapper.Mapper.Map<DEAScheduleViewModel, DEASchedule>(dEAScheduleDetails);

                    dEASchedule.DEAScheduleID = masterDataAddEdit.AddDEASchedule(dEASchedule);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.DEASchedule_ADD_EXCEPTION;
            }

            return Json(new { status = status, dEAScheduleDetails = dEASchedule }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDEASchedule(DEAScheduleViewModel dEAScheduleDetails)
        {
            string status = "true";
            DEASchedule dEASchedule = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dEASchedule = AutoMapper.Mapper.Map<DEAScheduleViewModel, DEASchedule>(dEAScheduleDetails);

                    await masterDataAddEdit.UpdateDEASchedule(dEASchedule);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.DEASchedule_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, dEAScheduleDetails = dEASchedule }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region MilitaryBranch

        public ActionResult MilitaryBranch()
        {
            return View();
        }

        public ActionResult AddMilitaryBranch(MilitaryBranchViewModel militaryBranchDetails)
        {
            string status = "true";

            MilitaryBranch militaryBranch = null;

            try
            {

                if (ModelState.IsValid)
                {

                    militaryBranch = AutoMapper.Mapper.Map<MilitaryBranchViewModel, MilitaryBranch>(militaryBranchDetails);
                    masterDataAddEdit.AddMilitaryBranch(militaryBranch);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Branch_ADD_EXCEPTION;
            }


            return Json(new { status = status, militaryBranchDetails = militaryBranch }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateMilitaryBranch(MilitaryBranchViewModel militaryBranchDetails)
        {
            string status = "true";

            MilitaryBranch militaryBranch = null;

            try
            {

                if (ModelState.IsValid)
                {

                    militaryBranch = AutoMapper.Mapper.Map<MilitaryBranchViewModel, MilitaryBranch>(militaryBranchDetails);
                    masterDataAddEdit.UpdateMilitaryBranch(militaryBranch);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Branch_UPDATE_EXCEPTION;
            }


            return Json(new { status = status, militaryBranchDetails = militaryBranch }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region MilitaryBranch

        public ActionResult MilitaryRank()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMilitaryRank(MilitaryRankViewModel militaryRankDetails)
        {
            string status = "true";

            MilitaryRank militaryRank = null;

            try
            {

                if (ModelState.IsValid)
                {

                    militaryRank = AutoMapper.Mapper.Map<MilitaryRankViewModel, MilitaryRank>(militaryRankDetails);
                    int id = masterDataAddEdit.AddMilitaryRank(militaryRank);
                    militaryRank.MilitaryRankID = id;
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Rank_ADD_EXCEPTION;
            }


            return Json(new { status = status, militaryRankDetails = militaryRank }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateMilitaryRank(MilitaryRankViewModel militaryRankDetails)
        {
            string status = "true";

            MilitaryRank militaryRank = null;

            try
            {

                if (ModelState.IsValid)
                {

                    militaryRank = AutoMapper.Mapper.Map<MilitaryRankViewModel, MilitaryRank>(militaryRankDetails);
                    masterDataAddEdit.UpdateMilitaryRank(militaryRank);


                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Rank_UPDATE_EXCEPTION;
            }


            return Json(new { status = status, militaryRankDetails = militaryRank }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult> SaveGroup(Group group)
        {
            string ErrorMessage = "";
            int GroupId = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    //GroupSave Method Here according to Id update group not then insert new group

                }
                else
                {
                    ErrorMessage = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                ErrorMessage = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                ErrorMessage = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { group = group, errorMessage = ErrorMessage }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MasterDataNew/AdmittingPrivileges
        public ActionResult AdmittingPrivileges()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmittingPrivileges(AdmittingPrivilegeViewModel admitingPrivilege)
        {
            string status = "true";
            AdmittingPrivilege privilege = null;

            try
            {
                if (ModelState.IsValid)
                {
                    privilege = AutoMapper.Mapper.Map<AdmittingPrivilegeViewModel, AdmittingPrivilege>(admitingPrivilege);

                    privilege.AdmittingPrivilegeID = masterDataAddEdit.AddAdmittingPrivileges(privilege);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Admitting_Privileges_ADD_EXCEPTION;
            }

            return Json(new { status = status, admitingPrivilege = privilege }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAdmittingPrivileges(AdmittingPrivilegeViewModel admitingPrivilege)
        {
            string status = "true";
            AdmittingPrivilege privilege = null;

            try
            {
                if (ModelState.IsValid)
                {
                    privilege = AutoMapper.Mapper.Map<AdmittingPrivilegeViewModel, AdmittingPrivilege>(admitingPrivilege);

                    await masterDataAddEdit.UpdateAdmittingPrivilege(privilege);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Admitting_Privileges_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, admitingPrivilege = privilege }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MasterDataNew/Certifications
        public ActionResult Certifications()
        {
            return View();
        }

        public ActionResult AddCertifications(CertificationViewModel certificationDetails)
        {
            string status = "true";
            Certification certification = null;

            try
            {
                if (ModelState.IsValid)
                {
                    certification = AutoMapper.Mapper.Map<CertificationViewModel, Certification>(certificationDetails);

                    certification.CertificationID = masterDataAddEdit.AddCertification(certification);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Certification_ADD_EXCEPTION;
            }

            return Json(new { status = status, certificationDetails = certification }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateCertifications(CertificationViewModel certificationDetails)
        {
            string status = "true";
            Certification certification = null;

            try
            {
                if (ModelState.IsValid)
                {
                    certification = AutoMapper.Mapper.Map<CertificationViewModel, Certification>(certificationDetails);

                    await masterDataAddEdit.UpdateCertification(certification);


                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Certification_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, certificationDetails = certification }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddHospital(HospitalViewModel hospitalDetails)
        {
            string status = "true";

            Hospital hospital = null;

            try
            {

                if (ModelState.IsValid)
                {

                    hospital = AutoMapper.Mapper.Map<HospitalViewModel, Hospital>(hospitalDetails);
                    masterDataAddEdit.AddHospital(hospital);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Hospital_ADD_EXCEPTION;
            }


            return Json(new { status = status, hospitalDetails = hospital }, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public ActionResult AddHospitalContactInfo(int hospitalId, HospitalContactInfoViewModel contactDetails)
        {
            string status = "true";

            HospitalContactInfo hospital = null;

            try
            {

                if (ModelState.IsValid)
                {

                    hospital = AutoMapper.Mapper.Map<HospitalContactInfoViewModel, HospitalContactInfo>(contactDetails);
                    masterDataAddEdit.AddHospitalContactInfo(hospitalId, hospital);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Hospital_Contact_ADD_EXCEPTION;
            }


            return Json(new { status = status, contactDetails = hospital }, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public ActionResult AddHospitalContactPerson(int hospitalId, int contactId, HospitalContactPersonViewModel personDetails)
        {
            string status = "true";

            HospitalContactPerson person = null;

            try
            {

                if (ModelState.IsValid)
                {

                    person = AutoMapper.Mapper.Map<HospitalContactPersonViewModel, HospitalContactPerson>(personDetails);
                    masterDataAddEdit.AddHospitalContactPerson(hospitalId, contactId, person);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Hospital_Contact_Person_ADD_EXCEPTION;
            }


            return Json(new { status = status, personDetails = person }, JsonRequestBehavior.AllowGet);


        }


        public ActionResult UpdateHospital(HospitalOnlyViewModel hospitalDetails)
        {

            string status = "true";

            Hospital hospital = null;

            try
            {

                if (ModelState.IsValid)
                {

                    hospital = AutoMapper.Mapper.Map<HospitalOnlyViewModel, Hospital>(hospitalDetails);
                    masterDataAddEdit.UpdateHospital(hospital);
                }

                else
                {

                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }

            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Hospital_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, hospitalDetails = hospital }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult UpdateHospitalContactInfo(HospitalContactInfoViewModel hospitalContactInfoViewModel)
        {

            string status = "true";
            HospitalContactInfo hospitalContactInfo = null;

            try
            {

                hospitalContactInfo = AutoMapper.Mapper.Map<HospitalContactInfoViewModel, HospitalContactInfo>(hospitalContactInfoViewModel);
                masterDataAddEdit.UpdateHospitalContact(hospitalContactInfo);

            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Hospital_Contact_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, HospitalContact = hospitalContactInfo }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult UpdateHospitalContactPerson(HospitalContactPersonViewModel hospitalContactPersonViewModel)
        {
            string status = "true";
            HospitalContactPerson hospitalContactPerson = null;

            hospitalContactPerson = AutoMapper.Mapper.Map<HospitalContactPersonViewModel, HospitalContactPerson>(hospitalContactPersonViewModel);
            masterDataAddEdit.UpdateHospitalContactPerson(hospitalContactPerson);

            return Json(new { status = status, HospitalContactPerson = hospitalContactPerson }, JsonRequestBehavior.AllowGet);
        }


        #region Insurance Carrier

        public ActionResult InsuranceCarrier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddInsuranceCarrier(InsuranceCarrierViewModel insuranceDetails)
        {
            string status = "true";

            InsuranceCarrier insurance = null;

            try
            {

                if (ModelState.IsValid)
                {

                    insurance = AutoMapper.Mapper.Map<InsuranceCarrierViewModel, InsuranceCarrier>(insuranceDetails);
                    masterDataAddEdit.AddInsuranceCarrier(insurance);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Insurance_Carrier_ADD_EXCEPTION;
            }


            return Json(new { status = status, insuranceDetails = insurance }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult AddInsuranceCarrierAddress(int insuranceId, InsuranceCarrierAddressViewModel addressDetails)
        {
            string status = "true";

            InsuranceCarrierAddress address = null;

            try
            {

                if (ModelState.IsValid)
                {

                    address = AutoMapper.Mapper.Map<InsuranceCarrierAddressViewModel, InsuranceCarrierAddress>(addressDetails);
                    masterDataAddEdit.AddInsuranceCarrierAddress(insuranceId, address);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Insurance_Carrier_Address_ADD_EXCEPTION;
            }


            return Json(new { status = status, addressDetails = address }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult UpdateInsuranceCarrier(InsuranceCarrierViewModel insuranceDetails)
        {

            string status = "true";
            InsuranceCarrier insurance = null;

            try
            {

                insurance = AutoMapper.Mapper.Map<InsuranceCarrierViewModel, InsuranceCarrier>(insuranceDetails);
                masterDataAddEdit.UpdateInsuranceCarrier(insurance);

            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Insurance_Carrier_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, insuranceDetails = insurance }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UpdateInsuranceCarrierAddress(InsuranceCarrierAddressViewModel addressDetails)
        {

            string status = "true";
            InsuranceCarrierAddress insurance = null;

            try
            {

                insurance = AutoMapper.Mapper.Map<InsuranceCarrierAddressViewModel, InsuranceCarrierAddress>(addressDetails);
                masterDataAddEdit.UpdateInsuranceCarrierAddress(insurance);

            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Insurance_Carrier_Address_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, addressDetails = insurance }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region MilitaryDischarges

        public ActionResult MilitaryDischarges()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMilitaryDischarges(MilitaryDischargeViewModel militaryDischargeDetails)
        {
            string status = "true";
            MilitaryDischarge militaryDischarge = null;

            try
            {
                if (ModelState.IsValid)
                {
                    militaryDischarge = AutoMapper.Mapper.Map<MilitaryDischargeViewModel, MilitaryDischarge>(militaryDischargeDetails);

                    militaryDischarge.MilitaryDischargeID = masterDataAddEdit.AddMilitaryDischarge(militaryDischarge);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Discharge_ADD_EXCEPTION;
            }

            return Json(new { status = status, militaryDischargeDetails = militaryDischarge }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMilitaryDischarges(MilitaryDischargeViewModel militaryDischargeDetails)
        {
            string status = "true";
            MilitaryDischarge militaryDischarge = null;

            try
            {
                if (ModelState.IsValid)
                {
                    militaryDischarge = AutoMapper.Mapper.Map<MilitaryDischargeViewModel, MilitaryDischarge>(militaryDischargeDetails);

                    await masterDataAddEdit.UpdateMilitaryDischarge(militaryDischarge);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Discharge_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, militaryDischargeDetails = militaryDischarge }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region MilitaryPresentDuties

        public ActionResult MilitaryPresentDuties()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMilitaryPresentDuties(MilitaryPresentDutyViewModel militaryPresentDutyDetails)
        {
            string status = "true";
            MilitaryPresentDuty militaryPresentDuty = null;

            try
            {
                if (ModelState.IsValid)
                {
                    militaryPresentDuty = AutoMapper.Mapper.Map<MilitaryPresentDutyViewModel, MilitaryPresentDuty>(militaryPresentDutyDetails);

                    militaryPresentDuty.MilitaryPresentDutyID = masterDataAddEdit.AddMilitaryPresentDuty(militaryPresentDuty);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Present_Duty_ADD_EXCEPTION;
            }

            return Json(new { status = status, militaryPresentDutyDetails = militaryPresentDuty }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateMilitaryPresentDuties(MilitaryPresentDutyViewModel militaryPresentDutyDetails)
        {
            string status = "true";
            MilitaryPresentDuty militaryPresentDuty = null;

            try
            {
                if (ModelState.IsValid)
                {
                    militaryPresentDuty = AutoMapper.Mapper.Map<MilitaryPresentDutyViewModel, MilitaryPresentDuty>(militaryPresentDutyDetails);

                    await masterDataAddEdit.UpdateMilitaryPresentDuty(militaryPresentDuty);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Military_Present_Duty_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, militaryPresentDutyDetails = militaryPresentDuty }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PracticeAccessibilityQuestions

        public ActionResult PracticeAccessibilityQuestions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPracticeAccessibilityQuestions(PracticeAccessibilityQuestionViewModel practiceAccessibilityQuestionDetails)
        {
            string status = "true";
            FacilityAccessibilityQuestion practiceAccessibilityQuestion = null;

            try
            {
                if (ModelState.IsValid)
                {
                    practiceAccessibilityQuestion = AutoMapper.Mapper.Map<PracticeAccessibilityQuestionViewModel, FacilityAccessibilityQuestion>(practiceAccessibilityQuestionDetails);

                    practiceAccessibilityQuestion.FacilityAccessibilityQuestionId = masterDataAddEdit.AddPracticeAccessibilityQuestions(practiceAccessibilityQuestion);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Practice_Accessibility_Questions_ADD_EXCEPTION;
            }

            return Json(new { status = status, practiceAccessibilityQuestionDetails = practiceAccessibilityQuestion }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePracticeAccessibilityQuestions(PracticeAccessibilityQuestionViewModel practiceAccessibilityQuestionDetails)
        {
            string status = "true";
            FacilityAccessibilityQuestion practiceAccessibilityQuestion = null;

            try
            {
                if (ModelState.IsValid)
                {
                    practiceAccessibilityQuestion = AutoMapper.Mapper.Map<PracticeAccessibilityQuestionViewModel, FacilityAccessibilityQuestion>(practiceAccessibilityQuestionDetails);

                    await masterDataAddEdit.UpdatePracticeAccessibilityQuestions(practiceAccessibilityQuestion);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Practice_Accessibility_Questions_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, practiceAccessibilityQuestionDetails = practiceAccessibilityQuestion }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PracticeServiceQuestions

        public ActionResult PracticeServiceQuestions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPracticeServiceQuestions(PracticeServiceQuestionViewModel practiceServiceQuestionDetails)
        {
            string status = "true";
            FacilityServiceQuestion practiceServiceQuestion = null;

            try
            {
                if (ModelState.IsValid)
                {
                    practiceServiceQuestion = AutoMapper.Mapper.Map<PracticeServiceQuestionViewModel, FacilityServiceQuestion>(practiceServiceQuestionDetails);

                    practiceServiceQuestion.FacilityServiceQuestionID = masterDataAddEdit.AddPracticeServiceQuestion(practiceServiceQuestion);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Practice_Service_Question_ADD_EXCEPTION;
            }

            return Json(new { status = status, practiceServiceQuestionDetails = practiceServiceQuestion }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePracticeServiceQuestions(PracticeServiceQuestionViewModel practiceServiceQuestionDetails)
        {
            string status = "true";
            FacilityServiceQuestion practiceServiceQuestion = null;

            try
            {
                if (ModelState.IsValid)
                {
                    practiceServiceQuestion = AutoMapper.Mapper.Map<PracticeServiceQuestionViewModel, FacilityServiceQuestion>(practiceServiceQuestionDetails);

                    await masterDataAddEdit.UpdatePracticeServiceQuestion(practiceServiceQuestion);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Practice_Service_Question_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, practiceServiceQuestionDetails = practiceServiceQuestion }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PracticeOpenStatusQuestions

        public ActionResult PracticeOpenStatusQuestions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPracticeOpenStatusQuestions(PracticeOpenStatusQuestionViewModel practiceOpenStatusQuestionDetails)
        {
            string status = "true";
            PracticeOpenStatusQuestion practiceOpenStatusQuestion = null;

            try
            {
                if (ModelState.IsValid)
                {
                    practiceOpenStatusQuestion = AutoMapper.Mapper.Map<PracticeOpenStatusQuestionViewModel, PracticeOpenStatusQuestion>(practiceOpenStatusQuestionDetails);

                    practiceOpenStatusQuestion.PracticeOpenStatusQuestionID = masterDataAddEdit.AddPracticeOpenStatusQuestion(practiceOpenStatusQuestion);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Practice_OpenStatus_Question_ADD_EXCEPTION;
            }

            return Json(new { status = status, practiceOpenStatusQuestionDetails = practiceOpenStatusQuestion }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePracticeOpenStatusQuestions(PracticeOpenStatusQuestionViewModel practiceOpenStatusQuestionDetails)
        {
            string status = "true";
            PracticeOpenStatusQuestion practiceOpenStatusQuestion = null;

            try
            {
                if (ModelState.IsValid)
                {
                    practiceOpenStatusQuestion = AutoMapper.Mapper.Map<PracticeOpenStatusQuestionViewModel, PracticeOpenStatusQuestion>(practiceOpenStatusQuestionDetails);

                    await masterDataAddEdit.UpdatePracticeOpenStatusQuestion(practiceOpenStatusQuestion);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Practice_OpenStatus_Question_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, practiceOpenStatusQuestionDetails = practiceOpenStatusQuestion }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProviderTypes

        public ActionResult ProviderTypes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProviderTypes(ProviderTypeViewModel providerTypeDetails)
        {
            string status = "true";
            ProviderType providerType = null;

            try
            {
                if (ModelState.IsValid)
                {
                    providerType = AutoMapper.Mapper.Map<ProviderTypeViewModel, ProviderType>(providerTypeDetails);

                    providerType.ProviderTypeID = masterDataAddEdit.AddProviderType(providerType);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.PROVIDERTYPE_ADD_EXCEPTION;
            }

            return Json(new { status = status, providerTypeDetails = providerType }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProviderTypes(ProviderTypeViewModel providerTypeDetails)
        {
            string status = "true";
            ProviderType providerType = null;

            try
            {
                if (ModelState.IsValid)
                {
                    providerType = AutoMapper.Mapper.Map<ProviderTypeViewModel, ProviderType>(providerTypeDetails);

                    await masterDataAddEdit.UpdateProviderType(providerType);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.PROVIDERTYPE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, providerTypeDetails = providerType }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        //
        // GET: /MasterDataNew/QualificationDegrees
        public ActionResult QualificationDegrees()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQualificationDegrees(QualificationDegreeViewModel qualificationDegreeDetails)
        {
            string status = "true";
            QualificationDegree qualificationDegree = null;

            try
            {
                if (ModelState.IsValid)
                {
                    qualificationDegree = AutoMapper.Mapper.Map<QualificationDegreeViewModel, QualificationDegree>(qualificationDegreeDetails);

                    qualificationDegree.QualificationDegreeID = masterDataAddEdit.AddQualificationDegree(qualificationDegree);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Qualification_Degree_ADD_EXCEPTION;
            }

            return Json(new { status = status, qualificationDegreeDetails = qualificationDegree }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateQualificationDegrees(QualificationDegreeViewModel qualificationDegreeDetails)
        {
            string status = "true";
            QualificationDegree qualificationDegree = null;

            try
            {
                if (ModelState.IsValid)
                {
                    qualificationDegree = AutoMapper.Mapper.Map<QualificationDegreeViewModel, QualificationDegree>(qualificationDegreeDetails);

                    await masterDataAddEdit.UpdateQualificationDegree(qualificationDegree);

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Qualification_Degree_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, qualificationDegreeDetails = qualificationDegree }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MasterDataNew/Hospitals
        public ActionResult Hospitals()
        {
            return View();
        }

        #region Questions

        public ActionResult Questions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(QuestionViewModel questionDetails)
        {
            string status = "true";

            Question question = null;

            try
            {

                if (ModelState.IsValid)
                {

                    question = AutoMapper.Mapper.Map<QuestionViewModel, Question>(questionDetails);
                    masterDataAddEdit.AddQuestion(question);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Question_ADD_EXCEPTION;
            }


            return Json(new { status = status, questionDetails = question }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuestion(QuestionViewModel questionDetails)
        {
            string status = "true";

            Question question = null;

            try
            {

                if (ModelState.IsValid)
                {

                    question = AutoMapper.Mapper.Map<QuestionViewModel, Question>(questionDetails);
                    masterDataAddEdit.UpdateQuestion(question);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Question_UPDATE_EXCEPTION;
            }


            return Json(new { status = status, questionDetails = question }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Question Category

        [HttpPost]
        public ActionResult AddQuestionCategory(QuestionCategoryViewModel questionCategoryDetails)
        {
            string status = "true";

            QuestionCategory questionCategory = null;

            try
            {

                if (ModelState.IsValid)
                {

                    questionCategory = AutoMapper.Mapper.Map<QuestionCategoryViewModel, QuestionCategory>(questionCategoryDetails);
                    masterDataAddEdit.AddQuestionCategory(questionCategory);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Question_Category_ADD_EXCEPTION;
            }


            return Json(new { status = status, questionCategoryDetails = questionCategory }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuestionCategory(QuestionCategoryViewModel questionCategoryDetails)
        {
            string status = "true";

            QuestionCategory questionCategory = null;

            try
            {

                if (ModelState.IsValid)
                {

                    questionCategory = AutoMapper.Mapper.Map<QuestionCategoryViewModel, QuestionCategory>(questionCategoryDetails);
                    masterDataAddEdit.UpdateQuestionCategory(questionCategory);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.Question_Category_UPDATE_EXCEPTION;
            }


            return Json(new { status = status, questionCategoryDetails = questionCategory }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region City

        public ActionResult Cities()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCities(CitiesViewModel cityDetails)
        {
            string status = "true";

            City city = null;

            try
            {

                if (ModelState.IsValid)
                {

                    city = AutoMapper.Mapper.Map<CitiesViewModel, City>(cityDetails);
                   city.CityID = masterDataAddEdit.AddCity(cityDetails.StateID, city);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }

            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.City_ADD_EXCEPTION;
            }


            return Json(new { status = status, cityDetails = city }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> UpdateCity(CitiesViewModel cityDetails)
        {
            return null;
        }

        #endregion


        public async Task<JsonResult> GetAllPlans() 
        {
            var data = await masterDataManager.GetAllPlanAsync();

            foreach (Plan plan in data.ToList())
            {
                foreach (PlanLOB planLOB in plan.PlanLOBs)
                {
                    planLOB.Plan = null;
                }
            }


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllLobs()  
        {
            var data = await masterDataManager.GetAllLOBAsync();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}