using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DEAScheduleController : Controller
    {
        /// <summary>
        /// IDEAScheduleService object reference
        /// </summary>
        private IDEAScheduleService _DEASchedule = null;

        /// <summary>
        /// DEAScheduleController constructor For DEAScheduleService
        /// </summary>
        public DEAScheduleController()
        {
            _DEASchedule = new DEAScheduleService();
        }

        //
        // GET: /CMS/DEASchedule/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DEASchedule";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DEASchedule.GetAll();
            DEAScheduleViewModel model = new DEAScheduleViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DEASchedule/Index.cshtml", model);
        }

        //
        // POST: /CMS/DEASchedule/AddEditDEASchedule
        [HttpPost]
        public ActionResult AddEditDEASchedule(string Code)
        {
            DEAScheduleViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DEASchedule";
                model = new DEAScheduleViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DEASchedule";
                model = _DEASchedule.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DEASchedule/_AddEditDEAScheduleForm.cshtml", model);
        }

        //
        // POST: /CMS/DEASchedule/SaveDEASchedule
        [HttpPost]
        public ActionResult SaveDEASchedule(DEAScheduleViewModel DEASchedule)
        {
            if (ModelState.IsValid)
            {
                int TempID = DEASchedule.DEAScheduleID;

                if (TempID == 0)
                {
                    DEASchedule.CreatedBy = "CMS Team";
                    DEASchedule.Source = "CMS Server";
                    DEASchedule = _DEASchedule.Create(DEASchedule);
                }
                else {
                    DEASchedule.LastModifiedBy = "CMS Team Update";
                    DEASchedule.Source = "CMS Server Update";
                    DEASchedule = _DEASchedule.Update(DEASchedule); }

                if (DEASchedule != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DEASchedule/_RowDEASchedule.cshtml", DEASchedule);

                    if (TempID == 0)
                        return Json(new { Message = "New DEASchedule Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DEASchedule Updated Successfully", Status = true, Type = "Edit", Template = Template });
                }
                else
                {
                    return Json(new { Message = "Sorry! Try Again", Status = false });
                }
            }
            return Json(new { Message = "Sorry! Try Again", Status = false });
        }
    }
}