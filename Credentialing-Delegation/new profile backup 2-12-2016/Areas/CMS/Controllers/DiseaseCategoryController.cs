using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DiseaseCategoryController : Controller
    {
        /// <summary>
        /// IDiseaseCategoryService object reference
        /// </summary>
        private IDiseaseCategoryService _DiseaseCategory = null;

        /// <summary>
        /// DiseaseCategoryController constructor For DiseaseCategoryService
        /// </summary>
        public DiseaseCategoryController()
        {
            _DiseaseCategory = new DiseaseCategoryService();
        }

        //
        // GET: /CMS/DiseaseCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DiseaseCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DiseaseCategory.GetAll();
            DiseaseCategoryViewModel model = new DiseaseCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DiseaseCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/DiseaseCategory/AddEditDiseaseCategory
        [HttpPost]
        public ActionResult AddEditDiseaseCategory(string Code)
        {
            DiseaseCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DiseaseCategory";
                model = new DiseaseCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DiseaseCategory";
                model = _DiseaseCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DiseaseCategory/_AddEditDiseaseCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/DiseaseCategory/SaveDiseaseCategory
        [HttpPost]
        public ActionResult SaveDiseaseCategory(DiseaseCategoryViewModel DiseaseCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = DiseaseCategory.DiseaseCategoryID;

                if (TempID == 0)
                {
                    DiseaseCategory.CreatedBy = "CMS Team";
                    DiseaseCategory.Source = "CMS Server";
                    DiseaseCategory = _DiseaseCategory.Create(DiseaseCategory);
                }
                else {
                    DiseaseCategory.LastModifiedBy = "CMS Team Update";
                    DiseaseCategory.Source = "CMS Server Update";
                    DiseaseCategory = _DiseaseCategory.Update(DiseaseCategory); }

                if (DiseaseCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DiseaseCategory/_RowDiseaseCategory.cshtml", DiseaseCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New DiseaseCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DiseaseCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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