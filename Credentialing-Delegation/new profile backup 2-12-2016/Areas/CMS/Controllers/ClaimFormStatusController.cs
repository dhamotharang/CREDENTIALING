using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ClaimFormStatusController : Controller
    {
        /// <summary>
        /// IClaimFormStatusService object reference
        /// </summary>
        private IClaimFormStatusService _ClaimFormStatus = null;

        /// <summary>
        /// ClaimFormStatusController constructor For ClaimFormStatusService
        /// </summary>
        public ClaimFormStatusController()
        {
            _ClaimFormStatus = new ClaimFormStatusService();
        }

        //
        // GET: /CMS/ClaimFormStatus/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ClaimFormStatus";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ClaimFormStatus.GetAll();
            ClaimFormStatusViewModel model = new ClaimFormStatusViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ClaimFormStatus/Index.cshtml", model);
        }

        //
        // POST: /CMS/ClaimFormStatus/AddEditClaimFormStatus
        [HttpPost]
        public ActionResult AddEditClaimFormStatus(string Code)
        {
            ClaimFormStatusViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ClaimFormStatus";
                model = new ClaimFormStatusViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ClaimFormStatus";
                model = _ClaimFormStatus.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ClaimFormStatus/_AddEditClaimFormStatusForm.cshtml", model);
        }

        //
        // POST: /CMS/ClaimFormStatus/SaveClaimFormStatus
        [HttpPost]
        public ActionResult SaveClaimFormStatus(ClaimFormStatusViewModel ClaimFormStatus)
        {
            if (ModelState.IsValid)
            {
                int TempID = ClaimFormStatus.ClaimFormStatusID;

                if (TempID == 0)
                {
                    ClaimFormStatus.CreatedBy = "CMS Team";
                    ClaimFormStatus.Source = "CMS Server";
                    ClaimFormStatus = _ClaimFormStatus.Create(ClaimFormStatus);
                }
                else {
                    ClaimFormStatus.LastModifiedBy = "CMS Team Update";
                    ClaimFormStatus.Source = "CMS Server Update";
                    ClaimFormStatus = _ClaimFormStatus.Update(ClaimFormStatus); }

                if (ClaimFormStatus != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ClaimFormStatus/_RowClaimFormStatus.cshtml", ClaimFormStatus);

                    if (TempID == 0)
                        return Json(new { Message = "New ClaimFormStatus Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ClaimFormStatus Updated Successfully", Status = true, Type = "Edit", Template = Template });
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