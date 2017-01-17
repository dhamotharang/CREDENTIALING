using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class PlainLanguageController : Controller
    {
        /// <summary>
        /// IPlainLanguageService object reference
        /// </summary>
        private IPlainLanguageService _PlainLanguage = null;

        /// <summary>
        /// PlainLanguageController constructor For PlainLanguageService
        /// </summary>
        public PlainLanguageController()
        {
            _PlainLanguage = new PlainLanguageService();
        }

        //
        // GET: /CMS/PlainLanguage/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "PlainLanguage";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _PlainLanguage.GetAll();
            PlainLanguageViewModel model = new PlainLanguageViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/PlainLanguage/Index.cshtml", model);
        }

        //
        // POST: /CMS/PlainLanguage/AddEditPlainLanguage
        [HttpPost]
        public ActionResult AddEditPlainLanguage(string Code)
        {
            PlainLanguageViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New PlainLanguage";
                model = new PlainLanguageViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit PlainLanguage";
                model = _PlainLanguage.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/PlainLanguage/_AddEditPlainLanguageForm.cshtml", model);
        }

        //
        // POST: /CMS/PlainLanguage/SavePlainLanguage
        [HttpPost]
        public ActionResult SavePlainLanguage(PlainLanguageViewModel PlainLanguage)
        {
            if (ModelState.IsValid)
            {
                int TempID = PlainLanguage.PlainLanguageID;

                if (TempID == 0)
                {
                    PlainLanguage.CreatedBy = "CMS Team";
                    PlainLanguage.Source = "CMS Server";
                    PlainLanguage = _PlainLanguage.Create(PlainLanguage);
                }
                else {
                    PlainLanguage.LastModifiedBy = "CMS Team Update";
                    PlainLanguage.Source = "CMS Server Update";
                    PlainLanguage = _PlainLanguage.Update(PlainLanguage); }

                if (PlainLanguage != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/PlainLanguage/_RowPlainLanguage.cshtml", PlainLanguage);

                    if (TempID == 0)
                        return Json(new { Message = "New PlainLanguage Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "PlainLanguage Updated Successfully", Status = true, Type = "Edit", Template = Template });
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