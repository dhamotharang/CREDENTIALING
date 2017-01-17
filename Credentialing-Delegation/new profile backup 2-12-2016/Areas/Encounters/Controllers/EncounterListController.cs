using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Models.EncounterList;
using PortalTemplate.Areas.Encounters.Services;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Encounters.Controllers
{
    public class EncounterListController : Controller
    {
        IEncounterList _EncounterListService;
        public EncounterListController()
        {
            _EncounterListService = new EncounterList();
        }

        // ShowAllEncounters() returns the partial view of list along with the counts of each
        public ActionResult ShowAllEncounters()
        {
            ViewBag.ListCounts = _EncounterListService.GetAllListCounts();
            return PartialView("~/Areas/Encounters/Views/EncounterList/_ListAllEncounters.cshtml");
        }
        public ActionResult GetScheduleListByIndex(int index, string sortingType, string sortBy, ScheduleListViewModel SearchObject)
        {

            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_TBodyScheduleEncounterList.cshtml", _EncounterListService.GetScheduleList());
        }

        // GetScheduleEncounterList() returns the Schedule encounter list
        public ActionResult GetScheduleEncounterList()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_ScheduleEncounterList.cshtml", _EncounterListService.GetScheduleEncounterList());
        }

        // GetActiveEncounterList() returns the Active encounter list
        public ActionResult GetActiveEncounterList()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_ActiveEncounterList.cshtml", _EncounterListService.GetActiveEncounterList());
        }

        // GetOpenEncounterList() returns the Open encounter list
        public ActionResult GetOpenEncounterList()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_OpenEncounterList.cshtml", _EncounterListService.GetOpenEncounterList());
        }

        public ActionResult GetEncounterClosedListByIndex(int index, string sortingType, string sortBy, EncounterListViewModel SearchObject)
        {

            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_TBodyEncounterClosedList.cshtml", _EncounterListService.GetEncounterClosedList());
        }


        // GetDraftEncounterList() returns the Draft encounter list
        public ActionResult GetDraftEncounterList()
        {

            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_DraftEncounterList.cshtml", _EncounterListService.GetDraftEncounterList());
        }

        // GetRAEncounterList() returns the Ready to code encounter list
        public ActionResult GetClosedEncounterList()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_ReadyToCodeEncounterList.cshtml", _EncounterListService.GetClosedEncounterList());
        }


        public ActionResult GetEncounterRejectionListByIndex(int index, string sortingType, string sortBy, EncounterListViewModel SearchObject)
        {

            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_TBodyRejectedEncounterList.cshtml", _EncounterListService.GetEncounterRejectedList());
        }

        // GetInactiveEncounterList() returns the Inactive encounter list
        public ActionResult GetInactiveEncounterList()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_InactiveEncounterList.cshtml", _EncounterListService.GetInactiveEncounterList());
        }
          // GetInactiveEncounterList() returns the Inactive encounter list
        public ActionResult GetRejectedEncounterList()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterList/List/_RejectedEncounterList.cshtml", _EncounterListService.GetRejectedEncounterList());
        }
        
	}
}