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
using AHC.CD.Entities.MasterProfile;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json;

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
        public async Task<ActionResult> UpdatePersonalDetailsAsync(int profileId, PersonalDetailViewModel personalDetail,int CDUserID=0)
        {
            string status = "true";
            PersonalDetail dataModelPersonalDetail = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(personalDetail.UpdateHistory, UserAuthID, "PersonalDetailID", personalDetail.PersonalDetailID, profileId);
                    }
                    else if (!isCCO && personalDetail.PersonalDetailID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Personal Detail";
                        tracker.userAuthId = userId;
                        tracker.objId = personalDetail.PersonalDetailID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdatePersonalDetailsAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(personalDetail, dataModelPersonalDetail, tracker);
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

            return Json(new { status = status, personalDetail = dataModelPersonalDetail }, JsonRequestBehavior.AllowGet);
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
        public async Task<string> GetUpdateHistoryOfRecord(int SectionTableID, string SectionName, int ProfileID)
        {
            var res = await profileManager.GetUpdateHistoryOfARecord(SectionName, SectionTableID, ProfileID);
            return JsonConvert.SerializeObject(res.ToList());
        }

        public async Task<string> GetUpdateHistoryOfDisclosureRecord(int SectionTableID, string SectionName, int ProfileID)
        {
            var res = await profileManager.GetUpdateHistoryOfDisclosureRecord(SectionName, SectionTableID, ProfileID);
            return JsonConvert.SerializeObject(res.ToList());
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateOtherLegalNameAsync(int profileId, OtherLegalNameViewModel otherLegalName, int CDUserID = 0)
        {
            string status = "true";
            OtherLegalName dataModelOtherLegalName = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;

            try
            {
                
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
                    dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);
                    //DocumentDTO document = CreateDocument(otherLegalName.File);
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);
                    //await profileManager.UpdateOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
                    DocumentDTO document = CreateDocument(otherLegalName.File);
                    if (isCCO)
                    {
                        if (otherLegalName.File != null && otherLegalName.DocumentPath != null)
                        {
                            dynamic UH = JsonConvert.DeserializeObject(otherLegalName.UpdateHistory);
                            UH.SupportingDocument = otherLegalName.DocumentPath;
                            otherLegalName.UpdateHistory = JsonConvert.SerializeObject(UH);
                        }
                        await profileManager.UpdateOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        string SectionName = "OtherLegalNameID";
                        await profileManager.SaveUpdateHistory(otherLegalName.UpdateHistory, UserAuthID, SectionName, otherLegalName.OtherLegalNameID, profileId);
                    }
                    else
                    {
                        //ChangedObject = otherLegalName.UpdateHistory;
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

                        //tracker.Documents = new List<ProfileDocumentUpdateTrackerBusinessModel>();
                        //tracker.Documents.Add(otherLegalNameDocumentTracker);
                        otherLegalName.File = null;
                        profileUpdateManager.AddProfileUpdateForProvider(otherLegalName, dataModelOtherLegalName, tracker);
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

            return Json(new { status = status, otherLegalName = dataModelOtherLegalName }, JsonRequestBehavior.AllowGet);
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
                ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Other Legal Name Details", "Removed");
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
        public async Task<ActionResult> UpdateHomeAddressAsync(int profileId, HomeAddressViewModel homeAddress,int CDUserID=0)
        {
            string status = "true";
            HomeAddress dataModelHomeAddress = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
                    dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateHomeAddressAsync(profileId, dataModelHomeAddress);

                    if (isCCO)
                    {
                        await profileManager.UpdateHomeAddressAsync(profileId, dataModelHomeAddress);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(homeAddress.UpdateHistory, UserAuthID, "HomeAddressID", homeAddress.HomeAddressID, profileId);
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

                        profileUpdateManager.AddProfileUpdateForProvider(homeAddress, dataModelHomeAddress, tracker);
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

            return Json(new { status = status, homeAddress = dataModelHomeAddress }, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> UpdateContactDetailsAsync(int profileId, ContactDetailViewModel contactDetail, int CDUserID = 0)
        {
            string status = "true";
            ContactDetail dataModelContactDetail = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelContactDetail = AutoMapper.Mapper.Map<ContactDetailViewModel, ContactDetail>(contactDetail);
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
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
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Contact Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(contactDetail.UpdateHistory, UserAuthID, "ContactDetailID", (int)contactDetail.ContactDetailID, profileId);
                    }
                    else if (!isCCO && contactDetail.ContactDetailID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Contact Detail";
                        tracker.userAuthId = userId;
                        tracker.objId = Convert.ToInt32(contactDetail.ContactDetailID);
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateContactDetailsAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(contactDetail, dataModelContactDetail, tracker);
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

            return Json(new { status = status, contactDetail = dataModelContactDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Personal Identification

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdatePersonalIdentificationAsync(int profileId, PersonalIdentificationViewModel personalIdentification, int CDUserID = 0)
        {
            string status = "true";
            PersonalIdentification dataModelPersonalIdentification = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
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
                        if (personalIdentification.DLCertificateFile != null && personalIdentification.DLCertificatePath != null)
                        {
                            dynamic UH = JsonConvert.DeserializeObject(personalIdentification.UpdateHistory);
                            UH.SupportingDocument = personalIdentification.DLCertificatePath;
                            personalIdentification.UpdateHistory = JsonConvert.SerializeObject(UH);
                        }
                        if (personalIdentification.SSNCertificateFile != null && personalIdentification.SSNCertificatePath != null)
                        {
                            dynamic UH = JsonConvert.DeserializeObject(personalIdentification.UpdateHistory);
                            UH.SupportingDocument = personalIdentification.SSNCertificatePath;
                            personalIdentification.UpdateHistory = JsonConvert.SerializeObject(UH);
                        }
                        await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Identifications", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(personalIdentification.UpdateHistory, UserAuthID, "PersonalIdentificationID", (int)personalIdentification.PersonalIdentificationID, profileId);
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

            return Json(new { status = status, personalIdentification = dataModelPersonalIdentification, personalIdentificationid = personalIdentification.PersonalIdentificationID }, JsonRequestBehavior.AllowGet);
        }  

        #endregion

        #region Birth Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateBirthInformationAsync(int profileId, BirthInformationViewModel birthInformation, int CDUserID = 0)
        {
            string status = "true";
            BirthInformation dataModelBirthInformation = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
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
                    }
                    else if (isCCO && birthInformation.BirthInformationID != 0)
                    {
                        if (birthInformation.BirthCertificateFile != null && birthInformation.BirthCertificatePath != null)
                        {
                            dynamic UH = JsonConvert.DeserializeObject(birthInformation.UpdateHistory);
                            UH.SupportingDocument = birthInformation.BirthCertificatePath;
                            birthInformation.UpdateHistory = JsonConvert.SerializeObject(UH);
                        }
                        await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Birth Information", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(birthInformation.UpdateHistory, UserAuthID, "BirthInformationID", birthInformation.BirthInformationID, profileId);
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

            return Json(new { status = status, birthInformation = dataModelBirthInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Visa Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdateVisaDetailAsync(int profileId, VisaDetailViewModel visaDetail, int CDUserID = 0)
        {
            string status = "true";
            VisaDetail dataModelVisaDetail = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
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
                    }
                    else if (isCCO && visaDetail.VisaDetailID != 0)
                    {
                        if (visaDetail.VisaInfo != null) {
                            if (visaDetail.VisaInfo.VisaCertificateFile != null && visaDetail.VisaInfo.VisaCertificatePath != null)
                            {
                                dynamic UH = JsonConvert.DeserializeObject(visaDetail.UpdateHistory);
                                UH.SupportingDocument = visaDetail.VisaInfo.VisaCertificatePath;
                                visaDetail.UpdateHistory = JsonConvert.SerializeObject(UH);
                            }
                            if (visaDetail.VisaInfo.GreenCardCertificateFile != null && visaDetail.VisaInfo.GreenCardCertificatePath != null)
                            {
                                dynamic UH = JsonConvert.DeserializeObject(visaDetail.UpdateHistory);
                                UH.SupportingDocument = visaDetail.VisaInfo.GreenCardCertificatePath;
                                visaDetail.UpdateHistory = JsonConvert.SerializeObject(UH);
                            }
                            if (visaDetail.VisaInfo.NationalIDCertificateFile != null && visaDetail.VisaInfo.NationalIDCertificatePath != null)
                            {
                                dynamic UH = JsonConvert.DeserializeObject(visaDetail.UpdateHistory);
                                UH.SupportingDocument = visaDetail.VisaInfo.NationalIDCertificatePath;
                                visaDetail.UpdateHistory = JsonConvert.SerializeObject(UH);
                            }
                        }
                        await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Visa Details", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(visaDetail.UpdateHistory, UserAuthID, "VisaDetailID", visaDetail.VisaDetailID, profileId);
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

            return Json(new { status = status, visaDetail = dataModelVisaDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Language

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateLanguagesAsync(int profileId, LanguageInfoViewModel languageInfo, int CDUserID = 0)
        {
            string status = "true";
            LanguageInfo dataModelLanguageInfo = null;
            bool isCCO = await GetUserRole();
            string UserAuthID = null;
            try
            {
                if (ModelState.IsValid)
                {
                    UserAuthID = CDUserID == 0 ? await GetUserAuthId() : profileManager.GetAuthID(CDUserID);
                    dataModelLanguageInfo = AutoMapper.Mapper.Map<LanguageInfoViewModel, LanguageInfo>(languageInfo);

                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);

                    //await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);

                    if (languageInfo.LanguageInfoID == 0)
                    {
                        await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Added");
                        await notificationManager.SaveNotificationDetailAsyncForAdd(notification, isCCO);
                    }
                    else if (isCCO && languageInfo.LanguageInfoID != 0)
                    {
                        await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Updated");
                        await notificationManager.SaveNotificationDetailAsync(notification);
                        await profileManager.SaveUpdateHistory(languageInfo.UpdateHistory, UserAuthID, "LanguageInfoID", languageInfo.LanguageInfoID, profileId);
                    }
                    else if (!isCCO && languageInfo.LanguageInfoID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Demographic";
                        tracker.SubSection = "Language Info";
                        tracker.userAuthId = userId;
                        tracker.objId = languageInfo.LanguageInfoID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/Demographic/UpdateLanguagesAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(languageInfo, dataModelLanguageInfo, tracker);
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

            return Json(new { status = status, languageInfo = dataModelLanguageInfo }, JsonRequestBehavior.AllowGet);
        }

        #endregion        

        #region Private Methods

        private async Task<ApplicationUser> GetUser()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            return user;
        }

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
        //    public  string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        //    {
        //        var memberInfo = GetPropertyInformation(propertyExpression.Body);
        //        if (memberInfo == null)
        //        {
        //            throw new ArgumentException(
        //                "No property reference expression was found.",
        //                "propertyExpression");
        //        }

        //        var attr = memberInfo.GetAttribute<DisplayNameAttribute>(false);
        //        if (attr == null)
        //        {
        //            return memberInfo.Name;
        //        }

        //        return attr.DisplayName;
        //    }
        //    public  MemberInfo GetPropertyInformation(Expression propertyExpression)
        //    {
        //        Debug.Assert(propertyExpression != null, "propertyExpression != null");
        //        MemberExpression memberExpr = propertyExpression as MemberExpression;
        //        if (memberExpr == null)
        //        {
        //            UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
        //            if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
        //            {
        //                memberExpr = unaryExpr.Operand as MemberExpression;
        //            }
        //        }

        //        if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
        //        {
        //            return memberExpr.Member;
        //        }

        //        return null;
        //    }
        //    public  T GetAttribute<T>(this MemberInfo member, bool isRequired)
        //where T : Attribute
        //    {
        //        var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

        //        if (attribute == null && isRequired)
        //        {
        //            throw new ArgumentException(
        //                string.Format(
        //                    CultureInfo.InvariantCulture,
        //                    "The {0} attribute must be defined on member {1}",
        //                    typeof(T).Name,
        //                    member.Name));
        //        }

        //        return (T)attribute;
        //    }
        
    }
}