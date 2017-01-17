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
using AHC.CD.WebUI.MVC.Models;


namespace AHC.CD.WebUI.MVC.Areas.PlanPDF.Controllers
{
    public class PDFMappingController : Controller
    {

        private IPdfMappingManager PDFManager = null;
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

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public PDFMappingController(IPdfMappingManager PDFManager, IErrorLogger errorLogger, IApplicationManager applicationManager)
        {
            this.PDFManager = PDFManager;
            this.errorLogger = errorLogger;
            this.applicationManager = applicationManager;

        }

        

        public ActionResult Index()
        {
            return View();
        }

        //Get profile data based on id
        public async Task<string> GetPDFMappingProfileData(int profileId, string templateName)
        {
            try
            {
                string UserAuthId = await GetUserAuthId();
                string pdfFileName = PDFManager.GeneratePlanFormPDF(profileId, templateName, UserAuthId);
                
                return JsonConvert.SerializeObject(pdfFileName);
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        #region Private Methods

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }


        #endregion


    }
}
