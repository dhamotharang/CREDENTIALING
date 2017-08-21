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
using System.Dynamic;
using Newtonsoft.Json;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Business.MasterData;
using AHC.CD.Resources.Rules;


namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalReferenceController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;
        private IMasterDataManager masterDataManager = null;


        public ProfessionalReferenceController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager, IMasterDataManager masterDataManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
            this.profileUpdateManager = profileUpdateManager;
            this.masterDataManager = masterDataManager;
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
        public async Task<ActionResult> AddProfessionalReference(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
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
        public async Task<ActionResult> UpdateProfessionalReference(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
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
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.PROFESSIONAL_REFERENCE_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Professional Reference";
                        tracker.SubSection = "Professional Reference Information";
                        tracker.userAuthId = userId;
                        tracker.objId = professionalReference.ProfessionalReferenceInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/ProfessionalReference/UpdateProfessionalReference?profileId=";

                        ProfessionalReferenceInfo referenceOldData = await profileUpdateManager.GetProfileDataByID(dataModelProfessionalReference, professionalReference.ProfessionalReferenceInfoID);
                        var providerType = referenceOldData.ProviderTypeID != null ? await masterDataManager.GetProviderTypeByIDAsync(referenceOldData.ProviderTypeID) : null;

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Professional Reference Detail";
                        uniqueRecord.Value = (providerType != null ? providerType.Title + " - " : "") + referenceOldData.FirstName + " " + referenceOldData.MiddleName + " " + referenceOldData.LastName;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        profileUpdateManager.AddProfileUpdateForProvider(professionalReference, dataModelProfessionalReference, tracker);
                        successMessage = SuccessMessage.PROFESSIONAL_REFERENCE_DETAIL_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, professionalReference = dataModelProfessionalReference }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProfessionalReference(int profileId, ProfessionalReferenceViewModel professionalReference)
        {
            string status = "true";
            ProfessionalReferenceInfo dataModelProfessionalReference = null;
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
                dataModelProfessionalReference = AutoMapper.Mapper.Map<ProfessionalReferenceViewModel, ProfessionalReferenceInfo>(professionalReference);
                var UserAuthID = UserDetail.Id;

                await profileManager.RemoveProfessionalReferenceAsync(profileId, dataModelProfessionalReference, UserAuthID);
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

            return Json(new { status = status, professionalReference = dataModelProfessionalReference, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetProfileData(int profileId)
        {
            try
            {
                AHC.CD.Entities.MasterProfile.Profile providers = profileManager.GetProviderInformation(profileId);
                providers.ContactDetail.EmailIDs = providers.ContactDetail.EmailIDs.Where(p => p.PreferenceType == PreferenceType.Primary).ToList();
                if ((providers.SpecialtyDetails.Where(p => p.PreferenceType == PreferenceType.Primary).ToList().Count) != 0)
                    providers.SpecialtyDetails = providers.SpecialtyDetails.Where(p => p.PreferenceType == PreferenceType.Primary && p.Status == "Active").ToList();
                else
                    providers.SpecialtyDetails = providers.SpecialtyDetails.Where(p => p.PreferenceType == PreferenceType.Secondary && p.Status == "Active").ToList();

                if (providers.PracticeLocationDetails.ToList().Find(p => p.IsPrimary == "YES") != null)
                {
                    providers.PracticeLocationDetails = providers.PracticeLocationDetails.ToList().Where(p => p.IsPrimary == "YES").ToList();
                    providers.PracticeLocationDetails.ToList().Last().Facility = profileManager.GetFacilityDetail(providers.PracticeLocationDetails.ToList().Last().Facility.FacilityID);
                }
                else if (providers.PracticeLocationDetails.ToList().Where(p => p.IsPrimary == "NO") != null && (providers.PracticeLocationDetails.Count != 0))
                {
                    providers.PracticeLocationDetails = providers.PracticeLocationDetails.ToList().Where(p => p.IsPrimary == "NO").ToList();
                    providers.PracticeLocationDetails.ToList().Last().Facility = profileManager.GetFacilityDetail(providers.PracticeLocationDetails.ToList().Last().FacilityId);
                }
                var EducationDetails = new List<Entities.MasterProfile.EducationHistory.EducationDetail>();
                if (providers.EducationDetails.Count != 0)
                    EducationDetails = providers.EducationDetails.ToList();
                providers.EducationDetails = new List<Entities.MasterProfile.EducationHistory.EducationDetail>();
                if (EducationDetails != null && EducationDetails.Count != 0)
                    providers.EducationDetails.Add(EducationDetails.Where(p => p.GraduationType == "Graduate").FirstOrDefault());
                return Json(new { providers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
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

            var roleIDs = RoleManager.Roles.ToList().Where(r => r.Name == "CCO" || r.Name == "CRA" || r.Name == "CRA" || r.Name == "TL").Select(r => r.Id).ToList();

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