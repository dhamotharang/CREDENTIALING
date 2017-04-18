using AHC.CD.WebUI.MVC.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using AHC.CD.Entities;
using AHC.CD.Business.Users;
using Newtonsoft.Json;
using System;
using AHC.CD.Entities.UserInfo;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class PrototypesController : Controller
    {

        private IUserManager _userManager = null;


        public PrototypesController(IUserManager userManager)
        {
            this._userManager = userManager;
        }
        //
        // GET: /Prototypes/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CCOSummary()
        {
            return View();
        }
        public ActionResult ProviderSummary()
        {
            return View();
        }

        public ActionResult CCOAssignment()
        {
            return View("~/Views/AdminConfig/CCOAssignment/CCOAssignment.cshtml");
        }
        public ActionResult CCMDashboard()
        {
            return View();
        }
        public ActionResult CCOTLAssignment()
        {
            return View();
        }
        public ActionResult CommitteeReport()
        {
            return View();
        }

        /// <summary>
        /// Provider Directory View Action Method
        /// </summary>
        /// <returns>cshtml viewpage</returns>
        public ActionResult ProviderDirectory()
        {
            return View();
        }

        public ActionResult ManagementDashboard()
        {
            return View();
        }

        public ActionResult ProviderDashboard()
        {
            return View();
        }

        /// <summary>
        /// Get Provider Json Data
        /// </summary>
        /// <returns>Json, Provider Object List</returns>
        public JsonResult GetProviders(int count = 10)
        {
            var data = PrototypeHelper.GetProviders(count);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Providers Summary Status Data
        /// </summary>
        /// <param name="profileStatus"></param>
        /// <param name="count"></param>
        /// <returns>JSON, Provider Object</returns>
        public JsonResult GetProvidersSummary(string profileStatus, int count = 10)
        {
            var data = PrototypeHelper.GetProvidersSummary(count, profileStatus);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCCOData()
        {
            var data = PrototypeHelper.GetCCOList().Take(9);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetTLData()
        {
            var data = PrototypeHelper.GetTLData();
            return Json(data, JsonRequestBehavior.AllowGet);

        }       

        /// <summary>
        /// Get Specialities Json Data
        /// </summary>
        /// <returns>Json, Speciality Object List</returns>
        public JsonResult GetSpecialties()
        {
            return Json(PrototypeHelper.GetSpecialties(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Expiry License Data Json Data
        /// </summary>
        /// <returns>Json, Expiry License Data Object List</returns>
        public JsonResult GetLicenseData()
        {
            return Json(PrototypeHelper.GetLicenseData(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Provider licenses Information list
        /// </summary>
        /// <param name="count">Number Of Provider Need</param>
        /// <param name="minDaysLeft">Min days left</param>
        /// <param name="maxDaysLeft">max days left</param>
        /// <param name="licenseTypeCode">license type code</param>
        /// <returns></returns>
        public JsonResult GetProvidersLicenseInformation(int count, int minDaysLeft, int maxDaysLeft, string licenseTypeCode)
        {
            return Json(PrototypeHelper.GetProvidersLicenseInformation(count, minDaysLeft, maxDaysLeft, licenseTypeCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get CCO Summary Status Data
        /// </summary>
        /// <param name="profileStatus"></param>
        /// <param name="count"></param>
        /// <returns>JSON, CCO Object</returns>
        public JsonResult GetCCOSummary(int count = 15)
        {
            var data = PrototypeHelper.GetCCOSummary(count);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get CCO Rank For Management Dashboard
        /// </summary>
        /// <param name="count">Required Number Of CCO Counts</param>
        /// <returns>List, CCO Rank Object List</returns>
        public JsonResult GetCCORankData(int count = 5)
        {
            return Json(PrototypeHelper.GetCCORanks(count), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Profile Completion Data Counts For Management Dashboard
        /// </summary>
        /// <returns>List, Object</returns>
        public JsonResult GetProfileCompletionData()
        {
            return Json(PrototypeHelper.GetProfileCompletionDataCount(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get CCO Report For Management Dashboard
        /// </summary>
        /// <param name="CCOCount">Required Number Of CCO Counts</param>
        /// <returns>List, CCO Report List</returns>
        public JsonResult GetCCOReportsData(int count = 5)
        {
            return Json(PrototypeHelper.GetCCOReports(count), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Provider Personal Details
        /// </summary>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>object, Provider Personal Details</returns>
        public JsonResult GetProviderPersonalDetails(int ProfileID = 10)
        {
            return Json(PrototypeHelper.GetProviderPersonalDetails(ProfileID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Provider Recent Task Performed
        /// </summary>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Task Performed for Provider</returns>
        public JsonResult GetProviderTasks(int count = 10, int ProfileID = 10)
        {
            return Json(PrototypeHelper.GetProviderTasks(count, ProfileID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Provider Credentialing Details
        /// </summary>
        /// <param name="count">Number Of Credentialing</param>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Provider Credentialing Details</returns>
        public JsonResult GetCredentialingDetails(int count = 2, int ProfileID = 10)
        {
            return Json(PrototypeHelper.GetCredentialingDetails(count, ProfileID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Provider Hospitals
        /// </summary>
        /// <param name="count">Number Of Hospitals</param>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Provider Hospitals</returns>
        public JsonResult GetProviderHospitals(int count = 5, int ProfileID = 10)
        {
            return Json(PrototypeHelper.GetProviderHospitals(count, ProfileID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Provider IPA/Groups
        /// </summary>
        /// <param name="count">Number Of IPA/Groups</param>
        /// <param name="ProfileID">Profile ID Of Provider</param>
        /// <returns>List Object, Provider Hospitals</returns>
        public JsonResult GetProviderGroups(int count = 5, int ProfileID = 10)
        {
            return Json(PrototypeHelper.GetProviderGroups(count, ProfileID), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetReminderNotification()
        {
            return PartialView("~/Views/Prototypes/Reminders/_reminderNotification.cshtml");
        }
    }
}