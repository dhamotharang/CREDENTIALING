using PortalTemplate.Areas.CredAxis.Models.EducationViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class EducationController : Controller
    {
        private IEducationServices _IEducationCode = null;

        public EducationController()
        {
            _IEducationCode = new EducationServices();
        }
        //
        // GET: /CredAxis/Demographics/
        public ActionResult Index(string ModeRequested)
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/_EducationHistoryIndexEditMode.cshtml", EducationData);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/_EducationHistoryIndex.cshtml", EducationData);
        }

        public ActionResult GetECFMGAddEditView(ECFMGViewModel model, string type)
        {
            ECFMGViewModel _ECFMGModel = new ECFMGViewModel();
            if (type.ToString() == "Add")
            {
                _ECFMGModel=model;
            }
            else
            {
                EducationMainViewModel EducationData = new EducationMainViewModel();
                EducationData = _IEducationCode.GetAllEducations();
                _ECFMGModel = EducationData.ECFMGs;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ECFMGDetails/_AddEditECFMGDetails.cshtml", _ECFMGModel);
        }
        public ActionResult GetGraduateSchoolAddEditView(GraduateSchoolViewModel model, string type)
        {
            List<GraduateSchoolViewModel> _GraduateSchoolModel = new List<GraduateSchoolViewModel>();
            if (type.ToString() == "Add")
            {
                _GraduateSchoolModel.Add(model);
            }
            else
            {
                EducationMainViewModel EducationData = new EducationMainViewModel();
                EducationData = _IEducationCode.GetAllEducations();
                _GraduateSchoolModel = EducationData.GradSchools;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/GraduateMedicalSchoolDetails/_AddEditGraduateSchoolDetails.cshtml", _GraduateSchoolModel);
        }
        public ActionResult GetPostGraduateAddEditView(PostGraduationViewModel model, string type)
        {
            List<PostGraduationViewModel> _PostGraduationModel = new List<PostGraduationViewModel>();
            if (type.ToString() == "Add")
            {
                _PostGraduationModel.Add(model);
            }
            else
            {
                EducationMainViewModel EducationData = new EducationMainViewModel();
                EducationData = _IEducationCode.GetAllEducations();
                _PostGraduationModel = EducationData.PostGradSchools;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/PostGraduationCMEDetails/_AddEditPostGraduateTrainingCMEDetail.cshtml", _PostGraduationModel);
        }
        public ActionResult GetResidencyAddEditView(ResidencyViewModel model, string type)
        {
            List<ResidencyViewModel> _ResidencyModel = new List<ResidencyViewModel>();
            if (type.ToString() == "Add")
            {
                _ResidencyModel.Add(model);
            }
            else
            {
                EducationMainViewModel EducationData = new EducationMainViewModel();
                EducationData = _IEducationCode.GetAllEducations();
                _ResidencyModel = EducationData.Residency;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ResidencyDetails/_AddEditResidencyDetails.cshtml", _ResidencyModel);
        }
        public ActionResult GetUnderGraduateAddEditView(UnderGraduationViewModel model, string type)
        {
            List<UnderGraduationViewModel> _UnderGraduationModel = new List<UnderGraduationViewModel>();
            if (type.ToString() == "Add")
            {
                _UnderGraduationModel.Add(model);
            }
            else {
                EducationMainViewModel EducationData = new EducationMainViewModel();
                EducationData = _IEducationCode.GetAllEducations();
                _UnderGraduationModel = EducationData.UnderGradSchools;
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/UnderGraduationOrProfessionalDetails/_AddEditUnderGradOrProfSchoolDetails.cshtml", _UnderGraduationModel);
        }
        [HttpPost]
        public ActionResult AddResidencyDetails(ResidencyViewModel data)
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            //EducationData.Residency.Add(data);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ResidencyDetails/_ViewResidencyDetails.cshtml", EducationData.Residency);
        }
        public ActionResult CancelAddResidencyDetails()
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ResidencyDetails/_ViewResidencyDetails.cshtml", EducationData.Residency);
        }
        [HttpPost]
        public ActionResult AddGraduationDetails(GraduateSchoolViewModel data)
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
          //  EducationData.GradSchools.Add(data);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/GraduateMedicalSchoolDetails/_ViewGraduateDetails.cshtml", EducationData.GradSchools);
        }
        public ActionResult CancelAddGraduationDetails()
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/GraduateMedicalSchoolDetails/_ViewGraduateDetails.cshtml", EducationData.GradSchools);
        }
        [HttpPost]
        public ActionResult AddPostGraduationDetails(PostGraduationViewModel data)
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            //EducationData.PostGradSchools.Add(data);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/PostGraduationCMEDetails/_ViewPostGraduateTrainingCMEDetail.cshtml", EducationData.PostGradSchools);
        }
        public ActionResult CancelAddPostGraduationDetails()
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/PostGraduationCMEDetails/_ViewPostGraduateTrainingCMEDetail.cshtml", EducationData.PostGradSchools);
        }

        public ActionResult RemoveUndergraduateDetails(string value)
        {
            EducationMainViewModel education = _IEducationCode.GetAllEducations();
            List<UnderGraduationViewModel> model = education.UnderGradSchools;
            model.RemoveAll(underGrad => underGrad.Id == int.Parse(value));
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/UnderGraduationOrProfessionalDetails/_ViewUndergraduateDetails.cshtml", model);
        }

        public ActionResult RemovePostGraduationDetails(string value)
        {
            EducationMainViewModel education = _IEducationCode.GetAllEducations();
            List<PostGraduationViewModel> model = education.PostGradSchools;
            model.RemoveAll(postGrad => postGrad.Id == int.Parse(value));
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/PostGraduationCMEDetails/_ViewPostGraduateTrainingCMEDetail.cshtml", model);
        }

        public ActionResult RemoveResidencyDetails(string value)
        {
            EducationMainViewModel education = _IEducationCode.GetAllEducations();
            List<ResidencyViewModel> model = education.Residency;
            model.RemoveAll(residency => residency.Id == int.Parse(value));
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ResidencyDetails/_ViewResidencyDetails.cshtml", model);
        }

        public ActionResult RemoveGraduateDetails(string value)
        {
            EducationMainViewModel education = _IEducationCode.GetAllEducations();
            List<GraduateSchoolViewModel> model = education.GradSchools;
            model.RemoveAll(grad => grad.Id == int.Parse(value));
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/GraduateMedicalSchoolDetails/_ViewGraduateDetails.cshtml", model);
        }

        [HttpPost]
        public ActionResult AddUnderGraduationDetails(UnderGraduationViewModel data)
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
           // EducationData.UnderGradSchools.Add(data);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/UnderGraduationOrProfessionalDetails/_ViewUndergraduateDetails.cshtml", EducationData.UnderGradSchools);
        }
        public ActionResult CancelAddUnderGraduationDetails()
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/UnderGraduationOrProfessionalDetails/_ViewUndergraduateDetails.cshtml", EducationData.UnderGradSchools);
        }
        [HttpPost]
        public ActionResult AddECFMGDetails(ECFMGViewModel data)
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            EducationData.ECFMGs = data;
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ECFMGDetails/_ViewECFMGDetails.cshtml", EducationData.ECFMGs);
        }
        public ActionResult CancelAddECFMGDetails()
        {
            EducationMainViewModel EducationData = new EducationMainViewModel();
            EducationData = _IEducationCode.GetAllEducations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ECFMGDetails/_ViewECFMGDetails.cshtml", EducationData.ECFMGs);
        }
        [HttpPost]
        public JsonResult AddEditEducations(EducationMainViewModel educationsCode)
        {
            if (ModelState.IsValid)
            {
                var AddEditEducation = _IEducationCode.AddEditEducations(educationsCode);
                return Json(new { Result = AddEditEducation, status = "done" });
            }

            return Json(new { status = "false" });
        }
        public ActionResult AddUnderGraduate()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/UnderGraduationOrProfessionalDetails/_AddEditUnderGradOrProfSchoolDetails.cshtml");
        }

        //Manideep Innamuri
        //for history of Post Graduation in Work history

        public ActionResult ViewUnderGraduationhistory(int value)
        {
            List<UnderGraduationViewModel> underGraduationViewModel = new List<UnderGraduationViewModel>();
            EducationMainViewModel EducationModel = new EducationMainViewModel();
            EducationModel = _IEducationCode.GetAllEducations();

            foreach (UnderGraduationViewModel UnderGraduationModel in EducationModel.UnderGradSchools)
            {
                if (value == UnderGraduationModel.Id)
                {
                    underGraduationViewModel.Add(UnderGraduationModel);
                }
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/UnderGraduationOrProfessionalDetails/_HistoryUnderGradOrProfSchoolDetails.cshtml", underGraduationViewModel);
        }
        public ActionResult ViewPostGraduationhistory(int value)
        {
            List<PostGraduationViewModel> postGraduationViewModel = new List<PostGraduationViewModel>();
            EducationMainViewModel EducationModel = new EducationMainViewModel();
            EducationModel = _IEducationCode.GetAllEducations();

            foreach (PostGraduationViewModel PostGraduationmodel in EducationModel.PostGradSchools)
            {
                if (value == PostGraduationmodel.Id)
                {
                    postGraduationViewModel.Add(PostGraduationmodel);
                }

            }


            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/PostGraduationCMEDetails/_HistoryPostGraduateTrainingCMEDetail.cshtml", postGraduationViewModel);
        }

        public ActionResult ViewGraduationhistory(int value)
        {
            List<GraduateSchoolViewModel> graduateSchoolViewModel = new List<GraduateSchoolViewModel>();
            EducationMainViewModel EducationModel = new EducationMainViewModel();
            EducationModel = _IEducationCode.GetAllEducations();

            foreach (GraduateSchoolViewModel Graduationmodel in EducationModel.GradSchools)
            {
                if (value == Graduationmodel.Id)
                {
                    graduateSchoolViewModel.Add(Graduationmodel);
                }
            }


            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/GraduateMedicalSchoolDetails/_HistoryGraduateSchoolDetails.cshtml", graduateSchoolViewModel);
        }

        public ActionResult ViewResidencyDetailshistory(int value)
        {
            List<ResidencyViewModel> residencyViewModel = new List<ResidencyViewModel>();
            EducationMainViewModel EducationModel = new EducationMainViewModel();
            EducationModel = _IEducationCode.GetAllEducations();

            foreach (ResidencyViewModel ResidencyViewModel in EducationModel.Residency)
            {
                if (value == ResidencyViewModel.Id)
                {
                    residencyViewModel.Add(ResidencyViewModel);
                }

            }


            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/ResidencyDetails/_HistoryResidencyDetails.cshtml", residencyViewModel);
        }
       
    }
}