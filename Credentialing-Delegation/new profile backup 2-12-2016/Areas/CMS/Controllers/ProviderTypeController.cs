using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ProviderTypeController : Controller
    {
        /// <summary>
        /// IProviderTypeService object reference
        /// </summary>
        private IProviderTypeService _ProviderType = null;

        /// <summary>
        /// ProviderTypeController constructor For ProviderTypeService
        /// </summary>
        public ProviderTypeController()
        {
            _ProviderType = new ProviderTypeService();
        }

        //
        // GET: /CMS/ProviderType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ProviderType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ProviderType.GetAll();
            ProviderTypeViewModel model = new ProviderTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ProviderType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ProviderType/AddEditProviderType
        [HttpPost]
        public ActionResult AddEditProviderType(string Code)
        {
            ProviderTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ProviderType";
                model = new ProviderTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ProviderType";
                model = _ProviderType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ProviderType/_AddEditProviderTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ProviderType/SaveProviderType
        [HttpPost]
        public ActionResult SaveProviderType(ProviderTypeViewModel ProviderType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ProviderType.ProviderTypeID;

                if (TempID == 0)
                {
                    ProviderType.CreatedBy = "CMS Team";
                    ProviderType.Source = "CMS Server";
                    ProviderType = _ProviderType.Create(ProviderType);
                }
                else {
                    ProviderType.LastModifiedBy = "CMS Team Update";
                    ProviderType.Source = "CMS Server Update";
                    ProviderType = _ProviderType.Update(ProviderType); }

                if (ProviderType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ProviderType/_RowProviderType.cshtml", ProviderType);

                    if (TempID == 0)
                        return Json(new { Message = "New ProviderType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ProviderType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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