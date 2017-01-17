using PortalTemplate.Areas.CredAxis.Models.PofessionalReferenceViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.PofessionalReferenceViewModel;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class ProfessionalReferencesController : Controller 
    {
         private IProfessionalReferencesService _ProfessionalReferencesCode = null;

         public ProfessionalReferencesController()
        {
            _ProfessionalReferencesCode = new ProfessionalReferenceService();
        }

         List<ProfessionalReferenceViewModel> theModel = new List<ProfessionalReferenceViewModel>();
        //
        // GET: /CredAxis/Demographics/
         public ActionResult Index(string ModeRequested)
        {
            theModel = _ProfessionalReferencesCode.GetAllProfessionalRef();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_AddEditProfessionalReferance.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_ViewProfessionalReference.cshtml", theModel);
        }
        /// <summary>
        /// Method to return Add Partial for Professional Reference
        /// </summary>
        /// <param name="professionalAffiliationMainModel"></param>
        /// <returns></returns>
        public ActionResult GetProfessionalReference(ProfessionalReferenceViewModel model, string ActionType)
        {
            var Action1 = "Edit"; var Action2 = "Add";
            if (ActionType == Action2)
            {
                theModel.Add(model);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_AddEditProfessionalReferance.cshtml", theModel);
            }
            theModel = _ProfessionalReferencesCode.GetAllProfessionalRef();
            if (ActionType == Action1)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_AddEditProfessionalReferance.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_ViewProfessionalReference.cshtml", theModel);
        }

        [HttpPost]
        public JsonResult AddEditProfessionalRef(ProfessionalReferenceViewModel professionalReferenceViewModel)
        {
            if (ModelState.IsValid)
            {
                var AddEditProfessionlReferences = _ProfessionalReferencesCode.AddEditProfessionalRef(professionalReferenceViewModel);
                return Json(new { Result = AddEditProfessionlReferences, status = "done" });
            }

            return Json(new { status = "false" });
        }


        public ActionResult ViewReferenceHistory(string value)
        {
               theModel = _ProfessionalReferencesCode.GetAllProfessionalRef().Where(i=>i.ProfessionalReferenceID==value).ToList();


               return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_ProfessionalReferenceHistory.cshtml", theModel);
        }
        public ActionResult RemoveRefrence(string value)
        {
            theModel = _ProfessionalReferencesCode.GetAllProfessionalRef().Where(i => i.ProfessionalReferenceID != value).ToList();


            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_ViewProfessionalReference.cshtml", theModel);
        }

    }
}