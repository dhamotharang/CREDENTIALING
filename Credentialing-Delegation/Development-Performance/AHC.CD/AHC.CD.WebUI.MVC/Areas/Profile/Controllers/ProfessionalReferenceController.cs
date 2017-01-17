using AHC.CD.Business;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference;
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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalReferenceController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public ProfessionalReferenceController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
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
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task <ActionResult> AddProfessionalReference( int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
        {
            string status = "true";
            ProfessionalReferenceInfo dataModelProfessionalReference = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalReference = AutoMapper.Mapper.Map<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>(professionalReference);

                    
                    await profileManager.AddProfessionalReferenceAsync(profileId, dataModelProfessionalReference);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Added");
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

            return Json(new { status = status, professionalReference = dataModelProfessionalReference }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task <ActionResult> UpdateProfessionalReference(int profileId,AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
        {
            string status = "true";
            ProfessionalReferenceInfo dataModelProfessionalReference = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalReference = AutoMapper.Mapper.Map<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>(professionalReference);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateProfessionalReferenceAsync(profileId, dataModelProfessionalReference);

                    if (isCCO)
                    {
                        
                        await profileManager.UpdateProfessionalReferenceAsync(profileId, dataModelProfessionalReference);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Professional Reference";
                        tracker.SubSection = "Professional Reference Info";
                        tracker.userAuthId = userId;
                        tracker.objId = professionalReference.ProfessionalReferenceInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/ProfessionalReference/UpdateProfessionalReference?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(professionalReference, dataModelProfessionalReference, tracker);
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

            return Json(new { status = status, professionalReference = dataModelProfessionalReference }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProfessionalReference(int profileId, ProfessionalReferenceViewModel professionalReference)
        {
            string status = "true";
            ProfessionalReferenceInfo dataModelProfessionalReference = null;
            bool isCCO = await GetUserRole();
            try
            {
                dataModelProfessionalReference = AutoMapper.Mapper.Map<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>(professionalReference);
                
                await profileManager.RemoveProfessionalReferenceAsync(profileId, dataModelProfessionalReference);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Removed");
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
                status = ExceptionMessage.PROFESSIONAL_REFRENCE_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, professionalReference = dataModelProfessionalReference }, JsonRequestBehavior.AllowGet);
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
    }
}