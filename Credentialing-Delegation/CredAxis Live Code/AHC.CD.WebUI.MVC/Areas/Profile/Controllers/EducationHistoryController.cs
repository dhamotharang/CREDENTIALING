using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Business.Profiles;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Resources.Document;
using AHC.CD.Entities.MasterData.Enums;
using PGChat;
using System.Dynamic;
using Newtonsoft.Json;
using AHC.CD.Business.MasterData;
using AHC.CD.Resources.Rules;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class EducationHistoryController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;
        private IMasterDataManager masterDataManager = null;

        public EducationHistoryController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager, IMasterDataManager masterDataManager)
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

        #region Education Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddEducationDetailAsync(int profileId, EducationDetailViewModel educationDetails)
        {
            string status = "true";
            EducationDetail education = null;
            bool isCCO = await GetUserRole();
            var educationType = "Education History Details";
            try
            {
                if (ModelState.IsValid)
                {                    

                    education = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);
                    DocumentDTO educationDocument = CreateDocument(educationDetails.CertificateDocumentFile);
                  
                    await profileManager.AddEducationDetailAsync(profileId, education, educationDocument);

                    if (educationDetails.EducationQualificationType == EducationQualificationType.UnderGraduate){
                        educationType = "Education History - Under Graduate/Professional Schools Details";
                    }
                    else if (educationDetails.EducationQualificationType == EducationQualificationType.Graduate){
                        educationType = "Education History - Graduate/Medical School Details";
                    }

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, educationType, "Added");
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
                status = ExceptionMessage.EDUCATION_DETAIL_CREATE_EXCEPTION;
            }

            return Json(new { status = status, educationDetails = education }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateEducationDetailAsync(int profileId, EducationDetailViewModel educationDetails)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            EducationDetail education = null;
            bool isCCO = await GetUserRole();
            var educationType = "Education History Details";
            try
            {
                if (ModelState.IsValid)
                {
                    education = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);

                    DocumentDTO document = CreateDocument(educationDetails.CertificateDocumentFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateEducationDetailAsync(profileId, education, document);

                    if (isCCO)
                    {
                       
                        await profileManager.UpdateEducationDetailAsync(profileId, education, document);
                        if (educationDetails.EducationQualificationType == EducationQualificationType.UnderGraduate)
                        {
                            educationType = "Education History - Under Graduate/Professional Schools Details";
                            successMessage = SuccessMessage.UNDER_GRADUATE_DETAIL_UPDATE_SUCCESS;
                        }
                        else if (educationDetails.EducationQualificationType == EducationQualificationType.Graduate)
                        {
                            educationType = "Education History - Graduate/Medical School Details";
                            successMessage = SuccessMessage.GRADUATE_DETAIL_UPDATE_SUCCESS;
                        }
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, educationType, "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.EDUCATION_CERTIFICATE_PATH, DocumentTitle.EDUCATION_CERTIFICATE, profileId);
                        if (documentTemporaryPath != null)
                        {
                            educationDetails.CertificatePath = documentTemporaryPath;
                            education.CertificatePath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Education History";

                        EducationDetail educationOldData = await profileUpdateManager.GetProfileDataByID(education, educationDetails.EducationDetailID);

                        dynamic uniqueRecord = new ExpandoObject();
                        
                        
                        if (educationDetails.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.UnderGraduate)
                        {
                            tracker.SubSection = "Under Graduate/Professional";
                            uniqueRecord.FieldName = "Under Graduate Detail";
                            successMessage = SuccessMessage.UNDER_GRADUATE_DETAIL_UPDATE_REQUEST_SUCCESS;
                        }
                        else if (educationDetails.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.Graduate)
                        {
                            tracker.SubSection = "Graduate/Medical";
                            uniqueRecord.FieldName = "Graduate Detail";
                            successMessage = SuccessMessage.GRADUATE_DETAIL_UPDATE_REQUEST_SUCCESS;
                        }

                        uniqueRecord.Value = (educationOldData.QualificationDegree != null ? educationOldData.QualificationDegree + " - " : "") +
                            educationOldData.SchoolInformation.SchoolName +
                            (educationOldData.SchoolInformation.Location != null ? " - " +
                            educationOldData.SchoolInformation.Location : "");

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        tracker.userAuthId = userId;
                        tracker.objId = educationDetails.EducationDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/EducationHistory/UpdateEducationDetailAsync?profileId=";

                        

                        educationDetails.CertificateDocumentFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(educationDetails, education, tracker);

                        //CnDHub.RequestForApprovalNotification();
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
                status = ExceptionMessage.EDUCATION_DETAIL_CREATE_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, educationDetails = education }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveEducationDetailAsync(int profileId, EducationDetailViewModel educationDetails)
        {
            string status = "true";
            EducationDetail dataModelEducationDetail = null;
            bool isCCO = await GetUserRole();
            ApplicationUser UserDetail = await GetUser();
            var educationType = "Education History Details";
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
                dataModelEducationDetail = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);
                var userid = UserDetail.Id;
            
              await profileManager.RemoveEducationDetailAsync(profileId, dataModelEducationDetail, userid);
              if (educationDetails.EducationQualificationType == EducationQualificationType.UnderGraduate)
              {
                  educationType = "Education History - Under Graduate/Professional Schools Details";
              }
              else if (educationDetails.EducationQualificationType == EducationQualificationType.Graduate)
              {
                  educationType = "Education History - Graduate/Medical School Details";
              }
              ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, educationType, "Removed");
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
                status = ExceptionMessage.EDUCATION_DETAIL_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, educationDetailViewModel = dataModelEducationDetail, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ECFMG Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateECFMGDetailAsync(int profileId, ECFMGDetailViewModel ecfmgDetails)
        {
            string status = "true";
            string successMessage = "";
            ECFMGDetail ecfmg = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    ecfmg = AutoMapper.Mapper.Map<ECFMGDetailViewModel, ECFMGDetail>(ecfmgDetails);

                    DocumentDTO document = CreateDocument(ecfmgDetails.ECFMGCertificateDocumentFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - ECFMG", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateECFMGDetailAsync(profileId, ecfmg, document);

                    if (ecfmgDetails.ECFMGDetailID == 0)
                    {
                     
                        await profileManager.UpdateECFMGDetailAsync(profileId, ecfmg, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - ECFMG", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                    }
                    else if (isCCO && ecfmgDetails.ECFMGDetailID != 0)
                    {
                      
                        await profileManager.UpdateECFMGDetailAsync(profileId, ecfmg, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - ECFMG", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.ECFMG_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && ecfmgDetails.ECFMGDetailID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.ECFMG_PATH, DocumentTitle.ECFMG_CERTIFICATE, profileId);
                        if (documentTemporaryPath != null)
                        {
                            ecfmg.ECFMGCertPath = documentTemporaryPath;
                            ecfmgDetails.ECFMGCertPath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Education History";
                        tracker.SubSection = "ECFMG Details";
                        tracker.userAuthId = userId;
                        tracker.objId = ecfmgDetails.ECFMGDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/EducationHistory/UpdateECFMGDetailAsync?profileId=";

                        ecfmgDetails.ECFMGCertificateDocumentFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(ecfmgDetails, ecfmg, tracker);
                        successMessage = SuccessMessage.ECFMG_DETAIL_UPDATE_REQUEST_SUCCESS;
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
                status = ExceptionMessage.ECFMG_CERTIFICATION_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, successMessage = successMessage, ecfmgDetails = ecfmg }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Residency/Internship/Fellowship Details

        //[HttpPost]
        //[AjaxAction]
        //[ProfileAuthorize(ProfileActionType.Add, false)]
        //public async Task<ActionResult> AddTrainingDetailAsync(int profileId, TrainingDetailViewModel TrainingDetails)
        //{
        //    string status = "true";
        //    TrainingDetail training = null;   
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            training = AutoMapper.Mapper.Map<TrainingDetailViewModel, TrainingDetail>(TrainingDetails);
        //            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Added");
        //            await notificationManager.SaveNotificationDetailAsync(notification);

        //            await profileManager.AddTrainingDetailAsync(profileId, training, null);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
        //        }
        //    }
        //    catch (DatabaseValidationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.ValidationError;
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ExceptionMessage.TRAINING_DETAIL_CREATE_EXCEPTION;
        //    }

        //    return Json(new { status = status, TrainingDetails = training }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[AjaxAction]
        //[ProfileAuthorize(ProfileActionType.Edit, false)]
        //public async Task<ActionResult> UpdateTrainingDetailAsync(int profileId, TrainingDetailViewModel TrainingDetails)
        //{
        //    string status = "true";
        //    TrainingDetail training = null;

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            training = AutoMapper.Mapper.Map<TrainingDetailViewModel, TrainingDetail>(TrainingDetails);
        //            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Training Details", "Updated");
        //            await notificationManager.SaveNotificationDetailAsync(notification);

        //            await profileManager.UpdateTrainingDetailAsync(profileId, training);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
        //        }
        //    }
        //    catch (DatabaseValidationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.ValidationError;
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ExceptionMessage.TRAINING_DETAIL_UPDATE_EXCEPTION;
        //    }

        //    return Json(new { status = status, TrainingDetails = training }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[AjaxAction]
        //[ProfileAuthorize(ProfileActionType.Add, false)]
        //public async Task<ActionResult> AddResidencyInternshipDetailAsync(int profileId, int trainingId, ResidencyInternshipDetailViewModel ResidencyDetails)
        //{
        //    string status = "true";
        //    ResidencyInternshipDetail residency = null;

        //    try
        //    {
        //        var data = Request;
        //        if (ModelState.IsValid)
        //        {
        //            residency = AutoMapper.Mapper.Map<ResidencyInternshipDetailViewModel, ResidencyInternshipDetail>(ResidencyDetails);
        //            DocumentDTO document = CreateDocument(ResidencyDetails.ResidecncyCertificateDocumentFile);
        //            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency Internship Details", "Added");
        //            await notificationManager.SaveNotificationDetailAsync(notification);

        //            await profileManager.AddResidencyInternshipDetailAsync(profileId, trainingId, residency, document);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
        //        }
        //    }            
        //    catch (ApplicationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log it

        //        status = ExceptionMessage.RESIDENCY_INTERNSHIP_DETAIL_CREATE_EXCEPTION;
        //    }

        //    return Json(new { status = status, ResidencyDetails = residency }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[AjaxAction]
        //[ProfileAuthorize(ProfileActionType.Edit, false)]
        //public async Task<ActionResult> UpdateResidencyInternshipDetailAsync(int profileId,int trainingId, ResidencyInternshipDetailViewModel ResidencyDetails)
        //{
        //    string status = "true";
        //    ResidencyInternshipDetail residency = null;

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            residency = AutoMapper.Mapper.Map<ResidencyInternshipDetailViewModel, ResidencyInternshipDetail>(ResidencyDetails);
        //            DocumentDTO document = CreateDocument(ResidencyDetails.ResidecncyCertificateDocumentFile);
        //            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency Internship Details", "Updated");
        //            await notificationManager.SaveNotificationDetailAsync(notification);

        //            await profileManager.UpdateResidencyInternshipDetailAsync(profileId, trainingId, residency, document);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
        //        }
        //    }           
        //    catch (ApplicationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log it

        //        status = ExceptionMessage.RESIDENCY_INTERNSHIP_DETAIL_UPDATE_EXCEPTION;
        //    }

        //    return Json(new { status = status, ResidencyDetails = residency }, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region PostGraduate Training/CME Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddCMECErtificationAsync(int profileId, CMECertificationViewModel CMEDetails)
        {
            string status = "true";
            CMECertification CME = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    CME = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);
                    DocumentDTO document = CreateDocument(CMEDetails.CertificateDocumentFile);
                    await profileManager.AddCMECertificationAsync(profileId, CME, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Post Graduate Training - CME Details", "Added");
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
                status = ExceptionMessage.CME_CERTIFICATION_CREATE_EXCEPTION;
            }

            return Json(new { status = status, CMEDetails = CME }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateCMECertificationAsync(int profileId, CMECertificationViewModel CMEDetails)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            CMECertification CME = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    CME = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);

                    DocumentDTO document = CreateDocument(CMEDetails.CertificateDocumentFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Post Graduate Training - CME Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateCMECertificationAsync(profileId, CME, document);

                    if (isCCO)
                    {
                      
                        await profileManager.UpdateCMECertificationAsync(profileId, CME, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Post Graduate Training - CME Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.CME_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.CME_CERTIFICATION_PATH, DocumentTitle.CME_CERTIFICATION, profileId);
                        if (documentTemporaryPath != null)
                        {
                            CMEDetails.CertificatePath = documentTemporaryPath;
                            CME.CertificatePath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Education History";
                        tracker.SubSection = "PostGraduate Training/CME";
                        tracker.userAuthId = userId;
                        tracker.objId = CMEDetails.CMECertificationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/EducationHistory/UpdateCMECertificationAsync?profileId=";

                        CMECertification cmeOldData = await profileUpdateManager.GetProfileDataByID(CME, CMEDetails.CMECertificationID);
                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Post Graduation Training/CME Detail";
                        uniqueRecord.Value = (cmeOldData.QualificationDegree != null ? cmeOldData.QualificationDegree + " - " : "") + cmeOldData.Certification;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        CMEDetails.CertificateDocumentFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(CMEDetails, CME, tracker);
                        successMessage = SuccessMessage.CME_DETAIL_UPDATE_REQUEST_SUCCESS;
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
                status = ExceptionMessage.CME_CERTIFICATION_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, CMEDetails = CME }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveCertificationDetailAsync(int profileId, CMECertificationViewModel CMEDetails)
        {
            string status = "true";
            CMECertification dataModelCMECertification = null;
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
                dataModelCMECertification = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);
                var UserAuthID = UserDetail.Id;
               
                await profileManager.RemoveCertificationDetailAsync(profileId, dataModelCMECertification, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Post Graduate Training - CME Details", "Removed");
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
                status = ExceptionMessage.CME_CERTIFICATION_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, certificationCMEViewModel = dataModelCMECertification, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Program Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddProgramDetailAsync(int profileId, ProgramDetailViewModel programDetails)
        {
            string status = "true";
            ProgramDetail program = null;
            bool isCCO = await GetUserRole();
            try
            {
                var data = Request;
                if (ModelState.IsValid)
                {

                    program = AutoMapper.Mapper.Map<ProgramDetailViewModel, ProgramDetail>(programDetails);
                    DocumentDTO document = CreateDocument(programDetails.ProgramDocumentPath);
                   
                    await profileManager.AddProgramDetailAsync(profileId, program, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Added");
                    await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                //Log it

                status = ExceptionMessage.PROGRAM_DETAIL_CREATE_EXCEPTION;
            }

            return Json(new { status = status, programDetails = program }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateProgramDetailAsync(int profileId, ProgramDetailViewModel programDetails)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            ProgramDetail program = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    program = AutoMapper.Mapper.Map<ProgramDetailViewModel, ProgramDetail>(programDetails);

                    DocumentDTO document = CreateDocument(programDetails.ProgramDocumentPath);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateProgramDetailAsync(profileId, program, document);

                    if (isCCO)
                    {
                      
                        await profileManager.UpdateProgramDetailAsync(profileId, program, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.PROGRAM_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.PROGRAM_PATH, DocumentTitle.PROGRAM_CERTIFICATE, profileId);
                        if (documentTemporaryPath != null)
                        {
                            program.DocumentPath = documentTemporaryPath;
                            programDetails.DocumentPath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Education History";
                        tracker.SubSection = "Residency/Internship/Fellowship";
                        tracker.userAuthId = userId;
                        tracker.objId = programDetails.ProgramDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/EducationHistory/UpdateProgramDetailAsync?profileId=";

                        ProgramDetail programOldData = await profileUpdateManager.GetProfileDataByID(program, programDetails.ProgramDetailID);
                        var specialtyDetail = programOldData.SpecialtyID != null ? await masterDataManager.GetSpecialtyByIDAsync(programOldData.SpecialtyID) : null;

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Program Detail";
                        uniqueRecord.Value = programOldData.ResidencyInternshipProgramType.ToString() +
                            (specialtyDetail != null ? " - " + specialtyDetail.Name : "") + " - " + programOldData.SchoolInformation.SchoolName +
                            (programOldData.SchoolInformation.Location != null ? " - " + programOldData.SchoolInformation.Location : "");

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        programDetails.ProgramDocumentPath = null;

                        profileUpdateManager.AddProfileUpdateForProvider(programDetails, program, tracker);
                        successMessage = SuccessMessage.PROGRAM_DETAIL_UPDATE_REQUEST_SUCCESS;
                    }
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                //Log it

                status = ExceptionMessage.PROGRAM_DETAIL_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, programDetails = program }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProgramDetailAsync(int profileId, ProgramDetailViewModel programDetails)
        {
            string status = "true";
            ProgramDetail dataModelProgramDetail = null;
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
                dataModelProgramDetail = AutoMapper.Mapper.Map<ProgramDetailViewModel, ProgramDetail>(programDetails);
                var UserAuthID = UserDetail.Id;
               
                await profileManager.RemoveProgramDetailAsync(profileId, dataModelProgramDetail, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Removed");
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
                status = ExceptionMessage.EDUCATION_DETAIL_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, residencyInternshipViewModel = dataModelProgramDetail, UserName = UserName }, JsonRequestBehavior.AllowGet);
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

            var roleIDs = RoleManager.Roles.ToList().Where(r => r.Name == "CCO" || r.Name == "CRA" || r.Name == "CRA").Select(r => r.Id).ToList();

            foreach (var id in roleIDs)
            {
                status = user.Roles.Any(r => r.RoleId == id);

                if(status)
                    break;
            }

            return status;
        }

        #endregion
    }
}