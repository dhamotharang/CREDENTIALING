using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.CredAxis.Models.ProfessionalLiabilityViewModel;
using PortalTemplate.Areas.CredAxis.Models.ProfessionalAffiliationViewModel;
namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class ProfessionalAffiliationController : Controller
    {
         private readonly IProfessionalAffiliationService _ProfessionlAffiliation = null;

         public ProfessionalAffiliationController()
        {
            _ProfessionlAffiliation = new ProfessionalAffiliationService();
        }

        //
        // GET: /CredAxis/Demographics/
         public ActionResult Index(string ModeRequested)
        {
            //List<ProfessionalAffiliationViewModel> theModel = new List<ProfessionalAffiliationViewModel>();

            List<ProfessionalAffiliationViewModel>  theModel = _ProfessionlAffiliation.GetAllProfessionalAffiliationCode();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_AddEditProfessionalAffiliation.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_ViewProfessionalAffiliation.cshtml", theModel);
        }

        /// <summary>
        /// Method to return Add Partial for Professional Affiliation
        /// </summary>
        /// <param name="professionalAffiliationMainModel"></param>
        /// <returns></returns>
        public ActionResult GetProfessionalAffiliationPartial(ProfessionalAffiliationViewModel emptydata,string value)
        {
            var Action1 = "Add";
            List<ProfessionalAffiliationViewModel> ProfessionalAffiliationEmptyModel = new List<ProfessionalAffiliationViewModel>();
            if (value == Action1)
            {
                ProfessionalAffiliationEmptyModel.Add(emptydata);
            }
           else
            {
                ProfessionalAffiliationEmptyModel = _ProfessionlAffiliation.GetAllProfessionalAffiliationCode();
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_AddEditProfessionalAffiliation.cshtml", ProfessionalAffiliationEmptyModel);
        }


        [HttpPost]
        public JsonResult AddEditProfessionalAffiliationCode(ProfessionalAffiliationMainModel professionalAffiliationMainModel)
        {
            if (ModelState.IsValid)
            {
                var AddEditProfessionalAffiliation = _ProfessionlAffiliation.AddEditProfessionalAffiliationCode(professionalAffiliationMainModel);
                return Json(new { Result = AddEditProfessionalAffiliation, status = "done" });
            }

            return Json(new { status = "false" });
        }
        [HttpPost]
        public ActionResult AddAffiliation(ProfessionalAffiliationViewModel Addaffiliatedata)
        {

            //List<ProfessionalAffiliationViewModel> Affilationdata = new List<ProfessionalAffiliationViewModel>();
            List<ProfessionalAffiliationViewModel>  Affilationdata = _ProfessionlAffiliation.GetAllProfessionalAffiliationCode();
           // Affilationdata.Add(Addaffiliatedata);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_ViewProfessionalAffiliation.cshtml", Affilationdata);
        }


        public ActionResult ViewAffiliationHistory(string value)
        {
            List<ProfessionalAffiliationViewModel> Affilationdata = _ProfessionlAffiliation.GetAllProfessionalAffiliationCode().Where(i=>i.ProfessionalAffiliationID==value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_ProfessionalAffiliationHistory.cshtml", Affilationdata);
        }

        public ActionResult ViewAffiliationHistoryforAll()
        {
            List<ProfessionalAffiliationViewModel> Affilationdata = _ProfessionlAffiliation.GetAllProfessionalAffiliationCode();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_ProfessionalAffiliationHistory.cshtml", Affilationdata);
        }
        public ActionResult removeaffiliationinfo(string value)
        {
            List<ProfessionalAffiliationViewModel> Affilationdata = _ProfessionlAffiliation.GetAllProfessionalAffiliationCode().Where(i => i.ProfessionalAffiliationID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_ViewProfessionalAffiliation.cshtml", Affilationdata);
        }

    }
}