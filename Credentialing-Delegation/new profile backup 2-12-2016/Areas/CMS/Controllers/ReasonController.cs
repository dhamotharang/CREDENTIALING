using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ReasonController : Controller
    {
        /// <summary>
        /// IReasonService object reference
        /// </summary>
        private IReasonService _Reason = null;

        /// <summary>
        /// ReasonController constructor For ReasonService
        /// </summary>
        public ReasonController()
        {
            _Reason = new ReasonService();
        }

        //
        // GET: /CMS/Reason/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Reason";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Reason.GetAll();
            ReasonViewModel model = new ReasonViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Reason/Index.cshtml", model);
        }

        //
        // POST: /CMS/Reason/AddEditReason
        [HttpPost]
        public ActionResult AddEditReason(string Code)
        {
            ReasonViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Reason";
                model = new ReasonViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Reason";
                model = _Reason.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Reason/_AddEditReasonForm.cshtml", model);
        }

        //
        // POST: /CMS/Reason/SaveReason
        [HttpPost]
        public ActionResult SaveReason(ReasonViewModel Reason)
        {
            if (ModelState.IsValid)
            {
                int TempID = Reason.ReasonID;

                if (TempID == 0)
                {
                    Reason.CreatedBy = "CMS Team";
                    Reason.Source = "CMS Server";
                    Reason = _Reason.Create(Reason);
                }
                else {
                    Reason.LastModifiedBy = "CMS Team Update";
                    Reason.Source = "CMS Server Update";
                    Reason = _Reason.Update(Reason); }

                if (Reason != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Reason/_RowReason.cshtml", Reason);

                    if (TempID == 0)
                        return Json(new { Message = "New Reason Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Reason Updated Successfully", Status = true, Type = "Edit", Template = Template });
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