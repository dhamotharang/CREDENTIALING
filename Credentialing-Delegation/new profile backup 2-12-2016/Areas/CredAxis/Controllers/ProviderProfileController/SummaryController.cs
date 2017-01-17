using PortalTemplate.Areas.CredAxis.Models.SummaryViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.CredAxis.Models.SummaryViewModel;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class SummaryController : Controller
    {
        private ISummaryService _SummaryCode = null;
        public SummaryController()
        {
            _SummaryCode = new SummaryService();
        }

        SummaryMainViewModel theModel = new SummaryMainViewModel();
        //
        // GET: /CredAxis/Summary/
        public ActionResult Index()
        {
            theModel = _SummaryCode.GetSummary();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/OtherTabs/_summary.cshtml", theModel);

            //return View();
        }
        public ActionResult GetData() {
            return Json(_SummaryCode.GetSummary(), JsonRequestBehavior.AllowGet);
        }
    }
}