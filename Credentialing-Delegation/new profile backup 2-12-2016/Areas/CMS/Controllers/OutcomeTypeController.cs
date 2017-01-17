using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class OutcomeTypeController : Controller
    {
        /// <summary>
        /// IOutcomeTypeService object reference
        /// </summary>
        private IOutcomeTypeService _OutcomeType = null;

        /// <summary>
        /// OutcomeTypeController constructor For OutcomeTypeService
        /// </summary>
        public OutcomeTypeController()
        {
            _OutcomeType = new OutcomeTypeService();
        }

        //
        // GET: /CMS/OutcomeType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "OutcomeType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _OutcomeType.GetAll();
            OutcomeTypeViewModel model = new OutcomeTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/OutcomeType/Index.cshtml", model);
        }

        //
        // POST: /CMS/OutcomeType/AddEditOutcomeType
        [HttpPost]
        public ActionResult AddEditOutcomeType(string Code)
        {
            OutcomeTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New OutcomeType";
                model = new OutcomeTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit OutcomeType";
                model = _OutcomeType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/OutcomeType/_AddEditOutcomeTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/OutcomeType/SaveOutcomeType
        [HttpPost]
        public ActionResult SaveOutcomeType(OutcomeTypeViewModel OutcomeType)
        {
            if (ModelState.IsValid)
            {
                int TempID = OutcomeType.OutcomeTypeID;

                if (TempID == 0)
                {
                    OutcomeType.CreatedBy = "CMS Team";
                    OutcomeType.Source = "CMS Server";
                    OutcomeType = _OutcomeType.Create(OutcomeType);
                }
                else {
                    OutcomeType.LastModifiedBy = "CMS Team Update";
                    OutcomeType.Source = "CMS Server Update";
                    OutcomeType = _OutcomeType.Update(OutcomeType); }

                if (OutcomeType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/OutcomeType/_RowOutcomeType.cshtml", OutcomeType);

                    if (TempID == 0)
                        return Json(new { Message = "New OutcomeType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "OutcomeType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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