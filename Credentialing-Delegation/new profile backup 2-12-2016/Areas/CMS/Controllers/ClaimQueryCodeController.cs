using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ClaimQueryCodeController : Controller
    {
        /// <summary>
        /// IClaimQueryCodeService object reference
        /// </summary>
        private IClaimQueryCodeService _ClaimQueryCode = null;

        /// <summary>
        /// ClaimQueryCodeController constructor For ClaimQueryCodeService
        /// </summary>
        public ClaimQueryCodeController()
        {
            _ClaimQueryCode = new ClaimQueryCodeService();
        }

        //
        // GET: /CMS/ClaimQueryCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Claim Query Code";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ClaimQueryCode.GetAll();
            ClaimQueryCodeViewModel model = new ClaimQueryCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ClaimQueryCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/ClaimQueryCode/AddEditClaimQueryCode
        [HttpPost]
        public ActionResult AddEditClaimQueryCode(string Code)
        {
            ClaimQueryCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Claim Query Code";
                model = new ClaimQueryCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Claim Query Code";
                model = _ClaimQueryCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ClaimQueryCode/_AddEditClaimQueryCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/ClaimQueryCode/SaveClaimQueryCode
        [HttpPost]
        public ActionResult SaveClaimQueryCode(ClaimQueryCodeViewModel ClaimQueryCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = ClaimQueryCode.ClaimQueryCodeID;

                if (TempID == 0)
                {
                    ClaimQueryCode.CreatedBy = "CMS Team";
                    ClaimQueryCode.Source = "CMS Server";
                    ClaimQueryCode = _ClaimQueryCode.Create(ClaimQueryCode);
                }
                else {
                    ClaimQueryCode.LastModifiedBy = "CMS Team Update";
                    ClaimQueryCode.Source = "CMS Server Update";
                    ClaimQueryCode = _ClaimQueryCode.Update(ClaimQueryCode); }

                if (ClaimQueryCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ClaimQueryCode/_RowClaimQueryCode.cshtml", ClaimQueryCode);

                    if (TempID == 0)
                        return Json(new { Message = "New Claim Query Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Claim Query Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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