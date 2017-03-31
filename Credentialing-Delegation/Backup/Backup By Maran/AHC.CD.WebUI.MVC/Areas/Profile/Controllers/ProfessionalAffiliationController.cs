using AHC.CD.Business;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalAffiliation;
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
using Newtonsoft.Json;
using System.Dynamic;
using AHC.CD.Resources.Rules;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalAffiliationController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public ProfessionalAffiliationController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
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
        [ProfileAuthorize(ProfileActionType.Add,false)]
        public async Task<ActionResult> AddProfessionalAffiliation(int profileId, ProfessionalAffiliationDetailViewModel professionalAffiliation)
        {
            string status = "true";
            ProfessionalAffiliationInfo dataModelProfessionalAffiliation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProfessionalAffiliation = AutoMapper.Mapper.Map<ProfessionalAffiliationDetailViewModel, ProfessionalAffiliationInfo>(professionalAffiliation);


                    await profileManager.AddProfessionalAffiliationAsync(profileId, dataModelProfessionalAffiliation);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Affiliation Details", "Added");
                    await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch(DatabaseValidationException ex)
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

            return Json( new {status = status, professionalAffiliation = dataModelProfessionalAffiliation  }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateProfessionalAffiliation(int profileId, ProfessionalAffiliationDetailViewModel professionalAffiliation)
        {
            string status  = "true";
            ProfessionalAffiliationInfo dataModelProfessionalAffiliation = null;
            bool isCCO = await GetUserRole();
            try
            {

                if (true)
                {
                    dataModelProfessionalAffiliation = AutoMapper.Mapper.Map<ProfessionalAffiliationDetailViewModel, ProfessionalAffiliationInfo>(professionalAffiliation);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Affiliation Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateProfessionalAffiliationAsync(profileId, dataModelProfessionalAffiliation);

                    if (isCCO)
                    {

                        await profileManager.UpdateProfessionalAffiliationAsync(profileId, dataModelProfessionalAffiliation);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Affiliation Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Professional Affiliation";
                        tracker.SubSection = "Professional Affiliation Detail";
                        tracker.userAuthId = userId;
                        tracker.objId = professionalAffiliation.ProfessionalAffiliationInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/ProfessionalAffiliation/UpdateProfessionalAffiliation?profileId=";

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Organization Name";
                        uniqueRecord.Value = professionalAffiliation.OrganizationName + 
                            (professionalAffiliation.PositionOfficeHeld != null ? " - " + professionalAffiliation.PositionOfficeHeld : "") + 
                            (professionalAffiliation.Member != null ? " - " + professionalAffiliation.Member : "");

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        profileUpdateManager.AddProfileUpdateForProvider(professionalAffiliation, dataModelProfessionalAffiliation, tracker);
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

            return Json(new { status = status, professionalAffiliation = dataModelProfessionalAffiliation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProfessionalAffiliation(int profileId, ProfessionalAffiliationDetailViewModel professionalAffiliation)
        {
            string status = "true";
            ProfessionalAffiliationInfo dataModelProfessionalAffiliation = null;
            bool isCCO = await GetUserRole();
            ApplicationUser UserDetail = await GetUser();
            string UserName = null;
            if (UserDetail.FullName != null)
            {
                UserName = UserDetail.FullName;
            }
            else
            {
                UserName = UserDetail.UserName;
            }
            try
            {
                dataModelProfessionalAffiliation = AutoMapper.Mapper.Map<ProfessionalAffiliationDetailViewModel, ProfessionalAffiliationInfo>(professionalAffiliation);
                var UserAuthID = UserDetail.Id;
                await profileManager.RemoveProfessionalAffiliationAsync(profileId, dataModelProfessionalAffiliation, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Affiliation Details", "Removed");
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
                status = ExceptionMessage.PROFESSIONAL_AFFILIATION_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, professionalAffiliation = dataModelProfessionalAffiliation,UserName=UserName }, JsonRequestBehavior.AllowGet);
        }


        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }
        private async Task<ApplicationUser> GetUser()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            return user;
        }
        private async Task<bool> GetUserRole()
        {
            var status = false;

            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            var roleIDs = RoleManager.Roles.ToList().Where(r => r.Name == "CCO" || r.Name == "CRA").Select(r => r.Id).ToList();

            foreach (var id in roleIDs)
            {
                status = user.Roles.Any(r => r.RoleId == id);

                if (status)
                    break;
            }

            return status;
        }
    }
}