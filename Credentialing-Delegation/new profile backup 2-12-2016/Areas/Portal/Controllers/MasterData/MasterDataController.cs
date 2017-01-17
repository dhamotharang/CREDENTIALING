using PortalTemplate.Areas.UM.Models;
using PortalTemplate.Areas.MH.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.Portal.Services.MasterData;

namespace PortalTemplate.Areas.Portal.Controllers.MasterData
{
    public class MasterDataController : Controller
    {
        CMSMasterData CMSMasterData;
        public MasterDataController()
        {
            CMSMasterData = new CMSMasterData();
        }
        //
        // GET: /Portal/MasterData/
        public ActionResult Index()
        {
            return View();
        }
        CommonMethods commonMethods = new CommonMethods();


        // GET: /Portal/MasterData/GetAllPlanName
        public JsonResult GetAllPlanName(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/PlanName.JSON");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var PlansList = serializer.Deserialize<List<PlanViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(PlansList, searchTerm, "Name");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }



        // GET: /Portal/MasterData/GetAllIPAGroup
        public JsonResult GetAllIPAGroup(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/PlanName.JSON");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var PlansList = serializer.Deserialize<List<PlanViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(PlansList, searchTerm, "CompanyName");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }


        // GET: /Portal/MasterData/GetAllSpecialty
        public JsonResult GetAllSpecialty(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/Specialty.json");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var PlansList = serializer.Deserialize<List<SpecialityViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(PlansList, searchTerm, "Name");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }



        // GET: /Portal/MasterData/GetAllProviderType
        public JsonResult GetAllProviderType(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/PlanName.JSON");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var PlansList = serializer.Deserialize<List<PlanViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(PlansList, searchTerm, "CompanyName");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }



        // GET: /Portal/MasterData/GetAllFacility
        public JsonResult GetAllFacility(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/PlanName.JSON");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var PlansList = serializer.Deserialize<List<PlanViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(PlansList, searchTerm, "CompanyName");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }


        // GET: /Portal/MasterData/
        public JsonResult GetAllCity(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/City.json");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var CityList = serializer.Deserialize<List<CityViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(CityList, searchTerm, "Name");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }


        // GET: /Portal/MasterData/
        public JsonResult GetAllIPAGro(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/PlanName.JSON");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var PlansList = serializer.Deserialize<List<PlanViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(PlansList, searchTerm, "CompanyName");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }


        // GET: /Portal/MasterData/
        public JsonResult GetAllIPAGr(string searchTerm)
        {
            if (searchTerm == null) { searchTerm = ""; }
            string file = Server.MapPath("~/Areas/Portal/Resources/MasterData/PlanName.JSON");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var PlansList = serializer.Deserialize<List<PlanViewModel>>(json);
            var filteredPersonList = commonMethods.Filter(PlansList, searchTerm, "CompanyName");
            var filteredPersonListInJson = serializer.Serialize(filteredPersonList);
            return Json(filteredPersonListInJson, JsonRequestBehavior.AllowGet);
        }

        #region CMS Master Data Abstraction

        // Get All CMS City Master Data
        public JsonResult GetAllCities(string searchTerm)
        {
            try
            {
                var cityList = CMSMasterData.GetAllCity();
                return Json(cityList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}