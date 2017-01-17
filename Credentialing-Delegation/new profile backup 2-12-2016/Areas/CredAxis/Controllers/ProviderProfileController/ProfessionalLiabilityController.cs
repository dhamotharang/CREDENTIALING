using PortalTemplate.Areas.CredAxis.Models.ProfessionalLiabilityViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class ProfessionalLiabilityController : Controller
    {
        private readonly IProfessionalLiabilityService _ProfessionalLiability = null;

        public ProfessionalLiabilityController()
        {
            _ProfessionalLiability = new ProfessionalLiabilityService();
        }

        //
        // GET: /CredAxis/Demographics/
        public ActionResult Index(string ModeRequested)
        {
            //List<ProfessionalLiabilityViewModel> theModel = new List<ProfessionalLiabilityViewModel>();

            List<ProfessionalLiabilityViewModel>  theModel = _ProfessionalLiability.GetAllProfessionalLiability();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_AddEditProfessionalLiability.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_ViewProfessionalLiability.cshtml", theModel);
        }
        /// <summary>
        ///  Method to return Add Partial for Professional liability
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProfessionalLiability(ProfessionalLiabilityViewModel model, string value)
        {
            var Action1 = "Add";
            List<ProfessionalLiabilityViewModel> ProfessionalLiabiltyEmptyModel = new List<ProfessionalLiabilityViewModel>();
            if (value == Action1)
            {
                ProfessionalLiabiltyEmptyModel.Add(model);
            }
            else
            {
                ProfessionalLiabiltyEmptyModel = _ProfessionalLiability.GetAllProfessionalLiability();
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_AddEditProfessionalLiability.cshtml", ProfessionalLiabiltyEmptyModel);
        }
        /// <summary>
        /// Method to return view partial for Professional liability 
        /// </summary>
        /// <param name="AddLiabilitydata"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AppendLiabilitydata(ProfessionalLiabilityViewModel AddLiabilitydata)
        {
            //List<ProfessionalLiabilityViewModel> Liabilitydata = new List<ProfessionalLiabilityViewModel>();
            List<ProfessionalLiabilityViewModel> Liabilitydata = _ProfessionalLiability.GetAllProfessionalLiability();
            //Liabilitydata.Add(AddLiabilitydata);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_ViewProfessionalLiability.cshtml", Liabilitydata);
        }

        public ActionResult ViewLiabilityHistory(string value)
        {

              List<ProfessionalLiabilityViewModel> Liabilitydata = _ProfessionalLiability.GetAllProfessionalLiability().Where(i=>i.ProfessionalLiabilityID==value).ToList();

              return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_ProfessionalLiabilityHistory.cshtml", Liabilitydata);
        }

        public ActionResult ViewLiabilityHistoryForAll()
        {

            List<ProfessionalLiabilityViewModel> Liabilitydata = _ProfessionalLiability.GetAllProfessionalLiability();

            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_ProfessionalLiabilityHistory.cshtml", Liabilitydata);
        }

        public ActionResult removeLiabililityinfo(string value)
        {

            List<ProfessionalLiabilityViewModel> Liabilitydata = _ProfessionalLiability.GetAllProfessionalLiability().Where(i => i.ProfessionalLiabilityID != value).ToList();

            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_ViewProfessionalLiability.cshtml", Liabilitydata);
        }
    }
}