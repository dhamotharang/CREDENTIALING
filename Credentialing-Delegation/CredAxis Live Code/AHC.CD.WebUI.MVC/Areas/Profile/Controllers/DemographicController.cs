using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.ActionFilter;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Resources.Document;
using System.Dynamic;
using Newtonsoft.Json;
using AHC.CD.Resources.Rules;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DemographicController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        // Change Notifications
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public DemographicController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager) // Change Notifications
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            // Change Notifications
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

        #region Personal Detail

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdatePersonalDetailsAsync(int profileId, PersonalDetailViewModel personalDetail)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            PersonalDetail dataModelPersonalDetail = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPersonalDetail = AutoMapper.Mapper.Map<PersonalDetailViewModel, PersonalDetail>(personalDetail);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);
                    //await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);

                    if (personalDetail.PersonalDetailID == 0)
                    {
                        // Change Notifications
                        await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Added");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                    }
                    else if (isCCO && personalDetail.PersonalDetailID != 0)
                    {
                        // Change Notifications

                        await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.PERSONAL_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && personalDetail.PersonalDetailID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Personal Details";
                        tracker.userAuthId = userId;
                        tracker.objId = personalDetail.PersonalDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdatePersonalDetailsAsync?profileId=";

                        //dynamic uniqueRecord = new ExpandoObject();
                        //uniqueRecord.FieldName = "Name";
                        //uniqueRecord.Value = personalDetail.SalutationType.ToString() + " " + personalDetail.FirstName + " " + personalDetail.MiddleName + " " + personalDetail.LastName;

                        //tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        profileUpdateManager.AddProfileUpdateForProvider(personalDetail, dataModelPersonalDetail, tracker);
                        successMessage = SuccessMessage.PERSONAL_DETAIL_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, personalDetail = dataModelPersonalDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region File Upload

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> FileUploadAsync(int profileId, ProfilePictureViewModel ProfilePic)
        {
            string status = "true";
            string newPath = "";
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    DocumentDTO document = CreateDocument(ProfilePic.ProfilePictureFile);
                    // Change Notifications
                    newPath = await profileManager.UpdateProfileImageAsync(profileId, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Profile Picture", "Uploaded");
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

            return Json(new { status = status, ProfileImagePath = newPath }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region File Remove

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> FileRemoveAsync(int profileId, string imagePath)
        {
            string status = "true";
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    await profileManager.RemoveProfileImageAsync(profileId, imagePath);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Profile Picture", "Removed");
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

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Other Legal Name

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddOtherLegalNameAsync(int profileId, OtherLegalNameViewModel otherLegalName)
        {
            string status = "true";
            OtherLegalName dataModelOtherLegalName = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);
                    DocumentDTO document = CreateDocument(otherLegalName.File);
                    var result = await profileManager.AddOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Added");
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

            return Json(new { status = status, otherLegalName = dataModelOtherLegalName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateOtherLegalNameAsync(int profileId, OtherLegalNameViewModel otherLegalName)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            OtherLegalName dataModelOtherLegalName = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);

                    //DocumentDTO document = CreateDocument(otherLegalName.File);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);

                    DocumentDTO document = CreateDocument(otherLegalName.File);
                    if (isCCO)
                    {
                       
                        await profileManager.UpdateOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.OTHER_LEGAL_NAME_UPDATE_SUCCESS;
                        }

                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string otherLegalDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.OTHER_LEGAL_NAME_PATH, DocumentTitle.OTHER_LEGAL_NAME, profileId);
                        //ProfileDocumentUpdateTrackerBusinessModel otherLegalNameDocumentTracker = new ProfileDocumentUpdateTrackerBusinessModel() { Title = "OTHER_LEGAL_DOCUMENT", DocumentPath = otherLegalDocumentTemporaryPath };

                        if (otherLegalDocumentTemporaryPath != null)
                        {
                            otherLegalName.DocumentPath = otherLegalDocumentTemporaryPath;
                            dataModelOtherLegalName.DocumentPath = otherLegalDocumentTemporaryPath;
                        }                        

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Other Legal Name";
                        tracker.userAuthId = userId;
                        tracker.objId = otherLegalName.OtherLegalNameID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateOtherLegalNameAsync?profileId=";

                        OtherLegalName legalName = await profileUpdateManager.GetProfileDataByID(dataModelOtherLegalName, otherLegalName.OtherLegalNameID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Other Legal Name Detail";
                        uniqueRecord.Value = legalName.OtherFirstName + " " + legalName.OtherMiddleName + " " + legalName.OtherLastName;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);

                        //tracker.Documents = new List<ProfileDocumentUpdateTrackerBusinessModel>();
                        //tracker.Documents.Add(otherLegalNameDocumentTracker);
                        otherLegalName.File = null;
                        profileUpdateManager.AddProfileUpdateForProvider(otherLegalName, dataModelOtherLegalName, tracker);
                        successMessage = SuccessMessage.OTHER_LEGAL_NAME_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, otherLegalName = dataModelOtherLegalName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveOtherLegalName(int profileId, OtherLegalNameViewModel otherLegalName)
        {
            string status = "true";
            OtherLegalName dataModelOtherLegalName = null;
            bool isCCO = await GetUserRole();
            try
            {
                dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);
                var UserAuthID = await GetUserAuthId();
                await profileManager.RemoveOtherLegalNameAsync(profileId, dataModelOtherLegalName, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Name Details", "Removed");
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
                status = ExceptionMessage.OTHER_LEGAL_NAME_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, otherLegalName = dataModelOtherLegalName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Home Address

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> AddHomeAddressAsync(int profileId, HomeAddressViewModel homeAddress)
        {
            string status = "true";
            HomeAddress dataModelHomeAddress = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);
                   
                    var result = await profileManager.AddHomeAddressAsync(profileId, dataModelHomeAddress);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Added");
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

            return Json(new { status = status, homeAddress = dataModelHomeAddress }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, true)]
        public async Task<ActionResult> UpdateHomeAddressAsync(int profileId, HomeAddressViewModel homeAddress)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            HomeAddress dataModelHomeAddress = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateHomeAddressAsync(profileId, dataModelHomeAddress);

                    if (isCCO)
                    {
                      
                        await profileManager.UpdateHomeAddressAsync(profileId, dataModelHomeAddress);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.HOME_ADDRESS_UPDATE_SUCCESS;
                        }
                    }
                    else
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Home Address";
                        tracker.userAuthId = userId;
                        tracker.objId = homeAddress.HomeAddressID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateHomeAddressAsync?profileId=";

                        HomeAddress homeAddressOldData = await profileUpdateManager.GetProfileDataByID(dataModelHomeAddress, homeAddress.HomeAddressID);

                        dynamic uniqueRecord = new ExpandoObject();
                        uniqueRecord.FieldName = "Home Address Detail";
                        uniqueRecord.Value = homeAddressOldData.Street + ", " + homeAddressOldData.City + ", " + homeAddressOldData.State + ", " + homeAddressOldData.ZipCode + ", " + homeAddressOldData.Country;

                        tracker.UniqueData = JsonConvert.SerializeObject(uniqueRecord);
                        
                        profileUpdateManager.AddProfileUpdateForProvider(homeAddress, dataModelHomeAddress, tracker);
                        successMessage = SuccessMessage.HOME_ADDRESS_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, homeAddress = dataModelHomeAddress }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemoveHomeAddress(int profileId, HomeAddressViewModel homeAddress)
        {
            string status = "true";
            HomeAddress dataModelHomeAddress = null;
            bool isCCO = await GetUserRole();
            try
            {
                dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);
                var UserAuthID = await GetUserAuthId();
                await profileManager.RemoveHomeAddressAsync(profileId, dataModelHomeAddress, UserAuthID);
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address Details", "Removed");
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
                status = ExceptionMessage.HOME_ADDRESS_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, homeAddress = dataModelHomeAddress }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Contact Detail

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateContactDetailsAsync(int profileId, ContactDetailViewModel contactDetail)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            ContactDetail dataModelContactDetail = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelContactDetail = AutoMapper.Mapper.Map<ContactDetailViewModel, ContactDetail>(contactDetail);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Contact Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateContactDetailAsync(profileId, dataModelContactDetail);

                    if (contactDetail.ContactDetailID == 0)
                    {
                       
                        await profileManager.UpdateContactDetailAsync(profileId, dataModelContactDetail);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Contact Details", "Added");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                    }
                    else if (isCCO && contactDetail.ContactDetailID != 0)
                    {
                       
                        await profileManager.UpdateContactDetailAsync(profileId, dataModelContactDetail);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Contact Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.CONTACT_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && contactDetail.ContactDetailID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Contact Details";
                        tracker.userAuthId = userId;
                        tracker.objId = Convert.ToInt32(contactDetail.ContactDetailID);
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateContactDetailsAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(contactDetail, dataModelContactDetail, tracker);
                        successMessage = SuccessMessage.CONTACT_DETAIL_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, contactDetail = dataModelContactDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Personal Identification

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdatePersonalIdentificationAsync(int profileId, PersonalIdentificationViewModel personalIdentification)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            PersonalIdentification dataModelPersonalIdentification = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPersonalIdentification = AutoMapper.Mapper.Map<PersonalIdentificationViewModel, PersonalIdentification>(personalIdentification);

                    //DocumentDTO dlDocument = CreateDocument(personalIdentification.DLCertificateFile);
                    //DocumentDTO ssnDocument = CreateDocument(personalIdentification.SSNCertificateFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Identifications", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);

                    DocumentDTO dlDocument = CreateDocument(personalIdentification.DLCertificateFile);
                    DocumentDTO ssnDocument = CreateDocument(personalIdentification.SSNCertificateFile);

                    if (personalIdentification.PersonalIdentificationID == 0)
                    {
                        
                        await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Identifications", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                    }
                    else if (isCCO && personalIdentification.PersonalIdentificationID != 0)
                    {
                        await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Identifications", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.PERSONAL_IDENTIFICATION_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && personalIdentification.PersonalIdentificationID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();
                        string dlDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(dlDocument, DocumentRootPath.DL_PATH, DocumentTitle.DL, profileId);
                        string ssnDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(ssnDocument, DocumentRootPath.SSN_PATH, DocumentTitle.SSN, profileId);

                        if (dlDocumentTemporaryPath != null)
                        {
                            personalIdentification.DLCertificatePath = dlDocumentTemporaryPath;
                            dataModelPersonalIdentification.DLCertificatePath = dlDocumentTemporaryPath;
                        }

                        if (ssnDocumentTemporaryPath != null)
                        {
                            personalIdentification.SSNCertificatePath = ssnDocumentTemporaryPath;
                            dataModelPersonalIdentification.SSNCertificatePath = ssnDocumentTemporaryPath;
                        }                            

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Personal Identification";
                        tracker.userAuthId = userId;
                        tracker.objId = personalIdentification.PersonalIdentificationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdatePersonalIdentificationAsync?profileId=";

                        personalIdentification.SSNCertificateFile = null;
                        personalIdentification.DLCertificateFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(personalIdentification, dataModelPersonalIdentification, tracker);
                        successMessage = SuccessMessage.PERSONAL_IDENTIFICATION_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, personalIdentification = dataModelPersonalIdentification, personalIdentificationid = personalIdentification.PersonalIdentificationID }, JsonRequestBehavior.AllowGet);
        }  

        #endregion

        #region Birth Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateBirthInformationAsync(int profileId, BirthInformationViewModel birthInformation)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            BirthInformation dataModelBirthInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelBirthInformation = AutoMapper.Mapper.Map<BirthInformationViewModel, BirthInformation>(birthInformation);

                    //DocumentDTO document = CreateDocument(birthInformation.BirthCertificateFile);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Birth Information", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);
                    //await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);

                    DocumentDTO document = CreateDocument(birthInformation.BirthCertificateFile);

                    if (birthInformation.BirthInformationID == 0)
                    {
                      
                        await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Birth Information", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                        successMessage = SuccessMessage.BIRTH_INFORMATION_ADD_SUCCESS;
                    }
                    else if (isCCO && birthInformation.BirthInformationID != 0)
                    {
                       
                        await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Birth Information", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.BIRTH_INFORMATION_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && birthInformation.BirthInformationID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        string birthCertificateDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(document, DocumentRootPath.BIRTH_CERTIFICATE_PATH, DocumentTitle.BIRTH_CERTIFICATE, profileId);

                        if (birthCertificateDocumentTemporaryPath != null)
                        {
                            birthInformation.BirthCertificatePath = birthCertificateDocumentTemporaryPath;
                            dataModelBirthInformation.BirthCertificatePath = birthCertificateDocumentTemporaryPath;
                        }                        

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Birth Information";
                        tracker.userAuthId = userId;
                        tracker.objId = birthInformation.BirthInformationID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateBirthInformationAsync?profileId=";

                        birthInformation.BirthCertificateFile = null;

                        profileUpdateManager.AddProfileUpdateForProvider(birthInformation, dataModelBirthInformation, tracker);
                        successMessage = SuccessMessage.BIRTH_INFORMATION_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, birthInformation = dataModelBirthInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Visa Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdateVisaDetailAsync(int profileId, VisaDetailViewModel visaDetail)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            VisaDetail dataModelVisaDetail = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelVisaDetail = AutoMapper.Mapper.Map<VisaDetailViewModel, VisaDetail>(visaDetail);

                    DocumentDTO visaDocument = null;
                    DocumentDTO greenCarddocument = null;
                    DocumentDTO nationalIDdocument = null;
                    if (visaDetail.VisaInfo != null)
                    {
                        visaDocument = CreateDocument(visaDetail.VisaInfo.VisaCertificateFile);
                        greenCarddocument = CreateDocument(visaDetail.VisaInfo.GreenCardCertificateFile);
                        nationalIDdocument = CreateDocument(visaDetail.VisaInfo.NationalIDCertificateFile);
                    }
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Visa Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);

                    if (visaDetail.VisaDetailID == 0)
                    {
                        await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Visa Details", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                        successMessage = SuccessMessage.VISA_DETAIL_ADD_SUCCESS;
                    }
                    else if (isCCO && visaDetail.VisaDetailID != 0)
                    {
                      
                        await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Visa Details", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.VISA_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && visaDetail.VisaDetailID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        if (visaDetail.VisaInfo != null)
                        {
                            string visaDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(visaDocument, DocumentRootPath.VISA_PATH, DocumentTitle.VISA, profileId);
                            string greenCardDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(greenCarddocument, DocumentRootPath.GREEN_CARD_PATH, DocumentTitle.GREEN_CARD, profileId);
                            string nationalIDDocumentTemporaryPath = profileUpdateManager.SaveDocumentTemporarily(nationalIDdocument, DocumentRootPath.NATIONAL_IDENTIFICATION_PATH, DocumentTitle.NATIONAL_IDENTIFICATION, profileId);

                            if (visaDocumentTemporaryPath != null)
                            {
                                visaDetail.VisaInfo.VisaCertificatePath = visaDocumentTemporaryPath;
                                dataModelVisaDetail.VisaInfo.VisaCertificatePath = visaDocumentTemporaryPath;
                            }
                            if (greenCardDocumentTemporaryPath != null)
                            {
                                visaDetail.VisaInfo.GreenCardCertificatePath = greenCardDocumentTemporaryPath;
                                dataModelVisaDetail.VisaInfo.GreenCardCertificatePath = greenCardDocumentTemporaryPath;
                            }
                            if (nationalIDDocumentTemporaryPath != null)
                            {
                                visaDetail.VisaInfo.NationalIDCertificatePath = nationalIDDocumentTemporaryPath;
                                dataModelVisaDetail.VisaInfo.NationalIDCertificatePath = nationalIDDocumentTemporaryPath;
                            }

                            visaDetail.VisaInfo.VisaCertificateFile = null;
                            visaDetail.VisaInfo.GreenCardCertificateFile = null;
                            visaDetail.VisaInfo.NationalIDCertificateFile = null;
                        }

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Citizenship Information";
                        tracker.userAuthId = userId;
                        tracker.objId = visaDetail.VisaDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateVisaDetailAsync?profileId=";                        

                        profileUpdateManager.AddProfileUpdateForProvider(visaDetail, dataModelVisaDetail, tracker);
                        successMessage = SuccessMessage.VISA_DETAIL_UPDATE_REQUEST_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, visaDetail = dataModelVisaDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Language

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateLanguagesAsync(int profileId, LanguageInfoViewModel languageInfo)
        {
            string status = "true";
            string ActionType = "Update";
            string successMessage = "";
            LanguageInfo dataModelLanguageInfo = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelLanguageInfo = AutoMapper.Mapper.Map<LanguageInfoViewModel, LanguageInfo>(languageInfo);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);

                    if (languageInfo.LanguageInfoID == 0)
                    {
                       
                        await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                        successMessage = SuccessMessage.LANGUAGES_DETAIL_ADD_SUCCESS;
                    }
                    else if (isCCO && languageInfo.LanguageInfoID != 0)
                    {
                        
                        await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);
                        if (Request.UrlReferrer.AbsolutePath.IndexOf(RequestSourcePath.RequestSource, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Updated");
                            await notificationManager.SaveNotificationDetailAsync(notification);
                            successMessage = SuccessMessage.LANGUAGES_DETAIL_UPDATE_SUCCESS;
                        }
                    }
                    else if (!isCCO && languageInfo.LanguageInfoID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Language Information";
                        tracker.userAuthId = userId;
                        tracker.objId = languageInfo.LanguageInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateLanguagesAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(languageInfo, dataModelLanguageInfo, tracker);
                        successMessage = SuccessMessage.LANGUAGES_DETAIL_UPDATE_SUCCESS;
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

            return Json(new { status = status, ActionType = ActionType, successMessage = successMessage, languageInfo = dataModelLanguageInfo }, JsonRequestBehavior.AllowGet);
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

            var roleIDs = RoleManager.Roles.ToList().Where(r => r.Name == "CCO" || r.Name == "CRA"||r.Name == "CRA"||r.Name=="TL").Select(r => r.Id).ToList();

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