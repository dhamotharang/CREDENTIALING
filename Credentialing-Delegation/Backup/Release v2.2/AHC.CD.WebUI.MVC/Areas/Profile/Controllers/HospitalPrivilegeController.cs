using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.WebUI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Business.Profiles;
using AHC.CD.Resources.Document;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class HospitalPrivilegeController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public HospitalPrivilegeController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
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
        public async Task<ActionResult> AddHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            string status = "true";

            string Id = "";

            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);
                    DocumentDTO hospitalDocument = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
                    Id = result.ToString();
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
            return Json(new { status = status, id = Id, hospitalPrivilegeDetail = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            string status = "true";
            bool isCCO = await GetUserRole();
            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);

                    DocumentDTO hospitalDocument = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);

                    if (isCCO)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string hospitalDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(hospitalDocument, DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, DocumentTitle.HOSPITAL_PRIVILEGE, profileId);
                        if (hospitalDocumentTemporaryPath != null)
                        {
                            hospitalPrivilege.HospitalPrevilegeLetterPath = hospitalDocumentTemporaryPath;
                            dataModelHospitalPrivilegeDetail.HospitalPrevilegeLetterPath = hospitalDocumentTemporaryPath;
                        }
                        hospitalPrivilege.HospitalPrevilegeLetterFile = null;

                        tracker.ProfileId = profileId;
                        tracker.Section = "Hospital Privilege";
                        tracker.SubSection = "Hospital Privilege Information";
                        tracker.userAuthId = userId;
                        tracker.objId = hospitalPrivilege.HospitalPrivilegeDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(hospitalPrivilege, dataModelHospitalPrivilegeDetail, tracker);
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
            return Json(new { status = status, hospitalPrivilegeDetail = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            string status = "true";
            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);

                    DocumentDTO hospitalDocument = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Renewed");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    ////DocumentDTO document = null;
                    //await profileManager.RenewHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, document);

                    if (isCCO)
                    {

                        //DocumentDTO document = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Renewed");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        //DocumentDTO document = null;
                        await profileManager.RenewHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string hospitalDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(hospitalDocument, DocumentRootPath.HOSPITAL_PRIVILEGE_PATH, DocumentTitle.HOSPITAL_PRIVILEGE, profileId);
                        if (hospitalDocumentTemporaryPath != null)
                        {
                            hospitalPrivilege.HospitalPrevilegeLetterPath = hospitalDocumentTemporaryPath;
                            dataModelHospitalPrivilegeDetail.HospitalPrevilegeLetterPath = hospitalDocumentTemporaryPath;
                        }
                        hospitalPrivilege.HospitalPrevilegeLetterFile = null;
                        tracker.ProfileId = profileId;
                        tracker.Section = "Hospital Privilege";
                        tracker.Section = "Hospital Privilege Information";
                        tracker.userAuthId = userId;
                        tracker.objId = hospitalPrivilege.HospitalPrivilegeDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/HospitalPrivilege/RenewHospitalPrivilegeAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(hospitalPrivilege, dataModelHospitalPrivilegeDetail, tracker);
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

            return Json(new { status = status, hospitalPrivilege = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateHospitalPrivilegeInfoAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeInformationViewModel hospitalPrivilegeInfo)
        {
            string status = "true";
            bool isCCO = await GetUserRole();
            HospitalPrivilegeInformation dataModelHospitalPrivilegeInformation = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeInformation = AutoMapper.Mapper.Map<HospitalPrivilegeInformationViewModel, HospitalPrivilegeInformation>(hospitalPrivilegeInfo);

                    //await profileManager.UpdateHospitalPrivilegeInformationAsync(profileId, dataModelHospitalPrivilegeInformation);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    if (hospitalPrivilegeInfo.HospitalPrivilegeInformationID == 0)
                    {
                        await profileManager.UpdateHospitalPrivilegeInformationAsync(profileId, dataModelHospitalPrivilegeInformation);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else if (isCCO && hospitalPrivilegeInfo.HospitalPrivilegeInformationID != 0)
                    {
                        await profileManager.UpdateHospitalPrivilegeInformationAsync(profileId, dataModelHospitalPrivilegeInformation);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else if (!isCCO && hospitalPrivilegeInfo.HospitalPrivilegeInformationID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Hospital Privilege";
                        tracker.SubSection = "Hospital Information";
                        tracker.userAuthId = userId;
                        tracker.objId = hospitalPrivilegeInfo.HospitalPrivilegeInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeInfoAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(hospitalPrivilegeInfo, dataModelHospitalPrivilegeInformation, tracker);
                    }


                    if (dataModelHospitalPrivilegeInformation.HospitalPrivilegeDetails == null || dataModelHospitalPrivilegeInformation.HospitalPrivilegeDetails.Count == 0)
                    {
                        dataModelHospitalPrivilegeInformation.HospitalPrivilegeDetails = new List<HospitalPrivilegeDetail>();
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
            return Json(new { status = status, hospitalPrivilegeInformation = dataModelHospitalPrivilegeInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveHospitalPrivilegeAsync(int profileId, HospitalPrivilegeDetailViewModel hospitalPrivilegeDetail)
        {
            string status = "true";
            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;

            try
            {
                dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilegeDetail);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveHospitalPrivilegeAsync(profileId, dataModelHospitalPrivilegeDetail);
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
                status = ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, hospitalPrivilege = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
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