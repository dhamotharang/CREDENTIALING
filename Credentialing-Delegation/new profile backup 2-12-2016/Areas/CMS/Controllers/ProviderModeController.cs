using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ProviderModeController : Controller
    {
        /// <summary>
        /// IProviderModeService object reference
        /// </summary>
        private IProviderModeService _ProviderMode = null;

        /// <summary>
        /// ProviderModeController constructor For ProviderModeService
        /// </summary>
        public ProviderModeController()
        {
            _ProviderMode = new ProviderModeService();
        }

        //
        // GET: /CMS/ProviderMode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ProviderMode";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ProviderMode.GetAll();
            ProviderModeViewModel model = new ProviderModeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ProviderMode/Index.cshtml", model);
        }

        //
        // POST: /CMS/ProviderMode/AddEditProviderMode
        [HttpPost]
        public ActionResult AddEditProviderMode(string Code)
        {
            ProviderModeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ProviderMode";
                model = new ProviderModeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ProviderMode";
                model = _ProviderMode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ProviderMode/_AddEditProviderModeForm.cshtml", model);
        }

        //
        // POST: /CMS/ProviderMode/SaveProviderMode
        [HttpPost]
        public ActionResult SaveProviderMode(ProviderModeViewModel ProviderMode)
        {
            if (ModelState.IsValid)
            {
                int TempID = ProviderMode.ProviderModeID;

                if (TempID == 0)
                {
                    ProviderMode.CreatedBy = "CMS Team";
                    ProviderMode.Source = "CMS Server";
                    ProviderMode = _ProviderMode.Create(ProviderMode);
                }
                else {
                    ProviderMode.LastModifiedBy = "CMS Team Update";
                    ProviderMode.Source = "CMS Server Update";
                    ProviderMode = _ProviderMode.Update(ProviderMode); }

                if (ProviderMode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ProviderMode/_RowProviderMode.cshtml", ProviderMode);

                    if (TempID == 0)
                        return Json(new { Message = "New ProviderMode Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ProviderMode Updated Successfully", Status = true, Type = "Edit", Template = Template });
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