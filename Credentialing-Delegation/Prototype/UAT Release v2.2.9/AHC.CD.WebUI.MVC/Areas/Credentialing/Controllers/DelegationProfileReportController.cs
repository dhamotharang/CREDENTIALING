using AHC.CD.Business.BusinessModels.DelegationProfileReport;
using AHC.CD.Business.Credentialing.DelegationProfileReport;
using AHC.CD.Entities.Credentialing.DelegationProfileReport;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.DelegationProfileReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class DelegationProfileReportController : Controller
    {
        private IDelegationProfileReportManager reportManager = null;

        public DelegationProfileReportController(IDelegationProfileReportManager reportManager)
        {
            this.reportManager = reportManager;            
        }

        //
        // GET: /Credentialing/DelegationProfileReport/
        public async Task<ActionResult> Index(int profileId)
        {
            ProviderGeneralInfoBussinessModel providerProfile = new ProviderGeneralInfoBussinessModel();
            List<ProviderPracitceInfoBusinessModel> locations = null;
            List<ProviderProfessionalDetailBusinessModel> specialtis = null;
            try
            {

                providerProfile = await reportManager.GetProfileDataByIdAsync(profileId, locations, specialtis);
                var profileReport = JsonConvert.SerializeObject(providerProfile);
                ViewBag.ProfileReport = profileReport;
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult SaveDelegationProfileReport(int requestId, ProfileReportViewModel report)
        {
            ProfileReport profileReport = null;
            string status = "true";
            try
            {
                profileReport = AutoMapper.Mapper.Map<ProfileReportViewModel, ProfileReport>(report);

                profileReport.ProfileReportId = reportManager.SaveDelegationProfileReport(requestId, profileReport);

                return Json(new { status = status, profileReport = profileReport }, JsonRequestBehavior.AllowGet);                
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JsonResult> GetDelegationProfileReport(int CredContractRequestId)
        {
            List<ProfileReport> pReports = new List<ProfileReport>();
            List<ProfileReportViewModel> profileReports = new List<ProfileReportViewModel>();
            string status = "true";
            try
            {
                ProfileReportViewModel profileReport = null;
                pReports = await reportManager.GetDelegationProfileReport(CredContractRequestId);

                foreach (ProfileReport item in pReports)
                {

                    profileReport = AutoMapper.Mapper.Map<ProfileReport, ProfileReportViewModel>(item);
                    profileReports.Add(profileReport);
                }

                return Json(new { status = status, profileReports = profileReports }, JsonRequestBehavior.AllowGet);                

            }
            catch (Exception)
            {
                
                throw;
            }

        }

        //public async Task<ActionResult> GetProfileDataByIdAsync(int profileId, List<ProviderPracitceInfoBusinessModel> locations, List<ProviderProfessionalDetailBusinessModel> specialtis)
        //{
        //    var status = "true";
        //    ProviderGeneralInfoBussinessModel providerProfile = new ProviderGeneralInfoBussinessModel();
        //    try
        //    {

        //        providerProfile = await reportManager.GetProfileDataByIdAsync(profileId, locations, specialtis);

        //    }
        //    catch (Exception ex)
        //    {
        //        status = ex.Message;
        //    }

        //    return Json(new { status = status, providerProfile = providerProfile }, JsonRequestBehavior.AllowGet);

        //}
	}
}