using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ClaimFrequencyCodeController : Controller
    {
        /// <summary>
        /// IClaimFrequencyCodeService object reference
        /// </summary>
        private IClaimFrequencyCodeService _ClaimFrequencyCode = null;

        /// <summary>
        /// ClaimFrequencyCodeController constructor For ClaimFrequencyCodeService
        /// </summary>
        public ClaimFrequencyCodeController()
        {
            _ClaimFrequencyCode = new ClaimFrequencyCodeService();
        }

        //
        // GET: /CMS/ClaimFrequencyCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Claim Frequency Code";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ClaimFrequencyCode.GetAll();
            ClaimFrequencyCodeViewModel model = new ClaimFrequencyCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ClaimFrequencyCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/ClaimFrequencyCode/AddEditClaimFrequencyCode
        [HttpPost]
        public ActionResult AddEditClaimFrequencyCode(string Code)
        {
            ClaimFrequencyCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Claim Frequency Code";
                model = new ClaimFrequencyCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Claim Frequency Code";
                model = _ClaimFrequencyCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ClaimFrequencyCode/_AddEditClaimFrequencyCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/ClaimFrequencyCode/SaveClaimFrequencyCode
        [HttpPost]
        public ActionResult SaveClaimFrequencyCode(ClaimFrequencyCodeViewModel ClaimFrequencyCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = ClaimFrequencyCode.ClaimFrequencyCodeID;

                if (TempID == 0)
                {
                    ClaimFrequencyCode.CreatedBy = "CMS Team";
                    ClaimFrequencyCode.Source = "CMS Server";
                    ClaimFrequencyCode = _ClaimFrequencyCode.Create(ClaimFrequencyCode);
                }
                else {
                    ClaimFrequencyCode.LastModifiedBy = "CMS Team Update";
                    ClaimFrequencyCode.Source = "CMS Server Update";
                    ClaimFrequencyCode = _ClaimFrequencyCode.Update(ClaimFrequencyCode); }

                if (ClaimFrequencyCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ClaimFrequencyCode/_RowClaimFrequencyCode.cshtml", ClaimFrequencyCode);

                    if (TempID == 0)
                        return Json(new { Message = "New Claim Frequency Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Claim Frequency Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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