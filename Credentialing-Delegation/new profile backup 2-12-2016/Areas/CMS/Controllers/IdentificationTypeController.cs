using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class IdentificationTypeController : Controller
    {
        /// <summary>
        /// IIdentificationTypeService object reference
        /// </summary>
        private IIdentificationTypeService _IdentificationType = null;

        /// <summary>
        /// IdentificationTypeController constructor For IdentificationTypeService
        /// </summary>
        public IdentificationTypeController()
        {
            _IdentificationType = new IdentificationTypeService();
        }

        //
        // GET: /CMS/IdentificationType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "IdentificationType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _IdentificationType.GetAll();
            IdentificationTypeViewModel model = new IdentificationTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/IdentificationType/Index.cshtml", model);
        }

        //
        // POST: /CMS/IdentificationType/AddEditIdentificationType
        [HttpPost]
        public ActionResult AddEditIdentificationType(string Code)
        {
            IdentificationTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New IdentificationType";
                model = new IdentificationTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit IdentificationType";
                model = _IdentificationType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/IdentificationType/_AddEditIdentificationTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/IdentificationType/SaveIdentificationType
        [HttpPost]
        public ActionResult SaveIdentificationType(IdentificationTypeViewModel IdentificationType)
        {
            if (ModelState.IsValid)
            {
                int TempID = IdentificationType.IdentificationTypeID;

                if (TempID == 0)
                {
                    IdentificationType.CreatedBy = "CMS Team";
                    IdentificationType.Source = "CMS Server";
                    IdentificationType = _IdentificationType.Create(IdentificationType);
                }
                else {
                    IdentificationType.LastModifiedBy = "CMS Team Update";
                    IdentificationType.Source = "CMS Server Update";
                    IdentificationType = _IdentificationType.Update(IdentificationType); }

                if (IdentificationType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/IdentificationType/_RowIdentificationType.cshtml", IdentificationType);

                    if (TempID == 0)
                        return Json(new { Message = "New IdentificationType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "IdentificationType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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