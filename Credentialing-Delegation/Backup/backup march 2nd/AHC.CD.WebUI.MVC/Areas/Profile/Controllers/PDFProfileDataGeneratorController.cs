using AHC.CD.Business.Profiles;
using AHC.CD.ErrorLogging;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class PDFProfileDataGeneratorController : Controller
    {
        private IPDFProfileDataGeneratorManager PDFManager = null;
        private IErrorLogger errorLogger = null;

        

        public PDFProfileDataGeneratorController(IPDFProfileDataGeneratorManager PDFManager, IErrorLogger errorLogger)
        {
            this.PDFManager = PDFManager;
            this.errorLogger = errorLogger;
        }

        // GET: Profile/PDFProfileDataGenerator
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetProfileData(int profileId)
        {
            var status = "true";
            var path = "";
            
            try
            {

                path = await this.PDFManager.GetProfileDataByIdAsync(profileId); 
                
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = ExceptionMessage.PROFILE_PDF_CREATION_EXCEPTION;
            }

            return Json(new { status = status, path = path }, JsonRequestBehavior.AllowGet);
            
        }
    }
}