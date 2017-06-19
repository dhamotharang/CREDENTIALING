using AHC.CD.Business.MasterData;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation;
using AHC.CD.WebUI.MVC.CustomHelpers;
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

        /// <summary>
        /// Method to get all Email Templates
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllEmailTemplates()
        {
            var data = await masterDataManager.GetAllEmailTemplatesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //get all AdmittingPrivileges data

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllAdmittingPrivileges()
        {
            var data = await masterDataManager.GetAllAdmittingPrivilegeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryPresentDuties()
        {
            var data = await masterDataManager.GetAllMilitaryPresentDutyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryDischarges()
        {
            var data = await masterDataManager.GetAllMilitaryDischargeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllCertificates()
        {
            var data = await masterDataManager.GetAllCertificationAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllQualificationDegrees()
        {
            var data = await masterDataManager.GetAllQualificationDegreeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllSpecialities()
        {
            var data = await masterDataManager.GetAllSpecialtyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllSchools()
        {
            var data = await masterDataManager.GetAllSchoolAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllspecialtyBoards()
        {
            var data = await masterDataManager.GetAllspecialtyBoardAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllVisaTypes()
        {
            var data = await masterDataManager.GetAllVisaTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllInsuaranceCompanyNames()
        {
            var data = await masterDataManager.GetAllInsuaranceCompanyNameAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllVisaStatus()
        {
            var data = await masterDataManager.GetAllVisaStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllLicenseStatus()
        {
            var data = await masterDataManager.GetAllLicenseStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllStaffCategories()
        {
            var data = await masterDataManager.GetAllStaffCategoryAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllPracticeServiceQuestions()
        {
            var data = await masterDataManager.GetAllPracticeServiceQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllPracticeOpenStatusQuestions()
        {
            var data = await masterDataManager.GetAllPracticeOpenStatusQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllPracticeAccessibilityQuestions()
        {
            var data = await masterDataManager.GetAllPracticeAccessibilityQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllProviderTypes()
        {
            var data = await masterDataManager.GetAllProviderTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllProviderLevels()
        {
            var data = await masterDataManager.GetAllProviderLevelsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllOrganizationGroupAsync()
        {
            var data = await masterDataManager.GetAllOrganizationGroupAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllDEASchedules()
        {
            var data = await masterDataManager.GetAllDEAScheduleAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllHospitals()
        {
            var data = await masterDataManager.GetAllHospitalAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllInsuranceCarriers()
        {
            var data = await masterDataManager.GetAllInsuranceCarrierAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllQuestions()
        {
            var data = await masterDataManager.GetAllQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllQuestionCategories()
        {
            var data = await masterDataManager.GetAllQuestionCategoriesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryBranches()
        {
            var data = await masterDataManager.GetAllMilitaryBranchAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryRanks()
        {
            var data = await masterDataManager.GetAllMilitaryRanks();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllCities()
        {
            var data = await masterDataManager.GetCitiesAllAsync();
            return JsonConvert.SerializeObject(data);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllGroups()
        {
            var data = await masterDataManager.GetAllGroupsAsync();
            return JsonConvert.SerializeObject(data);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllOrganizationGroups()
        {
            var data = await masterDataManager.GetAllOrganizationGroupsAsync();
            return JsonConvert.SerializeObject(data);
        }

        #endregion

        #region Add/Edit Master Data

        public ActionResult Index()
        {
            return View();
        }

        #region Organization Groups
        public ActionResult OrganizationGroups()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrganizationGroups(MasterDataOrganizationGroupViewModel organizationGroupDetails)
        {
            string status = "true";
            OrganizationGroup organizationGroup = null;

            try
            {
                if (ModelState.IsValid)
                {
                    organizationGroup = AutoMapper.Mapper.Map<MasterDataOrganizationGroupViewModel, OrganizationGroup>(organizationGroupDetails);

                    organizationGroup.OrganizationGroupID = masterDataAddEdit.AddOrganizationGroup(organizationGroup);
                    var urlToRemove = Url.Action("GetAllOrganizationGroups", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllOrganizationGroupAsync", "MasterData", new { Area = "Profile" });
                    var urlToRemove2 = Url.Action("GetAllOrganizationGroupAsync", "MasterDataNew");
                    HttpResponse.RemoveOutputCacheItem(urlToRemove2);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
        public async Task<ActionResult> UpdateOrganizationGroups(MasterDataOrganizationGroupViewModel organizationGroupDetails)
        {
            string status = "true";
            OrganizationGroup organizationGroup = null;

            try
            {
                if (ModelState.IsValid)
                {
                    organizationGroup = AutoMapper.Mapper.Map<MasterDataOrganizationGroupViewModel, OrganizationGroup>(organizationGroupDetails);

                    await masterDataAddEdit.UpdateOrganizationGroup(organizationGroup);
                    var urlToRemove = Url.Action("GetAllOrganizationGroups", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllOrganizationGroupAsync", "MasterData", new { Area = "Profile" });
                    var urlToRemove2 = Url.Action("GetAllOrganizationGroupAsync", "MasterDataNew");
                    HttpResponse.RemoveOutputCacheItem(urlToRemove2);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllGroups", "MasterDataNew");
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllGroups", "MasterDataNew");
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllProviderLevels", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllProviderLevels", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllProviderLevels", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllProviderLevels", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllVisaTypes", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllVisaTypes", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllVisaTypes", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllVisaTypes", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllVisaStatus", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllVisaStatus", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllVisaStatus", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllVisaStatus", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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

        #region Insuarance Company Name
        public ActionResult InsuaranceCompanyName()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddInsuaranceCompany(InsuaranceCompanyNameViewModel insuaranceCompanyNameDetails)
        {

            string status = "true";
            InsuaranceCompanyName insuaranceCompanyName = null;

            try
            {
                if (ModelState.IsValid)
                {
                    insuaranceCompanyName = AutoMapper.Mapper.Map<InsuaranceCompanyNameViewModel, InsuaranceCompanyName>(insuaranceCompanyNameDetails);

                    insuaranceCompanyName.InsuaranceCompanyNameID = masterDataAddEdit.AddInsuaranceCompany(insuaranceCompanyName);
                    var urlToRemove = Url.Action("GetAllInsuaranceCompanyNames", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllInsuaranceCompanyNames", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.Insuarance_Company_Name_ADD_EXCEPTION;
            }

            return Json(new { status = status, InsuaranceCompanyNameDetails = insuaranceCompanyName }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public async Task<ActionResult> UpdateInsuaranceCompanyNames(InsuaranceCompanyNameViewModel insuaranceCompanyNameDetails)
        {
            string status = "true";
            InsuaranceCompanyName insuaranceCompanyName = null;

            try
            {
                if (ModelState.IsValid)
                {
                    insuaranceCompanyName = AutoMapper.Mapper.Map<InsuaranceCompanyNameViewModel, InsuaranceCompanyName>(insuaranceCompanyNameDetails);

                    await masterDataAddEdit.UpdateInsuaranceCompany(insuaranceCompanyName);
                    var urlToRemove = Url.Action("GetAllInsuaranceCompanyNames", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllInsuaranceCompanyNames", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.Insuarance_Company_Name_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, insuaranceCompanyNameDetails = insuaranceCompanyName }, JsonRequestBehavior.AllowGet);

        }

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
                    var urlToRemove = Url.Action("GetAllLicenseStatus", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllLicenseStatus", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
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
                    var urlToRemove = Url.Action("GetAllLicenseStatus", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllLicenseStatus", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllStaffCategories", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllStaffCategories", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllStaffCategories", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllStaffCategories", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllspecialtyBoards", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllspecialtyBoards", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllspecialtyBoards", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllspecialtyBoards", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllSpecialities", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllSpecialities", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllSpecialities", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllSpecialities", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllSchools", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllSchools", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
        public ActionResult AddSchoolContactInfo(int SchoolId, SchoolContactInfoViewModel contactDetails)
        {
            string status = "true";

            SchoolContactInfo SchoolCon = null;

            try
            {

                if (ModelState.IsValid)
                {

                    SchoolCon = AutoMapper.Mapper.Map<SchoolContactInfoViewModel, SchoolContactInfo>(contactDetails);
                    masterDataAddEdit.AddSchoolContactInfo(SchoolId, SchoolCon);
                    var urlToRemove = Url.Action("GetAllSchools", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllSchools", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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


            return Json(new { status = status, contactDetails = SchoolCon }, JsonRequestBehavior.AllowGet);


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
                    var urlToRemove = Url.Action("GetAllSchools", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllSchools", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);

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

        [HttpPost]
        public ActionResult UpdateSchoolContactInfo(SchoolContactInfoViewModel schoolContactDetails)
        {
            string status = "true";
            SchoolContactInfo schoolContactInfo = null;

            try
            {
                if (ModelState.IsValid)
                {
                    schoolContactInfo = AutoMapper.Mapper.Map<SchoolContactInfoViewModel, SchoolContactInfo>(schoolContactDetails);

                    masterDataAddEdit.UpdateSchoolContactInfo(schoolContactInfo);
                    var urlToRemove = Url.Action("GetAllSchools", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllSchools", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.SCHOOL_CONTACT_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, schoolContactDetails = schoolContactInfo }, JsonRequestBehavior.AllowGet);
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
                    var urlToRemove = Url.Action("GetAllDEASchedules", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllDEASchedules", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);

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
                    var urlToRemove = Url.Action("GetAllDEASchedules", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllDEASchedules", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryBranches", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryBranches", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryBranches", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryBranches", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryRanks", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryRanks", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryRanks", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryRanks", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);

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
                    var urlToRemove = Url.Action("GetAllAdmittingPrivileges", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllAdmittingPrivileges", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllAdmittingPrivileges", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllAdmittingPrivileges", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllCertificates", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllCertificates", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllCertificates", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllCertificates", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);

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
                    var urlToRemove = Url.Action("GetAllHospitals", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllHospitals", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllHospitalContactInfoes", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllHospitalContactPersons", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllHospitals", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllHospitals", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                var urlToRemove = Url.Action("GetAllHospitalContactInfoes", "MasterData", new { Area = "Profile" });
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
            var urlToRemove = Url.Action("GetAllHospitalContactPersons", "MasterData", new { Area = "Profile" });
            HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllInsuranceCarriers", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllInsuranceCarriers", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllInsuranceCarriers", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllInsuranceCarriers", "MasterData", new { Area = "Profile" });
                    var urlToRemove2 = Url.Action("GetAllInsuranceCarrierAddresses", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove2);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                var urlToRemove = Url.Action("GetAllInsuranceCarriers", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetAllInsuranceCarriers", "MasterData", new { Area = "Profile" });
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                HttpResponse.RemoveOutputCacheItem(urlToRemove);

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
                var urlToRemove = Url.Action("GetAllInsuranceCarriers", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetAllInsuranceCarriers", "MasterData", new { Area = "Profile" });
                var urlToRemove2 = Url.Action("GetAllInsuranceCarrierAddresses", "MasterData", new { Area = "Profile" });
                HttpResponse.RemoveOutputCacheItem(urlToRemove2);
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryDischarges", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryDischarges", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryDischarges", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryDischarges", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryPresentDuties", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryPresentDuties", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllMilitaryPresentDuties", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllMilitaryPresentDuties", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllPracticeAccessibilityQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllPracticeAccessibilityQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllPracticeAccessibilityQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllPracticeAccessibilityQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllPracticeServiceQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllPracticeServiceQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllPracticeServiceQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllPracticeServiceQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllPracticeOpenStatusQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllPracticeOpenStatusQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllPracticeOpenStatusQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllPracticeOpenStatusQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllProviderTypes", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllProviderTypes", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllProviderTypes", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllProviderTypes", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllQualificationDegrees", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllQualificationDegrees", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllQualificationDegrees", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllQualificationDegrees", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllQuestions", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllQuestions", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllQuestionCategories", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllQuestionCategories", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                    var urlToRemove = Url.Action("GetAllQuestionCategories", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllQuestionCategories", "MasterData", new { Area = "Profile" });
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
        IEnumerable<LocationDetail> locations = null;
        [HttpPost]
        public async Task<ActionResult> AddCities(CitiesViewModel cityDetails)
        {
            string status = "true";

            City city = null;

            try
            {

                if (ModelState.IsValid)
                {

                    city = AutoMapper.Mapper.Map<CitiesViewModel, City>(cityDetails);
                    city.CityID = masterDataAddEdit.AddCity(cityDetails.StateID, city);

                    // Jugad Method to override another Jugad Method written in Location Controller
                    var data = await masterDataManager.GetCitiesAllAsync();
                    locations = data.ToList();
                    this.ControllerContext.HttpContext.Cache["LOCATIONS"] = locations;
                    var urlToRemove = Url.Action("GetAllCities", "MasterDataNew");
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
        public ActionResult UpdateCity(CitiesViewModel cityDetails)
        {
            string status = "true";

            City city = null;

            try
            {

                if (ModelState.IsValid)
                {

                    city = AutoMapper.Mapper.Map<CitiesViewModel, City>(cityDetails);
                    masterDataAddEdit.UpdateCity(city);
                    var urlToRemove = Url.Action("GetAllCities", "MasterDataNew");
                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.CITY_UPDATE_EXCEPTION;
            }


            return Json(new { status = status, cityDetails = city }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData", VaryByParam = "planID")]
        public async Task<JsonResult> GetAllLOBsOfPlanContractByPlanID(int planID)
        {
            //var data = await masterDataManager.GetAllLOBsOfPlanContractByPlanIDAsync(planID);
            var data = await masterDataManager.GetAllLOBsOfPlanByPlanIDAsync(planID);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllPlans()
        {
            var data = await masterDataManager.GetAllPlanAsync();

            foreach (Plan plan in data.ToList())
            {
                foreach (PlanLOB planLOB in plan.PlanLOBs)
                {
                    planLOB.Plan = null;
                }
            }

            return JsonConvert.SerializeObject(data);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]

        public async Task<JsonResult> GetAllPlanNames()
        {
            var data = await masterDataManager.GetAllPlanNamesAsync();


            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllInactivePlans()
        {
            var data = await masterDataManager.GetAllInactivePlansAsync();

            foreach (Plan plan in data.ToList())
            {
                foreach (PlanLOB planLOB in plan.PlanLOBs)
                {
                    planLOB.Plan = null;
                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllLobs()
        {
            var data = await masterDataManager.GetAllLOBAsync();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #region Office Manager

        public ActionResult OfficeManager()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllMasterBusinessContactPerson()
        {
            var data = await masterDataManager.GetAllMasterBusinessContactPersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddOfficeManager(FacilityEmployeeMasterDataViewModel officeManager)
        {
            string status = "true";
            MasterEmployee dataModelOfficeManager = null;
            try
            {
                dataModelOfficeManager = AutoMapper.Mapper.Map<FacilityEmployeeMasterDataViewModel, MasterEmployee>(officeManager);
                dataModelOfficeManager = await masterDataAddEdit.SaveOfficeManager(dataModelOfficeManager);
                var urlToRemove = Url.Action("GetAllMasterBusinessContactPerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.OFFICE_MANAGER_MASTER_DATA_ADD_EXCEPTION;
            }
            return Json(new { status = status, officeManager = dataModelOfficeManager }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateOfficeManager(FacilityEmployeeMasterDataViewModel officeManager)
        {
            string status = "true";
            MasterEmployee dataModelOfficeManager = null;
            try
            {
                dataModelOfficeManager = AutoMapper.Mapper.Map<FacilityEmployeeMasterDataViewModel, MasterEmployee>(officeManager);
                dataModelOfficeManager = await masterDataAddEdit.SaveOfficeManager(dataModelOfficeManager);
                var urlToRemove = Url.Action("GetAllMasterBusinessContactPerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.OFFICE_MANAGER_MASTER_DATA_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, officeManager = dataModelOfficeManager }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Billing Contact

        public ActionResult BillingContact()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllMasterBillingContactPerson()
        {
            var data = await masterDataManager.GetAllMasterBillingContactPersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddBillingContact(FacilityEmployeeMasterDataViewModel billingContact)
        {
            string status = "true";
            MasterEmployee dataModelBillingContact = null;
            try
            {
                dataModelBillingContact = AutoMapper.Mapper.Map<FacilityEmployeeMasterDataViewModel, MasterEmployee>(billingContact);
                dataModelBillingContact = await masterDataAddEdit.SaveBillingContact(dataModelBillingContact);
                var urlToRemove = Url.Action("GetAllMasterBillingContactPerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.BILLING_CONTACT_MASTER_DATA_ADD_EXCEPTION;
            }
            return Json(new { status = status, billingContact = dataModelBillingContact }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateBillingContact(FacilityEmployeeMasterDataViewModel billingContact)
        {
            string status = "true";
            MasterEmployee dataModelBillingContact = null;
            try
            {
                dataModelBillingContact = AutoMapper.Mapper.Map<FacilityEmployeeMasterDataViewModel, MasterEmployee>(billingContact);
                dataModelBillingContact = await masterDataAddEdit.SaveBillingContact(dataModelBillingContact);
                var urlToRemove = Url.Action("GetAllMasterBillingContactPerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.BILLING_CONTACT_MASTER_DATA_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, billingContact = dataModelBillingContact }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Payment And Remittance

        public ActionResult PaymentRemittance()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllMasterPaymentRemittancePerson()
        {
            var data = await masterDataManager.GetAllMasterPaymentRemittancePersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddPaymentRemittancePerson(PracticePaymentAndRemittanceMasterDataViewModel paymentRemittancePerson)
        {
            string status = "true";
            MasterPracticePaymentRemittancePerson dataModelPaymentRemittancePerson = null;
            try
            {
                dataModelPaymentRemittancePerson = AutoMapper.Mapper.Map<PracticePaymentAndRemittanceMasterDataViewModel, MasterPracticePaymentRemittancePerson>(paymentRemittancePerson);
                dataModelPaymentRemittancePerson = await masterDataAddEdit.SavePaymentAndRemittance(dataModelPaymentRemittancePerson);
                var urlToRemove = Url.Action("GetAllMasterPaymentRemittancePerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.PAYMENT_REMITTANCE_MASTER_DATA_ADD_EXCEPTION;
            }
            return Json(new { status = status, paymentRemittancePerson = dataModelPaymentRemittancePerson }, JsonRequestBehavior.AllowGet);
        }



        public async Task<JsonResult> UpdatePaymentRemittancePerson(PracticePaymentAndRemittanceMasterDataViewModel paymentRemittancePerson)
        {
            string status = "true";
            MasterPracticePaymentRemittancePerson dataModelPaymentRemittancePerson = null;
            try
            {
                dataModelPaymentRemittancePerson = AutoMapper.Mapper.Map<PracticePaymentAndRemittanceMasterDataViewModel, MasterPracticePaymentRemittancePerson>(paymentRemittancePerson);
                dataModelPaymentRemittancePerson = await masterDataAddEdit.SavePaymentAndRemittance(dataModelPaymentRemittancePerson);
                var urlToRemove = Url.Action("GetAllMasterPaymentRemittancePerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.PAYMENT_REMITTANCE_MASTER_DATA_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, paymentRemittancePerson = dataModelPaymentRemittancePerson }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Partner/Associate/Covering Colleague

        public ActionResult PartnerAssociateCoveringColleague()
        {
            return View();
        }

        #endregion

        #region Credentialing Contact

        public ActionResult CredentialingContact()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllMasterCredentialingContactPerson()
        {
            var data = await masterDataManager.GetAllMasterCredentialingContactPersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddCredentialingContact(FacilityEmployeeMasterDataViewModel credentialingContact)
        {
            string status = "true";
            MasterEmployee dataModelCredentialingContact = null;
            try
            {
                dataModelCredentialingContact = AutoMapper.Mapper.Map<FacilityEmployeeMasterDataViewModel, MasterEmployee>(credentialingContact);
                dataModelCredentialingContact = await masterDataAddEdit.SaveCredentialingContact(dataModelCredentialingContact);
                var urlToRemove = Url.Action("GetAllMasterCredentialingContactPerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.CREDENTIALING_CONTACT_MASTER_DATA_ADD_EXCEPTION;
            }
            return Json(new { status = status, credentialingContact = dataModelCredentialingContact }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateCredentialingContact(FacilityEmployeeMasterDataViewModel credentialingContact)
        {
            string status = "true";
            MasterEmployee dataModelCredentialingContact = null;
            try
            {
                dataModelCredentialingContact = AutoMapper.Mapper.Map<FacilityEmployeeMasterDataViewModel, MasterEmployee>(credentialingContact);
                dataModelCredentialingContact = await masterDataAddEdit.SaveCredentialingContact(dataModelCredentialingContact);
                var urlToRemove = Url.Action("GetAllMasterCredentialingContactPerson", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.CREDENTIALING_CONTACT_MASTER_DATA_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, credentialingContact = dataModelCredentialingContact }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region Profile SubSection

        public ActionResult ProfileSubSection()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllProfileSubSections()
        {
            var data = await masterDataManager.GetAllProfileSubSectionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public async Task<JsonResult> AddProfileSubSection(ProfileSubSectionViewModel profilesubsection)
        {
            string status = "true";
            ProfileSubSection dataModelProfileSubsection = null;
            try
            {
                dataModelProfileSubsection = AutoMapper.Mapper.Map<ProfileSubSectionViewModel, ProfileSubSection>(profilesubsection);
                dataModelProfileSubsection = await masterDataAddEdit.SaveProfileSubSection(dataModelProfileSubsection);
                var urlToRemove = Url.Action("GetAllProfileSubSections", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.PROFILE_SUBSECTION_MASTER_DATA_ADD_EXCEPTION;
            }
            return Json(new { status = status, profileSubSection = dataModelProfileSubsection }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateProfileSubSection(ProfileSubSectionViewModel profilesubsection)
        {
            string status = "true";
            ProfileSubSection dataModelProfileSubsection = null;
            try
            {
                dataModelProfileSubsection = AutoMapper.Mapper.Map<ProfileSubSectionViewModel, ProfileSubSection>(profilesubsection);
                await masterDataAddEdit.UpdateProfileSubSection(dataModelProfileSubsection);
                var urlToRemove = Url.Action("GetAllProfileSubSections", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.PROFILE_SUBSECTION_MASTER_DATA_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, profileSubSection = dataModelProfileSubsection }, JsonRequestBehavior.AllowGet);
        }


        #endregion
        #region Verification links

        public ActionResult VerificationLink()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllVerificationLinks()
        {
            var data = await masterDataManager.GetAllVerificationLinksAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddVerificationLink(VerificationLinkViewModel verificationlink)
        {
            string status = "true";
            VerificationLink dataModelVerificationLink = null;
            try
            {
                dataModelVerificationLink = AutoMapper.Mapper.Map<VerificationLinkViewModel, VerificationLink>(verificationlink);
                dataModelVerificationLink = await masterDataAddEdit.SaveVerificationLink(dataModelVerificationLink);
                var urlToRemove = Url.Action("GetAllVerificationLinks", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetAllVerificationLinks", "MasterData", new { Area = "Profile" });
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.VERIFICATION_LINK_MASTER_DATA_ADD_EXCEPTION;
            }
            return Json(new { status = status, verificationLink = dataModelVerificationLink }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateVerificationLinks(VerificationLinkViewModel verificationlink)
        {
            string status = "true";
            VerificationLink dataModelVerificationLink = null;
            try
            {
                dataModelVerificationLink = AutoMapper.Mapper.Map<VerificationLinkViewModel, VerificationLink>(verificationlink);
                dataModelVerificationLink = await masterDataAddEdit.SaveVerificationLink(dataModelVerificationLink);
                var urlToRemove = Url.Action("GetAllVerificationLinks", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetAllVerificationLinks", "MasterData", new { Area = "Profile" });
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.VERIFICATION_LINK_MASTER_DATA_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, verificationLink = dataModelVerificationLink }, JsonRequestBehavior.AllowGet);
        }

        public string InactivateVerificationLink(int verificationLinkID)
        {
            string status = "false";

            try
            {
                masterDataManager.InactivateVerificationLink(verificationLinkID);

                var urlToRemove = Url.Action("GetAllVerificationLinks", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetAllVerificationLinks", "MasterData", new { Area = "Profile" });
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
                status = "true";
                return status;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region Decredentialing Reasons

        public ActionResult DecredentialingReason()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllDecredentialingReasons()
        {
            var data = await masterDataManager.GetAllDecredentialingReasonsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddDecredentialingReason(DecredemtialingReasonViewModel decredentialingreason)
        {
            string status = "true";
            DecredentialingReason dataModelDecredentialingReason = null;
            try
            {
                dataModelDecredentialingReason = AutoMapper.Mapper.Map<DecredemtialingReasonViewModel, DecredentialingReason>(decredentialingreason);
                dataModelDecredentialingReason = await masterDataAddEdit.SaveDecredentialingReason(dataModelDecredentialingReason);
                var urlToRemove = Url.Action("GetAllDecredentialingReasons", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.VERIFICATION_LINK_MASTER_DATA_ADD_EXCEPTION;
            }
            return Json(new { status = status, decredentialReason = dataModelDecredentialingReason }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateDecredentialingReasons(DecredemtialingReasonViewModel decredentialingreason)
        {
            string status = "true";
            DecredentialingReason dataModelDecredentialingReason = null;
            try
            {
                dataModelDecredentialingReason = AutoMapper.Mapper.Map<DecredemtialingReasonViewModel, DecredentialingReason>(decredentialingreason);
                dataModelDecredentialingReason = await masterDataAddEdit.UpdateDecredentialingReason(dataModelDecredentialingReason);
                var urlToRemove = Url.Action("GetAllDecredentialingReasons", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
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
                status = ExceptionMessage.VERIFICATION_LINK_MASTER_DATA_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, decredentialingreason = dataModelDecredentialingReason }, JsonRequestBehavior.AllowGet);
        }

        public string InactivateDecredentialingReason(int DecredentialingReasonID)
        {
            string status = "false";

            try
            {
                masterDataManager.InactivateDecredentialingReason(DecredentialingReasonID);
                status = "true";
                var urlToRemove = Url.Action("GetAllDecredentialingReasons", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
                return status;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
        #region NotesTemplate

        public async Task<JsonResult> InactivateNotesTemplate(int NotesTemplateID)
        {
            string status = "false";

            try
            {
                await masterDataManager.InactivateNotesTemplte(NotesTemplateID);
                var urlToRemove = Url.Action("GetAllNotesTemplates", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetNotesTemplateByCode", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
                status = "true";
                return Json(new { status = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult NotesTemplate()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData", VaryByParam = "Code")]
        public JsonResult GetNotesTemplateByCode(string Code)
        {
            return Json(new { data = masterDataManager.GetNotesTemplateByCode(Code) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddNotesTemplate(NotesTemplate Template)
        {
            Template.CreatedDate = DateTime.Now;
            Template.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

            string status = "true";
            NotesTemplate NotesTemplate = null;

            try
            {

                NotesTemplate = AutoMapper.Mapper.Map<NotesTemplate, NotesTemplate>(Template);

                NotesTemplate.NotesTemplateID = masterDataAddEdit.AddNotestemplate(Template);

                var urlToRemove = Url.Action("GetAllNotesTemplates", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetNotesTemplateByCode", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(new { NotesTemplate = NotesTemplate }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateNotesTemplate(NotesTemplate Template)
        {
            Template.CreatedDate = DateTime.Now;
            Template.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
            bool result = masterDataAddEdit.UpdateNotestemplate(Template);
            if (result)
            {
                var urlToRemove = Url.Action("GetAllNotesTemplates", "MasterDataNew");
                var urlToRemove1 = Url.Action("GetNotesTemplateByCode", "MasterDataNew");
                HttpResponse.RemoveOutputCacheItem(urlToRemove);
                HttpResponse.RemoveOutputCacheItem(urlToRemove1);
            }
            return Json(new { status = result, NotesTemplate = Template }, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public JsonResult GetAllNotesTemplates()
        {
            var data = masterDataManager.GetAllNotesTemplates().Where(x => x.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Facility Information
        public ActionResult FacilityInformation()
        {
            return View();
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllMasterFacilityInformation()
        {
            var data = await masterDataManager.GetAllMasterFacilityInformationAsync();
            //return Json(data, JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(data);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllAccessibilityQuestions()
        {
            var data = await masterDataManager.GetAllAccessibilityQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllPracticeTypes()
        {
            var data = await masterDataManager.GetAllPracticeTypesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllServiceQuestions()
        {
            var data = await masterDataManager.GetAllServiceQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<JsonResult> AddFacilityAsync(PracticeLocationViewModel practiceLocationViewModel)
        {
            string status = "true";
            Facility dataModelFacilityInformation = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFacilityInformation = AutoMapper.Mapper.Map<PracticeLocationViewModel, Facility>(practiceLocationViewModel);

                    // OrganizationAccountId Has to be replaced with account information 
                    await masterDataManager.AddFacilityAsync(OrganizationAccountId.DefaultOrganizationAccountID, dataModelFacilityInformation);
                    var urlToRemove = Url.Action("GetAllMasterFacilityInformation", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllFacilities", "MasterData", new { Area = "Profile" });

                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
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
                status = ExceptionMessage.PRACTICE_LOCATION_CREATE_EXCEPTION;
            }

            return Json(new { status = status, facility = dataModelFacilityInformation }, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> UpdateFacilityAsync(PracticeLocationViewModel practiceLocationViewModel)
        {
            string status = "true";
            Facility dataModelFacilityInformation = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFacilityInformation = AutoMapper.Mapper.Map<PracticeLocationViewModel, Facility>(practiceLocationViewModel);

                    // OrganizationAccountId Has to be replaced with account information 
                    await masterDataManager.UpdateFacilityAsync(OrganizationAccountId.DefaultOrganizationAccountID, dataModelFacilityInformation);
                    var urlToRemove = Url.Action("GetAllMasterFacilityInformation", "MasterDataNew");
                    var urlToRemove1 = Url.Action("GetAllFacilities", "MasterData", new { Area = "Profile" });

                    HttpResponse.RemoveOutputCacheItem(urlToRemove);
                    HttpResponse.RemoveOutputCacheItem(urlToRemove1);
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
                status = ExceptionMessage.PRACTICE_LOCATION_CREATE_EXCEPTION;
            }

            return Json(new { status = status, facility = dataModelFacilityInformation }, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllMidLevelPractitioners()
        {
            var data = await masterDataManager.GetAllMidLevelPractitionersAsync();
            //return Json(data, JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(data);
        }
        #endregion
    }
}