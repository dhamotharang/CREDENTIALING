using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class LetterTemplateController : Controller
    {
        /// <summary>
        /// ILetterTemplateService object reference
        /// </summary>
        private ILetterTemplateService _LetterTemplate = null;

        /// <summary>
        /// LetterTemplateController constructor For LetterTemplateService
        /// </summary>
        public LetterTemplateController()
        {
            _LetterTemplate = new LetterTemplateService();
        }

        //
        // GET: /CMS/LetterTemplate/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "LetterTemplate";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _LetterTemplate.GetAll();
            LetterTemplateViewModel model = new LetterTemplateViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/LetterTemplate/Index.cshtml", model);
        }

        //
        // POST: /CMS/LetterTemplate/AddEditLetterTemplate
        [HttpPost]
        public ActionResult AddEditLetterTemplate(string Code)
        {
            LetterTemplateViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New LetterTemplate";
                model = new LetterTemplateViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit LetterTemplate";
                model = _LetterTemplate.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/LetterTemplate/_AddEditLetterTemplateForm.cshtml", model);
        }

        //
        // POST: /CMS/LetterTemplate/SaveLetterTemplate
        [HttpPost]
        public ActionResult SaveLetterTemplate(LetterTemplateViewModel LetterTemplate)
        {
            if (ModelState.IsValid)
            {
                int TempID = LetterTemplate.LetterTemplateID;

                if (TempID == 0)
                {
                    LetterTemplate.CreatedBy = "CMS Team";
                    LetterTemplate.Source = "CMS Server";
                    LetterTemplate = _LetterTemplate.Create(LetterTemplate);
                }
                else {
                    LetterTemplate.LastModifiedBy = "CMS Team Update";
                    LetterTemplate.Source = "CMS Server Update";
                    LetterTemplate = _LetterTemplate.Update(LetterTemplate); }

                if (LetterTemplate != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/LetterTemplate/_RowLetterTemplate.cshtml", LetterTemplate);

                    if (TempID == 0)
                        return Json(new { Message = "New LetterTemplate Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "LetterTemplate Updated Successfully", Status = true, Type = "Edit", Template = Template });
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