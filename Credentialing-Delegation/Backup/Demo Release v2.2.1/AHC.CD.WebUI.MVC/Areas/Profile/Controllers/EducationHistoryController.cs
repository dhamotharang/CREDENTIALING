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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class EducationHistoryController : Controller
    {
         private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public EducationHistoryController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
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

        #region Education Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddEducationDetailAsync(int profileId, EducationDetailViewModel educationDetails)
        {
            string status = "true";
            EducationDetail education = null;
           
            try
            {
                if (ModelState.IsValid)
                {
                    education = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);
                    DocumentDTO educationDocument = CreateDocument(educationDetails.CertificateDocumentFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddEducationDetailAsync(profileId, education, educationDocument);
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
            EducationDetail education = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    education = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);

                    DocumentDTO document = CreateDocument(educationDetails.CertificateDocumentFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateEducationDetailAsync(profileId, education, document);

                    //if (isCCO)
                    //{                        
                    //    DocumentDTO document = CreateDocument(educationDetails.CertificateDocumentFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateEducationDetailAsync(profileId, education, document);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Education History";

                    //    if (educationDetails.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.UnderGraduate)
                    //    {
                    //        tracker.SubSection = "Under Graduate/Professional";
                    //    }
                    //    else if (educationDetails.EducationQualificationType == AHC.CD.Entities.MasterData.Enums.EducationQualificationType.Graduate)
                    //    {
                    //        tracker.SubSection = "Graduate/Medical";
                    //    }
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = educationDetails.EducationDetailID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/EducationHistory/UpdateEducationDetailAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(educationDetails,education, tracker);
                    //}                  

                    
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
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveEducationDetailAsync(int profileId, EducationDetailViewModel educationDetails)
        {
            string status = "true";
            EducationDetail dataModelEducationDetail = null;

            try
            {
                dataModelEducationDetail = AutoMapper.Mapper.Map<EducationDetailViewModel, EducationDetail>(educationDetails);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveEducationDetailAsync(profileId, dataModelEducationDetail);
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

            return Json(new { status = status, educationDetailViewModel = dataModelEducationDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ECFMG Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateECFMGDetailAsync(int profileId, ECFMGDetailViewModel ecfmgDetails)
        {
            string status = "true";
            ECFMGDetail ecfmg = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    ecfmg = AutoMapper.Mapper.Map<ECFMGDetailViewModel, ECFMGDetail>(ecfmgDetails);

                    DocumentDTO document = CreateDocument(ecfmgDetails.ECFMGCertificateDocumentFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - ECFMG", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateECFMGDetailAsync(profileId, ecfmg, document);

                    //if (ecfmgDetails.ECFMGDetailID == 0)
                    //{                        
                    //    DocumentDTO document = CreateDocument(ecfmgDetails.ECFMGCertificateDocumentFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - ECFMG", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateECFMGDetailAsync(profileId, ecfmg, document);
                        
                    //}
                    //else if (isCCO && ecfmgDetails.ECFMGDetailID != 0)
                    //{                        
                    //    DocumentDTO document = CreateDocument(ecfmgDetails.ECFMGCertificateDocumentFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - ECFMG", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateECFMGDetailAsync(profileId, ecfmg, document);
                    //}
                    //else if (!isCCO && ecfmgDetails.ECFMGDetailID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Education History";
                    //    tracker.SubSection = "ECFMG Details"; 
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = ecfmgDetails.ECFMGDetailID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/EducationHistory/UpdateECFMGDetailAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(ecfmgDetails,ecfmg, tracker);
                    //}
                    
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

            return Json(new { status = status, ecfmgDetails = ecfmg }, JsonRequestBehavior.AllowGet);
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

            try
            {
                if (ModelState.IsValid)
                {
                    CME = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);
                    DocumentDTO document = CreateDocument(CMEDetails.CertificateDocumentFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Post Graduate Training - CME Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddCMECertificationAsync(profileId, CME, document);
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
            CMECertification CME = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    CME = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);

                    DocumentDTO document = CreateDocument(CMEDetails.CertificateDocumentFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Post Graduate Training - CME Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateCMECertificationAsync(profileId, CME, document);

                    //if (isCCO)
                    //{                        
                    //    DocumentDTO document = CreateDocument(CMEDetails.CertificateDocumentFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Post Graduate Training - CME Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateCMECertificationAsync(profileId, CME, document);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Education History";
                    //    tracker.SubSection = "PostGraduate Training/CME";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = CMEDetails.CMECertificationID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/EducationHistory/UpdateCMECertificationAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(CMEDetails,CME, tracker);
                    //}
                    
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

            return Json(new { status = status, CMEDetails = CME }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveCertificationDetailAsync(int profileId, CMECertificationViewModel CMEDetails)
        {
            string status = "true";
            CMECertification dataModelCMECertification = null;

            try
            {
                dataModelCMECertification = AutoMapper.Mapper.Map<CMECertificationViewModel, CMECertification>(CMEDetails);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "CME Certification Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveCertificationDetailAsync(profileId, dataModelCMECertification);
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

            return Json(new { status = status, certificationCMEViewModel = dataModelCMECertification }, JsonRequestBehavior.AllowGet);
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

            try
            {
                var data = Request;
                if (ModelState.IsValid)
                {
                    program = AutoMapper.Mapper.Map<ProgramDetailViewModel, ProgramDetail>(programDetails);
                    DocumentDTO document = CreateDocument(programDetails.ProgramDocumentPath);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddProgramDetailAsync(profileId, program, document);
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
            ProgramDetail program = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    program = AutoMapper.Mapper.Map<ProgramDetailViewModel, ProgramDetail>(programDetails);

                    DocumentDTO document = CreateDocument(programDetails.ProgramDocumentPath);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateProgramDetailAsync(profileId, program, document);

                    //if (isCCO)
                    //{                        
                    //    DocumentDTO document = CreateDocument(programDetails.ProgramDocumentPath);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Education History - Residency/Internship/Fellowship Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateProgramDetailAsync(profileId, program, document);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Education History";
                    //    tracker.SubSection = "Residency/Internship/Fellowship";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = programDetails.ProgramDetailID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/EducationHistory/UpdateProgramDetailAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(programDetails,program, tracker);
                    //}
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

            return Json(new { status = status, programDetails = program }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveProgramDetailAsync(int profileId, ProgramDetailViewModel programDetails)
        {
            string status = "true";
            ProgramDetail dataModelProgramDetail = null;

            try
            {
                dataModelProgramDetail = AutoMapper.Mapper.Map<ProgramDetailViewModel, ProgramDetail>(programDetails);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Program Detail", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveProgramDetailAsync(profileId, dataModelProgramDetail);
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

            return Json(new { status = status, residencyInternshipViewModel = dataModelProgramDetail }, JsonRequestBehavior.AllowGet);
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
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "CCO");           

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }

        #endregion
    }
}