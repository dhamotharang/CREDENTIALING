using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DateQualifierController : Controller
    {
        /// <summary>
        /// IDateQualifierService object reference
        /// </summary>
        private IDateQualifierService _DateQualifier = null;

        /// <summary>
        /// DateQualifierController constructor For DateQualifierService
        /// </summary>
        public DateQualifierController()
        {
            _DateQualifier = new DateQualifierService();
        }

        //
        // GET: /CMS/DateQualifier/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DateQualifier";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DateQualifier.GetAll();
            DateQualifierViewModel model = new DateQualifierViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DateQualifier/Index.cshtml", model);
        }

        //
        // POST: /CMS/DateQualifier/AddEditDateQualifier
        [HttpPost]
        public ActionResult AddEditDateQualifier(string Code)
        {
            DateQualifierViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DateQualifier";
                model = new DateQualifierViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DateQualifier";
                model = _DateQualifier.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DateQualifier/_AddEditDateQualifierForm.cshtml", model);
        }

        //
        // POST: /CMS/DateQualifier/SaveDateQualifier
        [HttpPost]
        public ActionResult SaveDateQualifier(DateQualifierViewModel DateQualifier)
        {
            if (ModelState.IsValid)
            {
                int TempID = DateQualifier.DateQualifierID;

                if (TempID == 0)
                {
                    DateQualifier.CreatedBy = "CMS Team";
                    DateQualifier.Source = "CMS Server";
                    DateQualifier = _DateQualifier.Create(DateQualifier);
                }
                else {
                    DateQualifier.LastModifiedBy = "CMS Team Update";
                    DateQualifier.Source = "CMS Server Update";
                    DateQualifier = _DateQualifier.Update(DateQualifier); }

                if (DateQualifier != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DateQualifier/_RowDateQualifier.cshtml", DateQualifier);

                    if (TempID == 0)
                        return Json(new { Message = "New DateQualifier Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DateQualifier Updated Successfully", Status = true, Type = "Edit", Template = Template });
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