using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using PortalTemplate.Areas.UM.Models.ViewModels.Queue;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Services;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class QueueController : Controller
    {
        // GET: UM/Queue
        List<QueueViewModel> QueueModel = new List<QueueViewModel>();
      
        // GET: UM/Queue
        IQueueServices service = new QueueServices();


        public ActionResult GetQueueListByIndex(int index, string sortingType, string sortBy, QueueViewModel SearchObject)
        {

            return null;
                //PartialView("~/Areas/Billing/Views/ClearingHouse/_TBodyClearingHouseList.cshtml", QueueModel.GetClearingHouseList());
        }




        public ActionResult GetNewTab(string QueueType, string QueueTab)
        {
            
            QueueModuleViewModel QueueModule = service.GetAuthList(QueueType, QueueTab);
            QueueModel = QueueModule.AuthorizationsList;
            //ViewBag.QueueCount = QueueModule.AuthorizationsCount;
            ViewBag.SubTabView = QueueModule.QueueSubTab;
            ViewBag.QueueType = QueueType;
            ViewBag.QueueTab = QueueTab;
            return PartialView("~/Areas/UM/Views/Queue/UMQueues/Queue/QueueGrid/_QueueGrid.cshtml", QueueModel);
        }

        public ActionResult GetQueue(string QueueType,string QueueTab)
        {

            QueueModuleViewModel QueueModule = service.GetAuthList(QueueType, QueueTab);
            //QueueModel = QueueModule.AuthorizationsList;
            //ViewBag.QueueCount = QueueModule.AuthorizationsCount;
            ViewBag.SubTabView = QueueModule.QueueSubTab;
            ViewBag.QueueType = QueueType;
            ViewBag.QueueTab = QueueTab;
            return PartialView("~/Areas/UM/Views/Queue/UMQueues/Queue/_Queue.cshtml", QueueModule);
        }


        public ActionResult GetFacilityQueue(string QueueType, string QueueTab)
        {
            try
            {
                FacilityQueueModuleViewModel FacilityQueueModel = service.GetFacilityAuthList(QueueType, QueueTab);
               // FacilityQueueModel = QueueModule;
              //  ViewBag.QueueCount = FacilityQueueModel.AuthorizationsCount;
                ViewBag.SubTabView = FacilityQueueModel.QueueSubTab;
                ViewBag.QueueType = QueueType;
                ViewBag.QueueTab = QueueTab;
                return PartialView("~/Areas/UM/Views/Queue/UMQueues/FacilityQueue/_FacilityQueue.cshtml", FacilityQueueModel);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public ActionResult GetQueueDataonRequestType(string QueueType, string QueueTab, string RequestType="")
        {

            QueueModuleViewModel QueueModule = service.GetAuthList(QueueType, QueueTab,null,RequestType);
            QueueModel = QueueModule.AuthorizationsList;   
            ViewBag.QueueType = QueueType;
            ViewBag.QueueTab = QueueTab;
            return PartialView(GetResourceLink(QueueType, QueueTab, "tabledata"), QueueModel); 
        }

        private string GetResourceLink(string QueueType, string QueueTab, string RequiredResource)
        {
            string link ="";
            if (RequiredResource.ToLower() == "main")
            {
                if (QueueType.ToUpper() == "PCP")
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/PCPQueue/_PCPQueue.cshtml"; 
                }
                else if (QueueType.ToUpper() == "FACILITY")
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/Facility/_Facility.cshtml"; 
                }
                else
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/Default/_Default.cshtml"; 
                }
            }
            else if (RequiredResource.ToLower() == "table")
            {
                if (QueueType.ToUpper() == "PCP")
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/PCPQueue/_PCPQueueTable.cshtml";
                }
                else if (QueueType.ToUpper() == "FACILITY")
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/Facility/_FacilityQueueTable.cshtml";
                }
                else
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/Default/_DefaultQueueTable.cshtml";
                }
            }
            else if (RequiredResource.ToLower() == "tabledata")
            {
                if (QueueType.ToUpper() == "PCP")
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/PCPQueue/_PCPQueueDataRows.cshtml";
                }
                else if (QueueType.ToUpper() == "FACILITY")
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/Facility/_FacilityQueueDataRows.cshtml";
                }
                else
                {
                    link = "~/Areas/UM/Views/Queue/UMQueues/Default/_DefaultQueueDataRows.cshtml";
                }
            }
            return link;
        }
    }
}