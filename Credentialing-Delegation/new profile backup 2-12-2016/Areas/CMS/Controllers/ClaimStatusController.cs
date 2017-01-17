using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ClaimStatusController : Controller
    {
        /// <summary>
        /// IClaimStatusService object reference
        /// </summary>
        private IClaimStatusService _ClaimStatus = null;

        /// <summary>
        /// ClaimStatusController constructor For ClaimStatusService
        /// </summary>
        public ClaimStatusController()
        {
            _ClaimStatus = new ClaimStatusService();
        }

        //
        // GET: /CMS/ClaimStatus/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ClaimStatus";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ClaimStatus.GetAll();
            ClaimStatusViewModel model = new ClaimStatusViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ClaimStatus/Index.cshtml", model);
        }

        //
        // POST: /CMS/ClaimStatus/AddEditClaimStatus
        [HttpPost]
        public ActionResult AddEditClaimStatus(string Code)
        {
            ClaimStatusViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ClaimStatus";
                model = new ClaimStatusViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ClaimStatus";
                model = _ClaimStatus.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ClaimStatus/_AddEditClaimStatusForm.cshtml", model);
        }

        //
        // POST: /CMS/ClaimStatus/SaveClaimStatus
        [HttpPost]
        public ActionResult SaveClaimStatus(ClaimStatusViewModel ClaimStatus)
        {
            if (ModelState.IsValid)
            {
                int TempID = ClaimStatus.ClaimStatusID;

                if (TempID == 0)
                {
                    ClaimStatus.CreatedBy = "CMS Team";
                    ClaimStatus.Source = "CMS Server";
                    ClaimStatus = _ClaimStatus.Create(ClaimStatus);
                }
                else {
                    ClaimStatus.LastModifiedBy = "CMS Team Update";
                    ClaimStatus.Source = "CMS Server Update";
                    ClaimStatus = _ClaimStatus.Update(ClaimStatus); }

                if (ClaimStatus != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ClaimStatus/_RowClaimStatus.cshtml", ClaimStatus);

                    if (TempID == 0)
                        return Json(new { Message = "New ClaimStatus Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ClaimStatus Updated Successfully", Status = true, Type = "Edit", Template = Template });
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