using PortalTemplate.Areas.CM.Models.Queue;
using PortalTemplate.Areas.CM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PortalTemplate.Areas.CM.Controllers
{
    public class QueueController : Controller
    {
        
        CMQueueServices queueServices = new CMQueueServices();
        List<QueueViewModel> QueueModel = new List<QueueViewModel>();

        // GET: /CM/Queue/
        // -----This method is to get the partials of either user queue or system identified queue - from CM Side menu ------
        public ActionResult GetQueue(string QueueType)
        {
            QueueModel = queueServices.GetQueueData(null, QueueType);
            string partialName = null;
            ViewBag.QueueType = QueueType;
            switch (QueueType)
            {
                case "SystemIdentifiedQueueTabs":
                    partialName = "~/Areas/CM/Views/Queue/_SystemIdentifiedQueue.cshtml";
                    break;
                case "UserQueueTabs":
                    partialName = "~/Areas/CM/Views/Queue/_UserQueue.cshtml";
                    break;
            }

            return PartialView(partialName, QueueModel);
           

        }
        // ----------This method is to get the Table body reloaded again in user and system identified queue----------
        public ActionResult GetUserQueueData(string QueueType)
        {
            string partialName = "";
            QueueModel = queueServices.GetQueueData(null, QueueType);
            switch (QueueType)
            {
                case "UserQueueTabs":
                    partialName = "~/Areas/CM/Views/Queue/_UserQueueBody.cshtml";
                    break;
                case "SystemIdentifiedQueueTabs":
                     partialName = "~/Areas/CM/Views/Queue/_SystemIdentifiedQueueBody.cshtml";
                     break;
            }   
            return PartialView(partialName,QueueModel);

        }
       
	}
}