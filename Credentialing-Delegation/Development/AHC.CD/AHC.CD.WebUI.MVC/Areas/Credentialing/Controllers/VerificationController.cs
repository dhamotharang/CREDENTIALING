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
using AHC.CD.Business.PSVVerification;

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

        public string VerifyStateLicense(string StateLicenseNumber)
        {
            try
            {
                StateLicenseVerification stateLicense = new StateLicenseVerification();
                var result = stateLicense.VerifyStateLicense(StateLicenseNumber);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string VerifyMedicareOptLicense(string NPINumber)
        {
            try
            {
                MedicareOptVerification MedicareVerification = new MedicareOptVerification();
                var result = MedicareVerification.VerifyMedicareOpt(NPINumber);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string VerifyBoardCertification(string NPINumber)
        {
            try
            {
                BoardCertificationVerification BoardCertificationVerification = new BoardCertificationVerification();
                var result = BoardCertificationVerification.VerifyBoardCertification(NPINumber);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string VerifyFederalDEALicense(string DEANumber, string LastName, string SSN, string TaxID)
        {
            try
            {
                DEAVerification DEAVerification = new DEAVerification();
                var result = DEAVerification.VerifyFederalDEA(DEANumber, LastName, SSN, TaxID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string VerifyOIG(string FirstName, string MiddleName, string LastName, string DOB)
        {
            try
            {
                OIGVerification OIGVerification = new OIGVerification();
                var result = OIGVerification.VerifyOIG(FirstName, MiddleName, LastName, DOB, "", "");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string VerifyCDSInformation(string CDSNumber)
        {
            try
            {
                CDSVerification CDSVerification = new CDSVerification();
                var result = CDSVerification.VerifyCDSInformation(CDSNumber);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult> InitiateNewPSV(int profileId, int credInfoId)
        {
            try
            {
                string userAuthId = await GetUserAuthId();
                var profileVerification = psvManager.InitiateNewPSV(profileId, userAuthId);

                var PSVData = JsonConvert.SerializeObject(profileVerification.ProfileVerificationDetails, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local});
                ViewBag.PSVData = PSVData;

                ViewBag.VerificationId = profileVerification.ProfileVerificationInfoId;

                var profile = await psvManager.GetProfileData(credInfoId);
                ViewBag.Profile = JsonConvert.SerializeObject(profile, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local});
                ViewBag.CredentialingInfoId = credInfoId;
                ViewBag.ProfileId = profileId;
                ViewBag.PsvType = "PlanPsv";

                return View("PrimarySource");
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet]
        public async Task<ActionResult> InitiatePSVPrototype(int profileId, int credInfoId)
        {
            try
            {
                string userAuthId = await GetUserAuthId();
                var profileVerification = psvManager.InitiateNewPSV(profileId, userAuthId);

                var PSVData = JsonConvert.SerializeObject(profileVerification.ProfileVerificationDetails, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
                ViewBag.PSVData = PSVData;

                ViewBag.VerificationId = profileVerification.ProfileVerificationInfoId;

                var profile = await psvManager.GetProfileData(credInfoId);
                ViewBag.Profile = JsonConvert.SerializeObject(profile, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
                ViewBag.CredentialingInfoId = credInfoId;
                ViewBag.ProfileId = profileId;
                ViewBag.PsvType = "PlanPsv";

                return View("AutomatedVerificationPrototype");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public async Task<ActionResult> InitiateProfilePSV(int Id)
        {
            try
            {
                string userAuthId = await GetUserAuthId();
                var profileVerification = psvManager.InitiateNewPSV(Id, userAuthId);

                var PSVData = JsonConvert.SerializeObject(profileVerification.ProfileVerificationDetails);
                ViewBag.PSVData = PSVData;
                ViewBag.VerificationId = profileVerification.ProfileVerificationInfoId;
                var profile = await psvManager.GetProfileDataByID(Id);
                ViewBag.ProfileData = JsonConvert.SerializeObject(profile);
                ViewBag.PsvType = "ProfilePsv";
                ViewBag.ProfileId = Id;

                return View("PrimarySource");
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public async Task<ActionResult> InitiateNewPSVForQuickUpdate(int profileId, int credInfoId)
        {
            try
            {
                string userAuthId = await GetUserAuthId();

                var profileVerification = psvManager.InitiateNewPSV(profileId, userAuthId);

                var PSVData = profileVerification.ProfileVerificationDetails;

                var VerificationId = profileVerification.ProfileVerificationInfoId;

                var profile = await psvManager.GetProfileData(credInfoId);

                return Json(new { profileVerification = profileVerification, PSVData = PSVData, VerificationId = VerificationId, profile = profile }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpGet]
        public string GetAllPSVList()
        {
            try
            {
                var psvData = psvManager.GetAllPSVList();
                //return Json(psvData, JsonRequestBehavior.AllowGet);
                return JsonConvert.SerializeObject(psvData, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local});
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


        public async Task<ActionResult> SetAllVerified(int credinfoId, int credVerificationId, List<int> verificationIDs)
        {
            try
            {
                string status = "true";
                //List<int> IDs = verificationIDs.ToList<int>();
                string userAuthId = await GetUserAuthId();

                psvManager.SetAllVerified(credinfoId, credVerificationId, userAuthId, verificationIDs);

                //return RedirectToAction("Application", "CnD", new { id = credinfoId });

                return Json(new { status = status, credinfoId = credinfoId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public ActionResult GetPSVReport(int credinfoId)
        {
            var status = "true";
            List<CredentialingProfileVerificationDetail> psvReport = null;
            try
            {
                psvReport = psvManager.GetPSVReport(credinfoId);
                
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            var psvReport1 =  JsonConvert.SerializeObject(psvReport, new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            return Json(new {status = status, psvReport = psvReport1 }, JsonRequestBehavior.AllowGet);
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
                    DocumentDTO verificationDocument = new DocumentDTO() { IsRemoved = false };
                    if (verificationDetail.VerificationResult.VerificationDocumentPath == null)
                    {
                        verificationDocument = CreateDocument(verificationDetail.VerificationResult.VerificationResultDocument);
                    }
                    var verificationDetailId = psvManager.UpdateVerifiedData(credVerificationInfoId, profileId, verification, verificationDocument, userAuthId,verificationDetail.VerificationResult.VerificationDocumentPath);
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

        [HttpGet]
        public ActionResult GetPSVHistory(int profileId)
        {
            var status = "true";
            List<ProfileVerificationDetail> psvHistory = null;
            try
            {
                psvHistory = psvManager.GetPSVHistory(profileId);

            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return Json(new { status = status, psvHistory = psvHistory }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetPSVDetailsForAProvider(int profileId)    
        {   
            var status = "true";
            ProfileVerificationInfo psvInfo = null;
            try
            {
                psvInfo = psvManager.GetPSVDetailsForAProvider(profileId);

            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return Json(new { status = status, psvInfo = psvInfo }, JsonRequestBehavior.AllowGet);  
        }


        public ActionResult AutomatedVerificationPrototype()
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

        public string GetLocationDatafromUSPS()
        {
            USPSService uspsService = new USPSService();
            var result = uspsService.ValidateLocationDetails();
            return result;
        }
	}
}