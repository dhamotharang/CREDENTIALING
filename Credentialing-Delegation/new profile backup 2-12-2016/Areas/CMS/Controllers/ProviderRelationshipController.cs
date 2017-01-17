using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ProviderRelationshipController : Controller
    {
        /// <summary>
        /// IProviderRelationshipService object reference
        /// </summary>
        private IProviderRelationshipService _ProviderRelationship = null;

        /// <summary>
        /// ProviderRelationshipController constructor For ProviderRelationshipService
        /// </summary>
        public ProviderRelationshipController()
        {
            _ProviderRelationship = new ProviderRelationshipService();
        }

        //
        // GET: /CMS/ProviderRelationship/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ProviderRelationship";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ProviderRelationship.GetAll();
            ProviderRelationshipViewModel model = new ProviderRelationshipViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ProviderRelationship/Index.cshtml", model);
        }

        //
        // POST: /CMS/ProviderRelationship/AddEditProviderRelationship
        [HttpPost]
        public ActionResult AddEditProviderRelationship(string Code)
        {
            ProviderRelationshipViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ProviderRelationship";
                model = new ProviderRelationshipViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ProviderRelationship";
                model = _ProviderRelationship.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ProviderRelationship/_AddEditProviderRelationshipForm.cshtml", model);
        }

        //
        // POST: /CMS/ProviderRelationship/SaveProviderRelationship
        [HttpPost]
        public ActionResult SaveProviderRelationship(ProviderRelationshipViewModel ProviderRelationship)
        {
            if (ModelState.IsValid)
            {
                int TempID = ProviderRelationship.ProviderRelationshipID;

                if (TempID == 0)
                {
                    ProviderRelationship.CreatedBy = "CMS Team";
                    ProviderRelationship.Source = "CMS Server";
                    ProviderRelationship = _ProviderRelationship.Create(ProviderRelationship);
                }
                else {
                    ProviderRelationship.LastModifiedBy = "CMS Team Update";
                    ProviderRelationship.Source = "CMS Server Update";
                    ProviderRelationship = _ProviderRelationship.Update(ProviderRelationship); }

                if (ProviderRelationship != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ProviderRelationship/_RowProviderRelationship.cshtml", ProviderRelationship);

                    if (TempID == 0)
                        return Json(new { Message = "New ProviderRelationship Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ProviderRelationship Updated Successfully", Status = true, Type = "Edit", Template = Template });
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