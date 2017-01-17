using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ProviderProfileSubSectionController : Controller
    {
        /// <summary>
        /// IProviderProfileSubSectionService object reference
        /// </summary>
        private IProviderProfileSubSectionService _ProviderProfileSubSection = null;

        /// <summary>
        /// ProviderProfileSubSectionController constructor For ProviderProfileSubSectionService
        /// </summary>
        public ProviderProfileSubSectionController()
        {
            _ProviderProfileSubSection = new ProviderProfileSubSectionService();
        }

        //
        // GET: /CMS/ProviderProfileSubSection/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ProviderProfileSubSection";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ProviderProfileSubSection.GetAll();
            ProviderProfileSubSectionViewModel model = new ProviderProfileSubSectionViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ProviderProfileSubSection/Index.cshtml", model);
        }

        //
        // POST: /CMS/ProviderProfileSubSection/AddEditProviderProfileSubSection
        [HttpPost]
        public ActionResult AddEditProviderProfileSubSection(string Code)
        {
            ProviderProfileSubSectionViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ProviderProfileSubSection";
                model = new ProviderProfileSubSectionViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ProviderProfileSubSection";
                model = _ProviderProfileSubSection.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ProviderProfileSubSection/_AddEditProviderProfileSubSectionForm.cshtml", model);
        }

        //
        // POST: /CMS/ProviderProfileSubSection/SaveProviderProfileSubSection
        [HttpPost]
        public ActionResult SaveProviderProfileSubSection(ProviderProfileSubSectionViewModel ProviderProfileSubSection)
        {
            if (ModelState.IsValid)
            {
                int TempID = ProviderProfileSubSection.ProviderProfileSubSectionID;

                if (TempID == 0)
                {
                    ProviderProfileSubSection.CreatedBy = "CMS Team";
                    ProviderProfileSubSection.Source = "CMS Server";
                    ProviderProfileSubSection = _ProviderProfileSubSection.Create(ProviderProfileSubSection);
                }
                else {
                    ProviderProfileSubSection.LastModifiedBy = "CMS Team Update";
                    ProviderProfileSubSection.Source = "CMS Server Update";
                    ProviderProfileSubSection = _ProviderProfileSubSection.Update(ProviderProfileSubSection); }

                if (ProviderProfileSubSection != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ProviderProfileSubSection/_RowProviderProfileSubSection.cshtml", ProviderProfileSubSection);

                    if (TempID == 0)
                        return Json(new { Message = "New ProviderProfileSubSection Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ProviderProfileSubSection Updated Successfully", Status = true, Type = "Edit", Template = Template });
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