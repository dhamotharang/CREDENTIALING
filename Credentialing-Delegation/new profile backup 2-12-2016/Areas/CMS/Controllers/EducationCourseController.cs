using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class EducationCourseController : Controller
    {
        /// <summary>
        /// IEducationCourseService object reference
        /// </summary>
        private IEducationCourseService _EducationCourse = null;

        /// <summary>
        /// EducationCourseController constructor For EducationCourseService
        /// </summary>
        public EducationCourseController()
        {
            _EducationCourse = new EducationCourseService();
        }

        //
        // GET: /CMS/EducationCourse/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "EducationCourse";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _EducationCourse.GetAll();
            EducationCourseViewModel model = new EducationCourseViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/EducationCourse/Index.cshtml", model);
        }

        //
        // POST: /CMS/EducationCourse/AddEditEducationCourse
        [HttpPost]
        public ActionResult AddEditEducationCourse(string Code)
        {
            EducationCourseViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New EducationCourse";
                model = new EducationCourseViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit EducationCourse";
                model = _EducationCourse.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/EducationCourse/_AddEditEducationCourseForm.cshtml", model);
        }

        //
        // POST: /CMS/EducationCourse/SaveEducationCourse
        [HttpPost]
        public ActionResult SaveEducationCourse(EducationCourseViewModel EducationCourse)
        {
            if (ModelState.IsValid)
            {
                int TempID = EducationCourse.EducationCourseID;

                if (TempID == 0)
                {
                    EducationCourse.CreatedBy = "CMS Team";
                    EducationCourse.Source = "CMS Server";
                    EducationCourse = _EducationCourse.Create(EducationCourse);
                }
                else {
                    EducationCourse.LastModifiedBy = "CMS Team Update";
                    EducationCourse.Source = "CMS Server Update";
                    EducationCourse = _EducationCourse.Update(EducationCourse); }

                if (EducationCourse != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/EducationCourse/_RowEducationCourse.cshtml", EducationCourse);

                    if (TempID == 0)
                        return Json(new { Message = "New EducationCourse Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "EducationCourse Updated Successfully", Status = true, Type = "Edit", Template = Template });
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