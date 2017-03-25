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
using AHC.CD.Entities.Forms;
using AHC.CD.Business.PlanForms;



namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class PDFMappingController : Controller
    {

        private IPdfMappingManager PDFManager = null;

        public IPlanFormManager planFormManager = null;

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

        public PDFMappingController(IProfileManager profileManager, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager, IPdfMappingManager PDFManager, IErrorLogger errorLogger, IApplicationManager applicationManager, IPlanFormManager planFormManager)
        {
            this.profileManager = profileManager;
            this.notificationManager = notificationManager;
            this.profileUpdateManager = profileUpdateManager;

            this.PDFManager = PDFManager;
            this.errorLogger = errorLogger;
            this.applicationManager = applicationManager;
            this.planFormManager = planFormManager;
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
                string pdfFileName = await PDFManager.GenerateGenericPlanFormPDF(profileId, templateName, UserAuthId);
                //DocumentRepositoryController documentObj = new DocumentRepositoryController(profileManager, errorLogger, notificationManager, profileUpdateManager);
                //OtherDocumentViewModel otherDocument = new OtherDocumentViewModel();
                //otherDocument.Title = templateName;                
                
                //otherDocument.IsPrivate = false;
                //otherDocument.DocumentPath ="/ApplicationDocument/GeneratedTemplatePdf/"+ pdfFileName;
                               
                return JsonConvert.SerializeObject(pdfFileName);
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public async Task<string> GetOldPDFMappingProfileData(int profileId, string templateName)
        {
            try
            {
                string UserAuthId = await GetUserAuthId();
                string pdfFileName = await PDFManager.GeneratePlanFormPDF(profileId, templateName, UserAuthId);
                return JsonConvert.SerializeObject(pdfFileName);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<string> GeneratePlanForm(List<int> profileIds, List<string> templateNames)
        {
            try
            {
                string UserAuthId = await GetUserAuthId();
                List<string> BulkyForms = new List<string>();
                List<PlanFormGenerationBusinessModel> bulkForm1 = new List<PlanFormGenerationBusinessModel>();
                List<PlanFormGenerationBusinessModel> bulkForm2 = new List<PlanFormGenerationBusinessModel>();
                foreach (var item in templateNames)
                {
                    if (item == "North_Carolina_Coventry_Uniform_Credentialing_Application" || item == "Tricare_Prime_Credentialing_Application" || item == "ALLIED_CREDENTIALING_APPLICATION_ACCESS2" || item == "ALLIED_CREDENTIALING_APPLICATION_ACCESS")
                    {
                        BulkyForms.Add(item);
                        //templateNames.Remove(item);
                    }
                }
                BulkyForms.ForEach(x=>templateNames.Remove(x));
                if(BulkyForms.Count > 0)
                {
                    bulkForm1 = await PDFManager.GenerateBulkForm(profileIds, BulkyForms, UserAuthId);
                }
                bulkForm2 =await PDFManager.GenerateFormsUsingADO(profileIds, templateNames, UserAuthId);

                bulkForm1.AddRange(bulkForm2);
                List<object> Result = new List<object>();
               // var res= bulkForm1.GroupBy(x => x.ProfileID, x => x.GeneratedFilePaths, (key, g) => new { GeneratedFilePaths = g, ProfileID = key });
               // var res = (from result in bulkForm1 group result by result.ProfileID into x select new { ProfileID = x.Key, Form = x.GroupBy(y => y.ProfileID,y=>y.GeneratedFilePaths,(key,g) => new {Formpath=g.ToList(),ProfileID=key})}).ToList();
                var res = (from result in bulkForm1 group result by result.ProfileID into x select new { ProfileID = x.Key, Form = x.Select(y => y.GeneratedFilePaths.ToList())}).ToList();
                foreach(var d in res)
                {
                      List<string> forms = new List<string>();
                    foreach(var c in  d.Form)
                    {
                        forms.AddRange(c);
                    }
                    var result = new { ProfileID = d.ProfileID, forms = forms };
                    Result.Add(result);
                }
                return JsonConvert.SerializeObject(Result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> GenerateBulkPlanForm(List<int> profileIds, List<string> templateNames)
        {
            try
            {
                string UserAuthId = await GetUserAuthId();
                List<PlanFormGenerationBusinessModel> bulkForm = await PDFManager.GenerateBulkForm(profileIds, templateNames, UserAuthId);
                return JsonConvert.SerializeObject(bulkForm);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> GeneratePackage(int profileId, List<string> pdflist)
        {
            string pdfFileName = "";
            try
            {
                var user = await GetUser();
                pdfFileName = PDFManager.CombinePdfs(profileId, pdflist, user.Id, user.FullName);
                return JsonConvert.SerializeObject(pdfFileName);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<string> GenerateBulkPackage(List<int> profileId, List<string> pdflist)
        {
            List<PackageGeneratorBusinessModel> bulkPackage = null;
            var user = await GetUser();
            try
            {
                bulkPackage = PDFManager.GenerateBulkPackage(profileId, pdflist, user.Id, user.FullName);
                return JsonConvert.SerializeObject(bulkPackage);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<string> GetAllPlanFormData()
        {

            List<PlanFormDetail> bulkPlanFormData = null;

            try
            {
                bulkPlanFormData = await planFormManager.GetAllPlanFormData();
                return JsonConvert.SerializeObject(bulkPlanFormData);
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

        private async Task<ApplicationUser> GetUser()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user;
        }

        
        #endregion
    }
}
