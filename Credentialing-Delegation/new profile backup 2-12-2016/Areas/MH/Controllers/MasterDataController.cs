using PortalTemplate.Areas.MH.Common;
using PortalTemplate.Areas.MH.IServices;
using PortalTemplate.Areas.MH.Models.ViewModels.MasterData;
using PortalTemplate.Areas.MH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
//using AccountManagement.Client;
using System.Web.UI;
using System.Web.Caching;

namespace PortalTemplate.Areas.MH.Controllers
{
    public class MasterDataController : Controller
    {
        IMasterDataService _masterDataService;
        public MasterDataController()
        {
            _masterDataService = new MasterDataService();
        }

        CommonMethods commonMethods = new CommonMethods();

        public JsonResult GetAllInsuranceCompanies(string searchTerm)
        {
            return Json(_masterDataService.GetAllInsuranceCompanies(searchTerm), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPlansForInsuranceCompany(string searchTerm)
        {
            return Json(_masterDataService.GetAllPlans(searchTerm), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCities(string searchTerm)
        {
            var cities = _masterDataService.GetAllCities(searchTerm);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        //[OutputCache(Duration = 10, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        //[OutputCache(CacheProfile = "Cache1Hour")]
        public JsonResult GetAllStates(string searchTerm)
        {
            var states = _masterDataService.GetAllStates(searchTerm);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult GetAllCounties(string searchTerm)
        {
            var counties = _masterDataService.GetAllCountries(searchTerm);
            return Json(counties, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCountries(string searchTerm)
        {
            var countries = _masterDataService.GetAllCountries(searchTerm);
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllPatientRelationships(string searchTerm)
        {
            var relationships = _masterDataService.GetAllPatientRelationships(searchTerm);
            return Json(relationships, JsonRequestBehavior.AllowGet);
        }

        /// <summary>This Method is Used to Get All the Master Data.
        /// </summary>
        //public void GetAllMasterData()
        //{
        //    var MasterData = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/MH/Views/web.config").AppSettings.Settings["AddNewMemebr"].Value.Split(',');
        //    foreach (string md in MasterData)
        //    {
        //        var method = md.Split(':')[0];
        //        var variable = md.Split(':')[1];
        //        ViewData[variable] = typeof(MasterDataService).GetMethod(method).Invoke(service, new object[0]);
        //    }
        //}

        /// <summary>This Method is Used to Get Provider Service Data.
        /// </summary>
        //public void GetProviderServiceData()
        //{
        //    var ServiceData = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/MH/Views/web.config").AppSettings.Settings["ProviderService"].Value.Split(',');
        //    foreach (string md in ServiceData)
        //    {
        //        var method = md.Split(':')[0];
        //        var variable = md.Split(':')[1];
        //        ViewData[variable] = typeof(ProviderService).GetMethod(method).Invoke(providerservice, new object[0]);
        //    }
        //}
	}
}