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
        private IFacilityManager facilityManager = null;
        private IProfileManager profileManager = null;
        private IChangeNotificationManager notificationManager;
        //private int profileId;
        public PracticeLocationController(IPracticeLocationManager practiceLocationManager, IOrganizationManager organizationManager, IErrorLogger errorLogger, IFacilityManager facilityManager, IProfileManager profileManager, IChangeNotificationManager notificationManager)
        {
            this.practiceLocationManager = practiceLocationManager;
            this.organizationManager = organizationManager;
            this.errorLogger = errorLogger;
            this.facilityManager = facilityManager;
            this.profileManager = profileManager;
            this.notificationManager = notificationManager;
        }

        // GET: Profile/PracticeLocation
        public ActionResult Index()
        {
            return View();
        }

        //#region Workers Compensation Information

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
                    //profileId = practiceLocationManager
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
        public async Task<ActionResult> RenewWorkersCompensationInformationAsync(int profileId, WorkersCompensationInfoViewModel workersCompensationInformation)
        {
            string status = "true";
            WorkersCompensationInformation dataWorkersCompensationInfo = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataWorkersCompensationInfo = AutoMapper.Mapper.Map<WorkersCompensationInfoViewModel, WorkersCompensationInformation>(workersCompensationInformation);
                    
                    //DocumentDTO document = null; NO DOCUMENT

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
                //              status = ExceptionMessage.FEDERAL_DEA_LICENSE_RENEW_EXCEPTION;
            }

            return Json(new { status = status, workersCompensationInformation = dataWorkersCompensationInfo }, JsonRequestBehavior.AllowGet);
        }



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
        //[HttpPost]
        //[AjaxAction]
        //[ProfileAuthorize(ProfileActionType.Edit, false)]
        //public async Task<ActionResult> UpdateWorkersCompensationInformationAsync(int profileId, WorkersCompensationInformationViewModel workersCompensationInformation)
        //{
        //    string status = "true";

        //    WorkersCompensationInformation dataModelWorkersCompensationInformation = null;

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            dataModelWorkersCompensationInformation = AutoMapper.Mapper.Map<WorkersCompensationInformationViewModel, WorkersCompensationInformation>(workersCompensationInformation);

        //            await profileManager.UpdateWorkersCompensationInformationAsync(profileId, dataModelWorkersCompensationInformation);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
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
        //    }
        //    return Json(new { status = status, workersCompensationInformation = dataModelWorkersCompensationInformation }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Primary Credentialing Contact

        //[HttpPost]
        //public async Task<ActionResult> UpdateCredentialingContactAsync(int profileId, PrimaryCredentialingContactViewModel credentialingContact)
        //{
        //    string status = "true";

        //    PrimaryCredentialingContact dataModelcredentialingContact = null;

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            dataModelcredentialingContact = AutoMapper.Mapper.Map<PrimaryCredentialingContactViewModel, PrimaryCredentialingContact>(credentialingContact);

        //            // await profileManager.UpdateWorkersCompensationInformationAsync(profileId, dataModelWorkersCompensationInformation);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
        //        }
        //    }
        //    catch (DatabaseValidationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.ValidationError;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorLogger.LogError(ex);
        //    }

        //    return Json(new { status = status, credentialingContact = dataModelcredentialingContact }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

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

        //For update Office Manager / Business Office Manager staff
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateOfficeManagerAsync(int profileId, int practiceLocationDetailId, int officemanagerID)
        {
            string status = "true";

            try
            {
                if (ModelState.IsValid)
                {
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Office Managers", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePracticeBusinessManagerAsync(practiceLocationDetailId, officemanagerID);
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
            //return Json(new { status = status, officemanager = officemanager }, JsonRequestBehavior.AllowGet);
            return null;
        }


        //To get all Office Manager / Business Office Manager staff for a particular location
        public async Task<ActionResult> GetAllBusinessOfficeManager()
        {
            var data = await facilityManager.GetAllBusinessOfficeManagerAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
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

        //For update Billing Contact
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateBillingContactAsync(int profileId, int practiceLocationDetailId, int billingcontactID)
        {
            string status = "true";

            try
            {
                if (ModelState.IsValid)
                {
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Billing Contact Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePracticeBillingContactAsync(practiceLocationDetailId, billingcontactID);
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
           // return Json(new { status = status, billingcontact = billingcontact }, JsonRequestBehavior.AllowGet);
            return null;
        }

        //To get all Billing Contact for a particular location
        public async Task<ActionResult> GetAllBiilingContactPerson()
        {
            var data = await facilityManager.GetAllBiilingContactPersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
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
           // Employee dataModelEmployee = null;

            try
            {
                if (ModelState.IsValid)
                {
                    //dataModelEmployee = AutoMapper.Mapper.Map<FacilityEmployeeViewModel, Employee>(paymentandremittance.FacilityEmployees);
                    //await practiceLocationManager.UpdateEmployeeForPaymentAndRemittanceAsync(paymentandremittance.PracticeLocationDetailID, dataModelEmployee);

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

        //For update Payment and Remittance
        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdatePaymentAndRemittanceAsync(int profileId, int practiceLocationDetailId, int paymentandremittanceID)
        {
            string status = "true";
            
            try
            {
                if (ModelState.IsValid)
                {
                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Payment and Remittance Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdatePracticeBillingContactAsync(practiceLocationDetailId, paymentandremittanceID);
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
            //return Json(new { status = status, paymentandremittance = paymentandremittance }, JsonRequestBehavior.AllowGet);
            return null;
        }

        //To get all Payment And Remittance for a particular location
        public async Task<ActionResult> GetAllPaymentAndRemittancePersons()
        {
            var data = await facilityManager.GetAllPaymentAndRemittancePersonsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
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


                   await  practiceLocationManager.AddPracticeLocationAsync(profileId, dataModelPracticeLocationDetailInformation);
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

        #region Midlevel

        

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> addMidlevelPractionersAsync(int profileId, MidLevelPractitionerViewModel midLevelPractitioner)
        {
            string status = "true";
            MidLevelPractitioner dataModelMidLevelPractitioner = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMidLevelPractitioner = AutoMapper.Mapper.Map<MidLevelPractitionerViewModel, MidLevelPractitioner>(midLevelPractitioner);
                    dataModelMidLevelPractitioner.ActivationDate = DateTime.Now;

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Mid Level Practioners Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.addMidLevelAsync(midLevelPractitioner.PracticeLocationDetailID, dataModelMidLevelPractitioner);
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
                status = ExceptionMessage.MID_LEVEL_CREATE_EXCEPTION;
            }
            return Json(new { status = status, midlevelPractioner = dataModelMidLevelPractitioner }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateMidLevelPractitionerAsync(int profileId, MidLevelPractitionerViewModel midLevelPractitioner)
        {
            string status = "true";
            MidLevelPractitioner dataModelMidLevelPractitioner = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelMidLevelPractitioner = AutoMapper.Mapper.Map<MidLevelPractitionerViewModel, MidLevelPractitioner>(midLevelPractitioner);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Mid Level Practioners Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdateMidLevelAsync(midLevelPractitioner.PracticeLocationDetailID, dataModelMidLevelPractitioner);
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
                status = ExceptionMessage.MID_LEVEL_CREATE_EXCEPTION;
            }
            return Json(new { status = status, midlevelPractioner = dataModelMidLevelPractitioner }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Supervising Provider


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> AddSupervisingProviderAsync(int profileId, SupervisingProviderViewModel supervisingProvider)
        {
            string status = "true";
            SupervisingProvider dataModelSupervisingProvider = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelSupervisingProvider = AutoMapper.Mapper.Map<SupervisingProviderViewModel, SupervisingProvider>(supervisingProvider);
                    dataModelSupervisingProvider.ActivationDate=DateTime.Now;

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Supervising Providers Details", "Added");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.AddSupervisingProviderAsync(supervisingProvider.PracticeLocationDetailID, dataModelSupervisingProvider);
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
                status = ExceptionMessage.SUPERVISING_PROVIDER_SAVE_EXCEPTION;
            }
            return Json(new { status = status, supervisingProvider = dataModelSupervisingProvider }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> UpdateSupervisingProviderAsync(int profileId, SupervisingProviderViewModel supervisingProvider)
        {
            string status = "true";
            SupervisingProvider dataModelSupervisingProvider = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelSupervisingProvider = AutoMapper.Mapper.Map<SupervisingProviderViewModel, SupervisingProvider>(supervisingProvider);

                    ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Practice Locations - Supervising Providers Details", "Updated");
                    await notificationManager.SaveNotificationDetailAsync(notification);

                    await practiceLocationManager.UpdateSupervisingProviderAsync(supervisingProvider.PracticeLocationDetailID, dataModelSupervisingProvider);
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
                status = ExceptionMessage.SUPERVISING_PROVIDER_SAVE_EXCEPTION;
            }
            return Json(new { status = status, supervisingProvider = dataModelSupervisingProvider }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Covering Colleagues


        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Add, false)]
        public async Task<ActionResult> AddPracticeColleagueAsync(PracticeColleagueViewModel practiceColleague)
        {
            string status = "true";
            PracticeColleague dataModelPracticeColleague = null;

            try
            {
                dataModelPracticeColleague = AutoMapper.Mapper.Map<PracticeColleagueViewModel, PracticeColleague>(practiceColleague);
                await practiceLocationManager.AddPracticeColleagueAsync(practiceColleague.PracticeLocationDetailID, dataModelPracticeColleague);
               
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
                status = ExceptionMessage.Partner_CREATE_EXCEPTION;
            }
            return Json(new { status = status, practiceColleague = dataModelPracticeColleague }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AjaxAction]
        [ProfileAuthorize(ProfileActionType.Edit, false)]
        public async Task<ActionResult> RemovePartnerAsync(PracticeColleagueViewModel practiceColleague)
        {
            string status = "true";
            PracticeColleague dataModelPracticeColleague = null;

            try
            {
                dataModelPracticeColleague = AutoMapper.Mapper.Map<PracticeColleagueViewModel, PracticeColleague>(practiceColleague);
                await practiceLocationManager.UpdatePracticeColleagueAsync(practiceColleague.PracticeLocationDetailID, dataModelPracticeColleague);

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
                status = ExceptionMessage.Partner_CREATE_EXCEPTION;
            }
            return Json(new { status = status, practiceColleague = dataModelPracticeColleague }, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}