using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class VisitTypeController : Controller
    {
        /// <summary>
        /// IVisitTypeService object reference
        /// </summary>
        private IVisitTypeService _VisitType = null;

        /// <summary>
        /// VisitTypeController constructor For VisitTypeService
        /// </summary>
        public VisitTypeController()
        {
            _VisitType = new VisitTypeService();
        }

        //
        // GET: /CMS/VisitType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "VisitType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _VisitType.GetAll();
            VisitTypeViewModel model = new VisitTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/VisitType/Index.cshtml", model);
        }

        //
        // POST: /CMS/VisitType/AddEditVisitType
        [HttpPost]
        public ActionResult AddEditVisitType(string Code)
        {
            VisitTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New VisitType";
                model = new VisitTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit VisitType";
                model = _VisitType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/VisitType/_AddEditVisitTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/VisitType/SaveVisitType
        [HttpPost]
        public ActionResult SaveVisitType(VisitTypeViewModel VisitType)
        {
            if (ModelState.IsValid)
            {
                int TempID = VisitType.VisitTypeID;

                if (TempID == 0)
                {
                    VisitType.CreatedBy = "CMS Team";
                    VisitType.Source = "CMS Server";
                    VisitType = _VisitType.Create(VisitType);
                }
                else {
                    VisitType.LastModifiedBy = "CMS Team Update";
                    VisitType.Source = "CMS Server Update";
                    VisitType = _VisitType.Update(VisitType); }

                if (VisitType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/VisitType/_RowVisitType.cshtml", VisitType);

                    if (TempID == 0)
                        return Json(new { Message = "New VisitType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "VisitType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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