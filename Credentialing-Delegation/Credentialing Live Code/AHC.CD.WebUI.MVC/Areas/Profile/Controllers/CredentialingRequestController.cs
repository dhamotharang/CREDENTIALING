using AHC.CD.Business;
using AHC.CD.Business.Notification;
using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.CredentialingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class CredentialingRequestController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;
        private ICredentilingRequestManager credentialingRequestManager = null;

        public CredentialingRequestController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager, ICredentilingRequestManager credentialingRequestManager)
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
            this.profileUpdateManager = profileUpdateManager;
            this.credentialingRequestManager = credentialingRequestManager;
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

        //
        // GET: /Profile/CredentialingRequest/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult InitiateCredentialingRequest(CredentialingRequestViewModel credentialingRequest)   
        {
            string status = "true";
            CredentialingRequest dataCredentialingRequest = null;
            try
            {
                dataCredentialingRequest = AutoMapper.Mapper.Map<CredentialingRequestViewModel, CredentialingRequest>(credentialingRequest);
                credentialingRequestManager.InitiateCredentialingRequestAsync(dataCredentialingRequest);

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
                status = ExceptionMessage.CREDENTIALING_REQUEST_EXCEPTION;
            }

            return Json(new { status = status}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CredentialingRequestInactive(CredentialingRequestViewModel credentialingRequest)  
        {
            string status = "true";
            CredentialingRequest dataCredentialingRequest = null;
            try
            {
                dataCredentialingRequest = AutoMapper.Mapper.Map<CredentialingRequestViewModel, CredentialingRequest>(credentialingRequest);
                credentialingRequestManager.CredentialingRequestInactiveAsync(dataCredentialingRequest);    

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
                status = ExceptionMessage.CREDENTIALING_REQUEST_EXCEPTION;
            }

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
	}
}