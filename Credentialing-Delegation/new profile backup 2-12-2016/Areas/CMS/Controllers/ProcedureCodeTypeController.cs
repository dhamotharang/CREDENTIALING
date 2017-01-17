using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ProcedureCodeTypeController : Controller
    {
        /// <summary>
        /// IProcedureCodeTypeService object reference
        /// </summary>
        private IProcedureCodeTypeService _ProcedureCodeType = null;

        /// <summary>
        /// ProcedureCodeTypeController constructor For ProcedureCodeTypeService
        /// </summary>
        public ProcedureCodeTypeController()
        {
            _ProcedureCodeType = new ProcedureCodeTypeService();
        }

        //
        // GET: /CMS/ProcedureCodeType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ProcedureCodeType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ProcedureCodeType.GetAll();
            ProcedureCodeTypeViewModel model = new ProcedureCodeTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ProcedureCodeType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ProcedureCodeType/AddEditProcedureCodeType
        [HttpPost]
        public ActionResult AddEditProcedureCodeType(string Code)
        {
            ProcedureCodeTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ProcedureCodeType";
                model = new ProcedureCodeTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ProcedureCodeType";
                model = _ProcedureCodeType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ProcedureCodeType/_AddEditProcedureCodeTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ProcedureCodeType/SaveProcedureCodeType
        [HttpPost]
        public ActionResult SaveProcedureCodeType(ProcedureCodeTypeViewModel ProcedureCodeType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ProcedureCodeType.ProcedureCodeTypeID;

                if (TempID == 0)
                {
                    ProcedureCodeType.CreatedBy = "CMS Team";
                    ProcedureCodeType.Source = "CMS Server";
                    ProcedureCodeType = _ProcedureCodeType.Create(ProcedureCodeType);
                }
                else {
                    ProcedureCodeType.LastModifiedBy = "CMS Team Update";
                    ProcedureCodeType.Source = "CMS Server Update";
                    ProcedureCodeType = _ProcedureCodeType.Update(ProcedureCodeType); }

                if (ProcedureCodeType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ProcedureCodeType/_RowProcedureCodeType.cshtml", ProcedureCodeType);

                    if (TempID == 0)
                        return Json(new { Message = "New ProcedureCodeType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ProcedureCodeType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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