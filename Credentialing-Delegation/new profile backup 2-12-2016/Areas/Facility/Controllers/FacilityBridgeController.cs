using PortalTemplate.Areas.Facility.Models;
using PortalTemplate.Areas.Facility.Models.MasterData;
using PortalTemplate.Areas.Facility.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Facility.Controllers
{
    public class FacilityBridgeController : Controller
    {
        FacilityService bridgeQueueService = new FacilityService();
        FacilityBridgeQueueViewModel QueueData = new FacilityBridgeQueueViewModel();
        List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
        List<FacilityBridgeQueueViewModel> FilteredBridgeQueueData = new List<FacilityBridgeQueueViewModel>();
        //
        // GET: /Facility/FacilityBridge/
        public ActionResult Index()
        {
            BridgeQueueData = bridgeQueueService.GetOpenedQueueData();
            ViewBag.RequestedCount = bridgeQueueService.GetRequestedQueueDataCount();
            ViewBag.ApprovedCount = bridgeQueueService.GetApprovedQueueDataCount();
            ViewBag.AssignedCount = bridgeQueueService.GetAssignedQueueDataCount();
            ViewBag.OpenedCount = bridgeQueueService.GetOpenedQueueDataCount();
            ViewBag.PendingCount = bridgeQueueService.GetPendingQueueDataCount();
            return View("~/Areas/Facility/Views/FacilityBridge/_FacilityList.cshtml", BridgeQueueData);
        }
        public ActionResult SearchFacility()
        {
            return View("~/Areas/Facility/Views/FacilityBridge/_SearchFacility.cshtml");
        }
        [HttpPost]
        public ActionResult GetFacility(FacilityBridgeQueueViewModel searchdata)
        {
            BridgeQueueData = bridgeQueueService.GetAllFacilities();
            return View("~/Areas/Facility/Views/FacilityBridge/_SearchFacilityResult.cshtml", BridgeQueueData);
        }
        [HttpPost]
        public ActionResult AddFacilityData(FacilityBridgeQueueViewModel facilityData)
        {
            BridgeQueueData = bridgeQueueService.GetOpenedQueueData();
            ViewBag.RequestedCount = bridgeQueueService.GetRequestedQueueDataCount();
            ViewBag.ApprovedCount = bridgeQueueService.GetApprovedQueueDataCount();
            ViewBag.AssignedCount = bridgeQueueService.GetAssignedQueueDataCount();
            ViewBag.OpenedCount = bridgeQueueService.GetOpenedQueueDataCount();
            ViewBag.PendingCount = bridgeQueueService.GetPendingQueueDataCount();
            return View("~/Areas/Facility/Views/FacilityBridge/_FacilityList.cshtml", BridgeQueueData);
        }
        public ActionResult GetQueueDataForEdit(string id)
        {
            QueueData = bridgeQueueService.GetQueueData(id);
            return View("~/Areas/Facility/Views/AddFacility/_FacilityBody.cshtml", QueueData);
        }
        public JsonResult GetAllFacilityData()
        {
            return Json(bridgeQueueService.GetAllFacilityData(), JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult GetQueueDataForView(string id)
        {
            QueueData = bridgeQueueService.GetQueueData(id);
            return View("~/Areas/Facility/Views/AddFacility/_FacilityViewBody.cshtml", QueueData);
        }
        public JsonResult GetAllCountries(string searchTerm)
        {
            return Json(bridgeQueueService.GetAllCountries(searchTerm), JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAllCities(string searchTerm)
        {
            return Json(bridgeQueueService.GetAllCities(searchTerm), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllStates(string searchTerm)
        {
            return Json(bridgeQueueService.GetAllStates(searchTerm), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllCounties(string searchTerm)
        {
            return Json(bridgeQueueService.GetAllCounties(searchTerm), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllOrganizations(string searchTerm)
        {
            return Json(bridgeQueueService.GetAllOrganizations(searchTerm), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOpenedQueueData()
        {
            BridgeQueueData = bridgeQueueService.GetOpenedQueueData();
            return View("~/Areas/Facility/Views/FacilityBridge/_FacilityOpenedQueue.cshtml", BridgeQueueData);
        }
        public ActionResult GetApprovedQueueData()
        {
            BridgeQueueData = bridgeQueueService.GetApprovedQueueData();
            return View("~/Areas/Facility/Views/FacilityBridge/_FacilityApprovedQueue.cshtml", BridgeQueueData);
        }
        public ActionResult GetAssignedQueueData()
        {
            BridgeQueueData = bridgeQueueService.GetAssignedQueueData();
            return View("~/Areas/Facility/Views/FacilityBridge/_FacilityAssignedQueue.cshtml", BridgeQueueData);
        }
        public ActionResult GetPendingQueueData()
        {
            BridgeQueueData = bridgeQueueService.GetPendingQueueData();
            return View("~/Areas/Facility/Views/FacilityBridge/_FacilityPendingQueue.cshtml", BridgeQueueData);
        }
    }
}