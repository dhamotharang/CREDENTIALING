using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class NotesTemplateController : Controller
    {
        /// <summary>
        /// INotesTemplateService object reference
        /// </summary>
        private INotesTemplateService _NotesTemplate = null;

        /// <summary>
        /// NotesTemplateController constructor For NotesTemplateService
        /// </summary>
        public NotesTemplateController()
        {
            _NotesTemplate = new NotesTemplateService();
        }

        //
        // GET: /CMS/NotesTemplate/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "NotesTemplate";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _NotesTemplate.GetAll();
            NotesTemplateViewModel model = new NotesTemplateViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/NotesTemplate/Index.cshtml", model);
        }

        //
        // POST: /CMS/NotesTemplate/AddEditNotesTemplate
        [HttpPost]
        public ActionResult AddEditNotesTemplate(string Code)
        {
            NotesTemplateViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New NotesTemplate";
                model = new NotesTemplateViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit NotesTemplate";
                model = _NotesTemplate.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/NotesTemplate/_AddEditNotesTemplateForm.cshtml", model);
        }

        //
        // POST: /CMS/NotesTemplate/SaveNotesTemplate
        [HttpPost]
        public ActionResult SaveNotesTemplate(NotesTemplateViewModel NotesTemplate)
        {
            if (ModelState.IsValid)
            {
                int TempID = NotesTemplate.NotesTemplateID;

                if (TempID == 0)
                {
                    NotesTemplate.CreatedBy = "CMS Team";
                    NotesTemplate.Source = "CMS Server";
                    NotesTemplate = _NotesTemplate.Create(NotesTemplate);
                }
                else {
                    NotesTemplate.LastModifiedBy = "CMS Team Update";
                    NotesTemplate.Source = "CMS Server Update";
                    NotesTemplate = _NotesTemplate.Update(NotesTemplate); }

                if (NotesTemplate != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/NotesTemplate/_RowNotesTemplate.cshtml", NotesTemplate);

                    if (TempID == 0)
                        return Json(new { Message = "New NotesTemplate Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "NotesTemplate Updated Successfully", Status = true, Type = "Edit", Template = Template });
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