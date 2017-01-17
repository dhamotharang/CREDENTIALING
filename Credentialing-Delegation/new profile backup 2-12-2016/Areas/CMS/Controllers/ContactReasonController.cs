using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ContactReasonController : Controller
    {
        /// <summary>
        /// IContactReasonService object reference
        /// </summary>
        private IContactReasonService _ContactReason = null;

        /// <summary>
        /// ContactReasonController constructor For ContactReasonService
        /// </summary>
        public ContactReasonController()
        {
            _ContactReason = new ContactReasonService();
        }

        //
        // GET: /CMS/ContactReason/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ContactReason";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ContactReason.GetAll();
            ContactReasonViewModel model = new ContactReasonViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ContactReason/Index.cshtml", model);
        }

        //
        // POST: /CMS/ContactReason/AddEditContactReason
        [HttpPost]
        public ActionResult AddEditContactReason(string Code)
        {
            ContactReasonViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ContactReason";
                model = new ContactReasonViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ContactReason";
                model = _ContactReason.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ContactReason/_AddEditContactReasonForm.cshtml", model);
        }

        //
        // POST: /CMS/ContactReason/SaveContactReason
        [HttpPost]
        public ActionResult SaveContactReason(ContactReasonViewModel ContactReason)
        {
            if (ModelState.IsValid)
            {
                int TempID = ContactReason.ContactReasonID;

                if (TempID == 0)
                {
                    ContactReason.CreatedBy = "CMS Team";
                    ContactReason.Source = "CMS Server";
                    ContactReason = _ContactReason.Create(ContactReason);
                }
                else {
                    ContactReason.LastModifiedBy = "CMS Team Update";
                    ContactReason.Source = "CMS Server Update";
                    ContactReason = _ContactReason.Update(ContactReason); }

                if (ContactReason != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ContactReason/_RowContactReason.cshtml", ContactReason);

                    if (TempID == 0)
                        return Json(new { Message = "New ContactReason Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ContactReason Updated Successfully", Status = true, Type = "Edit", Template = Template });
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