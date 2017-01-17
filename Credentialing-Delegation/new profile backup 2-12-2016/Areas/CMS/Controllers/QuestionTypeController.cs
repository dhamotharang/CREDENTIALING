using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class QuestionTypeController : Controller
    {
        /// <summary>
        /// IQuestionTypeService object reference
        /// </summary>
        private IQuestionTypeService _QuestionType = null;

        /// <summary>
        /// QuestionTypeController constructor For QuestionTypeService
        /// </summary>
        public QuestionTypeController()
        {
            _QuestionType = new QuestionTypeService();
        }

        //
        // GET: /CMS/QuestionType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "QuestionType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _QuestionType.GetAll();
            QuestionTypeViewModel model = new QuestionTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/QuestionType/Index.cshtml", model);
        }

        //
        // POST: /CMS/QuestionType/AddEditQuestionType
        [HttpPost]
        public ActionResult AddEditQuestionType(string Code)
        {
            QuestionTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New QuestionType";
                model = new QuestionTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit QuestionType";
                model = _QuestionType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/QuestionType/_AddEditQuestionTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/QuestionType/SaveQuestionType
        [HttpPost]
        public ActionResult SaveQuestionType(QuestionTypeViewModel QuestionType)
        {
            if (ModelState.IsValid)
            {
                int TempID = QuestionType.QuestionTypeID;

                if (TempID == 0)
                {
                    QuestionType.CreatedBy = "CMS Team";
                    QuestionType.Source = "CMS Server";
                    QuestionType = _QuestionType.Create(QuestionType);
                }
                else {
                    QuestionType.LastModifiedBy = "CMS Team Update";
                    QuestionType.Source = "CMS Server Update";
                    QuestionType = _QuestionType.Update(QuestionType); }

                if (QuestionType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/QuestionType/_RowQuestionType.cshtml", QuestionType);

                    if (TempID == 0)
                        return Json(new { Message = "New QuestionType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "QuestionType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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