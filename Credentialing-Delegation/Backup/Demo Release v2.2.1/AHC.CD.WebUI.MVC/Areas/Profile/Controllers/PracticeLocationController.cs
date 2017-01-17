using AHC.CD.Business;
using AHC.CD.Business.Account;
using AHC.CD.Business.DTO;
using AHC.CD.Business.Notification;
using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation;
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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class PracticeLocationController : Controller
    {
        private IPracticeLocationManager practiceLocationManager = null;
        private IErrorLogger errorLogger = null;
        IOrganizationManager organizationManager=null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        public PracticeLocationController(IPracticeLocationManager practiceLocationManager, IOrganizationManager organizationManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager)
        {
            this.practiceLocationManager = practiceLocationManager;
            this.organizationManager = organizationManager;
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

        #region Practice Location General Information

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> savePracticeLocationDetailInformaton(int profileId, PracticeLocationDetailViewModel practiceLocationDetailViewModel)
        {
            string status = "true";
            PracticeLocationDetail dataModelPracticeLocationDetailInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeLocationDetailInformation = AutoMapper.Mapper.Map<PracticeLocationDetailViewModel, PracticeLocationDetail>(practiceLocationDetailViewModel);

                    // OrganizationAccountId Has to be replaced with account information 
                    dataModelPracticeLocationDetailInformation.OrganizationId = OrganizationAccountId.DefaultOrganizationAccountID;

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.AddPracticeLocationAsync(profileId, dataModelPracticeLocationDetailInformation);
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
                status = ExceptionMessage.PRACTICE_LOCATION_CREATE_EXCEPTION;
            }

            return Json(new { status = status, practiceLocationDetail = dataModelPracticeLocationDetailInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> updatePracticeLocationDetailInformaton(int profileId, PracticeLocationDetailViewModel practiceLocationDetailViewModel)
        {
            string status = "true";
            PracticeLocationDetail dataModelPracticeLocationDetailInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeLocationDetailInformation = AutoMapper.Mapper.Map<PracticeLocationDetailViewModel, PracticeLocationDetail>(practiceLocationDetailViewModel);

                    dataModelPracticeLocationDetailInformation.OrganizationId = OrganizationAccountId.DefaultOrganizationAccountID;

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePracticeLocationAsync(dataModelPracticeLocationDetailInformation);
                    
                    //if (isCCO)
                    //{
                    //    // OrganizationAccountId Has to be replaced with account information 
                    //    dataModelPracticeLocationDetailInformation.OrganizationId = OrganizationAccountId.DefaultOrganizationAccountID;

                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await practiceLocationManager.UpdatePracticeLocationAsync(dataModelPracticeLocationDetailInformation);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = "Practice Location Detail";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = practiceLocationDetailViewModel.PracticeLocationDetailID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/updatePracticeLocationDetailInformaton?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(practiceLocationDetailViewModel,dataModelPracticeLocationDetailInformation, tracker);
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
                status = ExceptionMessage.PRACTICE_LOCATION_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, practiceLocationDetail = dataModelPracticeLocationDetailInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> RemovePracticeLocationAsync(int profileId, PracticeLocationDetailViewModel practiceLocationDetailViewModel)
        {
            string status = "true";
            PracticeLocationDetail dataModelPracticeLocationDetail = null;

            try
            {
                dataModelPracticeLocationDetail = AutoMapper.Mapper.Map<PracticeLocationDetailViewModel, PracticeLocationDetail>(practiceLocationDetailViewModel);
                //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Hospital Privilege Details", "Removed");
                //await notificationManager.SaveNotificationDetailAsync(notification);

                await practiceLocationManager.RemovePracticeLocationAsync(profileId, dataModelPracticeLocationDetail);
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
                status = ExceptionMessage.PRACTICE_LOCATION_REMOVE_EXCEPTION;
            }

            return Json(new { status = status, practiceLocation = dataModelPracticeLocationDetail }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Facility

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddFacilityAsync(int profileId, PracticeLocationViewModel practiceLocationViewModel)
        {
            string status = "true";
            Facility dataModelFacilityInformation = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFacilityInformation = AutoMapper.Mapper.Map<PracticeLocationViewModel, Facility>(practiceLocationViewModel);

                    // OrganizationAccountId Has to be replaced with account information 
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Facilities", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);


                    await organizationManager.AddFacilityAsync(OrganizationAccountId.DefaultOrganizationAccountID, dataModelFacilityInformation);
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
                status = ExceptionMessage.PRACTICE_LOCATION_CREATE_EXCEPTION;
            }

            return Json(new { status = status, facility = dataModelFacilityInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> UpdateFacilityAsync(int profileId, PracticeLocationViewModel practiceLocationViewModel)
        {
            string status = "true";
            Facility dataModelFacilityInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFacilityInformation = AutoMapper.Mapper.Map<PracticeLocationViewModel, Facility>(practiceLocationViewModel);

                    await organizationManager.UpdateFacilityAsync(OrganizationAccountId.DefaultOrganizationAccountID, dataModelFacilityInformation);
                    
                    //if (isCCO)
                    //{
                    //    // OrganizationAccountId Has to be replaced with account information 
                    //    await organizationManager.UpdateFacilityAsync(OrganizationAccountId.DefaultOrganizationAccountID, dataModelFacilityInformation);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = "Facility";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = practiceLocationViewModel.FacilityID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/UpdateFacilityAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(practiceLocationViewModel,dataModelFacilityInformation, tracker);
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
                status = ExceptionMessage.PRACTICE_LOCATION_CREATE_EXCEPTION;
            }

            return Json(new { status = status, facility = dataModelFacilityInformation }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Open Practice Status

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateOpenPracticeStatusAsync(int profileId, OpenPracticeStatusViewModel openPracticeStatus)
        {
            string status = "true";
            OpenPracticeStatus dataModelOpenPracticeStatus = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOpenPracticeStatus = AutoMapper.Mapper.Map<OpenPracticeStatusViewModel, OpenPracticeStatus>(openPracticeStatus);

                    await practiceLocationManager.UpdateOpenPracticeStatusAsync(openPracticeStatus.PracticeLocationDetailID, dataModelOpenPracticeStatus);
                    
                    //if (openPracticeStatus.OpenPracticeStatusID == 0)
                    //{
                    //    await practiceLocationManager.UpdateOpenPracticeStatusAsync(openPracticeStatus.PracticeLocationDetailID, dataModelOpenPracticeStatus);
                    //}
                    //else if (isCCO && openPracticeStatus.OpenPracticeStatusID != 0)
                    //{
                    //    await practiceLocationManager.UpdateOpenPracticeStatusAsync(openPracticeStatus.PracticeLocationDetailID, dataModelOpenPracticeStatus);
                    //}
                    //else if (!isCCO && openPracticeStatus.OpenPracticeStatusID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = "Open Practice Status";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = openPracticeStatus.OpenPracticeStatusID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/UpdateOpenPracticeStatusAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(openPracticeStatus, dataModelOpenPracticeStatus,tracker);
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
            return Json(new { status = status, openPracticeStatus = dataModelOpenPracticeStatus }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Practice Provider

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddPracticeProviderAsync(PracticeProviderViewModel practiceProvider)
        {
            string status = "true";
            PracticeProvider dataModelPracticeProvider = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeProvider = AutoMapper.Mapper.Map<PracticeProviderViewModel, PracticeProvider>(practiceProvider);
                    await practiceLocationManager.AddPracticeProviderAsync(practiceProvider.PracticeLocationID, dataModelPracticeProvider);
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
                status = ExceptionMessage.PracticeProvider_CREATE_EXCEPTION;
            }
            return Json(new { status = status, practiceProvider = dataModelPracticeProvider }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddPracticeProvidersAsync(List<PracticeProviderViewModel> practiceProviders)
        {
            string status = "true";
            var dataModelPracticeProviders = new List<PracticeProvider>();
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var practiceProvider in practiceProviders)
	                {
                        PracticeProvider dataModelPracticeProvider = null;
                        dataModelPracticeProvider = AutoMapper.Mapper.Map<PracticeProviderViewModel, PracticeProvider>(practiceProvider);
                        await practiceLocationManager.AddPracticeProviderAsync(practiceProvider.PracticeLocationID, dataModelPracticeProvider);
                        dataModelPracticeProviders.Add(dataModelPracticeProvider);
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
                status = ExceptionMessage.PracticeProvider_CREATE_EXCEPTION;
            }
            return Json(new { status = status, practiceProviders = dataModelPracticeProviders }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdatePracticeProviderAsync(int profileId, PracticeProviderViewModel practiceProvider)
        {
            string status = "true";
            PracticeProvider dataModelPracticeProvider = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeProvider = AutoMapper.Mapper.Map<PracticeProviderViewModel, PracticeProvider>(practiceProvider);

                    await practiceLocationManager.UpdatePracticeProviderAsync(dataModelPracticeProvider);

                    //if (isCCO)
                    //{
                    //    await practiceLocationManager.UpdatePracticeProviderAsync(dataModelPracticeProvider);
                    //}
                    //else 
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = dataModelPracticeProvider.Practice;
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = practiceProvider.PracticeProviderID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/UpdatePracticeProviderAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(practiceProvider, dataModelPracticeProvider, tracker);
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
                status = ExceptionMessage.PracticeProvider_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, practiceProvider = dataModelPracticeProvider }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> RemovePracticeProviderAsync(PracticeProviderViewModel practiceProvider)
        {
            string status = "true";
            PracticeProvider dataModelPracticeProvider = null;
            try
            {
                dataModelPracticeProvider = AutoMapper.Mapper.Map<PracticeProviderViewModel, PracticeProvider>(practiceProvider);
                await practiceLocationManager.RemovePracticeProviderAsync(practiceProvider.PracticeLocationID, dataModelPracticeProvider);

                //status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));

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
                status = ExceptionMessage.PracticeProvider_REMOVED_EXCEPTION;
            }
            return Json(new { status = status, practiceProvider = dataModelPracticeProvider }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Office Manager / Business Office Manager staff

        //For Add Office Manager / Business Office Manager staff
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> AddOfficeManagerAsync(int profileId, FacilityEmployeeViewModel officemanager)
        {
            string status = "true";
            Employee dataModelBusinessOfficeManager = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelBusinessOfficeManager = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(officemanager);
                    if (officemanager.EmployeeID == 0)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Managers", "Added");

                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await practiceLocationManager.UpdatePracticeBusinessManagerAsync(officemanager.PracticeLocationDetailID, dataModelBusinessOfficeManager);
                    }
                    else if (isCCO && officemanager.EmployeeID != 0)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Managers", "Added");

                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await practiceLocationManager.UpdatePracticeBusinessManagerAsync(officemanager.PracticeLocationDetailID, dataModelBusinessOfficeManager);
                    }
                    else if (!isCCO && officemanager.EmployeeID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Practice Location";
                        tracker.SubSection = "Office Manager";
                        tracker.userAuthId = userId;
                        tracker.objId = officemanager.EmployeeID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/PracticeLocation/AddOfficeManagerAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(officemanager,dataModelBusinessOfficeManager, tracker);
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
                status = ExceptionMessage.OFFICE_MANAGER_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, officemanager = dataModelBusinessOfficeManager }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Billing Contact

        //For Add Billing Contact
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> AddBillingContactAsync(int profileId, FacilityEmployeeViewModel billingcontact)
        {
            string status = "true";
            Employee dataModelBillingContact = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelBillingContact = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(billingcontact);
                    if (billingcontact.EmployeeID == 0)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Billing Contact Details", "Added");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await practiceLocationManager.UpdatePracticeBillingContactAsync(billingcontact.PracticeLocationDetailID, dataModelBillingContact);
                    }
                    else if (isCCO && billingcontact.EmployeeID != 0)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Billing Contact Details", "Added");
                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await practiceLocationManager.UpdatePracticeBillingContactAsync(billingcontact.PracticeLocationDetailID, dataModelBillingContact);
                    }
                    else if (!isCCO && billingcontact.EmployeeID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Practice Location";
                        tracker.SubSection = "Billing Contact";
                        tracker.userAuthId = userId;
                        tracker.objId = billingcontact.EmployeeID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/PracticeLocation/AddBillingContactAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(billingcontact,dataModelBillingContact, tracker);
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
                status = ExceptionMessage.BILLING_CONTACT_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, billingcontact = dataModelBillingContact }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Payment And Remittance

        //For Add Payment and Remittance
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> AddPaymentAndRemittanceAsync(int profileId, PracticePaymentAndRemittanceViewModel paymentandremittance)
        {
            string status = "true";

            PracticePaymentAndRemittance dataModelPracticePaymentAndRemittance = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticePaymentAndRemittance = AutoMapper.Mapper.Map<PracticePaymentAndRemittanceViewModel, PracticePaymentAndRemittance>(paymentandremittance);
                    if (paymentandremittance.PracticePaymentAndRemittanceID == 0)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Payment and Remittance Details", "Added");

                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await practiceLocationManager.UpdatePaymentAndRemittanceAsync(paymentandremittance.PracticeLocationDetailID, dataModelPracticePaymentAndRemittance);
                    }
                    else if (isCCO && paymentandremittance.PracticePaymentAndRemittanceID != 0)
                    {
                        ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Payment and Remittance Details", "Added");

                        await notificationManager.SaveNotificationDetailAsync(notification);

                        await practiceLocationManager.UpdatePaymentAndRemittanceAsync(paymentandremittance.PracticeLocationDetailID, dataModelPracticePaymentAndRemittance);
                    }
                    else if (!isCCO && paymentandremittance.PracticePaymentAndRemittanceID != 0)
                    {
                        string userId = await GetUserAuthId();
                        ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                        tracker.ProfileId = profileId;
                        tracker.Section = "Practice Location";
                        tracker.SubSection = "Payment and Remittance";
                        tracker.userAuthId = userId;
                        tracker.objId = paymentandremittance.PracticePaymentAndRemittanceID;
                        tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                        tracker.url = "/Profile/PracticeLocation/AddPaymentAndRemittanceAsync?profileId=";

                        profileUpdateManager.AddProfileUpdateForProvider(paymentandremittance,dataModelPracticePaymentAndRemittance, tracker);
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
                status = ExceptionMessage.PAYMENT_REMMITTANCE_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, paymentandremittance = dataModelPracticePaymentAndRemittance }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CredentialingContact

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddCredentialingContact(FacilityEmployeeViewModel credentialingContact)
        {
            string status = "true";
            Employee dataModelCredentialingContact = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCredentialingContact = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(credentialingContact);
                    await practiceLocationManager.AddCredentialingContactAsync(credentialingContact.PracticeLocationDetailID, dataModelCredentialingContact);
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
                status = ExceptionMessage.CREDENTIALING_CONTACT_ADD_EXCEPTION;
            }
            return Json(new { status = status, credentialingContact = dataModelCredentialingContact }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateCredentialingContact(int profileId, FacilityEmployeeViewModel credentialingContact)
        {
            string status = "true";
            Employee dataModelCredentialingContact = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCredentialingContact = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(credentialingContact);

                    await practiceLocationManager.UpdatePrimaryCredentialingContactAsync(profileId, dataModelCredentialingContact);

                    //if (isCCO)
                    //{
                    //    await practiceLocationManager.UpdatePrimaryCredentialingContactAsync(profileId, dataModelCredentialingContact);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = "Credentialing Contact";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = credentialingContact.EmployeeID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/UpdateCredentialingContact?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(credentialingContact, dataModelCredentialingContact, tracker);
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
                status = ExceptionMessage.CREDENTIALING_CONTACT_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, credentialingContact = dataModelCredentialingContact }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Worker Compensation

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateWorkersCompensationInformationAsync(int profileId, WorkersCompensationInfoViewModel workersCompensationInformation)
        {
            string status = "true";
            WorkersCompensationInformation dataModelWorkersCompensationInformation = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelWorkersCompensationInformation = AutoMapper.Mapper.Map<WorkersCompensationInfoViewModel, WorkersCompensationInformation>(workersCompensationInformation);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Workers Compensation Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdateWorkersCompensationInformationAsync(workersCompensationInformation.PracticeLocationDetailID, dataModelWorkersCompensationInformation);
                    
                    //if (workersCompensationInformation.WorkersCompensationInformationID == 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Workers Compensation Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await practiceLocationManager.UpdateWorkersCompensationInformationAsync(workersCompensationInformation.PracticeLocationDetailID, dataModelWorkersCompensationInformation);
                    //}
                    //else if (isCCO && workersCompensationInformation.WorkersCompensationInformationID != 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Workers Compensation Details", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await practiceLocationManager.UpdateWorkersCompensationInformationAsync(workersCompensationInformation.PracticeLocationDetailID, dataModelWorkersCompensationInformation);
                    //}
                    //else if (!isCCO && workersCompensationInformation.WorkersCompensationInformationID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = "workers Compensation Information";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = workersCompensationInformation.WorkersCompensationInformationID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/UpdateWorkersCompensationInformationAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(workersCompensationInformation,dataModelWorkersCompensationInformation, tracker);
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
                status = ExceptionMessage.WORKER_COMPENSATION_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, workersCompensationInformation = dataModelWorkersCompensationInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> RenewWorkersCompensationInformationAsync(int profileId, WorkersCompensationInfoViewModel workersCompensationInformation)
        {
            string status = "true";
            WorkersCompensationInformation dataWorkersCompensationInfo = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataWorkersCompensationInfo = AutoMapper.Mapper.Map<WorkersCompensationInfoViewModel, WorkersCompensationInformation>(workersCompensationInformation);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Workers Compensation Details", "Renewed");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.RenewWorkersCompensationInformationAsync(workersCompensationInformation.PracticeLocationDetailID, dataWorkersCompensationInfo);
                    
                    //if (isCCO)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Workers Compensation Details", "Renewed");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await practiceLocationManager.RenewWorkersCompensationInformationAsync(workersCompensationInformation.PracticeLocationDetailID, dataWorkersCompensationInfo);
                    //}
                    //else
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = "workers Compensation Information";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = workersCompensationInformation.WorkersCompensationInformationID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Renewal.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/RenewWorkersCompensationInformationAsync?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(workersCompensationInformation,dataWorkersCompensationInfo, tracker);
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

            return Json(new { status = status, workersCompensationInformation = dataWorkersCompensationInfo }, JsonRequestBehavior.AllowGet);
        }       

        #endregion

        #region Office Hours

        public async Task<ActionResult> updateOfficeHours(int profileId, ProviderPracticeOfficeHourViewModel providerPracticeOfficeHour)
        {
            string status = "true";
            ProviderPracticeOfficeHour dataModelProviderPracticeOfficeHour = null;
            bool isCCO = await GetUserRole();
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProviderPracticeOfficeHour = AutoMapper.Mapper.Map<ProviderPracticeOfficeHourViewModel, ProviderPracticeOfficeHour>(providerPracticeOfficeHour);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Hours", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdateProviderOfficeHourAsync(providerPracticeOfficeHour.PracticeLocationDetailID, dataModelProviderPracticeOfficeHour);
                    
                    //if (providerPracticeOfficeHour.PracticeOfficeHourID != 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Hours", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await practiceLocationManager.UpdateProviderOfficeHourAsync(providerPracticeOfficeHour.PracticeLocationDetailID, dataModelProviderPracticeOfficeHour);
                    //}
                    //else if (isCCO && providerPracticeOfficeHour.PracticeOfficeHourID != 0)
                    //{
                    //    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Hours", "Updated");
                    //    await notificationManager.SaveNotificationDetailAsync(notification);

                    //    await practiceLocationManager.UpdateProviderOfficeHourAsync(providerPracticeOfficeHour.PracticeLocationDetailID, dataModelProviderPracticeOfficeHour);
                    //}
                    //else if (!isCCO && providerPracticeOfficeHour.PracticeOfficeHourID != 0)
                    //{
                    //    string userId = await GetUserAuthId();
                    //    ProfileUpdateTrackerBusinessModel tracker = new ProfileUpdateTrackerBusinessModel();

                    //    tracker.ProfileId = profileId;
                    //    tracker.Section = "Practice Location";
                    //    tracker.SubSection = "Office Hours";
                    //    tracker.userAuthId = userId;
                    //    tracker.objId = providerPracticeOfficeHour.PracticeOfficeHourID;
                    //    tracker.ModificationType = AHC.CD.Entities.MasterData.Enums.ModificationType.Update.ToString();
                    //    tracker.url = "/Profile/PracticeLocation/updateOfficeHours?profileId=";

                    //    profileUpdateManager.AddProfileUpdateForProvider(providerPracticeOfficeHour,dataModelProviderPracticeOfficeHour, tracker);
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
                status = ExceptionMessage.OFFICE_MANAGER_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, providerPracticeOfficeHours = dataModelProviderPracticeOfficeHour }, JsonRequestBehavior.AllowGet);
        }

        #endregion

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
    }
}