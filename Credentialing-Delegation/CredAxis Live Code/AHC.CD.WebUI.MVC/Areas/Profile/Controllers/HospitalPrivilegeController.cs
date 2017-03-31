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
using AHC.CD.Business.MasterData;
using System.Dynamic;
using Newtonsoft.Json;
using AHC.CD.Resources.Rules;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class HospitalPrivilegeController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;
        private IMasterDataManager masterDataManager = null;

        public HospitalPrivilegeController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager, IMasterDataManager masterDataManager)
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
        public async Task<ActionResult> AddHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            string status = "true";

            string Id = "";
            bool isCCO = await GetUserRole();
            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);
                    DocumentDTO hospitalDocument = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                   
                    var result = await profileManager.AddHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Added");
                    await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
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
            string ActionType = "Update";
            string successMessage = "";
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
                       
                        await profileManager.UpdateHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.HOSPITAL_PRIVELEGE_DETAIL_UPDATE_SUCCESS;
                        }
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

                        HospitalPrivilegeDetail hospitalOldData = await profileUpdateManager.GetProfileDataByID(dataModelHospitalPrivilegeDetail, hospitalPrivilege.HospitalPrivilegeDetailID);
                        var hospitalContact = await masterDataManager.GetHospitalContactInfoByIDAsync(hospitalOldData.HospitalContactInfoID);
                        var hospitalDeatil = await masterDataManager.GetHospitalByIDAsync(hospitalOldData.HospitalID);
                        var specialtyDetail = hospitalOldData.SpecialtyID != null ? await masterDataManager.GetSpecialtyByIDAsync(hospitalOldData.SpecialtyID) : null;

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Hospital Privilege Detail";
                        uniqueRecord.Value = hospitalDeatil.HospitalName + " - " + hospitalContact.LocationName + (specialtyDetail != null ?  " - " + specialtyDetail.Name : "");

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        profileUpdateManager.AddProfileUpdateForProvider(hospitalPrivilege, dataModelHospitalPrivilegeDetail, tracker);
                        successMessage = SuccessMessage.HOSPITAL_PRIVELEGE_DETAIL_UPDATE_REQUEST_SUCCESS;
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
            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, hospitalPrivilegeDetail = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewHospitalPrivilegeAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeDetailViewModel hospitalPrivilege)
        {
            hospitalPrivilege.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
            string status = "true";
            string successMessage = "";
            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
            bool isCCO = await GetUserRole();
            try
            {
                //if (ModelState.IsValid)
                //{
                    dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilege);

                    DocumentDTO hospitalDocument = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Renewed");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    ////DocumentDTO document = null;
                    //await profileManager.RenewHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);

                    if (isCCO)
                    {

                        //DocumentDTO document = CreateDocument(hospitalPrivilege.HospitalPrevilegeLetterFile);
                       
                        //DocumentDTO document = null;
                        await profileManager.RenewHospitalPrivilegeDetailAsync(profileId, dataModelHospitalPrivilegeDetail, hospitalDocument);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Renewed");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.STATE_LICENSE_DETAIL_UPDATE_SUCCESS;
                        }
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
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/HospitalPrivilege/RenewHospitalPrivilegeAsync?profileId=";

                        HospitalPrivilegeDetail hospitalOldData = await profileUpdateManager.GetProfileDataByID(dataModelHospitalPrivilegeDetail, hospitalPrivilege.HospitalPrivilegeDetailID);
                        var hospitalContact = await masterDataManager.GetHospitalContactInfoByIDAsync(hospitalOldData.HospitalContactInfoID);
                        var hospitalDeatil = await masterDataManager.GetHospitalByIDAsync(hospitalOldData.HospitalID);
                        var specialtyDetail = hospitalOldData.SpecialtyID != null ? await masterDataManager.GetSpecialtyByIDAsync(hospitalOldData.SpecialtyID) : null;

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Hospital Privilege Detail";
                        uniqueRecord.Value = hospitalDeatil.HospitalName + " - " + hospitalContact.LocationName + (specialtyDetail != null ? " - " + specialtyDetail.Name : "");

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        profileUpdateManager.AddProfileUpdateForProvider(hospitalPrivilege, dataModelHospitalPrivilegeDetail, tracker);
                        successMessage = SuccessMessage.STATE_LICENSE_DETAIL_UPDATE_REQUEST_SUCCESS;
                    }
                //}
                //else
                //{
                   // status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                //}
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

            return Json(new { status = status, successMessage = successMessage, hospitalPrivilege = dataModelHospitalPrivilegeDetail }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateHospitalPrivilegeInfoAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege.HospitalPrivilegeInformationViewModel hospitalPrivilegeInfo)
        {
            string status = "true";
            string successMessage = "";
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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                    }
                    else if (isCCO && hospitalPrivilegeInfo.HospitalPrivilegeInformationID != 0)
                    {
                        await profileManager.UpdateHospitalPrivilegeInformationAsync(profileId, dataModelHospitalPrivilegeInformation);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.HOSPITAL_PRIVELEGE_INFO_UPDATE_SUCCESS;
                        }
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
                        successMessage = SuccessMessage.HOSPITAL_PRIVELEGE_INFO_UPDATE_REQUEST_SUCCESS;
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
            return Json(new { status = status, successMessage = successMessage, hospitalPrivilegeInformation = dataModelHospitalPrivilegeInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveHospitalPrivilegeAsync(int profileId, HospitalPrivilegeDetailViewModel hospitalPrivilegeDetail)
        {
            string status = "true";
            HospitalPrivilegeDetail dataModelHospitalPrivilegeDetail = null;
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
                dataModelHospitalPrivilegeDetail = AutoMapper.Mapper.Map<HospitalPrivilegeDetailViewModel, HospitalPrivilegeDetail>(hospitalPrivilegeDetail);
                var UserAuthID = UserDetail.Id;
               
                await profileManager.RemoveHospitalPrivilegeAsync(profileId, dataModelHospitalPrivilegeDetail,UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Removed");
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
                status = ExceptionMessage.HOSPITAL_PRIVILEGE_DETAIL_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, hospitalPrivilege = dataModelHospitalPrivilegeDetail, UserName = UserName }, JsonRequestBehavior.AllowGet);
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
        private async Task<ApplicationUser> GetUser()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            return user;
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

        #endregion
    }
}