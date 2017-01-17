using PortalTemplate.Areas.UM.CustomHelpers;
using PortalTemplate.Areas.UM.Models.ViewModels.Queue;
using PortalTemplate.Areas.UM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class DynamicListController : Controller
    {
        QueueServices service = new QueueServices();
        List<QueueViewModel> QueueModel = new List<QueueViewModel>();
     
        public ActionResult GetDynamicQueue(string QueueType, string QueueTab)
        {
        //    QueueModel = service.GetQueueData(QueueType, QueueTab);
        //    ViewBag.QueueCount = service.GetCount(QueueModel);
        //    XMLReader readXML = new XMLReader();
        //    var data = readXML.GetQueueCols();
        //    ViewBag.ColData = data;
         return PartialView("~/Areas/UM/Views/Queue/Common/_DynamicQueueTable.cshtml", QueueModel);
        }
	}
}