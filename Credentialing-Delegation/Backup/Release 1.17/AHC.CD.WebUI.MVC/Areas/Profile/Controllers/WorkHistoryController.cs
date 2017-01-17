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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class WorkHistoryController : Controller
    {
        IProfileManager profileManager;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;


        public WorkHistoryController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
        }

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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalWorkExperience = AutoMapper.Mapper.Map<ProfessionalWorkExperienceViewModel, ProfessionalWorkExperience>(professionalWorkExperience);
                    DocumentDTO document = CreateDocument(professionalWorkExperience.File);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Professional Work Experience ", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateProfessionalWorkExperienceAsync(profileId, dataModelProfessionalWorkExperience, document);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMilitaryServiceInformation = AutoMapper.Mapper.Map<MilitaryServiceInformationViewModel, MilitaryServiceInformation>(militaryServiceInformation);


                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Military Service Information ", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateMilitaryServiceInformationAsync(profileId, dataModelMilitaryServiceInformation);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPublicHealthService = AutoMapper.Mapper.Map<PublicHealthServiceViewModel, PublicHealthService>(publicHealthService);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Public Health Service Information ", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdatePublicHealthServiceAsync(profileId, dataModelPublicHealthService);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelWorkGap = AutoMapper.Mapper.Map<WorkGapViewModel, WorkGap>(workGap);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Work History Details - Work Gap Information ", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateWorkGapAsync(profileId, dataModelWorkGap);
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

        #endregion
    }
}