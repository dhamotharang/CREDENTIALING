using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AHC.CD.Business.Profiles;
using AHC.CD.Business;
using AHC.CD.Entities.MasterProfile;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DocumentationCheckListController : Controller
    {

        private IProfileManager profileManager = null; 

        public DocumentationCheckListController(IProfileManager profileManager)
        {
            this.profileManager = profileManager;
        }

        //
        // GET: /Profile/DocumentationCheckList/
        public ActionResult ViewDocumentationCheckList()
        {
            return View();
        }

        public string GetAllProfileDocuments(int ProfileId)
        {
            try
            {
                var documents = profileManager.GetAllProfileDocuments(ProfileId);
                return JsonConvert.SerializeObject(documents);
            }
            catch (Exception)
            {
                throw;
            }

            //return Json(documents, JsonRequestBehavior.AllowGet);
        }
        public string GetProfileDocumentConfig(int ProfileId)
        {
            try
            {
                var config = profileManager.GetProfileDocumentConfig(ProfileId);
                return JsonConvert.SerializeObject(config);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string SaveProfileDocumentConfig(int ProfileId,ProfileDocumentConfig documentConfig)
        {
            try
            {
                var config = profileManager.SaveProfileDocumentConfig(ProfileId,documentConfig);
                return JsonConvert.SerializeObject(config);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}