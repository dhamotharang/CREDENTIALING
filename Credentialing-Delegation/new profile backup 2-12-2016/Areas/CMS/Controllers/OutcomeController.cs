using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class OutcomeController : Controller
    {
        /// <summary>
        /// IOutcomeService object reference
        /// </summary>
        private IOutcomeService _Outcome = null;

        /// <summary>
        /// OutcomeController constructor For OutcomeService
        /// </summary>
        public OutcomeController()
        {
            _Outcome = new OutcomeService();
        }

        //
        // GET: /CMS/Outcome/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Outcome";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Outcome.GetAll();
            OutcomeViewModel model = new OutcomeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Outcome/Index.cshtml", model);
        }

        //
        // POST: /CMS/Outcome/AddEditOutcome
        [HttpPost]
        public ActionResult AddEditOutcome(string Code)
        {
            OutcomeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Outcome";
                model = new OutcomeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Outcome";
                model = _Outcome.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Outcome/_AddEditOutcomeForm.cshtml", model);
        }

        //
        // POST: /CMS/Outcome/SaveOutcome
        [HttpPost]
        public ActionResult SaveOutcome(OutcomeViewModel Outcome)
        {
            if (ModelState.IsValid)
            {
                int TempID = Outcome.OutcomeID;

                if (TempID == 0)
                {
                    Outcome.CreatedBy = "CMS Team";
                    Outcome.Source = "CMS Server";
                    Outcome = _Outcome.Create(Outcome);
                }
                else {
                    Outcome.LastModifiedBy = "CMS Team Update";
                    Outcome.Source = "CMS Server Update";
                    Outcome = _Outcome.Update(Outcome); }

                if (Outcome != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Outcome/_RowOutcome.cshtml", Outcome);

                    if (TempID == 0)
                        return Json(new { Message = "New Outcome Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Outcome Updated Successfully", Status = true, Type = "Edit", Template = Template });
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