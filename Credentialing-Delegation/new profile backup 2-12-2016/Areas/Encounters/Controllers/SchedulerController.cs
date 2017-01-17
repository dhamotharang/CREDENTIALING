using PortalTemplate.Areas.Encounters.Services;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Encounters.Controllers
{
    public class SchedulerController : Controller
    {
        //
        // GET: /Encounters/Scheduler/
        ISchedulerEvents schedulerEventsService = new SchedulerEvents();
        [HttpGet]
        public JsonResult GetEventsForDay(int year, int month, int day)
        {
            return Json(schedulerEventsService.GetSchedulesForDay(year, month, day), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEventsForMonth(int year, int month)
        {
            return Json(schedulerEventsService.GetSchedulesForMonth(year, month), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEventsForYear(int year)
        {
            return Json(schedulerEventsService.GetSchedulesForYear(year), JsonRequestBehavior.AllowGet);
        }
	}
}