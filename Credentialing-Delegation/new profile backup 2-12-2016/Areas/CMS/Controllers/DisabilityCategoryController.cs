using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DisabilityCategoryController : Controller
    {
        /// <summary>
        /// IDisabilityCategoryService object reference
        /// </summary>
        private IDisabilityCategoryService _DisabilityCategory = null;

        /// <summary>
        /// DisabilityCategoryController constructor For DisabilityCategoryService
        /// </summary>
        public DisabilityCategoryController()
        {
            _DisabilityCategory = new DisabilityCategoryService();
        }

        //
        // GET: /CMS/DisabilityCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DisabilityCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DisabilityCategory.GetAll();
            DisabilityCategoryViewModel model = new DisabilityCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DisabilityCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/DisabilityCategory/AddEditDisabilityCategory
        [HttpPost]
        public ActionResult AddEditDisabilityCategory(string Code)
        {
            DisabilityCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DisabilityCategory";
                model = new DisabilityCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DisabilityCategory";
                model = _DisabilityCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DisabilityCategory/_AddEditDisabilityCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/DisabilityCategory/SaveDisabilityCategory
        [HttpPost]
        public ActionResult SaveDisabilityCategory(DisabilityCategoryViewModel DisabilityCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = DisabilityCategory.DisabilityCategoryID;

                if (TempID == 0)
                {
                    DisabilityCategory.CreatedBy = "CMS Team";
                    DisabilityCategory.Source = "CMS Server";
                    DisabilityCategory = _DisabilityCategory.Create(DisabilityCategory);
                }
                else {
                    DisabilityCategory.LastModifiedBy = "CMS Team Update";
                    DisabilityCategory.Source = "CMS Server Update";
                    DisabilityCategory = _DisabilityCategory.Update(DisabilityCategory); }

                if (DisabilityCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DisabilityCategory/_RowDisabilityCategory.cshtml", DisabilityCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New DisabilityCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DisabilityCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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