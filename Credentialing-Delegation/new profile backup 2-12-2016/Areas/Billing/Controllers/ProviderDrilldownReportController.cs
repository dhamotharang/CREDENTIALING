using PortalTemplate.Areas.Billing.Models.PayerDrilldownReport;
using PortalTemplate.Areas.Billing.Services;
using PortalTemplate.Areas.Billing.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Controllers
{
    public class ProviderDrilldownReportController : Controller
    {
        readonly IProviderDrilldownReportService _ProviderDrilldownReportService;
        public ProviderDrilldownReportController()
        {
            _ProviderDrilldownReportService = new ProviderDrilldownReportService();
        }
        //
        // GET: /ProviderDrilldownReport/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetPayerClaimsCountReport(int BillingProviderId, int RenderingProviderId)
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Count/_CPayer.cshtml", _ProviderDrilldownReportService.GetPayerClaimsCountReport(BillingProviderId, RenderingProviderId));
        }

        public ActionResult GetPayerClaimsAmountReport(int BillingProviderId, int RenderingProviderId)
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Amount/_APayer.cshtml", _ProviderDrilldownReportService.GetPayerClaimsAmountReport(BillingProviderId, RenderingProviderId));
        }

        public ActionResult GetMemberClaimsCountReport(int PayerId, int BillingProviderId, int RenderingProviderId)
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Count/_CMember.cshtml", _ProviderDrilldownReportService.GetMemberClaimsCountReport(PayerId, BillingProviderId, RenderingProviderId));
        }

        public ActionResult GetMemberClaimsAmountReport(int PayerId, int BillingProviderId, int RenderingProviderId)
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Amount/_AMember.cshtml", _ProviderDrilldownReportService.GetMemberClaimsAmountReport(PayerId, BillingProviderId, RenderingProviderId));
        }

        public ActionResult GetRenderingProviderCountReport()
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Count/_CRenderingProvider.cshtml", _ProviderDrilldownReportService.GetRenderingProviderCountReport());
        }

        public ActionResult GetRenderingProviderAmountReport()
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Amount/_ARenderingProvider.cshtml", _ProviderDrilldownReportService.GetRenderingProviderAmountReport());
        }


        public ActionResult GetBillingClaimsCountReport(int RenderingProviderId)
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Count/_CBillingProvider.cshtml", _ProviderDrilldownReportService.GetBillingClaimsCountReport(RenderingProviderId));
        }

        public ActionResult GetBillingClaimsAmountReport(int RenderingProviderId)
        {

            return PartialView("~/Areas/Billing/Views/ProviderDrilldownReport/Amount/_ABillingProvider.cshtml", _ProviderDrilldownReportService.GetBillingClaimsAmountReport(RenderingProviderId));
        }

    }
}