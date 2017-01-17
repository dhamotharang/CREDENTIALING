using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ComplaintCategoryController : Controller
    {
        /// <summary>
        /// IComplaintCategoryService object reference
        /// </summary>
        private IComplaintCategoryService _ComplaintCategory = null;

        /// <summary>
        /// ComplaintCategoryController constructor For ComplaintCategoryService
        /// </summary>
        public ComplaintCategoryController()
        {
            _ComplaintCategory = new ComplaintCategoryService();
        }

        //
        // GET: /CMS/ComplaintCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ComplaintCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ComplaintCategory.GetAll();
            ComplaintCategoryViewModel model = new ComplaintCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ComplaintCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/ComplaintCategory/AddEditComplaintCategory
        [HttpPost]
        public ActionResult AddEditComplaintCategory(string Code)
        {
            ComplaintCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ComplaintCategory";
                model = new ComplaintCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ComplaintCategory";
                model = _ComplaintCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ComplaintCategory/_AddEditComplaintCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/ComplaintCategory/SaveComplaintCategory
        [HttpPost]
        public ActionResult SaveComplaintCategory(ComplaintCategoryViewModel ComplaintCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = ComplaintCategory.ComplaintCategoryID;

                if (TempID == 0)
                {
                    ComplaintCategory.CreatedBy = "CMS Team";
                    ComplaintCategory.Source = "CMS Server";
                    ComplaintCategory = _ComplaintCategory.Create(ComplaintCategory);
                }
                else {
                    ComplaintCategory.LastModifiedBy = "CMS Team Update";
                    ComplaintCategory.Source = "CMS Server Update";
                    ComplaintCategory = _ComplaintCategory.Update(ComplaintCategory); }

                if (ComplaintCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ComplaintCategory/_RowComplaintCategory.cshtml", ComplaintCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New ComplaintCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ComplaintCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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