using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.PrimarySourceVerificationController
{
    public class PSVController : Controller
    {
        //
        // GET: /CredAxis/PSV/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPSVStateLicense()
        {
            return PartialView("~/Areas/CredAxis/Views/PrimarySourceVerification/PSVTabs/_PSVTabIndex.cshtml");
        }

        public ActionResult GetViewStateLicenseVerification()
        {
            return PartialView("~/Areas/CredAxis/Views/PrimarySourceVerification/StateLicense/StateLicenseVerification/_ViewStateLicensesVerification.cshtml");
        }

        public ActionResult GetEditStateLicenseVerification()
        {
            return PartialView("~/Areas/CredAxis/Views/PrimarySourceVerification/StateLicense/StateLicenseVerification/_EditStateLicenseVerification.cshtml");
        }

        public ActionResult GetViewBoardCertificateVerification()
        {
            return PartialView("~/Areas/CredAxis/Views/PrimarySourceVerification/BoardCertificate/BoardCertificateVerification/_ViewBoardCertificate.cshtml");
        }

        public ActionResult GetEditBoardCertificateVerification()
        {
            return PartialView("~/Areas/CredAxis/Views/PrimarySourceVerification/BoardCertificate/BoardCertificateVerification/_EditBoardCertificate.cshtml");
        }
	}
}