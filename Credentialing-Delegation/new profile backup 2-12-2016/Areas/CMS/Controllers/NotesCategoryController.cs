using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class NotesCategoryController : Controller
    {
        /// <summary>
        /// INotesCategoryService object reference
        /// </summary>
        private INotesCategoryService _NotesCategory = null;

        /// <summary>
        /// NotesCategoryController constructor For NotesCategoryService
        /// </summary>
        public NotesCategoryController()
        {
            _NotesCategory = new NotesCategoryService();
        }

        //
        // GET: /CMS/NotesCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "NotesCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _NotesCategory.GetAll();
            NotesCategoryViewModel model = new NotesCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/NotesCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/NotesCategory/AddEditNotesCategory
        [HttpPost]
        public ActionResult AddEditNotesCategory(string Code)
        {
            NotesCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New NotesCategory";
                model = new NotesCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit NotesCategory";
                model = _NotesCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/NotesCategory/_AddEditNotesCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/NotesCategory/SaveNotesCategory
        [HttpPost]
        public ActionResult SaveNotesCategory(NotesCategoryViewModel NotesCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = NotesCategory.NotesCategoryID;

                if (TempID == 0)
                {
                    NotesCategory.CreatedBy = "CMS Team";
                    NotesCategory.Source = "CMS Server";
                    NotesCategory = _NotesCategory.Create(NotesCategory);
                }
                else {
                    NotesCategory.LastModifiedBy = "CMS Team Update";
                    NotesCategory.Source = "CMS Server Update";
                    NotesCategory = _NotesCategory.Update(NotesCategory); }

                if (NotesCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/NotesCategory/_RowNotesCategory.cshtml", NotesCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New NotesCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "NotesCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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