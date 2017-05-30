using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Resources.Document;
using System.Dynamic;
using Newtonsoft.Json;
using AHC.CD.Resources.Rules;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class IdentificationAndLicenseController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;
        public IdentificationAndLicenseController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
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

        #region State License

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddStateLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
        {
            string status = "true";
            StateLicenseInformation dataModelStateLicense = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelStateLicense = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);
                    DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);


                    await profileManager.AddStateLicenseAsync(profileId, dataModelStateLicense, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and License - State License Details", "Added");
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

            return Json(new { status = status, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateStateLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            StateLicenseInformation dataModelStateLicense = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelStateLicense = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);

                    DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateStateLicenseAsync(profileId, dataModelStateLicense, document);

                    if (isCCO)
                    {

                        await profileManager.UpdateStateLicenseAsync(profileId, dataModelStateLicense, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification And License - State License Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.STATE_LICENSE_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.STATE_LICENSE_PATH, DocumentTitle.STATE_LICENSE, profileId);
                        if (documentTemporaryPath != null)
                        {
                            stateLicense.StateLicenseDocumentPath = documentTemporaryPath;
                            dataModelStateLicense.StateLicenseDocumentPath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "State License";
                        tracker.userAuthId = userId;
                        tracker.objId = stateLicense.StateLicenseInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateStateLicenseAsync?profileId=";
                        //tracker.IncludeProperties = "StateLicenseInformation.ProviderType, StateLicenseInformation.StateLicenseStatus";

                        StateLicenseInformation stateLicenseOldData = await profileUpdateManager.GetProfileDataByID(dataModelStateLicense, stateLicense.StateLicenseInformationID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "State License Detail";
                        uniqueRecord.Value = stateLicenseOldData.LicenseNumber + (stateLicenseOldData.IssueState != null ? " - " + stateLicenseOldData.IssueState : "");

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        stateLicense.StateLicenseDocumentFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(stateLicense, dataModelStateLicense, tracker);

                        successMessage = SuccessMessage.STATE_LICENSE_DETAIL_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewStateLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
        {
            stateLicense.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            StateLicenseInformation dataModelStateLicense = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelStateLicense = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);

                    //DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);
                    ////DocumentDTO document = null;

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License Details", "Renewed");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.RenewStateLicenseAsync(profileId, dataModelStateLicense, document);


                    if (isCCO)
                    {

                        DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);
                        //DocumentDTO document = null;


                        await profileManager.RenewStateLicenseAsync(profileId, dataModelStateLicense, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification And License - State License Details", "Renewed");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.STATE_LICENSE_DETAIL_RENEW_SUCCESS;
                        }

                    }
                    else
                    {
                        DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);

                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string userId = await GetUserAuthId();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.STATE_LICENSE_PATH, DocumentTitle.STATE_LICENSE, profileId);
                        if (documentTemporaryPath != null)
                        {
                            stateLicense.StateLicenseDocumentPath = documentTemporaryPath;
                            dataModelStateLicense.StateLicenseDocumentPath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "State License";
                        tracker.userAuthId = userId;
                        tracker.objId = stateLicense.StateLicenseInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/RenewStateLicenseAsync?profileId=";

                        StateLicenseInformation stateLicenseOldData = await profileUpdateManager.GetProfileDataByID(dataModelStateLicense, stateLicense.StateLicenseInformationID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "State License Detail";
                        uniqueRecord.Value = stateLicenseOldData.LicenseNumber + (stateLicenseOldData.IssueState != null ? " - " + stateLicenseOldData.IssueState : "");

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        stateLicense.StateLicenseDocumentFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(stateLicense, dataModelStateLicense, tracker);

                        successMessage = SuccessMessage.STATE_LICENSE_DETAIL_RENEW_REQUEST_SUCCESS;
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
                //                status = ExceptionMessage.STATE_LICENSE_RENEW_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public async Task<ActionResult> RenewMedicareInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareInformationViewModel medicare)
        //{
        //    medicare.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
        //    string status = "true";
        //    MedicareInformation dataModelMedicare = null;
        //    bool isCCO = await GetUserRole();

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            dataModelMedicare = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(medicare);

        //            //DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);
        //            ////DocumentDTO document = null;

        //            //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License Details", "Renewed");
        //            //await notificationManager.SaveNotificationDetailAsync(notification);

        //            //await profileManager.RenewStateLicenseAsync(profileId, dataModelStateLicense, document);


        //            if (isCCO)
        //            {

        //                DocumentDTO document = CreateDocument(medicare.CertificateFile);
        //                //DocumentDTO document = null;


        //                await profileManager.RenewMedicareInformationAsync(profileId, dataModelMedicare, document);
        //                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification And License - Medicare Information", "Renewed");
        //                await notificationManager.SaveNotificationDetailAsync(notification);

        //            }
        //            else
        //            {
        //                DocumentDTO document = CreateDocument(medicare.CertificateFile);

        //                ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
        //                string userId = await GetUserAuthId();

        //                string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.MEDICARE_PATH, DocumentTitle.MEDICARE, profileId);
        //                if (documentTemporaryPath != null)
        //                {
        //                    medicare.CertificatePath = documentTemporaryPath;
        //                    dataModelMedicare.CertificatePath = documentTemporaryPath;
        //                }
        //                tracker.ProfileId = profileId;
        //                tracker.Section = "Identification And License";
        //                tracker.SubSection = "Medicare Information";
        //                tracker.userAuthId = userId;
        //                tracker.objId = medicare.MedicareInformationID;
        //                tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
        //                tracker.url = "/Profile/IdentificationAndLicense/RenewMedicareInformationAsync?profileId=";

        //                medicare.CertificateFile = null;

        //                profileUpdateManager.AddProfileUpdateForProvider(medicare, dataModelMedicare, tracker);
        //            }

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
        //        status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
        //        //                status = ExceptionMessage.STATE_LICENSE_RENEW_EXCEPTION;
        //    }

        //    return Json(new { status = status, stateLicense = dataModelMedicare }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveStateLicenseAsync(int profileId, StateLicenseViewModel stateLicense)
        {
            string status = "true";
            StateLicenseInformation dataModelStateLicenseInformation = null;
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
                dataModelStateLicenseInformation = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);
                var UserAuthID = UserDetail.Id;

                await profileManager.RemoveStateLicenseAsync(profileId, dataModelStateLicenseInformation, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification And License - State License Details", "Removed");
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
                status = ExceptionMessage.STATE_LICENSE_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, stateLicense = dataModelStateLicenseInformation, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Federal DEA

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddFederalDEALicenseAsync(int profileId, FederalDEAInformationViewModel federalDea)
        {
            string status = "true";
            FederalDEAInformation dataModelFederalDEA = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFederalDEA = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDea);
                    DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);

                    await profileManager.AddFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - DEA License Details", "Added");
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

                //status = ExceptionMessage.FEDERAL_DEA_LICENSE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateFederalDEALicenseAsync(int profileId, FederalDEAInformationViewModel federalDea)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            FederalDEAInformation dataModelFederalDEA = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFederalDEA = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDea);

                    DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - DEA License Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);

                    if (isCCO)
                    {

                        await profileManager.UpdateFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - DEA License Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.FEDERAL_DEA_UPDATE_SUCCESS;

                        }

                    }
                    else
                    {
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.DEA_PATH, DocumentTitle.DEA, profileId);

                        if (documentTemporaryPath != null)
                        {
                            federalDea.DEALicenceCertPath = documentTemporaryPath;
                            dataModelFederalDEA.DEALicenceCertPath = documentTemporaryPath;
                        }
                        string userId = await GetUserAuthId();
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "Federal DEA";
                        tracker.userAuthId = userId;
                        tracker.objId = federalDea.FederalDEAInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateFederalDEALicenseAsync?profileId=";

                        FederalDEAInformation federalDEAOldData = await profileUpdateManager.GetProfileDataByID(dataModelFederalDEA, federalDea.FederalDEAInformationID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Federal DEA Detail";
                        uniqueRecord.Value = federalDEAOldData.DEANumber + " - " + federalDEAOldData.StateOfReg;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        federalDea.DEALicenceCertFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(federalDea, dataModelFederalDEA, tracker);
                        successMessage = SuccessMessage.FEDERAL_DEA_UPDATE_REQUEST_SUCCESS;
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
                //                status = ExceptionMessage.FEDERAL_DEA_LICENSE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewFederalDEALicenseAsync(int profileId, FederalDEAInformationViewModel federalDea)
        {
            federalDea.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            FederalDEAInformation dataModelFederalDEA = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFederalDEA = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDea);

                    DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);
                    //DocumentDTO document = null;
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - DEA License Details", "Renewed");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.RenewFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);

                    if (isCCO)
                    {
                        //DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);
                        //DocumentDTO document = null;

                        await profileManager.RenewFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - DEA License Details", "Renewed");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.FEDERAL_DEA_RENEW_SUCCESS;

                        }

                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.DEA_PATH, DocumentTitle.DEA, profileId);
                        if (documentTemporaryPath != null)
                        {
                            federalDea.DEALicenceCertPath = documentTemporaryPath;
                            dataModelFederalDEA.DEALicenceCertPath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "Federal DEA";
                        tracker.userAuthId = userId;
                        tracker.objId = federalDea.FederalDEAInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/RenewFederalDEALicenseAsync?profileId=";

                        FederalDEAInformation federalDEAOldData = await profileUpdateManager.GetProfileDataByID(dataModelFederalDEA, federalDea.FederalDEAInformationID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Federal DEA Detail";
                        uniqueRecord.Value = federalDEAOldData.DEANumber + " - " + federalDEAOldData.StateOfReg;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        federalDea.DEALicenceCertFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(federalDea, dataModelFederalDEA, tracker);
                        successMessage = SuccessMessage.FEDERAL_DEA_RENEW_REQUEST_SUCCESS;
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
                //              status = ExceptionMessage.FEDERAL_DEA_LICENSE_RENEW_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveFederalDEALicense(int profileId, FederalDEAInformationViewModel federalDEAInformationViewModel)
        {
            string status = "true";
            FederalDEAInformation dataModelFederalDEAInformation = null;
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
                dataModelFederalDEAInformation = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDEAInformationViewModel);
                var UserAuthID = UserDetail.Id;

                dataModelFederalDEAInformation = await profileManager.RemoveFederalDEALicenseAsync(profileId, dataModelFederalDEAInformation, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - DEA License Details", "Removed");
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

            return Json(new { status = status, federalDea = dataModelFederalDEAInformation, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CDSCInformation

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddCDSCLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
            CDSCInformation dataModelCDSCLicense = null;
            string userId = await GetUserAuthId();
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCDSCLicense = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);

                    DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);

                    await profileManager.AddCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - CDS Information Details", "Added");
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
                //                status = ExceptionMessage.CDSC_LICENSE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateCDSCLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            CDSCInformation dataModelCDSCLicense = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCDSCLicense = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);

                    DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - CDSC Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);

                    if (isCCO)
                    {

                        await profileManager.UpdateCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - CDS Information Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.CDS_INFO_UPDATE_SUCCESS;

                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.CDSC_PATH, DocumentTitle.CDSC, profileId);
                        if (documentTemporaryPath != null)
                        {
                            CDSCInformation.CDSCCerificatePath = documentTemporaryPath;
                            dataModelCDSCLicense.CDSCCerificatePath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "CDS Information";
                        tracker.userAuthId = userId;
                        tracker.objId = CDSCInformation.CDSCInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateCDSCLicenseAsync?profileId=";

                        CDSCInformation cdsOldData = await profileUpdateManager.GetProfileDataByID(dataModelCDSCLicense, CDSCInformation.CDSCInformationID);
                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "CDS Detail";
                        uniqueRecord.Value = cdsOldData.CertNumber + " - " + cdsOldData.State;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        CDSCInformation.CDSCCerificateFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(CDSCInformation, dataModelCDSCLicense, tracker);
                        successMessage = SuccessMessage.CDS_INFO_UPDATE_REQUEST_SUCCESS;
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
                //                status = ExceptionMessage.CDSC_LICENSE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewCDSCLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel CDSCInformation)
        {
            CDSCInformation.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            CDSCInformation dataModelCDSCLicense = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCDSCLicense = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);

                    DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - CDSC Details", "Renewed");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.RenewCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);

                    if (isCCO)
                    {
                        //DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);


                        await profileManager.RenewCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - CDS Information Details", "Renewed");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.CDS_INFO_RENEW_SUCCESS;

                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.CDSC_PATH, DocumentTitle.CDSC, profileId);
                        if (documentTemporaryPath != null)
                        {
                            CDSCInformation.CDSCCerificatePath = documentTemporaryPath;
                            dataModelCDSCLicense.CDSCCerificatePath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "CDS Information";
                        tracker.userAuthId = userId;
                        tracker.objId = CDSCInformation.CDSCInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/RenewCDSCLicenseAsync?profileId=";

                        CDSCInformation cdsOldData = await profileUpdateManager.GetProfileDataByID(dataModelCDSCLicense, CDSCInformation.CDSCInformationID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "CDS Detail";
                        uniqueRecord.Value = cdsOldData.CertNumber + " - " + cdsOldData.State;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        CDSCInformation.CDSCCerificateFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(CDSCInformation, dataModelCDSCLicense, tracker);
                        successMessage = SuccessMessage.CDS_INFO_RENEW_REQUEST_SUCCESS;
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
                //                status = ExceptionMessage.CDSC_LICENSE_RENEW_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveCDSCLicenseAsync(int profileId, CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
            CDSCInformation dataModelCDSCInformation = null;
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

                dataModelCDSCInformation = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);
                var UserAuthID = UserDetail.Id;

                await profileManager.RemoveCDSCLicenseAsync(profileId, dataModelCDSCInformation, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - CDS Information Details", "Removed");
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
                status = ExceptionMessage.CDSC_LICENSE_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, cDSCInformation = dataModelCDSCInformation, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Medicare Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddMedicareInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareInformationViewModel MedicareInformation)
        {
            string status = "true";
            MedicareInformation dataModelMedicareInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicareInformation = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(MedicareInformation);
                    DocumentDTO document = CreateDocument(MedicareInformation.CertificateFile);

                    await profileManager.AddMedicareInformationAsync(profileId, dataModelMedicareInformation, document);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Medicare Details", "Added");
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
                //                status = ExceptionMessage.MEDICARE_CREATE_EXCEPTION;
            }

            return Json(new { status = status, MedicareInformation = dataModelMedicareInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateMedicareInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareInformationViewModel MedicareInformation)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            MedicareInformation dataModelMedicareInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicareInformation = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(MedicareInformation);

                    DocumentDTO document = CreateDocument(MedicareInformation.CertificateFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - Medicare Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateMedicareInformationAsync(profileId, dataModelMedicareInformation, document);

                    if (isCCO)
                    {

                        await profileManager.UpdateMedicareInformationAsync(profileId, dataModelMedicareInformation, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Medicare Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                            successMessage = SuccessMessage.MEDICARE_INFO_UPDATE_SUCCESS;

                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.MEDICARE_PATH, DocumentTitle.MEDICARE, profileId);
                        if (documentTemporaryPath != null)
                        {
                            MedicareInformation.CertificatePath = documentTemporaryPath;
                            dataModelMedicareInformation.CertificatePath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "Medicare Information";
                        tracker.userAuthId = userId;
                        tracker.objId = MedicareInformation.MedicareInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateMedicareInformationAsync?profileId=";

                        MedicareInformation medicareOldData = await profileUpdateManager.GetProfileDataByID(dataModelMedicareInformation, MedicareInformation.MedicareInformationID);
                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Medicare Detail";
                        uniqueRecord.Value = medicareOldData.LicenseNumber;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        MedicareInformation.CertificateFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(MedicareInformation, dataModelMedicareInformation, tracker);
                        successMessage = SuccessMessage.MEDICARE_INFO_UPDATE_REQUEST_SUCCESS;
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
                //                status = ExceptionMessage.MEDICARE_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, MedicareInformation = dataModelMedicareInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveMedicareInformation(int profileId, MedicareInformationViewModel medicareInformationViewModel)
        {
            string status = "true";
            MedicareInformation dataModelMedicareInformation = null;
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
                dataModelMedicareInformation = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(medicareInformationViewModel);
                var UserAuthID = UserDetail.Id;

                await profileManager.RemoveMedicareInformationAsync(profileId, dataModelMedicareInformation, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Medicare Details", "Removed");
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

            return Json(new { status = status, MedicareInfo = dataModelMedicareInformation, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Medicaid Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddMedicaidInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicaidInformationViewModel MedicaidInformation)
        {
            string status = "true";
            MedicaidInformation dataModelMedicaidInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformationViewModel, MedicaidInformation>(MedicaidInformation);
                    DocumentDTO document = CreateDocument(MedicaidInformation.CertificateFile);

                    await profileManager.AddMedicaidInformationAsync(profileId, dataModelMedicaidInformation, document);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Medicaid Details", "Added");
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
                //                status = ExceptionMessage.MEDICAID_CREATE_EXCEPTION;
            }

            return Json(new { status = status, MedicaidInformation = dataModelMedicaidInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateMedicaidInformationAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicaidInformationViewModel MedicaidInformation)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            MedicaidInformation dataModelMedicaidInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformationViewModel, MedicaidInformation>(MedicaidInformation);

                    DocumentDTO document = CreateDocument(MedicaidInformation.CertificateFile);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License  - Medicaid Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateMedicaidInformationAsync(profileId, dataModelMedicaidInformation, document);

                    if (isCCO)
                    {

                        await profileManager.UpdateMedicaidInformationAsync(profileId, dataModelMedicaidInformation, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Medicaid Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                            successMessage = SuccessMessage.MEDICARE_INFO_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string documentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.MEDICAID_PATH, DocumentTitle.MEDICAID, profileId);
                        if (documentTemporaryPath != null)
                        {
                            MedicaidInformation.CertificatePath = documentTemporaryPath;
                            dataModelMedicaidInformation.CertificatePath = documentTemporaryPath;
                        }
                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "Medicaid Information";
                        tracker.userAuthId = userId;
                        tracker.objId = MedicaidInformation.MedicaidInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateMedicaidInformationAsync?profileId=";

                        MedicaidInformation medicaidOldData = await profileUpdateManager.GetProfileDataByID(dataModelMedicaidInformation, MedicaidInformation.MedicaidInformationID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Medicaid Detail";
                        uniqueRecord.Value = medicaidOldData.LicenseNumber + " - " + medicaidOldData.State;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        MedicaidInformation.CertificateFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(MedicaidInformation, dataModelMedicaidInformation, tracker);
                        successMessage = SuccessMessage.MEDICAID_INFO_UPDATE_REQUEST_SUCCESS;
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
                //                status = ExceptionMessage.MEDICAID_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, MedicaidInformation = dataModelMedicaidInformation }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveMedicaidInformation(int profileId, MedicaidInformationViewModel medicaidInformationViewModel)
        {
            string status = "true";
            MedicaidInformation dataModelMedicaidInformation = null;
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
                dataModelMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformationViewModel, MedicaidInformation>(medicaidInformationViewModel);
                var UserAuthID = UserDetail.Id;

                await profileManager.RemoveMedicaidInformationAsync(profileId, dataModelMedicaidInformation, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Medicaid Details", "Removed");
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

            return Json(new { status = status, MedicaidInfo = dataModelMedicaidInformation, UserName = UserName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdateOtherIdentificationNumberAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.OtherIdentificationNumberViewModel OtherIdentificationNumber)
        {
            string status = "true";
            string successMessage = "";
            OtherIdentificationNumber dataModelOtherIdentificationNumber = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherIdentificationNumber = AutoMapper.Mapper.Map<OtherIdentificationNumberViewModel, OtherIdentificationNumber>(OtherIdentificationNumber);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - Other Identification Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateOtherIdentificationNumberAsync(profileId, dataModelOtherIdentificationNumber);

                    if (OtherIdentificationNumber.OtherIdentificationNumberID == 0)
                    {

                        await profileManager.UpdateOtherIdentificationNumberAsync(profileId, dataModelOtherIdentificationNumber);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Other Identification Details", "Added");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else if (isCCO && OtherIdentificationNumber.OtherIdentificationNumberID != 0)
                    {
                        dataModelOtherIdentificationNumber = AutoMapper.Mapper.Map<OtherIdentificationNumberViewModel, OtherIdentificationNumber>(OtherIdentificationNumber);

                        await profileManager.UpdateOtherIdentificationNumberAsync(profileId, dataModelOtherIdentificationNumber);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and Licenses - Other Identification Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.OTHER_IDENTIFICATION_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && OtherIdentificationNumber.OtherIdentificationNumberID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();


                        tracker.ProfileId = profileId;
                        tracker.Section = "Identification And License";
                        tracker.SubSection = "Other Identification Number";
                        tracker.userAuthId = userId;
                        tracker.objId = OtherIdentificationNumber.OtherIdentificationNumberID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateOtherIdentificationNumberAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(OtherIdentificationNumber, dataModelOtherIdentificationNumber, tracker);
                        successMessage = SuccessMessage.OTHER_IDENTIFICATION_UPDATE_REQUEST_SUCCESS;
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
                //                status = ExceptionMessage.OTHER_IDENTIFICATION_NUMBER_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, successMessage = successMessage, OtherIdentificationNumber = dataModelOtherIdentificationNumber }, JsonRequestBehavior.AllowGet);
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

        private async Task<ApplicationUser> GetUser()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            return user;
        }
        #endregion
    }
}