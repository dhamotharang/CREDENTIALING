using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class NoteSubjectController : Controller
    {
        /// <summary>
        /// INoteSubjectService object reference
        /// </summary>
        private INoteSubjectService _NoteSubject = null;

        /// <summary>
        /// NoteSubjectController constructor For NoteSubjectService
        /// </summary>
        public NoteSubjectController()
        {
            _NoteSubject = new NoteSubjectService();
        }

        //
        // GET: /CMS/NoteSubject/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "NoteSubject";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _NoteSubject.GetAll();
            NoteSubjectViewModel model = new NoteSubjectViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/NoteSubject/Index.cshtml", model);
        }

        //
        // POST: /CMS/NoteSubject/AddEditNoteSubject
        [HttpPost]
        public ActionResult AddEditNoteSubject(string Code)
        {
            NoteSubjectViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New NoteSubject";
                model = new NoteSubjectViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit NoteSubject";
                model = _NoteSubject.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/NoteSubject/_AddEditNoteSubjectForm.cshtml", model);
        }

        //
        // POST: /CMS/NoteSubject/SaveNoteSubject
        [HttpPost]
        public ActionResult SaveNoteSubject(NoteSubjectViewModel NoteSubject)
        {
            if (ModelState.IsValid)
            {
                int TempID = NoteSubject.NoteSubjectID;

                if (TempID == 0)
                {
                    NoteSubject.CreatedBy = "CMS Team";
                    NoteSubject.Source = "CMS Server";
                    NoteSubject = _NoteSubject.Create(NoteSubject);
                }
                else {
                    NoteSubject.LastModifiedBy = "CMS Team Update";
                    NoteSubject.Source = "CMS Server Update";
                    NoteSubject = _NoteSubject.Update(NoteSubject); }

                if (NoteSubject != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/NoteSubject/_RowNoteSubject.cshtml", NoteSubject);

                    if (TempID == 0)
                        return Json(new { Message = "New NoteSubject Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "NoteSubject Updated Successfully", Status = true, Type = "Edit", Template = Template });
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