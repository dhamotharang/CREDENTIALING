using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ResponsiblePersonTypeController : Controller
    {
        /// <summary>
        /// IResponsiblePersonTypeService object reference
        /// </summary>
        private IResponsiblePersonTypeService _ResponsiblePersonType = null;

        /// <summary>
        /// ResponsiblePersonTypeController constructor For ResponsiblePersonTypeService
        /// </summary>
        public ResponsiblePersonTypeController()
        {
            _ResponsiblePersonType = new ResponsiblePersonTypeService();
        }

        //
        // GET: /CMS/ResponsiblePersonType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ResponsiblePersonType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ResponsiblePersonType.GetAll();
            ResponsiblePersonTypeViewModel model = new ResponsiblePersonTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ResponsiblePersonType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ResponsiblePersonType/AddEditResponsiblePersonType
        [HttpPost]
        public ActionResult AddEditResponsiblePersonType(string Code)
        {
            ResponsiblePersonTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ResponsiblePersonType";
                model = new ResponsiblePersonTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ResponsiblePersonType";
                model = _ResponsiblePersonType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ResponsiblePersonType/_AddEditResponsiblePersonTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ResponsiblePersonType/SaveResponsiblePersonType
        [HttpPost]
        public ActionResult SaveResponsiblePersonType(ResponsiblePersonTypeViewModel ResponsiblePersonType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ResponsiblePersonType.ResponsiblePersonTypeID;

                if (TempID == 0)
                {
                    ResponsiblePersonType.CreatedBy = "CMS Team";
                    ResponsiblePersonType.Source = "CMS Server";
                    ResponsiblePersonType = _ResponsiblePersonType.Create(ResponsiblePersonType);
                }
                else {
                    ResponsiblePersonType.LastModifiedBy = "CMS Team Update";
                    ResponsiblePersonType.Source = "CMS Server Update";
                    ResponsiblePersonType = _ResponsiblePersonType.Update(ResponsiblePersonType); }

                if (ResponsiblePersonType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ResponsiblePersonType/_RowResponsiblePersonType.cshtml", ResponsiblePersonType);

                    if (TempID == 0)
                        return Json(new { Message = "New ResponsiblePersonType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ResponsiblePersonType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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