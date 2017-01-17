using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class PlanCategoryController : Controller
    {
        /// <summary>
        /// IPlanCategoryService object reference
        /// </summary>
        private IPlanCategoryService _PlanCategory = null;

        /// <summary>
        /// PlanCategoryController constructor For PlanCategoryService
        /// </summary>
        public PlanCategoryController()
        {
            _PlanCategory = new PlanCategoryService();
        }

        //
        // GET: /CMS/PlanCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "PlanCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _PlanCategory.GetAll();
            PlanCategoryViewModel model = new PlanCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/PlanCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/PlanCategory/AddEditPlanCategory
        [HttpPost]
        public ActionResult AddEditPlanCategory(string Code)
        {
            PlanCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New PlanCategory";
                model = new PlanCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit PlanCategory";
                model = _PlanCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/PlanCategory/_AddEditPlanCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/PlanCategory/SavePlanCategory
        [HttpPost]
        public ActionResult SavePlanCategory(PlanCategoryViewModel PlanCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = PlanCategory.PlanCategoryID;

                if (TempID == 0)
                {
                    PlanCategory.CreatedBy = "CMS Team";
                    PlanCategory.Source = "CMS Server";
                    PlanCategory = _PlanCategory.Create(PlanCategory);
                }
                else {
                    PlanCategory.LastModifiedBy = "CMS Team Update";
                    PlanCategory.Source = "CMS Server Update";
                    PlanCategory = _PlanCategory.Update(PlanCategory); }

                if (PlanCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/PlanCategory/_RowPlanCategory.cshtml", PlanCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New PlanCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "PlanCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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