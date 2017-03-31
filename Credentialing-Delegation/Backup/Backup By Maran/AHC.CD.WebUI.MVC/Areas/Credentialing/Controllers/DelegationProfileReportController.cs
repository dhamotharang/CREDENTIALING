using AHC.CD.Business.BusinessModels.DelegationProfileReport;
using AHC.CD.Business.Credentialing.DelegationProfileReport;
using AHC.CD.Business.Email;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.DelegationProfileReport;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.DelegationProfileReport;
using AHC.UtilityService;
using Newtonsoft.Json;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
	public class DelegationProfileReportController : Controller
	{
		private IDelegationProfileReportManager reportManager = null;
        private IEmailServiceManager emailServiceManager = null;
        public DelegationProfileReportController(IDelegationProfileReportManager reportManager, IEmailServiceManager emailServiceManager)
		{
			this.reportManager = reportManager;
            this.emailServiceManager = emailServiceManager;
		}

		//
		// GET: /Credentialing/DelegationProfileReport/
		public ActionResult Index(int profileId)
		{

				return View();
			
		}
				
		[HttpPost]
		public async Task<JsonResult> GetProviderProfile(int profileId, List<ProviderPracitceInfoBusinessModel> Locations, List<ProviderProfessionalDetailBusinessModel> Specialtis)
		{
			string status = "true";
			ProviderGeneralInfoBussinessModel providerProfile = new ProviderGeneralInfoBussinessModel();
			List<ProviderPracitceInfoBusinessModel> locations = Locations;
			List<ProviderProfessionalDetailBusinessModel> specialtis = Specialtis;
			try
			{


				providerProfile = await reportManager.GetProfileDataByIdAsync(profileId, locations, specialtis);
				var profileReport = providerProfile;
				return Json(new { status = status, profileReport = profileReport }, JsonRequestBehavior.AllowGet);
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

        [ValidateInput(false)]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SaveDocumentChecklist(DocumentCheckListPDFViewModel checklist)
        {
           
            string status = "true";
            string checkListPdfpath="";
           
            try
            {
               string baseUrl = GetBaseUrl();

               HtmlToPdf converter = new HtmlToPdf();
              string pdf_page_size = checklist.PageSize;
             PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
               pdf_page_size, true);

           string pdf_orientation = checklist.PageOrientation;
           PdfPageOrientation pdfOrientation =
               (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
               pdf_orientation, true);

           int webPageWidth = checklist.PageWidth;
           try
           {
               webPageWidth = Convert.ToInt32(webPageWidth);
           }
           catch { }

           int webPageHeight = checklist.PageHeight;
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
           converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
           converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
           converter.Options.ExternalLinksEnabled = true;
           converter.Options.KeepImagesTogether = true;
           
           byte[] pdfbytes = null;
          

               PdfDocument doc=null;
               if(baseUrl!=null)
                doc  = converter.ConvertHtmlString(checklist.PdfText, baseUrl);
               else
                doc = converter.ConvertHtmlString(checklist.PdfText);

              
              
               string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
               string fileName = uniqueKey + "-" + "Plan.pdf";
               // save pdf document
               pdfbytes=doc.Save();

               checkListPdfpath = emailServiceManager.SaveDocumentChecklistPDFFile(pdfbytes, checklist.ProfileID);
                    
                


                return Json(new { status = status,pdfPath=checkListPdfpath }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }
        }



        [ValidateInput(false)]
		[HttpPost]
		public ActionResult SaveDelegationProfileReport(int requestId, string reportString,string pdfhtmlText=null)
		{
			ProfileReport profileReport = null;
			string status = "true";
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = null;
            byte[] pdfbytes = null;
			try
			{

                if (pdfhtmlText != null)
                {
                    doc = converter.ConvertHtmlString(pdfhtmlText, null);
                    string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
                    string fileName = uniqueKey + "-" + "Plan.pdf";
                    pdfbytes=doc.Save();
                    string checkListPdfpath = emailServiceManager.SaveDelegatedPlanPDFFile(pdfbytes, requestId);
                }
                ProfileReportViewModel report = JsonConvert.DeserializeObject<ProfileReportViewModel>(reportString);
				profileReport = AutoMapper.Mapper.Map<ProfileReportViewModel, ProfileReport>(report);

				profileReport.ProfileReportId = reportManager.SaveDelegationProfileReport(requestId, profileReport);

				return Json(new { status = status, profileReport = profileReport }, JsonRequestBehavior.AllowGet);                
				
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<JsonResult> GetDelegationProfileReport(int CredContractRequestId)
		{
			List<ProfileReport> pReports = new List<ProfileReport>();
			List<ProfileReportViewModel> profileReports = new List<ProfileReportViewModel>();
			string status = "true";
			try
			{
				ProfileReportViewModel profileReport = null;
				pReports = await reportManager.GetDelegationProfileReport(CredContractRequestId);

				foreach (ProfileReport item in pReports)
				{

					profileReport = AutoMapper.Mapper.Map<ProfileReport, ProfileReportViewModel>(item);
					profileReports.Add(profileReport);
				}

				return Json(new { status = status, profileReports = profileReports }, JsonRequestBehavior.AllowGet);                

			}
			catch (Exception)
			{
				
				throw;
			}

		}

		//public async Task<ActionResult> GetProfileDataByIdAsync(int profileId, List<ProviderPracitceInfoBusinessModel> locations, List<ProviderProfessionalDetailBusinessModel> specialtis)
		//{
		//    var status = "true";
		//    ProviderGeneralInfoBussinessModel providerProfile = new ProviderGeneralInfoBussinessModel();
		//    try
		//    {

		//        providerProfile = await reportManager.GetProfileDataByIdAsync(profileId, locations, specialtis);

		//    }
		//    catch (Exception ex)
		//    {
		//        status = ex.Message;
		//    }

		//    return Json(new { status = status, providerProfile = providerProfile }, JsonRequestBehavior.AllowGet);

		//}
	}
}