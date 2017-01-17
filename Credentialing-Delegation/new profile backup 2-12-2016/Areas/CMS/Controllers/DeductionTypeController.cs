using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DeductionTypeController : Controller
    {
        /// <summary>
        /// IDeductionTypeService object reference
        /// </summary>
        private IDeductionTypeService _DeductionType = null;

        /// <summary>
        /// DeductionTypeController constructor For DeductionTypeService
        /// </summary>
        public DeductionTypeController()
        {
            _DeductionType = new DeductionTypeService();
        }

        //
        // GET: /CMS/DeductionType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DeductionType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DeductionType.GetAll();
            DeductionTypeViewModel model = new DeductionTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DeductionType/Index.cshtml", model);
        }

        //
        // POST: /CMS/DeductionType/AddEditDeductionType
        [HttpPost]
        public ActionResult AddEditDeductionType(string Code)
        {
            DeductionTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DeductionType";
                model = new DeductionTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DeductionType";
                model = _DeductionType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DeductionType/_AddEditDeductionTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/DeductionType/SaveDeductionType
        [HttpPost]
        public ActionResult SaveDeductionType(DeductionTypeViewModel DeductionType)
        {
            if (ModelState.IsValid)
            {
                int TempID = DeductionType.DeductionTypeID;

                if (TempID == 0)
                {
                    DeductionType.CreatedBy = "CMS Team";
                    DeductionType.Source = "CMS Server";
                    DeductionType = _DeductionType.Create(DeductionType);
                }
                else {
                    DeductionType.LastModifiedBy = "CMS Team Update";
                    DeductionType.Source = "CMS Server Update";
                    DeductionType = _DeductionType.Update(DeductionType); }

                if (DeductionType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DeductionType/_RowDeductionType.cshtml", DeductionType);

                    if (TempID == 0)
                        return Json(new { Message = "New DeductionType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DeductionType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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