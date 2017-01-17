using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class StaffCategoryController : Controller
    {
        /// <summary>
        /// IStaffCategoryService object reference
        /// </summary>
        private IStaffCategoryService _StaffCategory = null;

        /// <summary>
        /// StaffCategoryController constructor For StaffCategoryService
        /// </summary>
        public StaffCategoryController()
        {
            _StaffCategory = new StaffCategoryService();
        }

        //
        // GET: /CMS/StaffCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "StaffCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _StaffCategory.GetAll();
            StaffCategoryViewModel model = new StaffCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/StaffCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/StaffCategory/AddEditStaffCategory
        [HttpPost]
        public ActionResult AddEditStaffCategory(string Code)
        {
            StaffCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New StaffCategory";
                model = new StaffCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit StaffCategory";
                model = _StaffCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/StaffCategory/_AddEditStaffCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/StaffCategory/SaveStaffCategory
        [HttpPost]
        public ActionResult SaveStaffCategory(StaffCategoryViewModel StaffCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = StaffCategory.StaffCategoryID;

                if (TempID == 0)
                {
                    StaffCategory.CreatedBy = "CMS Team";
                    StaffCategory.Source = "CMS Server";
                    StaffCategory = _StaffCategory.Create(StaffCategory);
                }
                else {
                    StaffCategory.LastModifiedBy = "CMS Team Update";
                    StaffCategory.Source = "CMS Server Update";
                    StaffCategory = _StaffCategory.Update(StaffCategory); }

                if (StaffCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/StaffCategory/_RowStaffCategory.cshtml", StaffCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New StaffCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "StaffCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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