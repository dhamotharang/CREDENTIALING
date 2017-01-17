using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Services;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Encounters.Controllers
{
    public class ScheduleController : Controller
    {

        ISchedule _ScheduleService;

        public ScheduleController()
        {
            _ScheduleService = new Schedule();
        }

        // CreateSchedule() returns the Create partial view of scheduler
        public PartialViewResult CreateSchedule()
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_CreateSchedule.cshtml");
        }


        // GetProviderSelection() method return the partial view of selected provider which will intern call the GetProviderResult()
        public ActionResult GetProviderSelection()
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_SelectProvider.cshtml");
        }

        public ActionResult GetProviderResult(string ProviderSearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_ProviderResult.cshtml", _ScheduleService.GetProviderResultList(ProviderSearchParameter));
        }


        // GetSelectedProviderResult() will return the partial view which will contain the member information of corresponding Rendering provider
        public ActionResult GetSelectedProviderResult(string ProviderId)
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_ProviderSelectMember.cshtml", _ScheduleService.GerProviderSelectMembers(ProviderId));
        }


        public ActionResult GetMemberForScheduler()
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_MemberSelectMemberForScheduler.cshtml", _ScheduleService.GetMemberResultList());
        }



        public ActionResult GetMemberResult(string MemberSearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_MemberResult.cshtml", _ScheduleService.GetMemberResultList());
        }


        public PartialViewResult GetScheduleDetails(string MemberId)
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_GetScheduleDetails.cshtml", _ScheduleService.CreateEncounterDetails());
        }

        public PartialViewResult GetScheduleDetailsForMember(string MemberId)
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_GetScheduleDetailsForMember.cshtml", _ScheduleService.CreateEncounterDetails());
        }

        public ActionResult GetEncounterDetails()
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_GetEncounterDetails.cshtml", _ScheduleService.CreateEncounterDetails());
        }

        public ActionResult GetEncounterDetailsForMember()
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_GetEncounterDetailsForMember.cshtml", _ScheduleService.CreateEncounterDetails());
        }

        public ActionResult GetAddDocumentPartial()
        {
            return PartialView("~/Encounters/Views/Documents/_DocumentsAdd.cshtml", _ScheduleService.CreateEncounterDetails().Documents);
        }

        public ActionResult SaveScheduleDetails(EncounterViewModel ScheduleDetails)
        {
            return PartialView("~/Areas/Encounters/Views/Schedule/_GetScheduleDetails.cshtml", _ScheduleService.CreateEncounterDetails());
        }

        public ActionResult GetScheduleView()
        {
            return PartialView("~/Areas/Encounters/Views/ScheduleCRUD/_ViewSchedule.cshtml", _ScheduleService.GetScheduleView());
        }

        public ActionResult GetScheduleEdit()
        {
            return PartialView("~/Areas/Encounters/Views/ScheduleCRUD/_EditSchedule.cshtml", _ScheduleService.GetScheduleEdit());
        }
        //Json Methods
        public JsonResult GetAllSchedules()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}