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
    public class EncounterCRUDController : Controller
    {
        IEncounterCRUD _EncounterCRUDService;
        public EncounterCRUDController()
        {
            _EncounterCRUDService = new EncounterCRUD();
        }

        // ViewEncounter() to view the encounter from list
        public ActionResult ViewEncounter()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterCRUD/_ViewEncounter.cshtml", _EncounterCRUDService.ViewEncounter());
        }

        // EditEncounter() to Edit the encounter from list/view
        public ActionResult EditEncounter()
        {
            return PartialView("~/Areas/Encounters/Views/EncounterCRUD/_EditEncounter.cshtml", _EncounterCRUDService.EditEncounter());
        }
    }
}