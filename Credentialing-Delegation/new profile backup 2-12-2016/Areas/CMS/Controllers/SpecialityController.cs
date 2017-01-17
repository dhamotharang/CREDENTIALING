using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class SpecialityController : Controller
    {
        /// <summary>
        /// ISpecialityService object reference
        /// </summary>
        private ISpecialityService _Speciality = null;

        /// <summary>
        /// SpecialityController constructor For SpecialityService
        /// </summary>
        public SpecialityController()
        {
            _Speciality = new SpecialityService();
        }

        //
        // GET: /CMS/Speciality/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Speciality";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Speciality.GetAll();
            SpecialityViewModel model = new SpecialityViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Speciality/Index.cshtml", model);
        }

        //
        // POST: /CMS/Speciality/AddEditSpeciality
        [HttpPost]
        public ActionResult AddEditSpeciality(string Code)
        {
            SpecialityViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Speciality";
                model = new SpecialityViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Speciality";
                model = _Speciality.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Speciality/_AddEditSpecialityForm.cshtml", model);
        }

        //
        // POST: /CMS/Speciality/SaveSpeciality
        [HttpPost]
        public ActionResult SaveSpeciality(SpecialityViewModel Speciality)
        {
            if (ModelState.IsValid)
            {
                int TempID = Speciality.SpecialityID;

                if (TempID == 0)
                {
                    Speciality.CreatedBy = "CMS Team";
                    Speciality.Source = "CMS Server";
                    Speciality = _Speciality.Create(Speciality);
                }
                else {
                    Speciality.LastModifiedBy = "CMS Team Update";
                    Speciality.Source = "CMS Server Update";
                    Speciality = _Speciality.Update(Speciality); }

                if (Speciality != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Speciality/_RowSpeciality.cshtml", Speciality);

                    if (TempID == 0)
                        return Json(new { Message = "New Speciality Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Speciality Updated Successfully", Status = true, Type = "Edit", Template = Template });
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