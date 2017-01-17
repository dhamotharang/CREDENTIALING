using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class EmploymentTypeController : Controller
    {
        /// <summary>
        /// IEmploymentTypeService object reference
        /// </summary>
        private IEmploymentTypeService _EmploymentType = null;

        /// <summary>
        /// EmploymentTypeController constructor For EmploymentTypeService
        /// </summary>
        public EmploymentTypeController()
        {
            _EmploymentType = new EmploymentTypeService();
        }

        //
        // GET: /CMS/EmploymentType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "EmploymentType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _EmploymentType.GetAll();
            EmploymentTypeViewModel model = new EmploymentTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/EmploymentType/Index.cshtml", model);
        }

        //
        // POST: /CMS/EmploymentType/AddEditEmploymentType
        [HttpPost]
        public ActionResult AddEditEmploymentType(string Code)
        {
            EmploymentTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New EmploymentType";
                model = new EmploymentTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit EmploymentType";
                model = _EmploymentType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/EmploymentType/_AddEditEmploymentTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/EmploymentType/SaveEmploymentType
        [HttpPost]
        public ActionResult SaveEmploymentType(EmploymentTypeViewModel EmploymentType)
        {
            if (ModelState.IsValid)
            {
                int TempID = EmploymentType.EmploymentTypeID;

                if (TempID == 0)
                {
                    EmploymentType.CreatedBy = "CMS Team";
                    EmploymentType.Source = "CMS Server";
                    EmploymentType = _EmploymentType.Create(EmploymentType);
                }
                else {
                    EmploymentType.LastModifiedBy = "CMS Team Update";
                    EmploymentType.Source = "CMS Server Update";
                    EmploymentType = _EmploymentType.Update(EmploymentType); }

                if (EmploymentType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/EmploymentType/_RowEmploymentType.cshtml", EmploymentType);

                    if (TempID == 0)
                        return Json(new { Message = "New EmploymentType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "EmploymentType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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