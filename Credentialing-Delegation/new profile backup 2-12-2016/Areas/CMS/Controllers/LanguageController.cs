using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class LanguageController : Controller
    {
        /// <summary>
        /// ILanguageService object reference
        /// </summary>
        private ILanguageService _Language = null;

        /// <summary>
        /// LanguageController constructor For LanguageService
        /// </summary>
        public LanguageController()
        {
            _Language = new LanguageService();
        }

        //
        // GET: /CMS/Language/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Language";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Language.GetAll();
            LanguageViewModel model = new LanguageViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Language/Index.cshtml", model);
        }

        //
        // POST: /CMS/Language/AddEditLanguage
        [HttpPost]
        public ActionResult AddEditLanguage(string Code)
        {
            LanguageViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Language";
                model = new LanguageViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Language";
                model = _Language.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Language/_AddEditLanguageForm.cshtml", model);
        }

        //
        // POST: /CMS/Language/SaveLanguage
        [HttpPost]
        public ActionResult SaveLanguage(LanguageViewModel Language)
        {
            if (ModelState.IsValid)
            {
                int TempID = Language.LanguageID;

                if (TempID == 0)
                {
                    Language.CreatedBy = "CMS Team";
                    Language.Source = "CMS Server";
                    Language = _Language.Create(Language);
                }
                else {
                    Language.LastModifiedBy = "CMS Team Update";
                    Language.Source = "CMS Server Update";
                    Language = _Language.Update(Language); }

                if (Language != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Language/_RowLanguage.cshtml", Language);

                    if (TempID == 0)
                        return Json(new { Message = "New Language Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Language Updated Successfully", Status = true, Type = "Edit", Template = Template });
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