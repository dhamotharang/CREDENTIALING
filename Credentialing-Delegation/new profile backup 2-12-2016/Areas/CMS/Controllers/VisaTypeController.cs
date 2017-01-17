using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class VisaTypeController : Controller
    {
        /// <summary>
        /// IVisaTypeService object reference
        /// </summary>
        private IVisaTypeService _VisaType = null;

        /// <summary>
        /// VisaTypeController constructor For VisaTypeService
        /// </summary>
        public VisaTypeController()
        {
            _VisaType = new VisaTypeService();
        }

        //
        // GET: /CMS/VisaType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "VisaType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _VisaType.GetAll();
            VisaTypeViewModel model = new VisaTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/VisaType/Index.cshtml", model);
        }

        //
        // POST: /CMS/VisaType/AddEditVisaType
        [HttpPost]
        public ActionResult AddEditVisaType(string Code)
        {
            VisaTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New VisaType";
                model = new VisaTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit VisaType";
                model = _VisaType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/VisaType/_AddEditVisaTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/VisaType/SaveVisaType
        [HttpPost]
        public ActionResult SaveVisaType(VisaTypeViewModel VisaType)
        {
            if (ModelState.IsValid)
            {
                int TempID = VisaType.VisaTypeID;

                if (TempID == 0)
                {
                    VisaType.CreatedBy = "CMS Team";
                    VisaType.Source = "CMS Server";
                    VisaType = _VisaType.Create(VisaType);
                }
                else {
                    VisaType.LastModifiedBy = "CMS Team Update";
                    VisaType.Source = "CMS Server Update";
                    VisaType = _VisaType.Update(VisaType); }

                if (VisaType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/VisaType/_RowVisaType.cshtml", VisaType);

                    if (TempID == 0)
                        return Json(new { Message = "New VisaType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "VisaType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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