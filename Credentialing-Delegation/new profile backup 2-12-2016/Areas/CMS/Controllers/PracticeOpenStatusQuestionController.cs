using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class PracticeOpenStatusQuestionController : Controller
    {
        /// <summary>
        /// IPracticeOpenStatusQuestionService object reference
        /// </summary>
        private IPracticeOpenStatusQuestionService _PracticeOpenStatusQuestion = null;

        /// <summary>
        /// PracticeOpenStatusQuestionController constructor For PracticeOpenStatusQuestionService
        /// </summary>
        public PracticeOpenStatusQuestionController()
        {
            _PracticeOpenStatusQuestion = new PracticeOpenStatusQuestionService();
        }

        //
        // GET: /CMS/PracticeOpenStatusQuestion/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "PracticeOpenStatusQuestion";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _PracticeOpenStatusQuestion.GetAll();
            PracticeOpenStatusQuestionViewModel model = new PracticeOpenStatusQuestionViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/PracticeOpenStatusQuestion/Index.cshtml", model);
        }

        //
        // POST: /CMS/PracticeOpenStatusQuestion/AddEditPracticeOpenStatusQuestion
        [HttpPost]
        public ActionResult AddEditPracticeOpenStatusQuestion(string Code)
        {
            PracticeOpenStatusQuestionViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New PracticeOpenStatusQuestion";
                model = new PracticeOpenStatusQuestionViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit PracticeOpenStatusQuestion";
                model = _PracticeOpenStatusQuestion.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/PracticeOpenStatusQuestion/_AddEditPracticeOpenStatusQuestionForm.cshtml", model);
        }

        //
        // POST: /CMS/PracticeOpenStatusQuestion/SavePracticeOpenStatusQuestion
        [HttpPost]
        public ActionResult SavePracticeOpenStatusQuestion(PracticeOpenStatusQuestionViewModel PracticeOpenStatusQuestion)
        {
            if (ModelState.IsValid)
            {
                int TempID = PracticeOpenStatusQuestion.PracticeOpenStatusQuestionID;

                if (TempID == 0)
                {
                    PracticeOpenStatusQuestion.CreatedBy = "CMS Team";
                    PracticeOpenStatusQuestion.Source = "CMS Server";
                    PracticeOpenStatusQuestion = _PracticeOpenStatusQuestion.Create(PracticeOpenStatusQuestion);
                }
                else {
                    PracticeOpenStatusQuestion.LastModifiedBy = "CMS Team Update";
                    PracticeOpenStatusQuestion.Source = "CMS Server Update";
                    PracticeOpenStatusQuestion = _PracticeOpenStatusQuestion.Update(PracticeOpenStatusQuestion); }

                if (PracticeOpenStatusQuestion != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/PracticeOpenStatusQuestion/_RowPracticeOpenStatusQuestion.cshtml", PracticeOpenStatusQuestion);

                    if (TempID == 0)
                        return Json(new { Message = "New PracticeOpenStatusQuestion Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "PracticeOpenStatusQuestion Updated Successfully", Status = true, Type = "Edit", Template = Template });
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