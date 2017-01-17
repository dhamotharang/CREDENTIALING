using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class PremiumTypeController : Controller
    {
        /// <summary>
        /// IPremiumTypeService object reference
        /// </summary>
        private IPremiumTypeService _PremiumType = null;

        /// <summary>
        /// PremiumTypeController constructor For PremiumTypeService
        /// </summary>
        public PremiumTypeController()
        {
            _PremiumType = new PremiumTypeService();
        }

        //
        // GET: /CMS/PremiumType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "PremiumType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _PremiumType.GetAll();
            PremiumTypeViewModel model = new PremiumTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/PremiumType/Index.cshtml", model);
        }

        //
        // POST: /CMS/PremiumType/AddEditPremiumType
        [HttpPost]
        public ActionResult AddEditPremiumType(string Code)
        {
            PremiumTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New PremiumType";
                model = new PremiumTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit PremiumType";
                model = _PremiumType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/PremiumType/_AddEditPremiumTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/PremiumType/SavePremiumType
        [HttpPost]
        public ActionResult SavePremiumType(PremiumTypeViewModel PremiumType)
        {
            if (ModelState.IsValid)
            {
                int TempID = PremiumType.PremiumTypeID;

                if (TempID == 0)
                {
                    PremiumType.CreatedBy = "CMS Team";
                    PremiumType.Source = "CMS Server";
                    PremiumType = _PremiumType.Create(PremiumType);
                }
                else {
                    PremiumType.LastModifiedBy = "CMS Team Update";
                    PremiumType.Source = "CMS Server Update";
                    PremiumType = _PremiumType.Update(PremiumType); }

                if (PremiumType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/PremiumType/_RowPremiumType.cshtml", PremiumType);

                    if (TempID == 0)
                        return Json(new { Message = "New PremiumType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "PremiumType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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