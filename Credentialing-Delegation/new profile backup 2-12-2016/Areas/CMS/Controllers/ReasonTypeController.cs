using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ReasonTypeController : Controller
    {
        /// <summary>
        /// IReasonTypeService object reference
        /// </summary>
        private IReasonTypeService _ReasonType = null;

        /// <summary>
        /// ReasonTypeController constructor For ReasonTypeService
        /// </summary>
        public ReasonTypeController()
        {
            _ReasonType = new ReasonTypeService();
        }

        //
        // GET: /CMS/ReasonType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ReasonType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ReasonType.GetAll();
            ReasonTypeViewModel model = new ReasonTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ReasonType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ReasonType/AddEditReasonType
        [HttpPost]
        public ActionResult AddEditReasonType(string Code)
        {
            ReasonTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ReasonType";
                model = new ReasonTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ReasonType";
                model = _ReasonType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ReasonType/_AddEditReasonTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ReasonType/SaveReasonType
        [HttpPost]
        public ActionResult SaveReasonType(ReasonTypeViewModel ReasonType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ReasonType.ReasonTypeID;

                if (TempID == 0)
                {
                    ReasonType.CreatedBy = "CMS Team";
                    ReasonType.Source = "CMS Server";
                    ReasonType = _ReasonType.Create(ReasonType);
                }
                else {
                    ReasonType.LastModifiedBy = "CMS Team Update";
                    ReasonType.Source = "CMS Server Update";
                    ReasonType = _ReasonType.Update(ReasonType); }

                if (ReasonType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ReasonType/_RowReasonType.cshtml", ReasonType);

                    if (TempID == 0)
                        return Json(new { Message = "New ReasonType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ReasonType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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