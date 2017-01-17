using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.Portal.Services.PriorAuth.Queues;
using PortalTemplate.Areas.Portal.Models.PriorAuth.Queue;
using PortalTemplate.Areas.Portal.IServices.Queues;


namespace PortalTemplate.Areas.Portal.Controllers.PriorAuth
{
    public class PriorAuthQueueController : Controller
    {
        //
        // GET: /Portal/PriorAuthQueue/
        // GET: UM/Queue
       IQueueServices service = new QueueServices();
       List<QueueViewModel> QueueModel = new List<QueueViewModel>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetQueue(string QueueType, string QueueTab)
        {

            PortalQueueViewModel QueueModule = service.GetAuthList(QueueType, QueueTab);
            QueueModel = QueueModule.AuthorizationsList;
            ViewBag.QueueCount = QueueModule.AuthorizationsCount;
            ViewBag.SubTabView = QueueModule.QueueSubTab;
            ViewBag.QueueType = QueueType;
            ViewBag.QueueTab = QueueTab;
            return PartialView("~/Areas/Portal/Views/PriorAuth/Queue/_Queue.cshtml", QueueModel);
        }

        public ActionResult GetQueueListByIndex(int index, string sortingType, string sortBy, QueueViewModel SearchObject)
        {

            return null;
            //PartialView("~/Areas/Billing/Views/ClearingHouse/_TBodyClearingHouseList.cshtml", QueueModel.GetClearingHouseList());
        }
	}
}