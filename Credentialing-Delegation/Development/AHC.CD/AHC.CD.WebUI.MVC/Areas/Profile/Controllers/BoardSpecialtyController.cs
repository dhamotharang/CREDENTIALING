using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Resources.Document;
using Newtonsoft.Json;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class BoardSpecialtyController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public BoardSpecialtyController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
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

        #region Specialty

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddSpecialityDetailAsync(int profileId, SpecialtyDetailViewModel specialty)
        {
            string status = "true";
            SpecialtyDetail dataModelSpecialty = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelSpecialty = AutoMapper.Mapper.Map<SpecialtyDetailViewModel, SpecialtyDetail>(specialty);
                    DocumentDTO document = null;
                    if (specialty.SpecialtyBoardCertifiedDetail != null)
                    {
                        document = CreateDocument(specialty.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile);
                    }
                   
                    await profileManager.AddSpecialtyDetailAsync(profileId, dataModelSpecialty, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Speciality Details", "Added");
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
                status = ExceptionMessage.SPECIALITY_BOARD_CREATE_EXCEPTION;
            }

            return Json(new { status = status, specialty = dataModelSpecialty }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateSpecialityDetailAsync(int profileId, SpecialtyDetailViewModel SpecialtyDetail, int CDUserID = 0)
        {
            string status = "true";
            SpecialtyDetail dataModelSpecialty = null;
            SpecialtyDetail specialtyDetail = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelSpecialty = AutoMapper.Mapper.Map<SpecialtyDetailViewModel, SpecialtyDetail>(SpecialtyDetail);
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
                    DocumentDTO document = null;
                    if (SpecialtyDetail.SpecialtyBoardCertifiedDetail != null)
                    {
                        document = CreateDocument(SpecialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile);
                    }
                    if (isCCO)
                    {
                        if (SpecialtyDetail.SpecialtyBoardCertifiedDetail != null)
                        {
                            if (SpecialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile != null && SpecialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath != null)
                            {
                                dynamic UH = JsonConvert.DeserializeObject(SpecialtyDetail.UpdateHistory);
                                UH.SupportingDocument = SpecialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath;
                                SpecialtyDetail.UpdateHistory = JsonConvert.SerializeObject(UH);
                            }
                        }
                        specialtyDetail = await profileManager.UpdateSpecialtyDetailAsync(profileId, dataModelSpecialty, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Speciality Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(SpecialtyDetail.UpdateHistory, UserAuthID, "SpecialtyDetailID", SpecialtyDetail.SpecialtyDetailID, profileId);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string documentTemporaryPath = null;
                        if (document != null)
                        {
                            documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.SPECIALITY_BOARD_PATH, DocumentTitle.SPECIALITY_BOARD, profileId);
                        }
                        if (SpecialtyDetail.SpecialtyBoardCertifiedDetail != null)
                        {
                            if (documentTemporaryPath != null)
                            {
                                SpecialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = documentTemporaryPath;
                                dataModelSpecialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath = documentTemporaryPath;
                            }
                            SpecialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile = null;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Board Specialty";
                        tracker.SubSection = "Specialty Details";
                        tracker.userAuthId = userId;
                        tracker.objId = SpecialtyDetail.SpecialtyDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/BoardSpecialty/UpdateSpecialityDetailAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(SpecialtyDetail, dataModelSpecialty, tracker);

                        specialtyDetail = dataModelSpecialty;
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
                status = ExceptionMessage.SPECIALITY_BOARD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, specialty = specialtyDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        public async Task<ActionResult> RenewSpecialityDetailAsync(int profileId, SpecialtyDetailViewModel specialtyDetail,int CDUserID = 0)
        {
            specialtyDetail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
            string status = "true";
            SpecialtyDetail dataModelSpecialty = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
                    dataModelSpecialty = AutoMapper.Mapper.Map<SpecialtyDetailViewModel, SpecialtyDetail>(specialtyDetail);

                    DocumentDTO document = null;
                    if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                    {
                        document = CreateDocument(specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile);
                    }

                    if (isCCO)
                    {
                        if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                        {
                            if (specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile != null && specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath != null)
                            {
                                dynamic UH = JsonConvert.DeserializeObject(specialtyDetail.UpdateHistory);
                                UH.SupportingDocument = specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath;
                                specialtyDetail.UpdateHistory = JsonConvert.SerializeObject(UH);
                            }
                        }
                        await profileManager.RenewSpecialtyDetailAsync(profileId, dataModelSpecialty, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Speciality Details", "Renewed");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(specialtyDetail.UpdateHistory, UserAuthID, "SpecialtyDetailID", specialtyDetail.SpecialtyDetailID, profileId);
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.SPECIALITY_BOARD_PATH, DocumentTitle.SPECIALITY_BOARD, profileId);
                        if (specialtyDetail.SpecialtyBoardCertifiedDetail != null)
                        {
                            if (documentTemporaryPath != null)
                            {
                                specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificatePath = documentTemporaryPath;
                                dataModelSpecialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath = documentTemporaryPath;
                            }
                            specialtyDetail.SpecialtyBoardCertifiedDetail.BoardCertificateDocumentFile = null;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Board Specialty";
                        tracker.SubSection = "Specialty Details";
                        tracker.userAuthId = userId;
                        tracker.objId = specialtyDetail.SpecialtyDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/BoardSpecialty/RenewSpecialityDetailAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(specialtyDetail, dataModelSpecialty, tracker);
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
                status = ExceptionMessage.SPECIALITY_BOARD_RENEW_EXCEPTION;
            }

            return Json(new { status = status, specialty = dataModelSpecialty }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveSpecialityDetailAsync(int profileId, SpecialtyDetailViewModel specialty)
        {
            string status = "true";
            SpecialtyDetail dataModelSpecialtyDetail = null;
            bool isCCO = await GetUserRole();
            try
            {
                dataModelSpecialtyDetail = AutoMapper.Mapper.Map<SpecialtyDetailViewModel, SpecialtyDetail>(specialty);
               
                await profileManager.RemoveSpecialityDetailAsync(profileId, dataModelSpecialtyDetail);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Specialty Details", "Removed");
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
                status = ExceptionMessage.SPECIALITY_BOARD_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, specialty = dataModelSpecialtyDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Practice Interest

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdatePracticeInterestAsync(int profileId, PracticeInterestViewModel practiceInterest,int CDUserID = 0)
        {
            string status = "true";
            PracticeInterest dataModelPracticeInterest = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;   
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeInterest = AutoMapper.Mapper.Map<PracticeInterestViewModel, PracticeInterest>(practiceInterest);
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Speciality Details - Practice Interest", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdatePracticeInterestAsync(profileId, dataModelPracticeInterest);

                    if (practiceInterest.PracticeInterestID == 0)
                    {
                        await profileManager.UpdatePracticeInterestAsync(profileId, dataModelPracticeInterest);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Speciality Details - Practice Interest", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);

                    }
                    else if (isCCO && practiceInterest.PracticeInterestID != 0)
                    {
                      
                        await profileManager.UpdatePracticeInterestAsync(profileId, dataModelPracticeInterest);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Speciality Details - Practice Interest", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(practiceInterest.UpdateHistory, UserAuthID, "PracticeInterestID", practiceInterest.PracticeInterestID, profileId);
                    }
                    else if (!isCCO && practiceInterest.PracticeInterestID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Board Specialty";
                        tracker.SubSection = "Practice Interest";
                        tracker.userAuthId = userId;
                        tracker.objId = practiceInterest.PracticeInterestID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/BoardSpecialty/UpdatePracticeInterestAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(practiceInterest, dataModelPracticeInterest, tracker);
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
                status = ExceptionMessage.PRACTICE_INTERSET_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, practiceInterest = dataModelPracticeInterest }, JsonRequestBehavior.AllowGet);
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