using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class AdjustmentGroupCodeController : Controller
    {
        /// <summary>
        /// IAdjustmentGroupCodeService object reference
        /// </summary>
        private IAdjustmentGroupCodeService _AdjustmentGroupCode = null;

        /// <summary>
        /// AdjustmentGroupCodeController constructor For AdjustmentGroupCodeService
        /// </summary>
        public AdjustmentGroupCodeController()
        {
            _AdjustmentGroupCode = new AdjustmentGroupCodeService();
        }

        //
        // GET: /CMS/AdjustmentGroupCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "AdjustmentGroupCode";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _AdjustmentGroupCode.GetAll();
            AdjustmentGroupCodeViewModel model = new AdjustmentGroupCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/AdjustmentGroupCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/AdjustmentGroupCode/AddEditAdjustmentGroupCode
        [HttpPost]
        public ActionResult AddEditAdjustmentGroupCode(string Code)
        {
            AdjustmentGroupCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New AdjustmentGroupCode";
                model = new AdjustmentGroupCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit AdjustmentGroupCode";
                model = _AdjustmentGroupCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/AdjustmentGroupCode/_AddEditAdjustmentGroupCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/AdjustmentGroupCode/SaveAdjustmentGroupCode
        [HttpPost]
        public ActionResult SaveAdjustmentGroupCode(AdjustmentGroupCodeViewModel AdjustmentGroupCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = AdjustmentGroupCode.AdjustmentGroupCodeID;

                if (TempID == 0)
                {
                    AdjustmentGroupCode.CreatedBy = "CMS Team";
                    AdjustmentGroupCode.Source = "CMS Server";
                    AdjustmentGroupCode = _AdjustmentGroupCode.Create(AdjustmentGroupCode);
                }
                else {
                    AdjustmentGroupCode.LastModifiedBy = "CMS Team Update";
                    AdjustmentGroupCode.Source = "CMS Server Update";
                    AdjustmentGroupCode = _AdjustmentGroupCode.Update(AdjustmentGroupCode); }

                if (AdjustmentGroupCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/AdjustmentGroupCode/_RowAdjustmentGroupCode.cshtml", AdjustmentGroupCode);

                    if (TempID == 0)
                        return Json(new { Message = "New AdjustmentGroupCode Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "AdjustmentGroupCode Updated Successfully", Status = true, Type = "Edit", Template = Template });
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