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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelStateLicense = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);
                    DocumentDTO document = CreateDocument(stateLicense.StateLicenseDocumentFile);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddStateLicenseAsync(profileId, dataModelStateLicense, document);
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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateStateLicenseAsync(profileId, dataModelStateLicense, document);
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
                        tracker.Section = "Identification And Licenses";
                        tracker.SubSection = "State License";
                        tracker.userAuthId = userId;
                        tracker.objId = stateLicense.StateLicenseInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateStateLicenseAsync?profileId=";
                        //tracker.IncludeProperties = "StateLicenseInformation.ProviderType, StateLicenseInformation.StateLicenseStatus";

                        stateLicense.StateLicenseDocumentFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(stateLicense, dataModelStateLicense, tracker);
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

            return Json(new { status = status, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewStateLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
        {
            string status = "true";
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

                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License Details", "Renewed");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.RenewStateLicenseAsync(profileId, dataModelStateLicense, document);
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

                        stateLicense.StateLicenseDocumentFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(stateLicense, dataModelStateLicense, tracker);
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

            return Json(new { status = status, stateLicense = dataModelStateLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveStateLicenseAsync(int profileId, StateLicenseViewModel stateLicense)
        {
            string status = "true";
            StateLicenseInformation dataModelStateLicenseInformation = null;

            try
            {
                dataModelStateLicenseInformation = AutoMapper.Mapper.Map<StateLicenseViewModel, StateLicenseInformation>(stateLicense);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "State License Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveStateLicenseAsync(profileId, dataModelStateLicenseInformation);
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

            return Json(new { status = status, stateLicense = dataModelStateLicenseInformation }, JsonRequestBehavior.AllowGet);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFederalDEA = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDea);
                    DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - DEA License Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);


                    await profileManager.AddFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);

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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - DEA License Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
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

                        federalDea.DEALicenceCertFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(federalDea, dataModelFederalDEA, tracker);
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

            return Json(new { status = status, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewFederalDEALicenseAsync(int profileId, FederalDEAInformationViewModel federalDea)
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
                    ////DocumentDTO document = null;
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - DEA License Details", "Renewed");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.RenewFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);

                    if (isCCO)
                    {
                        //DocumentDTO document = CreateDocument(federalDea.DEALicenceCertFile);
                        //DocumentDTO document = null;
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - DEA License Details", "Renewed");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.RenewFederalDEALicenseAsync(profileId, dataModelFederalDEA, document);
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
                        federalDea.DEALicenceCertFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(federalDea, dataModelFederalDEA, tracker);
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

            return Json(new { status = status, federalDea = dataModelFederalDEA }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveFederalDEALicense(int profileId, FederalDEAInformationViewModel federalDEAInformationViewModel)
        {
            string status = "true";
            FederalDEAInformation dataModelFederalDEAInformation = null;

            try
            {
                dataModelFederalDEAInformation = AutoMapper.Mapper.Map<FederalDEAInformationViewModel, FederalDEAInformation>(federalDEAInformationViewModel);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Updated");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                dataModelFederalDEAInformation = await profileManager.RemoveFederalDEALicenseAsync(profileId, dataModelFederalDEAInformation);
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

            return Json(new { status = status, federalDea = dataModelFederalDEAInformation }, JsonRequestBehavior.AllowGet);
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
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCDSCLicense = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);

                    DocumentDTO document = CreateDocument(CDSCInformation.CDSCCerificateFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - CDSC Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);

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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - CDSC Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
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
                        tracker.SubSection = "CDSC Information";
                        tracker.userAuthId = userId;
                        tracker.objId = CDSCInformation.CDSCInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/UpdateCDSCLicenseAsync?profileId=";

                        CDSCInformation.CDSCCerificateFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(CDSCInformation, dataModelCDSCLicense, tracker);
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

            return Json(new { status = status, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewCDSCLicenseAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
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

                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - CDSC Details", "Renewed");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.RenewCDSCLicenseAsync(profileId, dataModelCDSCLicense, document);
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
                        tracker.SubSection = "CDSC Information";
                        tracker.userAuthId = userId;
                        tracker.objId = CDSCInformation.CDSCInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                        tracker.url = "/Profile/IdentificationAndLicense/RenewCDSCLicenseAsync?profileId=";
                        CDSCInformation.CDSCCerificateFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(CDSCInformation, dataModelCDSCLicense, tracker);
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

            return Json(new { status = status, CDSCInformation = dataModelCDSCLicense }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveCDSCLicenseAsync(int profileId, CDSCInformationViewModel CDSCInformation)
        {
            string status = "true";
            CDSCInformation dataModelCDSCInformation = null;

            try
            {
                dataModelCDSCInformation = AutoMapper.Mapper.Map<CDSCInformationViewModel, CDSCInformation>(CDSCInformation);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Specialty Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveCDSCLicenseAsync(profileId, dataModelCDSCInformation);
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

            return Json(new { status = status, cDSCInformation = dataModelCDSCInformation }, JsonRequestBehavior.AllowGet);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicareInformation = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(MedicareInformation);
                    DocumentDTO document = CreateDocument(MedicareInformation.CertificateFile);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - Medicate Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddMedicareInformationAsync(profileId, dataModelMedicareInformation, document);
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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - Medicare Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateMedicareInformationAsync(profileId, dataModelMedicareInformation, document);
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

                        MedicareInformation.CertificateFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(MedicareInformation, dataModelMedicareInformation, tracker);
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

            return Json(new { status = status, MedicareInformation = dataModelMedicareInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveMedicareInformation(int profileId, MedicareInformationViewModel medicareInformationViewModel)
        {
            string status = "true";
            MedicareInformation dataModelMedicareInformation = null;

            try
            {
                dataModelMedicareInformation = AutoMapper.Mapper.Map<MedicareInformationViewModel, MedicareInformation>(medicareInformationViewModel);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Updated");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveMedicareInformationAsync(profileId, dataModelMedicareInformation);
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

            return Json(new { status = status, MedicareInfo = dataModelMedicareInformation }, JsonRequestBehavior.AllowGet);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformationViewModel, MedicaidInformation>(MedicaidInformation);
                    DocumentDTO document = CreateDocument(MedicaidInformation.CertificateFile);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - Medicaid Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.AddMedicaidInformationAsync(profileId, dataModelMedicaidInformation, document);
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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License  - Medicaid Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateMedicaidInformationAsync(profileId, dataModelMedicaidInformation, document);
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

                        MedicaidInformation.CertificateFile = null;
                        profileUpdateManager.AddProfileUpdateForProvider(MedicaidInformation, dataModelMedicaidInformation, tracker);
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

            return Json(new { status = status, MedicaidInformation = dataModelMedicaidInformation }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveMedicaidInformation(int profileId, MedicaidInformationViewModel medicaidInformationViewModel)
        {
            string status = "true";
            MedicaidInformation dataModelMedicaidInformation = null;

            try
            {
                dataModelMedicaidInformation = AutoMapper.Mapper.Map<MedicaidInformationViewModel, MedicaidInformation>(medicaidInformationViewModel);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Professional Reference Details", "Updated");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveMedicaidInformationAsync(profileId, dataModelMedicaidInformation);
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

            return Json(new { status = status, MedicaidInfo = dataModelMedicaidInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdateOtherIdentificationNumberAsync(int profileId, AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.OtherIdentificationNumberViewModel OtherIdentificationNumber)
        {
            string status = "true";
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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - Other Identification Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateOtherIdentificationNumberAsync(profileId, dataModelOtherIdentificationNumber);
                    }
                    else if (isCCO && OtherIdentificationNumber.OtherIdentificationNumberID != 0)
                    {
                        dataModelOtherIdentificationNumber = AutoMapper.Mapper.Map<OtherIdentificationNumberViewModel, OtherIdentificationNumber>(OtherIdentificationNumber);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Identification and State License - Other Identification Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await profileManager.UpdateOtherIdentificationNumberAsync(profileId, dataModelOtherIdentificationNumber);
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

            return Json(new { status = status, OtherIdentificationNumber = dataModelOtherIdentificationNumber }, JsonRequestBehavior.AllowGet);
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