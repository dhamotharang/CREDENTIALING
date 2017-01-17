using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ClaimRelatedConditionCodeController : Controller
    {
        /// <summary>
        /// IClaimRelatedConditionCodeService object reference
        /// </summary>
        private IClaimRelatedConditionCodeService _ClaimRelatedConditionCode = null;

        /// <summary>
        /// ClaimRelatedConditionCodeController constructor For ClaimRelatedConditionCodeService
        /// </summary>
        public ClaimRelatedConditionCodeController()
        {
            _ClaimRelatedConditionCode = new ClaimRelatedConditionCodeService();
        }

        //
        // GET: /CMS/ClaimRelatedConditionCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Claim Related Condition Code";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ClaimRelatedConditionCode.GetAll();
            ClaimRelatedConditionCodeViewModel model = new ClaimRelatedConditionCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ClaimRelatedConditionCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/ClaimRelatedConditionCode/AddEditClaimRelatedConditionCode
        [HttpPost]
        public ActionResult AddEditClaimRelatedConditionCode(string Code)
        {
            ClaimRelatedConditionCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Claim Related Condition Code";
                model = new ClaimRelatedConditionCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Claim Related Condition Code";
                model = _ClaimRelatedConditionCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ClaimRelatedConditionCode/_AddEditClaimRelatedConditionCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/ClaimRelatedConditionCode/SaveClaimRelatedConditionCode
        [HttpPost]
        public ActionResult SaveClaimRelatedConditionCode(ClaimRelatedConditionCodeViewModel ClaimRelatedConditionCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = ClaimRelatedConditionCode.ClaimRelatedConditionCodeID;

                if (TempID == 0)
                {
                    ClaimRelatedConditionCode.CreatedBy = "CMS Team";
                    ClaimRelatedConditionCode.Source = "CMS Server";
                    ClaimRelatedConditionCode = _ClaimRelatedConditionCode.Create(ClaimRelatedConditionCode);
                }
                else {
                    ClaimRelatedConditionCode.LastModifiedBy = "CMS Team Update";
                    ClaimRelatedConditionCode.Source = "CMS Server Update";
                    ClaimRelatedConditionCode = _ClaimRelatedConditionCode.Update(ClaimRelatedConditionCode); }

                if (ClaimRelatedConditionCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ClaimRelatedConditionCode/_RowClaimRelatedConditionCode.cshtml", ClaimRelatedConditionCode);

                    if (TempID == 0)
                        return Json(new { Message = "New Claim Related Condition Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Claim Related Condition Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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