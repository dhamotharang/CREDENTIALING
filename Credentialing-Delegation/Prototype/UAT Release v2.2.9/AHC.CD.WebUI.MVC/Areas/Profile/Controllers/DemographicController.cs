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
            PersonalDetail dataModelPersonalDetail = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPersonalDetail = AutoMapper.Mapper.Map<PersonalDetailViewModel, PersonalDetail>(personalDetail);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);
                    await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);

                    //if (personalDetail.PersonalDetailID == 0)
                    //{                        
                    //    // Change Notifications
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);
                    //    await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);
                    //}
                    //else if (isCCO && personalDetail.PersonalDetailID != 0)
                    //{                        
                    //    // Change Notifications
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);
                    //    await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);
                    //}
                    //else if (!isCCO && personalDetail.PersonalDetailID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Personal Detail";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = personalDetail.PersonalDetailID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdatePersonalDetailsAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(personalDetail,dataModelPersonalDetail, tracker);
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
            try
            {
                if (ModelState.IsValid)
                {
                    DocumentDTO document = CreateDocument(ProfilePic.ProfilePictureFile);
                    // Change Notifications

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Profile Picture", "Uploaded");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    newPath = await profileManager.UpdateProfileImageAsync(profileId, document);
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
            try
            {
                if (ModelState.IsValid)
                {
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Profile Picture", "Removed");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.RemoveProfileImageAsync(profileId, imagePath);
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

            return Json(new { status = status}, JsonRequestBehavior.AllowGet);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);
                    DocumentDTO document = CreateDocument(otherLegalName.File);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
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
            OtherLegalName dataModelOtherLegalName = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);

                    DocumentDTO document = CreateDocument(otherLegalName.File);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);

                    //if (isCCO)
                    //{
                    //    DocumentDTO document = CreateDocument(otherLegalName.File);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Other Legal Names", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateOtherLegalNamesAsync(profileId, dataModelOtherLegalName, document);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Other Legal Name";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = otherLegalName.OtherLegalNameID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdateOtherLegalNameAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(otherLegalName,dataModelOtherLegalName, tracker);
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

            try
            {
                dataModelOtherLegalName = AutoMapper.Mapper.Map<OtherLegalNameViewModel, OtherLegalName>(otherLegalName);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Other Legal Name Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveOtherLegalNameAsync(profileId, dataModelOtherLegalName);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    var result = await profileManager.AddHomeAddressAsync(profileId, dataModelHomeAddress);
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
            HomeAddress dataModelHomeAddress = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateHomeAddressAsync(profileId, dataModelHomeAddress);

                    //if (isCCO)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Home Address", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateHomeAddressAsync(profileId, dataModelHomeAddress);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Home Address";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = homeAddress.HomeAddressID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdateHomeAddressAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(homeAddress,dataModelHomeAddress, tracker);
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

            try
            {
                dataModelHomeAddress = AutoMapper.Mapper.Map<HomeAddressViewModel, HomeAddress>(homeAddress);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Other Legal Name Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await profileManager.RemoveHomeAddressAsync(profileId, dataModelHomeAddress);
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
            ContactDetail dataModelContactDetail = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelContactDetail = AutoMapper.Mapper.Map<ContactDetailViewModel, ContactDetail>(contactDetail);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Contact Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateContactDetailAsync(profileId, dataModelContactDetail);

                    //if (contactDetail.ContactDetailID == 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Contact Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateContactDetailAsync(profileId, dataModelContactDetail);
                    //}
                    //else if (isCCO && contactDetail.ContactDetailID != 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Contact Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateContactDetailAsync(profileId, dataModelContactDetail);
                    //}
                    //else if (!isCCO && contactDetail.ContactDetailID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Contact Detail";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = contactDetail.ContactDetailID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdateContactDetailsAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(contactDetail,dataModelContactDetail, tracker);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, contactDetail = dataModelContactDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Personal Identification

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdatePersonalIdentificationAsync(int profileId, PersonalIdentificationViewModel personalIdentification)
        {
            string status = "true";
            PersonalIdentification dataModelPersonalIdentification = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPersonalIdentification = AutoMapper.Mapper.Map<PersonalIdentificationViewModel, PersonalIdentification>(personalIdentification);

                    DocumentDTO dlDocument = CreateDocument(personalIdentification.DLCertificateFile);
                    DocumentDTO ssnDocument = CreateDocument(personalIdentification.SSNCertificateFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Identifications", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);

                    //if (personalIdentification.PersonalIdentificationID == 0)
                    //{
                    //    DocumentDTO dlDocument = CreateDocument(personalIdentification.DLCertificateFile);
                    //    DocumentDTO ssnDocument = CreateDocument(personalIdentification.SSNCertificateFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Identifications", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);
                    //}
                    //else if (isCCO && personalIdentification.PersonalIdentificationID != 0)
                    //{
                    //    DocumentDTO dlDocument = CreateDocument(personalIdentification.DLCertificateFile);
                    //    DocumentDTO ssnDocument = CreateDocument(personalIdentification.SSNCertificateFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Identifications", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdatePersonalIdentificationAsync(profileId, dataModelPersonalIdentification, dlDocument, ssnDocument);
                    //}
                    //else if (!isCCO && personalIdentification.PersonalIdentificationID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Personal Identification";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = personalIdentification.PersonalIdentificationID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdatePersonalIdentificationAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(personalIdentification,dataModelPersonalIdentification, tracker);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, personalIdentification = dataModelPersonalIdentification }, JsonRequestBehavior.AllowGet);
        }  

        #endregion

        #region Birth Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateBirthInformationAsync(int profileId, BirthInformationViewModel birthInformation)
        {
            string status = "true";
            BirthInformation dataModelBirthInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelBirthInformation = AutoMapper.Mapper.Map<BirthInformationViewModel, BirthInformation>(birthInformation);

                    DocumentDTO document = CreateDocument(birthInformation.BirthCertificateFile);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Birth Information", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);

                    //if (birthInformation.BirthInformationID == 0)
                    //{
                    //    DocumentDTO document = CreateDocument(birthInformation.BirthCertificateFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Birth Information", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);
                    //}
                    //else if (isCCO && birthInformation.BirthInformationID != 0)
                    //{
                    //    DocumentDTO document = CreateDocument(birthInformation.BirthCertificateFile);
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Birth Information", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateBirthInformationAsync(profileId, dataModelBirthInformation, document);
                    //}
                    //else if (!isCCO && birthInformation.BirthInformationID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Birth Information";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = birthInformation.BirthInformationID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdateBirthInformationAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(birthInformation,dataModelBirthInformation, tracker);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, birthInformation = dataModelBirthInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Visa Details

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, true)]
        public async Task<ActionResult> UpdateVisaDetailAsync(int profileId, VisaDetailViewModel visaDetail)
        {
            string status = "true";
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
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Visa Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);

                    //if (visaDetail.VisaDetailID == 0)
                    //{
                    //    DocumentDTO visaDocument = null;
                    //    DocumentDTO greenCarddocument = null;
                    //    DocumentDTO nationalIDdocument = null;
                    //    if (visaDetail.VisaInfo != null)
                    //    {
                    //        visaDocument = CreateDocument(visaDetail.VisaInfo.VisaCertificateFile);
                    //        greenCarddocument = CreateDocument(visaDetail.VisaInfo.GreenCardCertificateFile);
                    //        nationalIDdocument = CreateDocument(visaDetail.VisaInfo.NationalIDCertificateFile);
                    //    }
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Visa Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);
                    //}
                    //else if (isCCO && visaDetail.VisaDetailID != 0)
                    //{
                    //    DocumentDTO visaDocument = null;
                    //    DocumentDTO greenCarddocument = null;
                    //    DocumentDTO nationalIDdocument = null;
                    //    if (visaDetail.VisaInfo != null)
                    //    {
                    //        visaDocument = CreateDocument(visaDetail.VisaInfo.VisaCertificateFile);
                    //        greenCarddocument = CreateDocument(visaDetail.VisaInfo.GreenCardCertificateFile);
                    //        nationalIDdocument = CreateDocument(visaDetail.VisaInfo.NationalIDCertificateFile);
                    //    }
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Visa Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateVisaInformationAsync(profileId, dataModelVisaDetail, visaDocument, greenCarddocument, nationalIDdocument);
                    //}
                    //else if (!isCCO && visaDetail.VisaDetailID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Visa Detail";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = visaDetail.VisaDetailID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdateVisaDetailAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(visaDetail,dataModelVisaDetail, tracker);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, visaDetail = dataModelVisaDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Language

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateLanguagesAsync(int profileId, LanguageInfoViewModel languageInfo)
        {
            string status = "true";
            LanguageInfo dataModelLanguageInfo = null;
            bool isCCO = await GetUserRole();

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelLanguageInfo = AutoMapper.Mapper.Map<LanguageInfoViewModel, LanguageInfo>(languageInfo);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);

                    //if (languageInfo.LanguageInfoID == 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);
                    //}
                    //else if (isCCO && languageInfo.LanguageInfoID != 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Languages", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await profileManager.UpdateLanguageInformationAsync(profileId, dataModelLanguageInfo);
                    //}
                    //else if (!isCCO && languageInfo.LanguageInfoID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Demographic";
                    //    tracker.SubSection = "Language Info";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = languageInfo.LanguageInfoID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/Demographic/UpdateLanguagesAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(languageInfo, dataModelLanguageInfo,tracker);
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, languageInfo = dataModelLanguageInfo }, JsonRequestBehavior.AllowGet);
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