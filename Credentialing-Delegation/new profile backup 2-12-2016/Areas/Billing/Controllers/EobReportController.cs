using PortalTemplate.Areas.Billing.Models.EOB_Report;
using PortalTemplate.Areas.Billing.Services;
using PortalTemplate.Areas.Billing.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Controllers
{
    public class EobReportController : Controller
    {
        readonly IEOBReportService _EOBReportService;

        public EobReportController()
        {
            _EOBReportService = new EOBReportService();
        }
        //
        // GET: /EOBReport/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEobReportOfRenderingProviderByDos()
        {
            return PartialView("~/Areas/Billing/Views/EOBReport/_EOBReportRenderingProviderByDateOfService.cshtml", _EOBReportService.GetEobReportOfRenderingProviderByDos());
        }

        public ActionResult GetEobReportOfRenderingProviderByPaymentDate()
        {
            return PartialView("~/Areas/Billing/Views/EOBReport/_EOBReportRenderingProviderByPaymentDate.cshtml", _EOBReportService.GetEobReportOfRenderingProviderByPaymentDate());
        }

        public ActionResult GetEobReportOfServiceLocationByDos()
        {
            return PartialView("~/Areas/Billing/Views/EOBReport/_EOBReportServiceLocationByDateOfService.cshtml", _EOBReportService.GetEobReportOfServiceLocationByDos());
        }

        public ActionResult GetEobReportOfServiceLocationByPaymentDate()
        {
            return PartialView("~/Areas/Billing/Views/EOBReport/_EOBReportServiceLocationByPaymentDate.cshtml", _EOBReportService.GetEobReportOfServiceLocationByPaymentDate());
        }



        public ActionResult GetEobReport()
        {
            return PartialView("~/Areas/Billing/Views/EOBReport/Index.cshtml");
        }

        public ActionResult GetPayerToProviderReport()
        {
            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Index.cshtml");
        }

        public ActionResult GetProviderToPayerReport()
        {
            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Index.cshtml");
        }
    }
}