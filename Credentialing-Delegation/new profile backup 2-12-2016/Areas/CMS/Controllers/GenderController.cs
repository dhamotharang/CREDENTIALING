using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class GenderController : Controller
    {
        /// <summary>
        /// IGenderService object reference
        /// </summary>
        private IGenderService _Gender = null;

        /// <summary>
        /// GenderController constructor For GenderService
        /// </summary>
        public GenderController()
        {
            _Gender = new GenderService();
        }

        //
        // GET: /CMS/Gender/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Gender";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Gender.GetAll();
            GenderViewModel model = new GenderViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Gender/Index.cshtml", model);
        }

        //
        // POST: /CMS/Gender/AddEditGender
        [HttpPost]
        public ActionResult AddEditGender(string Code)
        {
            GenderViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Gender";
                model = new GenderViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Gender";
                model = _Gender.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Gender/_AddEditGenderForm.cshtml", model);
        }

        //
        // POST: /CMS/Gender/SaveGender
        [HttpPost]
        public ActionResult SaveGender(GenderViewModel Gender)
        {
            if (ModelState.IsValid)
            {
                int TempID = Gender.GenderID;

                if (TempID == 0)
                {
                    Gender.CreatedBy = "CMS Team";
                    Gender.Source = "CMS Server";
                    Gender = _Gender.Create(Gender);
                }
                else {
                    Gender.LastModifiedBy = "CMS Team Update";
                    Gender.Source = "CMS Server Update";
                    Gender = _Gender.Update(Gender); }

                if (Gender != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Gender/_RowGender.cshtml", Gender);

                    if (TempID == 0)
                        return Json(new { Message = "New Gender Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Gender Updated Successfully", Status = true, Type = "Edit", Template = Template });
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