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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class PracticeLocationController : Controller
    {
        private IPracticeLocationManager practiceLocationManager = null;
        private IErrorLogger errorLogger = null;
        IOrganizationManager organizationManager=null;
        private IChangeNotificationManager notificationManager;

        public PracticeLocationController(IPracticeLocationManager practiceLocationManager, IOrganizationManager organizationManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager)
        {
            this.practiceLocationManager = practiceLocationManager;
            this.organizationManager = organizationManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeLocationDetailInformation = AutoMapper.Mapper.Map<PracticeLocationDetailViewModel, PracticeLocationDetail>(practiceLocationDetailViewModel);

                    // OrganizationAccountId Has to be replaced with account information 
                    dataModelPracticeLocationDetailInformation.OrganizationId = OrganizationAccountId.DefaultOrganizationAccountID;

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePracticeLocationAsync(dataModelPracticeLocationDetailInformation);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelFacilityInformation = AutoMapper.Mapper.Map<PracticeLocationViewModel, Facility>(practiceLocationViewModel);

                    // OrganizationAccountId Has to be replaced with account information 
                    await organizationManager.UpdateFacilityAsync(OrganizationAccountId.DefaultOrganizationAccountID, dataModelFacilityInformation);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelOpenPracticeStatus = AutoMapper.Mapper.Map<OpenPracticeStatusViewModel, OpenPracticeStatus>(openPracticeStatus);
                    await practiceLocationManager.UpdateOpenPracticeStatusAsync(openPracticeStatus.PracticeLocationDetailID, dataModelOpenPracticeStatus);
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
        public async Task<ActionResult> UpdatePracticeProviderAsync(PracticeProviderViewModel practiceProvider)
        {
            string status = "true";
            PracticeProvider dataModelPracticeProvider = null;
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticeProvider = AutoMapper.Mapper.Map<PracticeProviderViewModel, PracticeProvider>(practiceProvider);
                    await practiceLocationManager.UpdatePracticeProviderAsync(dataModelPracticeProvider);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelBusinessOfficeManager = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(officemanager);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Managers", "Added");

                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePracticeBusinessManagerAsync(officemanager.PracticeLocationDetailID, dataModelBusinessOfficeManager);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelBillingContact = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(billingcontact);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Billing Contact Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePracticeBillingContactAsync(billingcontact.PracticeLocationDetailID, dataModelBillingContact);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPracticePaymentAndRemittance = AutoMapper.Mapper.Map<PracticePaymentAndRemittanceViewModel, PracticePaymentAndRemittance>(paymentandremittance);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Payment and Remittance Details", "Added");

                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePaymentAndRemittanceAsync(paymentandremittance.PracticeLocationDetailID, dataModelPracticePaymentAndRemittance);
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
        public async Task<ActionResult> UpdateCredentialingContact(int practiceLocationDetailId, FacilityEmployeeViewModel credentialingContact)
        {
            string status = "true";
            Employee dataModelCredentialingContact = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelCredentialingContact = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(credentialingContact);
                    await practiceLocationManager.UpdatePrimaryCredentialingContactAsync(practiceLocationDetailId, dataModelCredentialingContact);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelWorkersCompensationInformation = AutoMapper.Mapper.Map<WorkersCompensationInfoViewModel, WorkersCompensationInformation>(workersCompensationInformation);
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Workers Compensation Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdateWorkersCompensationInformationAsync(workersCompensationInformation.PracticeLocationDetailID, dataModelWorkersCompensationInformation);
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

            try
            {
                if (ModelState.IsValid)
                {
                    dataWorkersCompensationInfo = AutoMapper.Mapper.Map<WorkersCompensationInfoViewModel, WorkersCompensationInformation>(workersCompensationInformation);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Workers Compensation Details", "Renewed");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.RenewWorkersCompensationInformationAsync(workersCompensationInformation.PracticeLocationDetailID, dataWorkersCompensationInfo);
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
            try
            {
                if (ModelState.IsValid)
                {
                    dataModelProviderPracticeOfficeHour = AutoMapper.Mapper.Map<ProviderPracticeOfficeHourViewModel, ProviderPracticeOfficeHour>(providerPracticeOfficeHour);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Hours", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdateProviderOfficeHourAsync(providerPracticeOfficeHour.PracticeLocationDetailID, dataModelProviderPracticeOfficeHour);
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
    }
}