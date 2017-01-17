using PortalTemplate.Areas.Portal.Models.ProviderBridge.BridgeQueue;
using PortalTemplate.Areas.Portal.Services.ProviderBridge.BridgeQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.ProviderBridge.BridgeQueue
{
    public class BridgeQueueController : Controller
    {
        BridgeQueueService bridgeQueueService = new BridgeQueueService();
        List<BridgeQueueViewModel> BridgeQueueData = new List<BridgeQueueViewModel>();
        List<BridgeQueueViewModel> FilteredBridgeQueueData = new List<BridgeQueueViewModel>();
        //
        // GET: /Portal/BridgeQueue/
        public ActionResult Index()
        {
            BridgeQueueData = bridgeQueueService.GetQueueData();
            FilterData("All", BridgeQueueData);

            return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueue.cshtml", FilteredBridgeQueueData);

        }
        public ActionResult GetPenningList(string pending)
        {
            TempData["tabVal"] = pending;
            ViewBag.tabVal = pending;          
            BridgeQueueData = bridgeQueueService.GetQueueData();          
            FilterData("All", BridgeQueueData);
            return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueueBody.cshtml", FilteredBridgeQueueData);
        }
        public ActionResult GetApprovedList(string approved)
        {
            TempData["tabVal"] = approved;
            ViewBag.tabVal = approved;          

            //tempVal = ViewBag.tabVal;
            BridgeQueueData = bridgeQueueService.GetApprovedQueueData();           
            return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueueBody.cshtml", BridgeQueueData);
        }
        public ActionResult GetRejectedList(string Rejected)
        {
            TempData["tabVal"] = Rejected;           
            ViewBag.tabVal = Rejected;          
     
            BridgeQueueData = bridgeQueueService.GetRejectedQueueData();
          return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueueBody.cshtml", BridgeQueueData);
        }
        public ActionResult GetFilteredList(string filterType, string ViewbagType)
        {
                       
            if (ViewbagType == "Approved")
            {
                BridgeQueueData = bridgeQueueService.GetApprovedQueueData();
                FilterData(filterType, BridgeQueueData);
            }
            else if (ViewbagType == "Rejected")
            {
                BridgeQueueData = bridgeQueueService.GetRejectedQueueData();
                FilterData(filterType, BridgeQueueData);
            }
            else
            {
                BridgeQueueData = bridgeQueueService.GetQueueData();
                FilterData(filterType, BridgeQueueData);
            }
            return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueueBody.cshtml", FilteredBridgeQueueData);
        }

        /// <summary>
        /// Get the List of Bridge Request of a Particular Type e.g Open, Assigned, Approved and Pending Request
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public ActionResult GetBridgeRequestListType()
        {
            try
            {
                BridgeQueueData = bridgeQueueService.GetQueueDataByRequestType();
                //return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueueBody.cshtml", BridgeQueueData);
                return PartialView("~/Areas/Portal/Views/ProviderBridge/BridgeQueue/_BridgeQueue.cshtml", BridgeQueueData);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void FilterData(string filterType, List<BridgeQueueViewModel> BridgeQueueData)
        {
            if (filterType == "claims")
            {
                FilteredBridgeQueueData = BridgeQueueData.Where(x => x.QueueStatus.Contains("Assigned")).ToList();
            }
            else if (filterType == "um")
            {
                FilteredBridgeQueueData = BridgeQueueData.Where(x => x.QueueStatus.Contains("Pending")).ToList();
            }
            else
            {
                FilteredBridgeQueueData = BridgeQueueData.Where(x => x.QueueStatus.Contains("Open")).ToList();
                GetRequestCount(BridgeQueueData);
            }
        }

        private void GetRequestCount(List<BridgeQueueViewModel> BridgeQueueData)
        {
            ViewBag.Request = BridgeQueueData.Count;
            ViewBag.OpenRequest = BridgeQueueData.Where(x => x.QueueStatus.Contains("Open")).ToList().Count;
            ViewBag.AssignedRequest = BridgeQueueData.Where(x => x.QueueStatus.Contains("Assigned")).ToList().Count;
            ViewBag.ApprovedRequest = BridgeQueueData.Where(x => x.QueueStatus.Contains("Approved")).ToList().Count;
            ViewBag.PendingRequest = BridgeQueueData.Where(x => x.QueueStatus.Contains("Pending")).ToList().Count;

        }
    }
}