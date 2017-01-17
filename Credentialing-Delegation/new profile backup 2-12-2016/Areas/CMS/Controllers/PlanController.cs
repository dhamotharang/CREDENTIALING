using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class PlanController : Controller
    {
        /// <summary>
        /// IPlanService object reference
        /// </summary>
        private IPlanService _Plan = null;

        /// <summary>
        /// PlanController constructor For PlanService
        /// </summary>
        public PlanController()
        {
            _Plan = new PlanService();
        }

        //
        // GET: /CMS/Plan/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Plan";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Plan.GetAll();
            PlanViewModel model = new PlanViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Plan/Index.cshtml", model);
        }

        //
        // POST: /CMS/Plan/AddEditPlan
        [HttpPost]
        public ActionResult AddEditPlan(string Code)
        {
            PlanViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Plan";
                model = new PlanViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Plan";
                model = _Plan.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Plan/_AddEditPlanForm.cshtml", model);
        }

        //
        // POST: /CMS/Plan/SavePlan
        [HttpPost]
        public ActionResult SavePlan(PlanViewModel Plan)
        {
            if (ModelState.IsValid)
            {
                int TempID = Plan.PlanID;

                if (TempID == 0)
                {
                    Plan.CreatedBy = "CMS Team";
                    Plan.Source = "CMS Server";
                    Plan = _Plan.Create(Plan);
                }
                else {
                    Plan.LastModifiedBy = "CMS Team Update";
                    Plan.Source = "CMS Server Update";
                    Plan = _Plan.Update(Plan); }

                if (Plan != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Plan/_RowPlan.cshtml", Plan);

                    if (TempID == 0)
                        return Json(new { Message = "New Plan Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Plan Updated Successfully", Status = true, Type = "Edit", Template = Template });
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