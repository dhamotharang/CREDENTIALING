using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ClaimTypeController : Controller
    {
        /// <summary>
        /// IClaimTypeService object reference
        /// </summary>
        private IClaimTypeService _ClaimType = null;

        /// <summary>
        /// ClaimTypeController constructor For ClaimTypeService
        /// </summary>
        public ClaimTypeController()
        {
            _ClaimType = new ClaimTypeService();
        }

        //
        // GET: /CMS/ClaimType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ClaimType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ClaimType.GetAll();
            ClaimTypeViewModel model = new ClaimTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ClaimType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ClaimType/AddEditClaimType
        [HttpPost]
        public ActionResult AddEditClaimType(string Code)
        {
            ClaimTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ClaimType";
                model = new ClaimTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ClaimType";
                model = _ClaimType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ClaimType/_AddEditClaimTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ClaimType/SaveClaimType
        [HttpPost]
        public ActionResult SaveClaimType(ClaimTypeViewModel ClaimType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ClaimType.ClaimTypeID;

                if (TempID == 0)
                {
                    ClaimType.CreatedBy = "CMS Team";
                    ClaimType.Source = "CMS Server";
                    ClaimType = _ClaimType.Create(ClaimType);
                }
                else {
                    ClaimType.LastModifiedBy = "CMS Team Update";
                    ClaimType.Source = "CMS Server Update";
                    ClaimType = _ClaimType.Update(ClaimType); }

                if (ClaimType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ClaimType/_RowClaimType.cshtml", ClaimType);

                    if (TempID == 0)
                        return Json(new { Message = "New ClaimType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ClaimType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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