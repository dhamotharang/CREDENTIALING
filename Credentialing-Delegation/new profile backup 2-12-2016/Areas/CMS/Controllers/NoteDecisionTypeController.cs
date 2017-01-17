using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class NoteDecisionTypeController : Controller
    {
        /// <summary>
        /// INoteDecisionTypeService object reference
        /// </summary>
        private INoteDecisionTypeService _NoteDecisionType = null;

        /// <summary>
        /// NoteDecisionTypeController constructor For NoteDecisionTypeService
        /// </summary>
        public NoteDecisionTypeController()
        {
            _NoteDecisionType = new NoteDecisionTypeService();
        }

        //
        // GET: /CMS/NoteDecisionType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "NoteDecisionType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _NoteDecisionType.GetAll();
            NoteDecisionTypeViewModel model = new NoteDecisionTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/NoteDecisionType/Index.cshtml", model);
        }

        //
        // POST: /CMS/NoteDecisionType/AddEditNoteDecisionType
        [HttpPost]
        public ActionResult AddEditNoteDecisionType(string Code)
        {
            NoteDecisionTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New NoteDecisionType";
                model = new NoteDecisionTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit NoteDecisionType";
                model = _NoteDecisionType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/NoteDecisionType/_AddEditNoteDecisionTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/NoteDecisionType/SaveNoteDecisionType
        [HttpPost]
        public ActionResult SaveNoteDecisionType(NoteDecisionTypeViewModel NoteDecisionType)
        {
            if (ModelState.IsValid)
            {
                int TempID = NoteDecisionType.NoteDecisionTypeID;

                if (TempID == 0)
                {
                    NoteDecisionType.CreatedBy = "CMS Team";
                    NoteDecisionType.Source = "CMS Server";
                    NoteDecisionType = _NoteDecisionType.Create(NoteDecisionType);
                }
                else {
                    NoteDecisionType.LastModifiedBy = "CMS Team Update";
                    NoteDecisionType.Source = "CMS Server Update";
                    NoteDecisionType = _NoteDecisionType.Update(NoteDecisionType); }

                if (NoteDecisionType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/NoteDecisionType/_RowNoteDecisionType.cshtml", NoteDecisionType);

                    if (TempID == 0)
                        return Json(new { Message = "New NoteDecisionType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "NoteDecisionType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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