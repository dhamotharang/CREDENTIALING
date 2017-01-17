using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class StateLicenseStatusController : Controller
    {
        /// <summary>
        /// IStateLicenseStatusService object reference
        /// </summary>
        private IStateLicenseStatusService _StateLicenseStatus = null;

        /// <summary>
        /// StateLicenseStatusController constructor For StateLicenseStatusService
        /// </summary>
        public StateLicenseStatusController()
        {
            _StateLicenseStatus = new StateLicenseStatusService();
        }

        //
        // GET: /CMS/StateLicenseStatus/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "StateLicenseStatus";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _StateLicenseStatus.GetAll();
            StateLicenseStatusViewModel model = new StateLicenseStatusViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/StateLicenseStatus/Index.cshtml", model);
        }

        //
        // POST: /CMS/StateLicenseStatus/AddEditStateLicenseStatus
        [HttpPost]
        public ActionResult AddEditStateLicenseStatus(string Code)
        {
            StateLicenseStatusViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New StateLicenseStatus";
                model = new StateLicenseStatusViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit StateLicenseStatus";
                model = _StateLicenseStatus.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/StateLicenseStatus/_AddEditStateLicenseStatusForm.cshtml", model);
        }

        //
        // POST: /CMS/StateLicenseStatus/SaveStateLicenseStatus
        [HttpPost]
        public ActionResult SaveStateLicenseStatus(StateLicenseStatusViewModel StateLicenseStatus)
        {
            if (ModelState.IsValid)
            {
                int TempID = StateLicenseStatus.StateLicenseStatusID;

                if (TempID == 0)
                {
                    StateLicenseStatus.CreatedBy = "CMS Team";
                    StateLicenseStatus.Source = "CMS Server";
                    StateLicenseStatus = _StateLicenseStatus.Create(StateLicenseStatus);
                }
                else {
                    StateLicenseStatus.LastModifiedBy = "CMS Team Update";
                    StateLicenseStatus.Source = "CMS Server Update";
                    StateLicenseStatus = _StateLicenseStatus.Update(StateLicenseStatus); }

                if (StateLicenseStatus != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/StateLicenseStatus/_RowStateLicenseStatus.cshtml", StateLicenseStatus);

                    if (TempID == 0)
                        return Json(new { Message = "New StateLicenseStatus Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "StateLicenseStatus Updated Successfully", Status = true, Type = "Edit", Template = Template });
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