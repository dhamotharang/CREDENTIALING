using AHC.CD.WebUI.MVC.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class PrototypesController : Controller
    {
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
            return View("~/Views/Prototypes/CCOAssignment/CCOAssignment.cshtml");
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
            var data = PrototypeHelper.GetCCOList();
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
    }
}