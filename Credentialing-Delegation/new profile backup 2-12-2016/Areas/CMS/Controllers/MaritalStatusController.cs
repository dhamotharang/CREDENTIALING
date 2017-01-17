using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class MaritalStatusController : Controller
    {
        /// <summary>
        /// IMaritalStatusService object reference
        /// </summary>
        private IMaritalStatusService _MaritalStatus = null;

        /// <summary>
        /// MaritalStatusController constructor For MaritalStatusService
        /// </summary>
        public MaritalStatusController()
        {
            _MaritalStatus = new MaritalStatusService();
        }

        //
        // GET: /CMS/MaritalStatus/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "MaritalStatus";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _MaritalStatus.GetAll();
            MaritalStatusViewModel model = new MaritalStatusViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/MaritalStatus/Index.cshtml", model);
        }

        //
        // POST: /CMS/MaritalStatus/AddEditMaritalStatus
        [HttpPost]
        public ActionResult AddEditMaritalStatus(string Code)
        {
            MaritalStatusViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New MaritalStatus";
                model = new MaritalStatusViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit MaritalStatus";
                model = _MaritalStatus.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/MaritalStatus/_AddEditMaritalStatusForm.cshtml", model);
        }

        //
        // POST: /CMS/MaritalStatus/SaveMaritalStatus
        [HttpPost]
        public ActionResult SaveMaritalStatus(MaritalStatusViewModel MaritalStatus)
        {
            if (ModelState.IsValid)
            {
                int TempID = MaritalStatus.MaritalStatusID;

                if (TempID == 0)
                {
                    MaritalStatus.CreatedBy = "CMS Team";
                    MaritalStatus.Source = "CMS Server";
                    MaritalStatus = _MaritalStatus.Create(MaritalStatus);
                }
                else {
                    MaritalStatus.LastModifiedBy = "CMS Team Update";
                    MaritalStatus.Source = "CMS Server Update";
                    MaritalStatus = _MaritalStatus.Update(MaritalStatus); }

                if (MaritalStatus != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/MaritalStatus/_RowMaritalStatus.cshtml", MaritalStatus);

                    if (TempID == 0)
                        return Json(new { Message = "New MaritalStatus Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "MaritalStatus Updated Successfully", Status = true, Type = "Edit", Template = Template });
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