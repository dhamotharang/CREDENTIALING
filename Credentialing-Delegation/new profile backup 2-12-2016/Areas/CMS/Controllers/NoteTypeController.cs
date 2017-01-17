using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class NoteTypeController : Controller
    {
        /// <summary>
        /// INoteTypeService object reference
        /// </summary>
        private INoteTypeService _NoteType = null;

        /// <summary>
        /// NoteTypeController constructor For NoteTypeService
        /// </summary>
        public NoteTypeController()
        {
            _NoteType = new NoteTypeService();
        }

        //
        // GET: /CMS/NoteType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "NoteType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _NoteType.GetAll();
            NoteTypeViewModel model = new NoteTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/NoteType/Index.cshtml", model);
        }

        //
        // POST: /CMS/NoteType/AddEditNoteType
        [HttpPost]
        public ActionResult AddEditNoteType(string Code)
        {
            NoteTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New NoteType";
                model = new NoteTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit NoteType";
                model = _NoteType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/NoteType/_AddEditNoteTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/NoteType/SaveNoteType
        [HttpPost]
        public ActionResult SaveNoteType(NoteTypeViewModel NoteType)
        {
            if (ModelState.IsValid)
            {
                int TempID = NoteType.NoteTypeID;

                if (TempID == 0)
                {
                    NoteType.CreatedBy = "CMS Team";
                    NoteType.Source = "CMS Server";
                    NoteType = _NoteType.Create(NoteType);
                }
                else {
                    NoteType.LastModifiedBy = "CMS Team Update";
                    NoteType.Source = "CMS Server Update";
                    NoteType = _NoteType.Update(NoteType); }

                if (NoteType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/NoteType/_RowNoteType.cshtml", NoteType);

                    if (TempID == 0)
                        return Json(new { Message = "New NoteType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "NoteType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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