using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DepartmentController : Controller
    {
        /// <summary>
        /// IDepartmentService object reference
        /// </summary>
        private IDepartmentService _Department = null;

        /// <summary>
        /// DepartmentController constructor For DepartmentService
        /// </summary>
        public DepartmentController()
        {
            _Department = new DepartmentService();
        }

        //
        // GET: /CMS/Department/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Department";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Department.GetAll();
            DepartmentViewModel model = new DepartmentViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Department/Index.cshtml", model);
        }

        //
        // POST: /CMS/Department/AddEditDepartment
        [HttpPost]
        public ActionResult AddEditDepartment(string Code)
        {
            DepartmentViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Department";
                model = new DepartmentViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Department";
                model = _Department.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Department/_AddEditDepartmentForm.cshtml", model);
        }

        //
        // POST: /CMS/Department/SaveDepartment
        [HttpPost]
        public ActionResult SaveDepartment(DepartmentViewModel Department)
        {
            if (ModelState.IsValid)
            {
                int TempID = Department.DepartmentID;

                if (TempID == 0)
                {
                    Department.CreatedBy = "CMS Team";
                    Department.Source = "CMS Server";
                    Department = _Department.Create(Department);
                }
                else {
                    Department.LastModifiedBy = "CMS Team Update";
                    Department.Source = "CMS Server Update";
                    Department = _Department.Update(Department); }

                if (Department != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Department/_RowDepartment.cshtml", Department);

                    if (TempID == 0)
                        return Json(new { Message = "New Department Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Department Updated Successfully", Status = true, Type = "Edit", Template = Template });
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