using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class VisaStatusController : Controller
    {
        /// <summary>
        /// IVisaStatusService object reference
        /// </summary>
        private IVisaStatusService _VisaStatus = null;

        /// <summary>
        /// VisaStatusController constructor For VisaStatusService
        /// </summary>
        public VisaStatusController()
        {
            _VisaStatus = new VisaStatusService();
        }

        //
        // GET: /CMS/VisaStatus/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "VisaStatus";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _VisaStatus.GetAll();
            VisaStatusViewModel model = new VisaStatusViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/VisaStatus/Index.cshtml", model);
        }

        //
        // POST: /CMS/VisaStatus/AddEditVisaStatus
        [HttpPost]
        public ActionResult AddEditVisaStatus(string Code)
        {
            VisaStatusViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New VisaStatus";
                model = new VisaStatusViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit VisaStatus";
                model = _VisaStatus.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/VisaStatus/_AddEditVisaStatusForm.cshtml", model);
        }

        //
        // POST: /CMS/VisaStatus/SaveVisaStatus
        [HttpPost]
        public ActionResult SaveVisaStatus(VisaStatusViewModel VisaStatus)
        {
            if (ModelState.IsValid)
            {
                int TempID = VisaStatus.VisaStatusID;

                if (TempID == 0)
                {
                    VisaStatus.CreatedBy = "CMS Team";
                    VisaStatus.Source = "CMS Server";
                    VisaStatus = _VisaStatus.Create(VisaStatus);
                }
                else {
                    VisaStatus.LastModifiedBy = "CMS Team Update";
                    VisaStatus.Source = "CMS Server Update";
                    VisaStatus = _VisaStatus.Update(VisaStatus); }

                if (VisaStatus != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/VisaStatus/_RowVisaStatus.cshtml", VisaStatus);

                    if (TempID == 0)
                        return Json(new { Message = "New VisaStatus Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "VisaStatus Updated Successfully", Status = true, Type = "Edit", Template = Template });
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