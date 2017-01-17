using AHC.CD.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHC.CD.Entities.MasterProfile.ProfessionalLiability;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability;
using System.Web.Mvc;
using System.Threading.Tasks;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Resources.Messages;
using AHC.CD.Exceptions;
using AHC.CD.ErrorLogging;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.Notification;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.Profiles;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Resources.Document;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalLiabilityController : Controller
    {
        // GET: Profile/ProfessionalLiability
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public ProfessionalLiabilityController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
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

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> AddProfessionalLiabilityAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability.ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;
            bool isCCO = await GetUserRole();
            try
            {

                if (ModelState.IsValid)
                {
                    dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);
                    DocumentDTO document = CreateDocument(professionalLiability.InsuranceCertificateFile);

                    
                    await profileManager.AddProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Added");
                    await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);

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

            return Json(new { status = status, professionalLiability = dataModelProfessionalLiability }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, true)]
        public async Task<ActionResult> UpdateProfessionalLiabilityAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability.ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);

                    DocumentDTO document = CreateDocument(professionalLiability.InsuranceCertificateFile);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);

                    if (isCCO)
                    {
                        
                        await profileManager.UpdateProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, DocumentTitle.PROFESSIONAL_LIABILITY, profileId);
                        if (documentTemporaryPath != null)
                        {
                            professionalLiability.InsuranceCertificatePath = documentTemporaryPath;
                            dataModelProfessionalLiability.InsuranceCertificatePath = documentTemporaryPath;
                        }
                        professionalLiability.InsuranceCertificateFile = null;

                        tracker.ProfileId = profileId;
                        tracker.Section = "Professional Liability";
                        tracker.SubSection = "Professional Liability Info";
                        tracker.userAuthId = userId;
                        tracker.objId = professionalLiability.ProfessionalLiabilityInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/ProfessionalLiability/UpdateProfessionalLiabilityAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(professionalLiability, dataModelProfessionalLiability, tracker);
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

            return Json(new { status = status, professionalLiability = dataModelProfessionalLiability }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewProfessionalLiabilityAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability.ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);

                    DocumentDTO document = CreateDocument(professionalLiability.InsuranceCertificateFile);
                    ////DocumentDTO document = null;

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Renewed");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.RenewProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);

                    if (isCCO)
                    {
                        //DocumentDTO document = CreateDocument(professionalLiability.InsuranceCertificateFile);
                        //DocumentDTO document = null;

                        
                        await profileManager.RenewProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Renewed");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.PROFESSIONAL_LIABILITY_PATH, DocumentTitle.PROFESSIONAL_LIABILITY, profileId);
                        if (documentTemporaryPath != null)
                        {
                            professionalLiability.InsuranceCertificatePath = documentTemporaryPath;
                            dataModelProfessionalLiability.InsuranceCertificatePath = documentTemporaryPath;
                        }
                        professionalLiability.InsuranceCertificateFile = null;
                        tracker.ProfileId = profileId;
                        tracker.Section = "Professional Liability";
                        tracker.SubSection = "Professional Liability Info";
                        tracker.userAuthId = userId;
                        tracker.objId = professionalLiability.ProfessionalLiabilityInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/ProfessionalLiability/RenewProfessionalLiabilityAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(professionalLiability, dataModelProfessionalLiability, tracker);
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

            return Json(new { status = status, professionalLiability = dataModelProfessionalLiability }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProfessionalLiability(int profileId, ProfessionalLiabilityInfoViewModel professionalLiability)
        {
            string status = "true";
            ProfessionalLiabilityInfo dataModelProfessionalLiability = null;
            bool isCCO = await GetUserRole();
            try
            {
                dataModelProfessionalLiability = AutoMapper.Mapper.Map<ProfessionalLiabilityInfoViewModel, ProfessionalLiabilityInfo>(professionalLiability);
                
                await profileManager.RemoveProfessionalLiabilityAsync(profileId, dataModelProfessionalLiability);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Liability Details", "Removed");
                await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
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
                status = ExceptionMessage.HOME_ADDRESS_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, professionalLiability = dataModelProfessionalLiability }, JsonRequestBehavior.AllowGet);
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