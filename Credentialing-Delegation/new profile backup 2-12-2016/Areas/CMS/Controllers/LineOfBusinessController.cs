using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class LineOfBusinessController : Controller
    {
        /// <summary>
        /// ILineOfBusinessService object reference
        /// </summary>
        private ILineOfBusinessService _LineOfBusiness = null;

        /// <summary>
        /// LineOfBusinessController constructor For LineOfBusinessService
        /// </summary>
        public LineOfBusinessController()
        {
            _LineOfBusiness = new LineOfBusinessService();
        }

        //
        // GET: /CMS/LineOfBusiness/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "LineOfBusiness";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _LineOfBusiness.GetAll();
            LineOfBusinessViewModel model = new LineOfBusinessViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/LineOfBusiness/Index.cshtml", model);
        }

        //
        // POST: /CMS/LineOfBusiness/AddEditLineOfBusiness
        [HttpPost]
        public ActionResult AddEditLineOfBusiness(string Code)
        {
            LineOfBusinessViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New LineOfBusiness";
                model = new LineOfBusinessViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit LineOfBusiness";
                model = _LineOfBusiness.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/LineOfBusiness/_AddEditLineOfBusinessForm.cshtml", model);
        }

        //
        // POST: /CMS/LineOfBusiness/SaveLineOfBusiness
        [HttpPost]
        public ActionResult SaveLineOfBusiness(LineOfBusinessViewModel LineOfBusiness)
        {
            if (ModelState.IsValid)
            {
                int TempID = LineOfBusiness.LineOfBusinessID;

                if (TempID == 0)
                {
                    LineOfBusiness.CreatedBy = "CMS Team";
                    LineOfBusiness.Source = "CMS Server";
                    LineOfBusiness = _LineOfBusiness.Create(LineOfBusiness);
                }
                else {
                    LineOfBusiness.LastModifiedBy = "CMS Team Update";
                    LineOfBusiness.Source = "CMS Server Update";
                    LineOfBusiness = _LineOfBusiness.Update(LineOfBusiness); }

                if (LineOfBusiness != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/LineOfBusiness/_RowLineOfBusiness.cshtml", LineOfBusiness);

                    if (TempID == 0)
                        return Json(new { Message = "New LineOfBusiness Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "LineOfBusiness Updated Successfully", Status = true, Type = "Edit", Template = Template });
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