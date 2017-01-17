using AHC.CD.Business.Credentialing.AppointmentInfo;
using AHC.CD.Business.DocumentWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using Newtonsoft.Json;
using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.Business;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.Entities.Credentialing.AppointmentInformation;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class CCMController : Controller
    {
        private IProfileManager profileManager = null;
        private IApplicationManager applicationManager = null;
        private IAppointmentManager appointmentManager = null;
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

        public CCMController(IAppointmentManager appointmentManager, IApplicationManager applicationManager, IProfileManager profileManager)
        {
            this.appointmentManager = appointmentManager;
            this.applicationManager = applicationManager;
            this.profileManager = profileManager;
        }

        //
        // GET: /Credentialing/Index/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Credentialing/SPA/
        public async Task<ActionResult> SPA(int id)
        {
            AHC.CD.Entities.MasterProfile.Profile profileData = null;
            profileData = await profileManager.GetByIdAsync(id);

            ViewBag.profileData = Json(new { profileData }, JsonRequestBehavior.AllowGet);
            return View();
        }


        public async Task<ActionResult> Application(int id)
        {
            try
            {
                ViewBag.CredentialingInfoID = id;
                ViewBag.CredentialingInfo = JsonConvert.SerializeObject(await appointmentManager.GetCredentialinfoByID(id));
                //CredentialingAppointmentDetailViewModel credentialingAppointmentDetailViewModel=
                return View("SPA");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> GetAllCredentialingFilterList()
        {
            var data = await appointmentManager.GetAllFilteredCredentialInfoList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AjaxAction]
        public async Task<ActionResult> CCMActionUploadAsync(CredentialingAppointmentDetailViewModel credentialingAppointmentDetailViewModel)
        {
            string status = "true";
            CredentialingAppointmentDetail credentialingAppointmentDetail = null;
            try
            {
                
                if (ModelState.IsValid)
                {
                    credentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetailViewModel, CredentialingAppointmentDetail>(credentialingAppointmentDetailViewModel);
                    DocumentDTO document = CreateDocument(credentialingAppointmentDetailViewModel.CredentialingAppointmentResult.SignatureFile);
                    // Change Notifications
                    await appointmentManager.SaveResultForScheduledAppointment(credentialingAppointmentDetailViewModel.CredentialingAppointmentDetailID, document, credentialingAppointmentDetail.CredentialingAppointmentResult, await GetUserAuthId());
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new { status = status, data = credentialingAppointmentDetail }, JsonRequestBehavior.AllowGet);
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

        #endregion

	}
}