using Newtonsoft.Json;
using PortalTemplate.Areas.Billing.Models.Clearing_House;
using PortalTemplate.Areas.Billing.Services;
using PortalTemplate.Areas.Billing.Services.IServices;
using PortalTemplate.Areas.Billing.Services.Services;
using PortalTemplate.Models.PratianComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Billing.Controllers
{
    public class ClearingHouseController : Controller
    {
        readonly IClearingHouseService _clearingHouseService;
        public ClearingHouseController()
        {
            _clearingHouseService = new ClearingHouseService();

        }
        //
        // GET: /ClearingHouse/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetClearingHouseList()
        {

            return PartialView("~/Areas/Billing/Views/ClearingHouse/_Settings1.cshtml", _clearingHouseService.GetClearingHouseList());
        }


        public ActionResult GetClearingHouseListByIndex(int index, string sortingType, string sortBy, ClearingHouseViewModel SearchObject)
        {

            return PartialView("~/Areas/Billing/Views/ClearingHouse/_TBodyClearingHouseList.cshtml", _clearingHouseService.GetClearingHouseList());
        }



        public ActionResult AddClearingHouse()
        {
            return PartialView("~/Areas/Billing/Views/ClearingHouse/_ClearingHouseAddEdit.cshtml", _clearingHouseService.AddClearingHouse());
        }

        public ActionResult ViewClearingHouse(string ClearingHouseId)
        {

            return PartialView("~/Areas/Billing/Views/ClearingHouse/_ClearingHouseView.cshtml", _clearingHouseService.ViewClearingHouse(ClearingHouseId));
        }


        public ActionResult EditClearingHouse(string ClearingHouseId)
        {

            return PartialView("~/Areas/Billing/Views/ClearingHouse/_ClearingHouseAddEdit.cshtml", _clearingHouseService.EditClearingHouse(ClearingHouseId));
        }

        [HttpPost]
        public ActionResult AddClearingHouse(ClearingHouseViewModel ClearingHouseObject)
        {
            return null;
        }

        [HttpGet]
        public ActionResult GetAllPayer()
        {
            string file = Server.MapPath("~/Resources/Billing/PayerListOfClearingHouse.json");
            string json = System.IO.File.ReadAllText(file);


            return Json(json, JsonRequestBehavior.AllowGet);
        }





    }
}