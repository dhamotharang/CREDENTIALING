using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class QuestionController : Controller
    {
        /// <summary>
        /// IQuestionService object reference
        /// </summary>
        private IQuestionService _Question = null;

        /// <summary>
        /// QuestionController constructor For QuestionService
        /// </summary>
        public QuestionController()
        {
            _Question = new QuestionService();
        }

        //
        // GET: /CMS/Question/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Question";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Question.GetAll();
            QuestionViewModel model = new QuestionViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Question/Index.cshtml", model);
        }

        //
        // POST: /CMS/Question/AddEditQuestion
        [HttpPost]
        public ActionResult AddEditQuestion(string Code)
        {
            QuestionViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Question";
                model = new QuestionViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Question";
                model = _Question.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Question/_AddEditQuestionForm.cshtml", model);
        }

        //
        // POST: /CMS/Question/SaveQuestion
        [HttpPost]
        public ActionResult SaveQuestion(QuestionViewModel Question)
        {
            if (ModelState.IsValid)
            {
                int TempID = Question.QuestionID;

                if (TempID == 0)
                {
                    Question.CreatedBy = "CMS Team";
                    Question.Source = "CMS Server";
                    Question = _Question.Create(Question);
                }
                else {
                    Question.LastModifiedBy = "CMS Team Update";
                    Question.Source = "CMS Server Update";
                    Question = _Question.Update(Question); }

                if (Question != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Question/_RowQuestion.cshtml", Question);

                    if (TempID == 0)
                        return Json(new { Message = "New Question Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Question Updated Successfully", Status = true, Type = "Edit", Template = Template });
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