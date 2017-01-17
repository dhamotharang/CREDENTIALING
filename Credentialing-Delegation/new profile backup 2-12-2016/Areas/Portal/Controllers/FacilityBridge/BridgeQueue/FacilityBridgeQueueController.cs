using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.Portal.Services.ProviderBridge.BridgeQueue;
using PortalTemplate.Areas.Portal.Models.ProviderBridge.BridgeQueue;

namespace PortalTemplate.Areas.Portal.Controllers.FacilityBridge.BridgeQueue
{
    public class FacilityBridgeQueueController : Controller
    {
        BridgeQueueService bridgeQueueService = new BridgeQueueService();
        List<BridgeQueueViewModel> BridgeQueueData = new List<BridgeQueueViewModel>();
        //
        // GET: /Portal/BridgeQueue/
        public ActionResult Index()
        {
            BridgeQueueData = bridgeQueueService.GetQueueData();

            return PartialView("~/Areas/Portal/Views/FacilityBridge/BridgeQueue/_BridgeQueue.cshtml", BridgeQueueData);
        }
        public ActionResult GetPenningList()
        {
            BridgeQueueData = bridgeQueueService.GetQueueData();

            return PartialView("~/Areas/Portal/Views/FacilityBridge/BridgeQueue/_BridgeQueueBody.cshtml", BridgeQueueData);
        }
        public ActionResult GetApprovedList()
        {
            BridgeQueueData = bridgeQueueService.GetApprovedQueueData();

            return PartialView("~/Areas/Portal/Views/FacilityBridge/BridgeQueue/_BridgeQueueBody.cshtml", BridgeQueueData);
        }
        public ActionResult GetRejectedList()
        {
            BridgeQueueData = bridgeQueueService.GetRejectedQueueData();

            return PartialView("~/Areas/Portal/Views/FacilityBridge/BridgeQueue/_BridgeQueueBody.cshtml", BridgeQueueData);
        }
	}
}