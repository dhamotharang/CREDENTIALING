using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ClaimValueCodeController : Controller
    {
        /// <summary>
        /// IClaimValueCodeService object reference
        /// </summary>
        private IClaimValueCodeService _ClaimValueCode = null;

        /// <summary>
        /// ClaimValueCodeController constructor For ClaimValueCodeService
        /// </summary>
        public ClaimValueCodeController()
        {
            _ClaimValueCode = new ClaimValueCodeService();
        }

        //
        // GET: /CMS/ClaimValueCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Claim Value Code";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ClaimValueCode.GetAll();
            ClaimValueCodeViewModel model = new ClaimValueCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ClaimValueCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/ClaimValueCode/AddEditClaimValueCode
        [HttpPost]
        public ActionResult AddEditClaimValueCode(string Code)
        {
            ClaimValueCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Claim Value Code";
                model = new ClaimValueCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Claim Value Code";
                model = _ClaimValueCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ClaimValueCode/_AddEditClaimValueCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/ClaimValueCode/SaveClaimValueCode
        [HttpPost]
        public ActionResult SaveClaimValueCode(ClaimValueCodeViewModel ClaimValueCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = ClaimValueCode.ClaimValueCodeID;

                if (TempID == 0)
                {
                    ClaimValueCode.CreatedBy = "CMS Team";
                    ClaimValueCode.Source = "CMS Server";
                    ClaimValueCode = _ClaimValueCode.Create(ClaimValueCode);
                }
                else {
                    ClaimValueCode.LastModifiedBy = "CMS Team Update";
                    ClaimValueCode.Source = "CMS Server Update";
                    ClaimValueCode = _ClaimValueCode.Update(ClaimValueCode); }

                if (ClaimValueCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ClaimValueCode/_RowClaimValueCode.cshtml", ClaimValueCode);

                    if (TempID == 0)
                        return Json(new { Message = "New Claim Value Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Claim Value Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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