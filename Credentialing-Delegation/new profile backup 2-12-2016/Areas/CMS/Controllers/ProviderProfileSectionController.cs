using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ProviderProfileSectionController : Controller
    {
        /// <summary>
        /// IProviderProfileSectionService object reference
        /// </summary>
        private IProviderProfileSectionService _ProviderProfileSection = null;

        /// <summary>
        /// ProviderProfileSectionController constructor For ProviderProfileSectionService
        /// </summary>
        public ProviderProfileSectionController()
        {
            _ProviderProfileSection = new ProviderProfileSectionService();
        }

        //
        // GET: /CMS/ProviderProfileSection/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ProviderProfileSection";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ProviderProfileSection.GetAll();
            ProviderProfileSectionViewModel model = new ProviderProfileSectionViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ProviderProfileSection/Index.cshtml", model);
        }

        //
        // POST: /CMS/ProviderProfileSection/AddEditProviderProfileSection
        [HttpPost]
        public ActionResult AddEditProviderProfileSection(string Code)
        {
            ProviderProfileSectionViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ProviderProfileSection";
                model = new ProviderProfileSectionViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ProviderProfileSection";
                model = _ProviderProfileSection.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ProviderProfileSection/_AddEditProviderProfileSectionForm.cshtml", model);
        }

        //
        // POST: /CMS/ProviderProfileSection/SaveProviderProfileSection
        [HttpPost]
        public ActionResult SaveProviderProfileSection(ProviderProfileSectionViewModel ProviderProfileSection)
        {
            if (ModelState.IsValid)
            {
                int TempID = ProviderProfileSection.ProviderProfileSectionID;

                if (TempID == 0)
                {
                    ProviderProfileSection.CreatedBy = "CMS Team";
                    ProviderProfileSection.Source = "CMS Server";
                    ProviderProfileSection = _ProviderProfileSection.Create(ProviderProfileSection);
                }
                else {
                    ProviderProfileSection.LastModifiedBy = "CMS Team Update";
                    ProviderProfileSection.Source = "CMS Server Update";
                    ProviderProfileSection = _ProviderProfileSection.Update(ProviderProfileSection); }

                if (ProviderProfileSection != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ProviderProfileSection/_RowProviderProfileSection.cshtml", ProviderProfileSection);

                    if (TempID == 0)
                        return Json(new { Message = "New ProviderProfileSection Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ProviderProfileSection Updated Successfully", Status = true, Type = "Edit", Template = Template });
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