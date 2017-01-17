using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class QuestionCategoryController : Controller
    {
        /// <summary>
        /// IQuestionCategoryService object reference
        /// </summary>
        private IQuestionCategoryService _QuestionCategory = null;

        /// <summary>
        /// QuestionCategoryController constructor For QuestionCategoryService
        /// </summary>
        public QuestionCategoryController()
        {
            _QuestionCategory = new QuestionCategoryService();
        }

        //
        // GET: /CMS/QuestionCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "QuestionCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _QuestionCategory.GetAll();
            QuestionCategoryViewModel model = new QuestionCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/QuestionCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/QuestionCategory/AddEditQuestionCategory
        [HttpPost]
        public ActionResult AddEditQuestionCategory(string Code)
        {
            QuestionCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New QuestionCategory";
                model = new QuestionCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit QuestionCategory";
                model = _QuestionCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/QuestionCategory/_AddEditQuestionCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/QuestionCategory/SaveQuestionCategory
        [HttpPost]
        public ActionResult SaveQuestionCategory(QuestionCategoryViewModel QuestionCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = QuestionCategory.QuestionCategoryID;

                if (TempID == 0)
                {
                    QuestionCategory.CreatedBy = "CMS Team";
                    QuestionCategory.Source = "CMS Server";
                    QuestionCategory = _QuestionCategory.Create(QuestionCategory);
                }
                else {
                    QuestionCategory.LastModifiedBy = "CMS Team Update";
                    QuestionCategory.Source = "CMS Server Update";
                    QuestionCategory = _QuestionCategory.Update(QuestionCategory); }

                if (QuestionCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/QuestionCategory/_RowQuestionCategory.cshtml", QuestionCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New QuestionCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "QuestionCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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