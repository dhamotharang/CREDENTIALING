using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DecredentialingReasonController : Controller
    {
        /// <summary>
        /// IDecredentialingReasonService object reference
        /// </summary>
        private IDecredentialingReasonService _DecredentialingReason = null;

        /// <summary>
        /// DecredentialingReasonController constructor For DecredentialingReasonService
        /// </summary>
        public DecredentialingReasonController()
        {
            _DecredentialingReason = new DecredentialingReasonService();
        }

        //
        // GET: /CMS/DecredentialingReason/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DecredentialingReason";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DecredentialingReason.GetAll();
            DecredentialingReasonViewModel model = new DecredentialingReasonViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DecredentialingReason/Index.cshtml", model);
        }

        //
        // POST: /CMS/DecredentialingReason/AddEditDecredentialingReason
        [HttpPost]
        public ActionResult AddEditDecredentialingReason(string Code)
        {
            DecredentialingReasonViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DecredentialingReason";
                model = new DecredentialingReasonViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DecredentialingReason";
                model = _DecredentialingReason.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DecredentialingReason/_AddEditDecredentialingReasonForm.cshtml", model);
        }

        //
        // POST: /CMS/DecredentialingReason/SaveDecredentialingReason
        [HttpPost]
        public ActionResult SaveDecredentialingReason(DecredentialingReasonViewModel DecredentialingReason)
        {
            if (ModelState.IsValid)
            {
                int TempID = DecredentialingReason.DecredentialingReasonID;

                if (TempID == 0)
                {
                    DecredentialingReason.CreatedBy = "CMS Team";
                    DecredentialingReason.Source = "CMS Server";
                    DecredentialingReason = _DecredentialingReason.Create(DecredentialingReason);
                }
                else {
                    DecredentialingReason.LastModifiedBy = "CMS Team Update";
                    DecredentialingReason.Source = "CMS Server Update";
                    DecredentialingReason = _DecredentialingReason.Update(DecredentialingReason); }

                if (DecredentialingReason != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DecredentialingReason/_RowDecredentialingReason.cshtml", DecredentialingReason);

                    if (TempID == 0)
                        return Json(new { Message = "New DecredentialingReason Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DecredentialingReason Updated Successfully", Status = true, Type = "Edit", Template = Template });
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