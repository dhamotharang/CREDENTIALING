using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class FeeScheduleController : Controller
    {
        /// <summary>
        /// IFeeScheduleService object reference
        /// </summary>
        private IFeeScheduleService _FeeSchedule = null;

        /// <summary>
        /// FeeScheduleController constructor For FeeScheduleService
        /// </summary>
        public FeeScheduleController()
        {
            _FeeSchedule = new FeeScheduleService();
        }

        //
        // GET: /CMS/FeeSchedule/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "FeeSchedule";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _FeeSchedule.GetAll();
            FeeScheduleViewModel model = new FeeScheduleViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/FeeSchedule/Index.cshtml", model);
        }

        //
        // POST: /CMS/FeeSchedule/AddEditFeeSchedule
        [HttpPost]
        public ActionResult AddEditFeeSchedule(string Code)
        {
            FeeScheduleViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New FeeSchedule";
                model = new FeeScheduleViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit FeeSchedule";
                model = _FeeSchedule.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/FeeSchedule/_AddEditFeeScheduleForm.cshtml", model);
        }

        //
        // POST: /CMS/FeeSchedule/SaveFeeSchedule
        [HttpPost]
        public ActionResult SaveFeeSchedule(FeeScheduleViewModel FeeSchedule)
        {
            if (ModelState.IsValid)
            {
                int TempID = FeeSchedule.FeeScheduleID;

                if (TempID == 0)
                {
                    FeeSchedule.CreatedBy = "CMS Team";
                    FeeSchedule.Source = "CMS Server";
                    FeeSchedule = _FeeSchedule.Create(FeeSchedule);
                }
                else {
                    FeeSchedule.LastModifiedBy = "CMS Team Update";
                    FeeSchedule.Source = "CMS Server Update";
                    FeeSchedule = _FeeSchedule.Update(FeeSchedule); }

                if (FeeSchedule != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/FeeSchedule/_RowFeeSchedule.cshtml", FeeSchedule);

                    if (TempID == 0)
                        return Json(new { Message = "New FeeSchedule Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "FeeSchedule Updated Successfully", Status = true, Type = "Edit", Template = Template });
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