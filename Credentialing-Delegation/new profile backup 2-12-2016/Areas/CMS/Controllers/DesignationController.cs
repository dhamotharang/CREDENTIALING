using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DesignationController : Controller
    {
        /// <summary>
        /// IDesignationService object reference
        /// </summary>
        private IDesignationService _Designation = null;

        /// <summary>
        /// DesignationController constructor For DesignationService
        /// </summary>
        public DesignationController()
        {
            _Designation = new DesignationService();
        }

        //
        // GET: /CMS/Designation/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Designation";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Designation.GetAll();
            DesignationViewModel model = new DesignationViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Designation/Index.cshtml", model);
        }

        //
        // POST: /CMS/Designation/AddEditDesignation
        [HttpPost]
        public ActionResult AddEditDesignation(string Code)
        {
            DesignationViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Designation";
                model = new DesignationViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Designation";
                model = _Designation.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Designation/_AddEditDesignationForm.cshtml", model);
        }

        //
        // POST: /CMS/Designation/SaveDesignation
        [HttpPost]
        public ActionResult SaveDesignation(DesignationViewModel Designation)
        {
            if (ModelState.IsValid)
            {
                int TempID = Designation.DesignationID;

                if (TempID == 0)
                {
                    Designation.CreatedBy = "CMS Team";
                    Designation.Source = "CMS Server";
                    Designation = _Designation.Create(Designation);
                }
                else {
                    Designation.LastModifiedBy = "CMS Team Update";
                    Designation.Source = "CMS Server Update";
                    Designation = _Designation.Update(Designation); }

                if (Designation != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Designation/_RowDesignation.cshtml", Designation);

                    if (TempID == 0)
                        return Json(new { Message = "New Designation Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Designation Updated Successfully", Status = true, Type = "Edit", Template = Template });
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