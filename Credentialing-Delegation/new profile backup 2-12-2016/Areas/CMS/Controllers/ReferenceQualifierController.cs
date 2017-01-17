using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ReferenceQualifierController : Controller
    {
        /// <summary>
        /// IReferenceQualifierService object reference
        /// </summary>
        private IReferenceQualifierService _ReferenceQualifier = null;

        /// <summary>
        /// ReferenceQualifierController constructor For ReferenceQualifierService
        /// </summary>
        public ReferenceQualifierController()
        {
            _ReferenceQualifier = new ReferenceQualifierService();
        }

        //
        // GET: /CMS/ReferenceQualifier/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ReferenceQualifier";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ReferenceQualifier.GetAll();
            ReferenceQualifierViewModel model = new ReferenceQualifierViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ReferenceQualifier/Index.cshtml", model);
        }

        //
        // POST: /CMS/ReferenceQualifier/AddEditReferenceQualifier
        [HttpPost]
        public ActionResult AddEditReferenceQualifier(string Code)
        {
            ReferenceQualifierViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ReferenceQualifier";
                model = new ReferenceQualifierViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ReferenceQualifier";
                model = _ReferenceQualifier.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ReferenceQualifier/_AddEditReferenceQualifierForm.cshtml", model);
        }

        //
        // POST: /CMS/ReferenceQualifier/SaveReferenceQualifier
        [HttpPost]
        public ActionResult SaveReferenceQualifier(ReferenceQualifierViewModel ReferenceQualifier)
        {
            if (ModelState.IsValid)
            {
                int TempID = ReferenceQualifier.ReferenceQualifierID;

                if (TempID == 0)
                {
                    ReferenceQualifier.CreatedBy = "CMS Team";
                    ReferenceQualifier.Source = "CMS Server";
                    ReferenceQualifier = _ReferenceQualifier.Create(ReferenceQualifier);
                }
                else {
                    ReferenceQualifier.LastModifiedBy = "CMS Team Update";
                    ReferenceQualifier.Source = "CMS Server Update";
                    ReferenceQualifier = _ReferenceQualifier.Update(ReferenceQualifier); }

                if (ReferenceQualifier != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ReferenceQualifier/_RowReferenceQualifier.cshtml", ReferenceQualifier);

                    if (TempID == 0)
                        return Json(new { Message = "New ReferenceQualifier Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ReferenceQualifier Updated Successfully", Status = true, Type = "Edit", Template = Template });
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