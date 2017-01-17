using PortalTemplate.Areas.UM.Models.ViewModels.History;
using PortalTemplate.Areas.UM.Services;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class HistoryController : Controller
    {
        HistoryServices service = new HistoryServices();
        List<MemberHistoriesViewModel> HistoryModel = new List<MemberHistoriesViewModel>();
        public ActionResult GetHistoryListData()
        {
            HistoryModel = service.GetUMHistory("");
            return PartialView("~/Areas/UM/Views/History/UMHistory/HistoryList/_UMAuthHistory.cshtml", HistoryModel);
        }
    }
}