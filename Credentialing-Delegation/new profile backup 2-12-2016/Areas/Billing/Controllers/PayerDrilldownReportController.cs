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
    public class PayerDrilldownReportController : Controller
    {
        readonly IPayerDrilldownReportService _payerDrilldownReportService;
        public PayerDrilldownReportController()
        {
            _payerDrilldownReportService = new PayerDrilldownReportService();
        }
        //
        // GET: /PayerDrilldownReport/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetPayerClaimsCountReport()
        {
            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Count/_CPayer.cshtml", _payerDrilldownReportService.GetPayerClaimsCountReport());
        }

        public ActionResult GetPayerClaimsAmountReport()
        {
            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Amount/_APayer.cshtml", _payerDrilldownReportService.GetPayerClaimsAmountReport());
        }

        public ActionResult GetMemberClaimsCountReport(int PayerId, int BillingProviderId, int RenderingProviderId)
        {
            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Count/_CMember.cshtml", _payerDrilldownReportService.GetMemberClaimsCountReport(PayerId, BillingProviderId, RenderingProviderId));
        }

        public ActionResult GetMemberClaimsAmountReport(int PayerId, int BillingProviderId, int RenderingProviderId)
        {

            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Amount/_AMember.cshtml", _payerDrilldownReportService.GetMemberClaimsAmountReport(PayerId, BillingProviderId, RenderingProviderId));
        }

        public ActionResult GetRenderingProviderCountReport(int PayerId, int BillingProviderId)
        {
            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Count/_CRenderingProvider.cshtml", _payerDrilldownReportService.GetRenderingProviderCountReport(PayerId, BillingProviderId));
        }

        public ActionResult GetRenderingProviderAmountReport(int PayerId, int BillingProviderId)
        {
            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Amount/_ARenderingProvider.cshtml", _payerDrilldownReportService.GetRenderingProviderAmountReport(PayerId, BillingProviderId));
        }


        public ActionResult GetBillingClaimsCountReport(int PayerId)
        {
            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Count/_CBillingProvider.cshtml", _payerDrilldownReportService.GetBillingClaimsCountReport(PayerId));
        }

        public ActionResult GetBillingClaimsAmountReport(int PayerId)
        {

            return PartialView("~/Areas/Billing/Views/PayerDrilldownReport/Amount/_ABillingProvider.cshtml", _payerDrilldownReportService.GetBillingClaimsAmountReport(PayerId));
        }

    }
}