using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class MDCCodeController : Controller
    {
        /// <summary>
        /// IMDCCodeService object reference
        /// </summary>
        private IMDCCodeService _MDCCode = null;

        /// <summary>
        /// MDCCodeController constructor For MDCCodeService
        /// </summary>
        public MDCCodeController()
        {
            _MDCCode = new MDCCodeService();
        }

        //
        // GET: /CMS/MDCCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "MDC Code";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _MDCCode.GetAll();
            MDCCodeViewModel model = new MDCCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/MDCCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/MDCCode/AddEditMDCCode
        [HttpPost]
        public ActionResult AddEditMDCCode(string Code)
        {
            MDCCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New MDC Code";
                model = new MDCCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit MDC Code";
                model = _MDCCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/MDCCode/_AddEditMDCCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/MDCCode/SaveMDCCode
        [HttpPost]
        public ActionResult SaveMDCCode(MDCCodeViewModel MDCCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = MDCCode.MDCCodeID;

                if (TempID == 0)
                {
                    MDCCode.CreatedBy = "CMS Team";
                    MDCCode.Source = "CMS Server";
                    MDCCode = _MDCCode.Create(MDCCode);
                }
                else {
                    MDCCode.LastModifiedBy = "CMS Team Update";
                    MDCCode.Source = "CMS Server Update";
                    MDCCode = _MDCCode.Update(MDCCode); }

                if (MDCCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/MDCCode/_RowMDCCode.cshtml", MDCCode);

                    if (TempID == 0)
                        return Json(new { Message = "New MDC Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "MDC Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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