using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class EDISegmentQualifierController : Controller
    {
        /// <summary>
        /// IEDISegmentQualifierService object reference
        /// </summary>
        private IEDISegmentQualifierService _EDISegmentQualifier = null;

        /// <summary>
        /// EDISegmentQualifierController constructor For EDISegmentQualifierService
        /// </summary>
        public EDISegmentQualifierController()
        {
            _EDISegmentQualifier = new EDISegmentQualifierService();
        }

        //
        // GET: /CMS/EDISegmentQualifier/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "EDISegmentQualifier";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _EDISegmentQualifier.GetAll();
            EDISegmentQualifierViewModel model = new EDISegmentQualifierViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/EDISegmentQualifier/Index.cshtml", model);
        }

        //
        // POST: /CMS/EDISegmentQualifier/AddEditEDISegmentQualifier
        [HttpPost]
        public ActionResult AddEditEDISegmentQualifier(string Code)
        {
            EDISegmentQualifierViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New EDISegmentQualifier";
                model = new EDISegmentQualifierViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit EDISegmentQualifier";
                model = _EDISegmentQualifier.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/EDISegmentQualifier/_AddEditEDISegmentQualifierForm.cshtml", model);
        }

        //
        // POST: /CMS/EDISegmentQualifier/SaveEDISegmentQualifier
        [HttpPost]
        public ActionResult SaveEDISegmentQualifier(EDISegmentQualifierViewModel EDISegmentQualifier)
        {
            if (ModelState.IsValid)
            {
                int TempID = EDISegmentQualifier.EDISegmentQualifierID;

                if (TempID == 0)
                {
                    EDISegmentQualifier.CreatedBy = "CMS Team";
                    EDISegmentQualifier.Source = "CMS Server";
                    EDISegmentQualifier = _EDISegmentQualifier.Create(EDISegmentQualifier);
                }
                else {
                    EDISegmentQualifier.LastModifiedBy = "CMS Team Update";
                    EDISegmentQualifier.Source = "CMS Server Update";
                    EDISegmentQualifier = _EDISegmentQualifier.Update(EDISegmentQualifier); }

                if (EDISegmentQualifier != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/EDISegmentQualifier/_RowEDISegmentQualifier.cshtml", EDISegmentQualifier);

                    if (TempID == 0)
                        return Json(new { Message = "New EDISegmentQualifier Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "EDISegmentQualifier Updated Successfully", Status = true, Type = "Edit", Template = Template });
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