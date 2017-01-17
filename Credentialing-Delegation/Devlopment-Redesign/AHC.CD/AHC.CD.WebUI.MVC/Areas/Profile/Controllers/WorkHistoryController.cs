using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.Profiles;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Resources.Document;


namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class WorkHistoryController : Controller
    {
        IProfileManager profileManager;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public WorkHistoryController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
            this.profileUpdateManager = profileUpdateManager;
        }

        protected ApplicationUserManager _authUserManager;
        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        #region CV Upload
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddCVAsync(int profileId, CVInformationViewModel cvInformation)
        {
            string status = "true";
            CVInformation dataModelCVInformation = new CVInformation();
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {

                    dataModelCVInformation = AutoMapper.Mapper.Map<CVInformationViewModel, CVInformation>(cvInformation);
                    DocumentDTO document = CreateDocument(cvInformation.CVFile);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CV Information", "Added");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.AddCVAsync(profileId, dataModelCVInformation, document);

                    if (isCCO || cvInformation.CVInformationID == 0)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CV Information", "Added");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        dataModelCVInformation = await profileManager.AddCVAsync(profileId, dataModelCVInformation, document);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.CV_DOCUMENT_PATH, DocumentTitle.CV, profileId);
                        if (documentTemporaryPath != null)
                        {
                            dataModelCVInformation.CVDocumentPath = documentTemporaryPath;
                            cvInformation.CVDocumentPath = documentTemporaryPath;
                        }
                        cvInformation.CVFile = null;

                        tracker.ProfileId = profileId;
                        tracker.Section = "Work History";
                        tracker.SubSection = "CV";
                        tracker.userAuthId = userId;
                        tracker.objId = cvInformation.CVInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/WorkHistory/AddCVAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(cvInformation, dataModelCVInformation, tracker);
                    }
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
                
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.CV_UPLOADED_EXCEPTION;
            }
            return Json(new { status = status, dataModelCVInformation = dataModelCVInformation }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Professional Work Experience

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperienceViewModel professionalWorkExperience)
        {
            string status = "true";
            ProfessionalWorkExperience dataModelProfessionalWorkExperience = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalWorkExperience = AutoMapper.Mapper.Map<ProfessionalWorkExperienceViewModel, ProfessionalWorkExperience>(professionalWorkExperience);
                    DocumentDTO document = CreateDocument(professionalWorkExperience.File);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddProfessionalWorkExperienceAsync(profileId, dataModelProfessionalWorkExperience, document);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, professionalWorkExperience = dataModelProfessionalWorkExperience }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperienceViewModel professionalWorkExperience)
        {
            string status = "true";
            ProfessionalWorkExperience dataModelProfessionalWorkExperience = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalWorkExperience = AutoMapper.Mapper.Map<ProfessionalWorkExperienceViewModel, ProfessionalWorkExperience>(professionalWorkExperience);

                    DocumentDTO document = CreateDocument(professionalWorkExperience.File);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Professional Work Experience ", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateProfessionalWorkExperienceAsync(profileId, dataModelProfessionalWorkExperience, document);

                    if (isCCO)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Professional Work Experience ", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateProfessionalWorkExperienceAsync(profileId, dataModelProfessionalWorkExperience, document);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.PROFESSIONAL_WORK_EXPERIENCE_PATH, DocumentTitle.PROFESSIONAL_WORK_EXPERIENCE, profileId);
                        if (documentTemporaryPath != null)
                        {
                            professionalWorkExperience.WorkExperienceDocPath = documentTemporaryPath;
                            dataModelProfessionalWorkExperience.WorkExperienceDocPath = documentTemporaryPath;
                        }
                        professionalWorkExperience.File = null;

                        tracker.ProfileId = profileId;
                        tracker.Section = "Work History";
                        tracker.SubSection = "Professional Work Experience";
                        tracker.userAuthId = userId;
                        tracker.objId = professionalWorkExperience.ProfessionalWorkExperienceID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/WorkHistory/UpdateProfessionalWorkExperienceAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(professionalWorkExperience, dataModelProfessionalWorkExperience, tracker);
                    }

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, professionalWorkExperience = dataModelProfessionalWorkExperience }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProfessionalWorkExperienceAsync(int profileId, ProfessionalWorkExperienceViewModel professionalWorkExperience)
        {
            string status = "true";
            ProfessionalWorkExperience dataModelProfessionalWorkExperience = null;

            try
            {
                dataModelProfessionalWorkExperience = AutoMapper.Mapper.Map<ProfessionalWorkExperienceViewModel, ProfessionalWorkExperience>(professionalWorkExperience);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work Gap Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveProfessionalWorkExperienceAsync(profileId, dataModelProfessionalWorkExperience);
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }

            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }

            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFESSIONAL_WORK_EXPERIENCE_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, professionalWorkExperience = dataModelProfessionalWorkExperience }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Military Service Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformationViewModel militaryServiceInformation)
        {
            string status = "true";
            MilitaryServiceInformation dataModelMilitaryServiceInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMilitaryServiceInformation = AutoMapper.Mapper.Map<MilitaryServiceInformationViewModel, MilitaryServiceInformation>(militaryServiceInformation);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Military Service Information ", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddMilitaryServiceInformationAsync(profileId, dataModelMilitaryServiceInformation);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.MILITARY_SERVICE_INFORMATION_CREATE_EXCEPTION;
            }

            return Json(new { status = status, militaryServiceInformation = dataModelMilitaryServiceInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformationViewModel militaryServiceInformation)
        {
            string status = "true";
            MilitaryServiceInformation dataModelMilitaryServiceInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMilitaryServiceInformation = AutoMapper.Mapper.Map<MilitaryServiceInformationViewModel, MilitaryServiceInformation>(militaryServiceInformation);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Military Service Information ", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateMilitaryServiceInformationAsync(profileId, dataModelMilitaryServiceInformation);

                    if (isCCO)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Military Service Information ", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateMilitaryServiceInformationAsync(profileId, dataModelMilitaryServiceInformation);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Work History";
                        tracker.SubSection = "Military Service Information";
                        tracker.userAuthId = userId;
                        tracker.objId = militaryServiceInformation.MilitaryServiceInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/WorkHistory/UpdateMilitaryServiceInformationAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(militaryServiceInformation, dataModelMilitaryServiceInformation, tracker);
                    }

                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.MILITARY_SERVICE_INFORMATION_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, militaryServiceInformation = dataModelMilitaryServiceInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveMilitaryServiceInformationAsync(int profileId, MilitaryServiceInformationViewModel militaryServiceInformation)
        {
            string status = "true";
            MilitaryServiceInformation dataModelMilitaryServiceInformation = null;

            try
            {
                dataModelMilitaryServiceInformation = AutoMapper.Mapper.Map<MilitaryServiceInformationViewModel, MilitaryServiceInformation>(militaryServiceInformation);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work Gap Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveMilitaryServiceInformationAsync(profileId, dataModelMilitaryServiceInformation);
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }

            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }

            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.MILITARY_SERVICE_INFORMATION_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, militaryServiceInformation = dataModelMilitaryServiceInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Public Health Service

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddPublicHealthServiceAsync(int profileId, PublicHealthServiceViewModel publicHealthService)
        {
            string status = "true";
            PublicHealthService dataModelPublicHealthService = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPublicHealthService = AutoMapper.Mapper.Map<PublicHealthServiceViewModel, PublicHealthService>(publicHealthService);


                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Public Health Service Information ", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddPublicHealthServiceAsync(profileId, dataModelPublicHealthService);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PUBLIC_HEALTH_SERVICE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, publicHealthService = dataModelPublicHealthService }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdatePublicHealthServiceAsync(int profileId, PublicHealthServiceViewModel publicHealthService)
        {
            string status = "true";
            PublicHealthService dataModelPublicHealthService = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPublicHealthService = AutoMapper.Mapper.Map<PublicHealthServiceViewModel, PublicHealthService>(publicHealthService);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Public Health Service Information ", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdatePublicHealthServiceAsync(profileId, dataModelPublicHealthService);

                    if (isCCO)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Public Health Service Information ", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdatePublicHealthServiceAsync(profileId, dataModelPublicHealthService);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Work History";
                        tracker.SubSection = "Public Health Service";
                        tracker.userAuthId = userId;
                        tracker.objId = publicHealthService.PublicHealthServiceID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/WorkHistory/UpdatePublicHealthServiceAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(publicHealthService, dataModelPublicHealthService, tracker);
                    }
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PUBLIC_HEALTH_SERVICE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, publicHealthService = dataModelPublicHealthService }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemovePublicHealthServiceAsync(int profileId, PublicHealthServiceViewModel publicHealthService)
        {
            string status = "true";
            PublicHealthService dataModelPublicHealthService = null;

            try
            {
                dataModelPublicHealthService = AutoMapper.Mapper.Map<PublicHealthServiceViewModel, PublicHealthService>(publicHealthService);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work Gap Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemovePublicHealthServiceAsync(profileId, dataModelPublicHealthService);
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }

            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }

            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PUBLIC_HEALTH_SERVICE_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, publicHealthService = dataModelPublicHealthService }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region WorkGap

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddWorkGapAsync(int profileId, WorkGapViewModel workGap)
        {
            string status = "true";
            WorkGap dataModelWorkGap = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelWorkGap = AutoMapper.Mapper.Map<WorkGapViewModel, WorkGap>(workGap);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Work Gap Information ", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddWorkGapAsync(profileId, dataModelWorkGap);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.WORK_GAP_CREATE_EXCEPTION;
            }

            return Json(new { status = status, workGap = dataModelWorkGap }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateWorkGapAsync(int profileId, WorkGapViewModel workGap)
        {
            string status = "true";
            WorkGap dataModelWorkGap = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelWorkGap = AutoMapper.Mapper.Map<WorkGapViewModel, WorkGap>(workGap);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Work Gap Information ", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateWorkGapAsync(profileId, dataModelWorkGap);

                    if (isCCO)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Work Gap Information ", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateWorkGapAsync(profileId, dataModelWorkGap);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Work History";
                        tracker.SubSection = "Work Gap";
                        tracker.userAuthId = userId;
                        tracker.objId = workGap.WorkGapID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/WorkHistory/UpdateWorkGapAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(workGap, dataModelWorkGap, tracker);
                    }
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.WORK_GAP_CREATE_EXCEPTION;
            }

            return Json(new { status = status, workGap = dataModelWorkGap }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveWorkGap(int profileId, WorkGapViewModel workGap)
        {
            string status = "true";
            WorkGap dataModelWorkGap = null;

            try
            {
                dataModelWorkGap = AutoMapper.Mapper.Map<WorkGapViewModel, WorkGap>(workGap);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work Gap Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveWorkGapAsync(profileId, dataModelWorkGap);
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }

            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }

            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.WORK_GAP_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, workGap = dataModelWorkGap }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private Methods

        private DocumentDTO CreateDocument(HttpPostedFileBase file, bool isRemoved = false)
        {
            DocumentDTO document = new DocumentDTO() { IsRemoved = isRemoved };
            if (file != null)
            {
                document.FileName = file.FileName;
                document.InputStream = file.InputStream;
            }

            return document;
        }

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }

        private async Task<bool> GetUserRole()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "CCO");

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }

        #endregion
    }
}