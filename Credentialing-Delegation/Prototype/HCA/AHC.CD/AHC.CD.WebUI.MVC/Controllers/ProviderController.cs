using AHC.CachingService;
using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;

using AHC.CD.Data.EFRepository;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Search;
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
        public ProviderController()
        {
           
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProvider() 
        {
            return View();
        }


      //  public ActionResult Get()
      //  {
      //      //providersManager.GetAllIndividualProviders();
      //      //return View(providersManager.GetAllIndividualProviders());
      //      return null;
      //  }

      //  public JsonResult SaveProvider(ProviderAddViewModel ProviderAddViewModel)
      //  {
      //      string message;

      //      int providerID = 0;
      //      try
      //      {
      //          //Individual individual = ProviderTransformer.TransformToIndividual(ProviderAddViewModel);
      //          ////cache.Set<Individual>(User.Identity.Name, individual);
      //          //providerID = providersManager.SaveIndividualProvider(individual);

      //          message = CompletionMessages.PROVIDER_SAVED_SUCCESSFULLY;
      //      }
      //      catch (Exception)
      //      {
      //          message = CompletionMessages.PROVIDER_SAVED_UNSUCCESSFULLY;

      //      }

      //      return Json(new { ProviderID = providerID, Message = message }, JsonRequestBehavior.AllowGet);


      //  }


      //  //[HttpPost]
      //  //public async Task<ActionResult> CreateProvider(object providerAddViewModel)
      //  //{
      //  //    if (ModelState.IsValid)
      //  //    {
 
      //  //        // Convert ProviderAddViewModel to Individual domain model type
      //  //        Individual individualProvider = new Individual();
      //  //        await providersManager.SaveIndividualProviderAsync(individualProvider);
      //  //        // return the view after successfull provider creation
      //  //    }
      //  //    return View();
      //  //}

        public async Task<ActionResult> SearchProvider()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SearchProviderJson(SearchProviderFilterVM searchFilter)
        {            
            return Json(200);
        }

      //  //public async Task<ActionResult> GetCategoriesData()
      //  //{
      //  //    IEnumerable<ProviderCategory> categoriesList = await providersManager.GetAllCategoriesAsync();


      //  //    return Json(ProviderTransformer.TransformProviderCategory(categoriesList), JsonRequestBehavior.AllowGet);

      //  //}

      //  //public async Task<ActionResult> GetGroupData()
      //  //{
      //  //    IEnumerable<Group> groupList = await providersManager.GetAllGroupsAsync();


      //  //    return Json(ProviderTransformer.TransformGroup(groupList), JsonRequestBehavior.AllowGet);

      //  //}

      //  [HttpPost]
      //  public bool IsEmailExist(string email)
      //  {
      //      bool status = providersManager.IsEmailExists(email);

      //      return status;

      //  }

      ////  [HttpPost]
      //  //public bool IsPhoneNoExist(ContactInfo contactInfo)
      //  //{
      //  //    bool status = providersManager.IsPhoneNoExists(contactInfo.CountryCode,contactInfo.PhoneNo);

      //  //    return status;

      //  //}

        
      //  [HttpPost]
      //  public ActionResult SaveProviderDoc(HttpPostedFileBase file, int providerID)
      //  {
      //      string message = null;
      //      try
      //      {
      //          DocumentDTO documentDTO = new DocumentDTO 
      //          {
      //              InputStream = file.InputStream, 
      //              ApplicationRootFolder = Server.MapPath("~"), 
      //              ProviderID = providerID, 
      //              DocumentTypeID = 3, 
      //              FileName = file.FileName 
      //          };
      //          documentManager.SaveOrUpdateProviderDocument(documentDTO);
      //          message = CompletionMessages.CV_UPLOADED_SUCCESSFULLY;

      //      }catch(Exception ){
      //          message = ExceptionMessage.CV_UPLOADED_EXCEPTION;
      //      }


      //      ViewBag.Message = message;

      //      return View();

      //  }

      //  public string init()
      //  {
      //      DBInitializer.Seed();
      //      return "DB initialized";
      //  }
   
    }
}