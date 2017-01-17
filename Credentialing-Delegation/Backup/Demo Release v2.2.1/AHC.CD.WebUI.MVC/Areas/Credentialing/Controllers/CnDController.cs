using AHC.CD.Business;
using AHC.CD.Business.Credentialing.AppointmentInfo;
using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList;
using AHC.CD.WebUI.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class CnDController : Controller
    {
        private IApplicationRepositoryManager formManager = null;
        private IAppointmentManager appointmentManager = null;
        private IApplicationManager applicationManager = null;
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;

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

        public CnDController(IApplicationManager applicationManager, IApplicationRepositoryManager formManager, IProfileManager profileManager, IAppointmentManager appointmentManager, IErrorLogger errorLogger)
        {
            this.applicationManager = applicationManager;
            this.formManager = formManager;
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            this.appointmentManager = appointmentManager;
        }

        // GET: Credentialing/Index
       
        public async Task<ActionResult> Index(int id)
        {
           AHC.CD.Entities.MasterProfile.Profile profileData = null;
            profileData = await profileManager.GetByIdAsync(id);

            ViewBag.profileData = Json(new { profileData }, JsonRequestBehavior.AllowGet);
            return View();
        }
        public ActionResult PrintCheckList()
        {
            return PartialView();
        }
        //---- Credentialing Action Appointment -----------
        public ActionResult CredentialingAppointment()
        {
            return View();
        }

        //---- Auditing -----------
        public ActionResult Auditing()
        {
            return View();
        }

        public async Task<ActionResult> Application(int id)
        {
            try
            {
                ViewBag.CredentialingInfoID = id;
                ViewBag.CredentialingInfo = JsonConvert.SerializeObject(await applicationManager.GetCredentialingInfoByIdAsync(id));
                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
                
        public async Task<JsonResult> ApplicationRepository(int profileId, string template)
        {
            var status = "true";
            var path = "";
            //var template = "AHC Provider Profile for Wellcare - BLANK_new.pdf";
            try
            {

                path = await formManager.GetProfileDataByIdAsync(profileId, template); 

            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, path = path }, JsonRequestBehavior.AllowGet);            
            
        }

        [HttpPost]
        public async Task<JsonResult> AddApplication(int profileId, string path)
        {
            var status = "true";
            

            try
            {
                //path = await formManager.GetProfileDataByIdAsync(profileId, path);

            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, path = path }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> CCMAction(int credentialingInfoID, CredentialingAppointmentDetailViewModel credentialingAppointmentDetail)
        {
            var status = "true";
            CredentialingAppointmentDetail dataCredentialingAppointmentDetail = null;
            try
            {
                dataCredentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetailViewModel, CredentialingAppointmentDetail>(credentialingAppointmentDetail, dataCredentialingAppointmentDetail);
                int id = await appointmentManager.UpdateAppointmentDetails(credentialingInfoID, dataCredentialingAppointmentDetail, await GetUserAuthId());                
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }
            return Json(new { status = status, data = dataCredentialingAppointmentDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SetAppointment(int[] ProviderIDArray, string AppointmentDate)
        {
            var status = "true";
            List<int> scheduledAppointments = null;

            try
            {
                scheduledAppointments = await appointmentManager.ScheduleAppointmentForMany(ProviderIDArray.ToList(), Convert.ToDateTime(AppointmentDate), await GetUserAuthId());
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, data = scheduledAppointments }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<JsonResult> RemoveAppointment(int ProviderID)
        {
            var status = "true";

            try
            {
                int id = await appointmentManager.RemoveScheduledAppointmentForIndividual(ProviderID, await GetUserAuthId());
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, data = ProviderID }, JsonRequestBehavior.AllowGet);

        }

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }
    }
}