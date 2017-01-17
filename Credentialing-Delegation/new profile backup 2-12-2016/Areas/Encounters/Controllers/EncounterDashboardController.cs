using PortalTemplate.Areas.Encounters.Models.Dashboard;
using PortalTemplate.Areas.Encounters.Services;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Encounters.Controllers
{
    public class EncounterDashboardController : Controller
    {
        IEncounterDashboard _EncounterDashboardService;

        public EncounterDashboardController()
        {
            _EncounterDashboardService = new EncounterDashboard();
        }


        // GetEncounterDashboard() method returns the Partial view along with the required data.
        public ActionResult GetEncounterDashboard()
        {
            return PartialView("~/Areas/Encounters/Views/Dashboard/_DashBoard.cshtml", _EncounterDashboardService.GetEncounterDashboard());
        }

        // GetDashboardBiscuits() method returns the Partial view along with the biscuits required data.
        //public ActionResult GetDashboardBiscuits()
        //{
        //    return PartialView("~/Areas/Encounters/Views/Dashboard/_Biscuits.cshtml", _EncounterDashboardService.GetDashboardBiscuits());
        //}



    }
}