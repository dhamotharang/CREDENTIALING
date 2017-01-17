using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHC.CD.Business.Profiles;
using SelectPdf;
using AHC.UtilityService;
using AHC.CD.Business.Email;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfileReportController : Controller
    {
        private IProfileReportManager ProfileReportManager;
        
        public ProfileReportController(IProfileReportManager reportManager)
        {
            this.ProfileReportManager = reportManager;            
        }
        //
        // GET: /Profile/ProfileReport/
        public ActionResult Index()
        {
            return View("profileReport");
        }

        public JsonResult LoadProfileReport() {  
           
            return Json(ProfileReportManager.GetProfileReport(), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveProfileReport(string pdfhtml)
        {

            string status = "true";
            string pdfpath = "";

            try
            {
                string baseUrl = GetBaseUrl();

                HtmlToPdf converter = new HtmlToPdf();
                string pdf_page_size = "A4";
                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                  pdf_page_size, true);

                string pdf_orientation = "Portrait";
                PdfPageOrientation pdfOrientation =
                    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                    pdf_orientation, true);

                int webPageWidth = 1024;
                try
                {
                    webPageWidth = Convert.ToInt32(webPageWidth);
                }
                catch { }

                int webPageHeight = 0;
                try
                {
                    webPageHeight = Convert.ToInt32(webPageHeight);
                }
                catch { }
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfOrientation; 
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                converter.Options.DisplayFooter = true;
                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
                converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;
                converter.Options.ExternalLinksEnabled = true;
                converter.Options.KeepImagesTogether = true;

                converter.Options.MarginTop = 10;
                converter.Options.MarginBottom = 10;
                converter.Options.MarginLeft = 25;
                converter.Options.MarginRight = 10;
               
                byte[] pdfbytes = null;


                PdfDocument doc = null;
                if (baseUrl != null)
                    doc = converter.ConvertHtmlString(pdfhtml, baseUrl);

                else
                    doc = converter.ConvertHtmlString(pdfhtml);



                //doc = converter.ConvertUrl("http://localhost:26598/profile/profilereport");
                string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
                string fileName = uniqueKey + "-" + "Profile.pdf";
                // save pdf document
                pdfbytes = doc.Save();

                pdfpath = ProfileReportManager.SaveProfileReportPDFFile(pdfbytes);




                return Json(new { status = status, pdfPath = pdfpath }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetBaseUrl()
        {
            var request = HttpContext.ApplicationInstance.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;


            if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";


            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            return baseUrl;
        }
	}
}