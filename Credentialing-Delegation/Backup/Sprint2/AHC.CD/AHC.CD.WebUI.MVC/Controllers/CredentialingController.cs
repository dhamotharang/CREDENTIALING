using AHC.CD.Business;
using AHC.CD.WebUI.MVC.Models.Utility.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHC.CD.Entities.Credentialing;
using System.Threading.Tasks;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Credentialing;
using AHC.CD.Entities.Credentialing.DTO;

namespace AHC.CD.WebUI.MVC.Controllers
{
   
    public class CredentialingController : Controller
    {
        IIndividualCredentialingManager individualCredentialingManager=null;
        IProvidersManager providersManager = null;
        public CredentialingController(IIndividualCredentialingManager individualCredentialingManager, IProvidersManager providersManager)
        {
            this.individualCredentialingManager = individualCredentialingManager;
            this.providersManager = providersManager;
        }

        // GET: Credentialing
        public async Task<ActionResult> Index(int providerID)
        {
            var result = await providersManager.GetProviderByIdAsync(providerID);
            ViewBag.Provider = result; 
            return View();
        }


        public async Task<JsonResult> Plans(int providerID)
        {
            IEnumerable<Plan> plans = await individualCredentialingManager.GetAllNonInitiatedPlansForProviderAsync(providerID);
            return Json(PlanTransformer.TransformPlan(plans),JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ExistingPlans(int providerID)
        {
            IEnumerable<IndividualPlan> plans = await individualCredentialingManager.GetAllCredentialedIndividualPlansAsync(providerID);
            return Json(plans, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> InitCredentialing(InitiateCreadentialingVM initiateCreadentialing)
        {

            initiateCreadentialing.CredentialedBy = User.Identity.Name;
            await individualCredentialingManager.InitiateCredentialingAsync(PlanTransformer.TransformToCredentialDetailsDTO(initiateCreadentialing));
            return Json(null);
        }


        public ActionResult EncounterTracker()
        {
            return View();
        }

    }
}