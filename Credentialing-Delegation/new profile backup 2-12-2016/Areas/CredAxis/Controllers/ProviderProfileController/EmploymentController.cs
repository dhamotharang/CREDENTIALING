using PortalTemplate.Areas.CredAxis.Models.EmploymentInformationVieModel;
using PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.EmploymentInformationVieModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class EmploymentController : Controller
    {
        private readonly IEmploymentServices _EmploymentHistory = null;

        public EmploymentController()
        {
            _EmploymentHistory = new EmploymentServices();
        }

        // GET: /CredAxis/Demographics/
        public ActionResult Index(string ModeRequested)
        {
            //EmploymentInformationMainViewModel theModel = new EmploymentInformationMainViewModel();

            EmploymentInformationMainViewModel theModel = _EmploymentHistory.GetAllEmploymentInformations();
            if (ModeRequested == "EDIT")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_EmploymentInfoIndexEditMode.cshtml", theModel);
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_EmploymentInfoIndex.cshtml", theModel);
        }

        [HttpPost]
        public JsonResult AddEditEmploymentInformations(EmploymentInformationMainViewModel employmentInformations)
        {
            if (ModelState.IsValid)
            {
                var AddEditEmploymentInformation = _EmploymentHistory.AddEditEmploymentInformations(employmentInformations);
                return Json(new { Result = AddEditEmploymentInformation, status = "done" });
            }

            return Json(new { status = "false" });
        }
        /// <summary>
        /// Method to return Add Partial for groupinformation
        /// </summary>
        /// <returns></returns>
        public ActionResult Addgroupinformation(GroupInformationViewModel data, string value)
        {
            var Action1 = "Add";
            List<GroupInformationViewModel> GroupEmptyModel = new List<GroupInformationViewModel>();
            EmploymentInformationMainViewModel EmployModel = new EmploymentInformationMainViewModel();
            if (value == Action1)
            {
                GroupEmptyModel.Add(data);
            }
            else
            {
                EmployModel = _EmploymentHistory.GetAllEmploymentInformations();
                for (int i = 0; i < EmployModel.groupInformation.Count(); i++)
                {
                    GroupEmptyModel.Add(EmployModel.groupInformation[i]);
                }
            }
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_AddEditGroupInformation.cshtml", GroupEmptyModel);
        }
        [HttpPost]
        public ActionResult AppendGroupdata(GroupInformationViewModel Addgroupdata)
        {
            EmploymentInformationMainViewModel GroupInfordata = new EmploymentInformationMainViewModel();
            GroupInfordata = _EmploymentHistory.GetAllEmploymentInformations();
            GroupInfordata.groupInformation.Add(Addgroupdata);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_GroupInformation.cshtml", GroupInfordata.groupInformation);
        }

        public ActionResult AppendEmploydata()
        {
            //EmploymentInformationMainViewModel theModel = new EmploymentInformationMainViewModel();
            EmploymentInformationMainViewModel theModel = _EmploymentHistory.GetAllEmploymentInformations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_AddEditEmploymentInformation.cshtml", theModel.employmentInformation);
        }
        public ActionResult ViewEmployData()
        {
            //EmploymentInformationMainViewModel theModel = new EmploymentInformationMainViewModel();
            EmploymentInformationMainViewModel theModel = _EmploymentHistory.GetAllEmploymentInformations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_EmploymentInformation.cshtml", theModel.employmentInformation);
        }



        public ActionResult GetGroupInfoData()
        {
            EmploymentInformationMainViewModel GroupInfordata = new EmploymentInformationMainViewModel();
            GroupInfordata = _EmploymentHistory.GetAllEmploymentInformations();
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_GroupInformation.cshtml", GroupInfordata.groupInformation);
        }
        public ActionResult viewEmployHistory(string value)
        {
            List<EmploymentInformationViewModel> EmploymentHistory = new List<EmploymentInformationViewModel>();
            EmploymentInformationMainViewModel EmployModel = new EmploymentInformationMainViewModel();
            EmployModel = _EmploymentHistory.GetAllEmploymentInformations();
            EmploymentHistory.Add(EmployModel.employmentInformation);
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_EmploymentInfoHistory.cshtml", EmploymentHistory);
        }

        //Returns history of each group information
        public ActionResult ViewGrouphistory(string value)
        {
            List<GroupInformationViewModel> GroupHistoryModel = new List<GroupInformationViewModel>();
            EmploymentInformationMainViewModel EmployModel = new EmploymentInformationMainViewModel();
            EmployModel = _EmploymentHistory.GetAllEmploymentInformations();

            //foreach (GroupInformationViewModel groupmodel in EmployModel.groupInformation)
            //{
            //    if(value==groupmodel.GroupInformationViewID)
            //    {
            //        GroupHistoryModel.Add(groupmodel);
            //    }

            //}

            GroupHistoryModel = EmployModel.groupInformation.Where(i => i.GroupInformationViewID == value).ToList();


            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_GroupInformationHistory.cshtml", GroupHistoryModel);
        }
        public ActionResult removegroupinfo(string value)
        {
            List<GroupInformationViewModel> GroupHistoryModel = new List<GroupInformationViewModel>();
            EmploymentInformationMainViewModel EmployModel = new EmploymentInformationMainViewModel();
            EmployModel = _EmploymentHistory.GetAllEmploymentInformations();
           GroupHistoryModel= EmployModel.groupInformation.Where(i => i.GroupInformationViewID != value).ToList();
           return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_GroupInformation.cshtml", GroupHistoryModel);
        }

    }
}