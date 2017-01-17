using PortalTemplate.Areas.CredAxis.Models.SpecialtyViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class SpecialityController : Controller
    {
        private readonly ISpecialityService _SpecialityCode = null;

        public SpecialityController()
        {
            _SpecialityCode = new SpecialityService();
        }

        //
        // GET: /CredAxis/Demographics/
        public ActionResult Index(string ModeRequested)
        {
            //List<SpecialtyDetailViewModel> theModel = new List<SpecialtyDetailViewModel>();
            List<SpecialtyDetailViewModel> theModel = _SpecialityCode.GetAllSpeciality();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Specialty/_AddEditSpecialityBoard.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Specialty/_ViewSpecialityBoard.cshtml", theModel);
        }
        /// <summary>
        /// Method to return Add Partial for Professional Affiliation
        /// </summary>
        /// <param name="professionalAffiliationMainModel"></param>
        /// <returns></returns>
        public ActionResult GetSpecialityBoardDetailPartial(SpecialtyDetailViewModel model, string ActionType)
        {
            var Action1 = "Edit"; var Action2 = "Add";
            List<SpecialtyDetailViewModel> SpecialityBoardDetailEmptyModel = new List<SpecialtyDetailViewModel>();
            if (ActionType == Action2)
            {
                SpecialityBoardDetailEmptyModel.Add(model);
            }
            if (ActionType == Action1)
            {
                SpecialityBoardDetailEmptyModel = _SpecialityCode.GetAllSpeciality();
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Specialty/_AddEditSpecialityBoard.cshtml", SpecialityBoardDetailEmptyModel);
        }

        [HttpPost]
        public JsonResult AddEditSpeciality(SpecialityMainViewModel specialityMainViewModel)
        {
            if (ModelState.IsValid)
            {
                var AddEditSpecialityCode = _SpecialityCode.AddEditSpeciality(specialityMainViewModel);
                return Json(new { Result = AddEditSpecialityCode, status = "done" });
            }

            return Json(new { status = "false" });
        }

        public ActionResult ViewSpecialityDetailshistory(int value)
        {
            List<SpecialtyDetailViewModel> specialtyDetailModel = new List<SpecialtyDetailViewModel>();
            List<SpecialtyDetailViewModel> specialtyDetail = new List<SpecialtyDetailViewModel>();
            //List<SpecialityMainViewModel> specialityViewModel = new List<SpecialityMainViewModel>();
            specialtyDetailModel = _SpecialityCode.GetAllSpeciality();

            foreach (SpecialtyDetailViewModel specialtyDetailViewModel in specialtyDetailModel)
            {
                if (value == specialtyDetailViewModel.SpecialtyDetailId)
                {
                    specialtyDetail.Add(specialtyDetailViewModel);
                }

            }


            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Specialty/_HistorySpecialtyDetails.cshtml", specialtyDetail);
        }

        public ActionResult RemoveSpecialty(string ModeRequested,int id)
        {
            //List<SpecialtyDetailViewModel> theModel = new List<SpecialtyDetailViewModel>();
            List<SpecialtyDetailViewModel> theModel = _SpecialityCode.GetAllSpeciality();
            for (var i = 0; i < theModel.Count; i++)
            {
                if (theModel[i].SpecialtyDetailId == id)
                {
                    theModel.RemoveAt(i);
                }
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Specialty/_ViewSpecialityBoard.cshtml", theModel);
        }
    }
}