using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class SuffixController : Controller
    {
        /// <summary>
        /// ISuffixService object reference
        /// </summary>
        private ISuffixService _Suffix = null;

        /// <summary>
        /// SuffixController constructor For SuffixService
        /// </summary>
        public SuffixController()
        {
            _Suffix = new SuffixService();
        }

        //
        // GET: /CMS/Suffix/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Suffix";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Suffix.GetAll();
            SuffixViewModel model = new SuffixViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Suffix/Index.cshtml", model);
        }

        //
        // POST: /CMS/Suffix/AddEditSuffix
        [HttpPost]
        public ActionResult AddEditSuffix(string Code)
        {
            SuffixViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Suffix";
                model = new SuffixViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Suffix";
                model = _Suffix.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Suffix/_AddEditSuffixForm.cshtml", model);
        }

        //
        // POST: /CMS/Suffix/SaveSuffix
        [HttpPost]
        public ActionResult SaveSuffix(SuffixViewModel Suffix)
        {
            if (ModelState.IsValid)
            {
                int TempID = Suffix.SuffixID;

                if (TempID == 0)
                {
                    Suffix.CreatedBy = "CMS Team";
                    Suffix.Source = "CMS Server";
                    Suffix = _Suffix.Create(Suffix);
                }
                else {
                    Suffix.LastModifiedBy = "CMS Team Update";
                    Suffix.Source = "CMS Server Update";
                    Suffix = _Suffix.Update(Suffix); }

                if (Suffix != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Suffix/_RowSuffix.cshtml", Suffix);

                    if (TempID == 0)
                        return Json(new { Message = "New Suffix Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Suffix Updated Successfully", Status = true, Type = "Edit", Template = Template });
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