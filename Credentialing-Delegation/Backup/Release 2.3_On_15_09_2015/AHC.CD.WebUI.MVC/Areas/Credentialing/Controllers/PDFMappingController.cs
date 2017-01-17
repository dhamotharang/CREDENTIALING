using AHC.CD.Business.Profiles;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.BusinessModels.PDFGenerator;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterData.Enums;
using System.Configuration;
using System.Xml;
using System.ComponentModel;
using iTextSharp.text.pdf;
using System.IO;
using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.Business.PdfGeneration;
using Newtonsoft.Json;
using AHC.CD.WebUI.MVC.Areas.Profile.Controllers;
using AHC.CD.Business;
using AHC.CD.Business.Notification;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.DocumentRepository;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Exceptions;
using AHC.CD.WebUI.MVC.Models;


namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class PDFMappingController : Controller
    {

        private IPdfMappingManager PDFManager = null;

        private IProfileManager profileManager = null;
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;

        //private IPDFProfileDataGeneratorManager PDFManager = null;
        private IErrorLogger errorLogger = null;
        protected ApplicationUserManager _authUserManager;
        private IApplicationManager applicationManager = null;


        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }
        public PDFMappingController(IProfileManager profileManager,IChangeNotificationManager notificationManager,IProfileUpdateManager profileUpdateManager,IPdfMappingManager PDFManager, IErrorLogger errorLogger, IApplicationManager applicationManager)
        {
            this.profileManager = profileManager;
            this.notificationManager = notificationManager;
            this.profileUpdateManager = profileUpdateManager;

            this.PDFManager = PDFManager;
            this.errorLogger = errorLogger;
            this.applicationManager = applicationManager;

        }

        

        public ActionResult Index()
        {
            return View();
        }

        //Get profile data based on id
        public string GetPDFMappingProfileData(int profileId,string templateName)
        {
            try
            {
                string pdfFileName = PDFManager.GeneratePlanFormPDF(profileId, templateName);
                DocumentRepositoryController documentObj = new DocumentRepositoryController(profileManager, errorLogger, notificationManager, profileUpdateManager);
                OtherDocumentViewModel otherDocument = new OtherDocumentViewModel();
                otherDocument.Title = templateName;                
                
                otherDocument.IsPrivate = false;
                otherDocument.DocumentPath ="/ApplicationDocument/GeneratedTemplatePdf/"+ pdfFileName;
                               
                return JsonConvert.SerializeObject(pdfFileName);
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public string GenerateBulkPlanForm(List<int> profileIds, List<string> templateNames)
        {
            try
            {
                List<PlanFormGenerationBusinessModel> bulkForm = PDFManager.GenerateBulkForm(profileIds, templateNames);
                return JsonConvert.SerializeObject(bulkForm);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string GeneratePackage(int profileId,List<string> pdflist)
        {
            string pdfFileName = "";
            try
            {
                pdfFileName = PDFManager.CombinePdfs(profileId,pdflist);
                return JsonConvert.SerializeObject(pdfFileName);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GenerateBulkPackage(List<int> profileId, List<string> pdflist)
        {
            List<PackageGeneratorBusinessModel> bulkPackage = null;
            try
            {
                bulkPackage = PDFManager.GenerateBulkPackage(profileId, pdflist);
                return JsonConvert.SerializeObject(bulkPackage);
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
