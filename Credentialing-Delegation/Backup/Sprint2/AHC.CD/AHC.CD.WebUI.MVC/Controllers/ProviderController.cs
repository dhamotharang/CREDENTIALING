using AHC.CachingService;
using AHC.CD.Business;
using AHC.CD.Business.DTO;
using AHC.CD.Data.EFRepository;
using AHC.CD.Entities.ProfileDemographicInfo;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Search;
using AHC.CD.WebUI.MVC.Models.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
   
    public class ProviderController : Controller
    {
        // GET: Provider
        IProvidersManager providersManager = null;
        IDocumentsManager documentManager = null;
        ICachingService cache = null;
        public ProviderController(IProvidersManager providersManager,IDocumentsManager documentManager, ICachingService cache)
        {
            this.providersManager = providersManager;
            this.documentManager = documentManager;
            this.cache = cache;
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProvider() 
        {
            return View();
        }


        public ActionResult Get()
        {
            providersManager.GetAllIndividualProviders();
            return View(providersManager.GetAllIndividualProviders());
        }

        public JsonResult SaveProvider(ProviderAddViewModel ProviderAddViewModel)
        {
            string message;

            int providerID = 0;
            try
            {
                Individual individual = ProviderTransformer.TransformToIndividual(ProviderAddViewModel);
                //cache.Set<Individual>(User.Identity.Name, individual);
                providerID = providersManager.SaveIndividualProvider(individual);

                message = CompletionMessages.PROVIDER_SAVED_SUCCESSFULLY;
            }
            catch (Exception)
            {
                message = CompletionMessages.PROVIDER_SAVED_UNSUCCESSFULLY;

            }

            return Json(new { ProviderID = providerID, Message = message }, JsonRequestBehavior.AllowGet);


        }


        //[HttpPost]
        //public async Task<ActionResult> CreateProvider(object providerAddViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
 
        //        // Convert ProviderAddViewModel to Individual domain model type
        //        Individual individualProvider = new Individual();
        //        await providersManager.SaveIndividualProviderAsync(individualProvider);
        //        // return the view after successfull provider creation
        //    }
        //    return View();
        //}

        public async Task<ActionResult> SearchProvider()
        {
            IEnumerable<ProviderCategory> categorieslist = await providersManager.GetAllCategoriesAsync();

            ViewBag.Category = categorieslist;
            ViewBag.ProviderStatus = Enum.GetValues(typeof(ProviderStatus)).Cast<ProviderStatus>();
            
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SearchProviderJson(SearchProviderFilterVM searchFilter)
        {
            IEnumerable<Individual> providersList = await providersManager.GetAllIndividualProvidersByAsync(searchFilter.CategoryID, searchFilter.ProviderStatus);
            // convert domain model into view model and convert to json
            var resultList = SearchIndividualTransformer.TransformToProviderSearchVM(providersList);
            return Json(resultList);
        }

        public async Task<ActionResult> GetCategoriesData()
        {
            IEnumerable<ProviderCategory> categoriesList = await providersManager.GetAllCategoriesAsync();


            return Json(ProviderTransformer.TransformProviderCategory(categoriesList), JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> GetGroupData()
        {
            IEnumerable<Group> groupList = await providersManager.GetAllGroupsAsync();


            return Json(ProviderTransformer.TransformGroup(groupList), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public bool IsEmailExist(string email)
        {
            bool status = providersManager.IsEmailExists(email);

            return status;

        }

        [HttpPost]
        public bool IsPhoneNoExist(ContactInfo contactInfo)
        {
            bool status = providersManager.IsPhoneNoExists(contactInfo.CountryCode,contactInfo.PhoneNo);

            return status;

        }

        
        [HttpPost]
        public ActionResult SaveProviderDoc(HttpPostedFileBase file, int providerID)
        {
            string message = null;
            try
            {
                DocumentDTO documentDTO = new DocumentDTO 
                {
                    InputStream = file.InputStream, 
                    ApplicationRootFolder = Server.MapPath("~"), 
                    ProviderID = providerID, 
                    DocumentTypeID = 3, 
                    FileName = file.FileName 
                };
                documentManager.SaveOrUpdateProviderDocument(documentDTO);
                message = CompletionMessages.CV_UPLOADED_SUCCESSFULLY;

            }catch(Exception ){
                message = ExceptionMessage.CV_UPLOADED_EXCEPTION;
            }


            ViewBag.Message = message;

            return View();

        }

        public string init()
        {
            DBInitializer.Seed();
            return "DB initialized";
        }
   
    }
}