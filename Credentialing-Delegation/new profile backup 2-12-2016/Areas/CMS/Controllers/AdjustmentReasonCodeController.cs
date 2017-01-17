using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class AdjustmentReasonCodeController : Controller
    {
        /// <summary>
        /// IAdjustmentReasonCodeService object reference
        /// </summary>
        private IAdjustmentReasonCodeService _AdjustmentReasonCode = null;

        /// <summary>
        /// AdjustmentReasonCodeController constructor For AdjustmentReasonCodeService
        /// </summary>
        public AdjustmentReasonCodeController()
        {
            _AdjustmentReasonCode = new AdjustmentReasonCodeService();
        }

        //
        // GET: /CMS/AdjustmentReasonCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "AdjustmentReasonCode";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _AdjustmentReasonCode.GetAll();
            AdjustmentReasonCodeViewModel model = new AdjustmentReasonCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/AdjustmentReasonCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/AdjustmentReasonCode/AddEditAdjustmentReasonCode
        [HttpPost]
        public ActionResult AddEditAdjustmentReasonCode(string Code)
        {
            AdjustmentReasonCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New AdjustmentReasonCode";
                model = new AdjustmentReasonCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit AdjustmentReasonCode";
                model = _AdjustmentReasonCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/AdjustmentReasonCode/_AddEditAdjustmentReasonCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/AdjustmentReasonCode/SaveAdjustmentReasonCode
        [HttpPost]
        public ActionResult SaveAdjustmentReasonCode(AdjustmentReasonCodeViewModel AdjustmentReasonCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = AdjustmentReasonCode.AdjustmentReasonCodeID;

                if (TempID == 0)
                {
                    AdjustmentReasonCode.CreatedBy = "CMS Team";
                    AdjustmentReasonCode.Source = "CMS Server";
                    AdjustmentReasonCode = _AdjustmentReasonCode.Create(AdjustmentReasonCode);
                }
                else {
                    AdjustmentReasonCode.LastModifiedBy = "CMS Team Update";
                    AdjustmentReasonCode.Source = "CMS Server Update";
                    AdjustmentReasonCode = _AdjustmentReasonCode.Update(AdjustmentReasonCode); }

                if (AdjustmentReasonCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/AdjustmentReasonCode/_RowAdjustmentReasonCode.cshtml", AdjustmentReasonCode);

                    if (TempID == 0)
                        return Json(new { Message = "New AdjustmentReasonCode Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "AdjustmentReasonCode Updated Successfully", Status = true, Type = "Edit", Template = Template });
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