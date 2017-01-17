using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class SeverityController : Controller
    {
        /// <summary>
        /// ISeverityService object reference
        /// </summary>
        private ISeverityService _Severity = null;

        /// <summary>
        /// SeverityController constructor For SeverityService
        /// </summary>
        public SeverityController()
        {
            _Severity = new SeverityService();
        }

        //
        // GET: /CMS/Severity/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Severity";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Severity.GetAll();
            SeverityViewModel model = new SeverityViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Severity/Index.cshtml", model);
        }

        //
        // POST: /CMS/Severity/AddEditSeverity
        [HttpPost]
        public ActionResult AddEditSeverity(string Code)
        {
            SeverityViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Severity";
                model = new SeverityViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Severity";
                model = _Severity.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Severity/_AddEditSeverityForm.cshtml", model);
        }

        //
        // POST: /CMS/Severity/SaveSeverity
        [HttpPost]
        public ActionResult SaveSeverity(SeverityViewModel Severity)
        {
            if (ModelState.IsValid)
            {
                int TempID = Severity.SeverityID;

                if (TempID == 0)
                {
                    Severity.CreatedBy = "CMS Team";
                    Severity.Source = "CMS Server";
                    Severity = _Severity.Create(Severity);
                }
                else {
                    Severity.LastModifiedBy = "CMS Team Update";
                    Severity.Source = "CMS Server Update";
                    Severity = _Severity.Update(Severity); }

                if (Severity != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Severity/_RowSeverity.cshtml", Severity);

                    if (TempID == 0)
                        return Json(new { Message = "New Severity Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Severity Updated Successfully", Status = true, Type = "Edit", Template = Template });
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