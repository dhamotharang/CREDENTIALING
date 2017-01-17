using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ProviderLevelController : Controller
    {
        /// <summary>
        /// IProviderLevelService object reference
        /// </summary>
        private IProviderLevelService _ProviderLevel = null;

        /// <summary>
        /// ProviderLevelController constructor For ProviderLevelService
        /// </summary>
        public ProviderLevelController()
        {
            _ProviderLevel = new ProviderLevelService();
        }

        //
        // GET: /CMS/ProviderLevel/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ProviderLevel";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ProviderLevel.GetAll();
            ProviderLevelViewModel model = new ProviderLevelViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ProviderLevel/Index.cshtml", model);
        }

        //
        // POST: /CMS/ProviderLevel/AddEditProviderLevel
        [HttpPost]
        public ActionResult AddEditProviderLevel(string Code)
        {
            ProviderLevelViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ProviderLevel";
                model = new ProviderLevelViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ProviderLevel";
                model = _ProviderLevel.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ProviderLevel/_AddEditProviderLevelForm.cshtml", model);
        }

        //
        // POST: /CMS/ProviderLevel/SaveProviderLevel
        [HttpPost]
        public ActionResult SaveProviderLevel(ProviderLevelViewModel ProviderLevel)
        {
            if (ModelState.IsValid)
            {
                int TempID = ProviderLevel.ProviderLevelID;

                if (TempID == 0)
                {
                    ProviderLevel.CreatedBy = "CMS Team";
                    ProviderLevel.Source = "CMS Server";
                    ProviderLevel = _ProviderLevel.Create(ProviderLevel);
                }
                else {
                    ProviderLevel.LastModifiedBy = "CMS Team Update";
                    ProviderLevel.Source = "CMS Server Update";
                    ProviderLevel = _ProviderLevel.Update(ProviderLevel); }

                if (ProviderLevel != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ProviderLevel/_RowProviderLevel.cshtml", ProviderLevel);

                    if (TempID == 0)
                        return Json(new { Message = "New ProviderLevel Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ProviderLevel Updated Successfully", Status = true, Type = "Edit", Template = Template });
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