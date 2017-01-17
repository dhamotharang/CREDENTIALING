using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DEAScheduleTypeController : Controller
    {
        /// <summary>
        /// IDEAScheduleTypeService object reference
        /// </summary>
        private IDEAScheduleTypeService _DEAScheduleType = null;

        /// <summary>
        /// DEAScheduleTypeController constructor For DEAScheduleTypeService
        /// </summary>
        public DEAScheduleTypeController()
        {
            _DEAScheduleType = new DEAScheduleTypeService();
        }

        //
        // GET: /CMS/DEAScheduleType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DEAScheduleType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DEAScheduleType.GetAll();
            DEAScheduleTypeViewModel model = new DEAScheduleTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DEAScheduleType/Index.cshtml", model);
        }

        //
        // POST: /CMS/DEAScheduleType/AddEditDEAScheduleType
        [HttpPost]
        public ActionResult AddEditDEAScheduleType(string Code)
        {
            DEAScheduleTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DEAScheduleType";
                model = new DEAScheduleTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DEAScheduleType";
                model = _DEAScheduleType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DEAScheduleType/_AddEditDEAScheduleTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/DEAScheduleType/SaveDEAScheduleType
        [HttpPost]
        public ActionResult SaveDEAScheduleType(DEAScheduleTypeViewModel DEAScheduleType)
        {
            if (ModelState.IsValid)
            {
                int TempID = DEAScheduleType.DEAScheduleTypeID;

                if (TempID == 0)
                {
                    DEAScheduleType.CreatedBy = "CMS Team";
                    DEAScheduleType.Source = "CMS Server";
                    DEAScheduleType = _DEAScheduleType.Create(DEAScheduleType);
                }
                else {
                    DEAScheduleType.LastModifiedBy = "CMS Team Update";
                    DEAScheduleType.Source = "CMS Server Update";
                    DEAScheduleType = _DEAScheduleType.Update(DEAScheduleType); }

                if (DEAScheduleType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DEAScheduleType/_RowDEAScheduleType.cshtml", DEAScheduleType);

                    if (TempID == 0)
                        return Json(new { Message = "New DEAScheduleType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DEAScheduleType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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