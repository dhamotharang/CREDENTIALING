using PortalTemplate.Areas.CredAxis.Models.WorkHistoryViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using PortalTemplate.Areas.CredAxisProduct.Models.ProviderProfileViewModel.WorkHistoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class WorkHistoryController : Controller
    {
        private readonly IWorkHistoryService _WorkHistoryCode = null;

        public WorkHistoryController()
        {
            _WorkHistoryCode = new WorkHistoryService();
        }

        WorkHistoryMainViewModel WorkHistoryModel = new WorkHistoryMainViewModel();
        //
        // GET: /CredAxis/WorkHistory/
        public ActionResult Index(string ModeRequested)
        {
            //WorkHistoryMainViewModel theModel = new WorkHistoryMainViewModel();

            WorkHistoryMainViewModel theModel = _WorkHistoryCode.GetAllWorkHistory();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/_WorkHistoryEditMode.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/_WorkHistory.cshtml", theModel);
        }

        // GET: /CredAxis/GetProfessionalWorkExperienceAddPartial/
        public ActionResult GetProfessionalWorkExperienceAddEditPartial(ProfessionalWorkExperienceViewModel model, string ActionType)
        {
            var Action1 = "Edit"; var Action2 = "Add";
            if (ActionType == Action2)
            {
                WorkHistoryModel.professionalWorkExperience.Add(model);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/ProfessionalWorkExperience/_AddEditProfessionalWorkExperience.cshtml", WorkHistoryModel.professionalWorkExperience);
            }
            WorkHistoryModel = _WorkHistoryCode.GetAllWorkHistory();
            if (ActionType == Action1)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/ProfessionalWorkExperience/_AddEditProfessionalWorkExperience.cshtml", WorkHistoryModel.professionalWorkExperience);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/ProfessionalWorkExperience/_ViewProfessionalWorkExperience.cshtml", WorkHistoryModel.professionalWorkExperience);
        }
        // GET: /CredAxis/MilitaryServiceAddEditPartial/

        public ActionResult GetMilitaryServiceAddEditPartial(MilitaryServiceViewModel model, string ActionType)
        {
            var Action1 = "Edit"; var Action2 = "Add";
            if (ActionType == Action2)
            {
                WorkHistoryModel.militaryService.Add(model);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/MilitaryService/_AddEditMilitaryServiceInfo.cshtml", WorkHistoryModel.militaryService);
            }
            WorkHistoryModel = _WorkHistoryCode.GetAllWorkHistory();
            if (ActionType == Action1)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/MilitaryService/_AddEditMilitaryServiceInfo.cshtml", WorkHistoryModel.militaryService);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/MilitaryService/_ViewMilitaryServiceInfo.cshtml", WorkHistoryModel.militaryService);

        }
        // GET: /CredAxis/PublicHealthService/
        public ActionResult GetPublicHealthServiceAddEditPartial(PublicHealthServicesViewModel model, string ActionType)
        {
            var Action1 = "Edit"; var Action2 = "Add";
            if (ActionType == Action2)
            {
                WorkHistoryModel.publicHealthService.Add(model);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/PublicHealthService/_AddEditPublicHealthService.cshtml", WorkHistoryModel.publicHealthService);
            }
            WorkHistoryModel = _WorkHistoryCode.GetAllWorkHistory();
            if (ActionType == Action1)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/PublicHealthService/_AddEditPublicHealthService.cshtml", WorkHistoryModel.publicHealthService);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/PublicHealthService/_ViewPublicHealthService.cshtml", WorkHistoryModel.publicHealthService);
        }

        // GET: /CredAxis/GetWorkGapAddEditPartial/
        public ActionResult GetWorkGapAddEditPartial(WorkGapViewModel model, string ActionType)
        {
            var Action1 = "Edit"; var Action2 = "Add";
            if (ActionType == Action2)
            {
                WorkHistoryModel.workGap.Add(model);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/WorkGap/_AddEditWorkGap.cshtml", WorkHistoryModel.workGap);
            }
            WorkHistoryModel = _WorkHistoryCode.GetAllWorkHistory();
            if (ActionType == Action1)
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/WorkGap/_AddEditWorkGap.cshtml", WorkHistoryModel.workGap);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/WorkGap/_ViewWorkGap.cshtml", WorkHistoryModel.workGap);
        }

        [HttpPost]
        public JsonResult AddEditWorkHistory(WorkHistoryMainViewModel workHistoryMainViewModel)
        {
            if (ModelState.IsValid)
            {
                var AddEditWorkHistory = _WorkHistoryCode.AddEditWorkHistory(workHistoryMainViewModel);
                return Json(new { Result = AddEditWorkHistory, status = "done" });
            }

            return Json(new { status = "false" });
        }


        public ActionResult RemoveProfessionalWorkExp(int value)
        {
            List<ProfessionalWorkExperienceViewModel> GroupHistoryModel = new List<ProfessionalWorkExperienceViewModel>();
            WorkHistoryMainViewModel theModel = new WorkHistoryMainViewModel();
            theModel = _WorkHistoryCode.GetAllWorkHistory();
            GroupHistoryModel = theModel.professionalWorkExperience.Where(i => i.ID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/ProfessionalWorkExperience/_ViewProfessionalWorkExperience.cshtml", GroupHistoryModel);
        }
        public ActionResult RemoveMilitaryServiceInfo(int value)
        {
            List<MilitaryServiceViewModel> GroupHistoryModel = new List<MilitaryServiceViewModel>();
            WorkHistoryMainViewModel theModel = new WorkHistoryMainViewModel();
            theModel = _WorkHistoryCode.GetAllWorkHistory();
            GroupHistoryModel = theModel.militaryService.Where(i => i.ID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/MilitaryService/_ViewMilitaryServiceInfo.cshtml", GroupHistoryModel);
        }
        public ActionResult RemovePublicHealthService(int value)
        {
            List<PublicHealthServicesViewModel> GroupHistoryModel = new List<PublicHealthServicesViewModel>();
            WorkHistoryMainViewModel theModel = new WorkHistoryMainViewModel();
            theModel = _WorkHistoryCode.GetAllWorkHistory();
            GroupHistoryModel = theModel.publicHealthService.Where(i => i.ID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/PublicHealthService/_ViewPublicHealthService.cshtml", GroupHistoryModel);
        }
        public ActionResult RemoveWorkGap(int value)
        {
            List<WorkGapViewModel> GroupHistoryModel = new List<WorkGapViewModel>();
            WorkHistoryMainViewModel theModel = new WorkHistoryMainViewModel();
            theModel = _WorkHistoryCode.GetAllWorkHistory();
            GroupHistoryModel = theModel.workGap.Where(i => i.ID != value).ToList();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/WorkGap/_ViewWorkGap.cshtml", GroupHistoryModel);
        }
    }
}
