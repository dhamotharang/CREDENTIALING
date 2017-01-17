using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DocumentRepository;
using AHC.CD.WebUI.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.Profiles;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Resources.Document;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DocumentRepositoryController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        // Change Notifications
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public DocumentRepositoryController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager) // Change Notifications
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            // Change Notifications
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

        public async Task<string> GetDocumentRepositoryProfileDataAsync(int profileId)
        {
            string UserAuthId = await GetUserAuthId();
            int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            bool isCCO = await GetUserRole();
            return JsonConvert.SerializeObject(await profileManager.GetDocumentRepositoryDataAsync(profileId, CDUserId, isCCO));

        }

        [HttpPost]
        public async Task<ActionResult> AddOtherDocumentAsync(int profileId, OtherDocumentViewModel otherDocument)
        {
            string status = "true";
            OtherDocument dataModelOtherDocument = null;

            try
            {
                if (ModelState.IsValid && otherDocument.File != null)
                {
                    string UserAuthId = await GetUserAuthId();
                    int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
                    otherDocument.ModifiedBy = CDUserId.ToString();

                    dataModelOtherDocument = AutoMapper.Mapper.Map<OtherDocumentViewModel, OtherDocument>(otherDocument);
                    DocumentDTO document = CreateDocument(otherDocument.File);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Added");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddOtherDocumentAsync(profileId, dataModelOtherDocument, document);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, otherDocument = dataModelOtherDocument }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddProfileDocumentAsync(int profileId, OtherDocumentViewModel otherDocument)
        {
            string status = "true";
            OtherDocument dataModelOtherDocument = null;

            try
            {
                if (ModelState.IsValid && otherDocument.File != null)
                {
                    string UserAuthId = await GetUserAuthId();
                    int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
                    otherDocument.ModifiedBy = CDUserId.ToString();

                    dataModelOtherDocument = AutoMapper.Mapper.Map<OtherDocumentViewModel, OtherDocument>(otherDocument);
                    DocumentDTO document = CreateDocument(otherDocument.File);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Added");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddProfileDocumentAsync(profileId, dataModelOtherDocument, document);
                    dataModelOtherDocument = (OtherDocument)result;
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, otherDocument = dataModelOtherDocument }, JsonRequestBehavior.AllowGet);
        }




        public async Task<ActionResult> AddPlanDocumentAsync(int profileId, OtherDocumentViewModel otherDocument)
        {
            string status = "true";
            OtherDocument dataModelOtherDocument = null;

            try
            {
                if (ModelState.IsValid && otherDocument.File != null)
                {
                    string UserAuthId = await GetUserAuthId();
                    int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
                    otherDocument.ModifiedBy = CDUserId.ToString();

                    dataModelOtherDocument = AutoMapper.Mapper.Map<OtherDocumentViewModel, OtherDocument>(otherDocument);
                    DocumentDTO document = CreateDocument(otherDocument.File);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Added");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddPlanDocumentAsync(profileId, dataModelOtherDocument, document);
                    dataModelOtherDocument = (OtherDocument)result;
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, otherDocument = dataModelOtherDocument }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateOtherDocumentAsync(int profileId, OtherDocumentViewModel otherDocument)
        {
            string status = "true";
            OtherDocument dataModelOtherDocument = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    string UserAuthId = await GetUserAuthId();
                    int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
                    otherDocument.ModifiedBy = CDUserId.ToString();

                    dataModelOtherDocument = AutoMapper.Mapper.Map<OtherDocumentViewModel, OtherDocument>(otherDocument);

                    //DocumentDTO document = CreateDocument(otherDocument.File);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateOtherDocumentAsync(profileId, dataModelOtherDocument, document);
                    DocumentDTO document = null;
                    if(otherDocument.File != null)
                    {
                        document = CreateDocument(otherDocument.File);
                    }
                    if (isCCO)
                    {
                       
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        string documentpath = await profileManager.UpdateOtherDocumentAsync(profileId, dataModelOtherDocument, document);
                        dataModelOtherDocument.DocumentPath = documentpath;
                    }
                    else
                    {
                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.OTHER_DOCUMENT_PATH, DocumentTitle.OTHER_DOCUMENT, profileId);
                        if (documentTemporaryPath != null)
                        {
                            dataModelOtherDocument.DocumentPath = documentTemporaryPath;
                            otherDocument.DocumentPath = documentTemporaryPath;
                        }
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Document Repository";
                        tracker.SubSection = "Other Document";
                        tracker.userAuthId = userId;
                        tracker.objId = otherDocument.OtherDocumentID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/DocumentRepository/UpdateOtherDocumentAsync?profileId=";

                        otherDocument.File = null;
                        profileUpdateManager.AddProfileUpdateForProvider(otherDocument, dataModelOtherDocument, tracker);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, otherDocument = dataModelOtherDocument }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveOtherDocument(int profileId, int OtherDocumentID)
        {
            string status = "true";
            OtherDocumentViewModel otherDocument = new OtherDocumentViewModel { };
            otherDocument.OtherDocumentID = OtherDocumentID;

            OtherDocument dataModelOtherDocument = null;

            try
            {
                dataModelOtherDocument = AutoMapper.Mapper.Map<OtherDocumentViewModel, OtherDocument>(otherDocument);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Other Legal Name Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveOtherDocumentAsync(profileId, dataModelOtherDocument);
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
                status = ExceptionMessage.OTHER_DOUCUMENT_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, otherDocument = dataModelOtherDocument }, JsonRequestBehavior.AllowGet);
        }

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