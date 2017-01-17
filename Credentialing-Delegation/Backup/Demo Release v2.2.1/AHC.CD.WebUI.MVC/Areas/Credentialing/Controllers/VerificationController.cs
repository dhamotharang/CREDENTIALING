using AHC.CD.Business.Credentialing.PSVInfo;
using AHC.CD.ErrorLogging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.PSVInfo;
using AHC.CD.WebUI.MVC.CustomHelpers;


namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class VerificationController : Controller
    {

        private IPSVManager psvManager = null;
        private IErrorLogger errorLogger = null;

        public VerificationController(IPSVManager psvManager, IErrorLogger errorLogger)
        {
            this.psvManager = psvManager;
            this.errorLogger = errorLogger;            
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


        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet]
        public ActionResult PrimarySource()
        {          
            
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> InitiateNewPSV(int credinfoId)
        {
            try
            {

                string userAuthId = await GetUserAuthId();
                ViewBag.CredentialingInfoId = credinfoId;
                ViewBag.VerificationId = psvManager.InitiateNewPSV(credinfoId, userAuthId);                

                var profile = await psvManager.GetProfileVerificationData(credinfoId);
                ViewBag.Profile = JsonConvert.SerializeObject(profile);

                ViewBag.ProfileId = profile.ProfileID;

                return View("PrimarySource");
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet]
        public ActionResult GetAllPSVList()
        {
            try
            {
                var psvData = psvManager.GetAllPSVList();
                return Json(psvData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [AjaxAction]
        [HttpPost]
        public async Task<ActionResult> AddPSVDetail(int credVerificationInfoId, int profileId, ProfileVerificationDetailViewModel verificationDetail)
        {
            string status = "true";
            ProfileVerificationDetail verification = null;
            try
            {
                if (ModelState.IsValid)
                {
                    string userAuthId = await GetUserAuthId();

                    verification = AutoMapper.Mapper.Map<ProfileVerificationDetailViewModel, ProfileVerificationDetail>(verificationDetail);
                    DocumentDTO verificationDocument = CreateDocument(verificationDetail.VerificationResult.VerificationResultDocument);

                    var verificationDetailId = psvManager.AddVerifiedData(credVerificationInfoId, profileId, verification, verificationDocument, userAuthId);
                    //verification.ProfileVerificationDetailId = verificationDetailId;
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
                
            }
            catch (Exception)
            {
                throw;
            }

            return Json(new {verificationDetail = verification, status = status}, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> SetAllVerified(int credinfoId, int credVerificationId)
        {
            try
            {
                string userAuthId = await GetUserAuthId();

                psvManager.SetAllVerified(credinfoId, credVerificationId, userAuthId);

                return RedirectToAction("Application", "CnD", new { id = credinfoId });

                //return View("~/Credentialing/CnD/Application/" + credinfoId);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetPendingPSVList(int credinfoId)
        {            
            try
            {
                string userAuthId = await GetUserAuthId();
                ViewBag.CredentialingInfoId = credinfoId;
                //ViewBag.VerificationId = psvManager.InitiateNewPSV(credinfoId, userAuthId);

                var profile = await psvManager.GetProfileVerificationData(credinfoId);
                ViewBag.Profile = JsonConvert.SerializeObject(profile);

                ViewBag.ProfileId = profile.ProfileID;

                var credentialingVerification = psvManager.GetPendingPSV(credinfoId);
                ViewBag.VerificationId = credentialingVerification.CredentialingVerificationInfoId;
                var pendingPSVData = JsonConvert.SerializeObject(credentialingVerification.ProfileVerificationInfo.ProfileVerificationDetails);
                ViewBag.PendingPSVData = pendingPSVData;
                

                return View("PrimarySource");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Status = "error";
                return View("Index");
            }
            
        }

        [HttpGet]
        public ActionResult GetPSVReport(int credinfoId)
        {
            var status = "true";
            List<ProfileVerificationDetail> psvReport = null;
            try
            {
                psvReport = psvManager.GetPSVReport(credinfoId);
                
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return Json(new {status = status, psvReport = psvReport }, JsonRequestBehavior.AllowGet);
        }

        [AjaxAction]
        [HttpPost]
        public async Task<ActionResult> UpdatePSVDetail(int credVerificationInfoId, int profileId, ProfileVerificationDetailViewModel verificationDetail)
        {
            string status = "true";
            ProfileVerificationDetail verification = null;
            try
            {
                if (ModelState.IsValid)
                {
                    string userAuthId = await GetUserAuthId();

                    verification = AutoMapper.Mapper.Map<ProfileVerificationDetailViewModel, ProfileVerificationDetail>(verificationDetail);
                    DocumentDTO verificationDocument = CreateDocument(verificationDetail.VerificationResult.VerificationResultDocument);

                    var verificationDetailId = psvManager.UpdateVerifiedData(credVerificationInfoId, profileId, verification, verificationDocument, userAuthId);
                    //verification.ProfileVerificationDetailId = verificationDetailId;
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Json(new { verificationDetail = verification, status = status }, JsonRequestBehavior.AllowGet);
        }

        #region Private Method

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

        

        //public void test(ProfileVerificationDetailViewModel pvdv)
        //{ 
            
        //}
	}
}