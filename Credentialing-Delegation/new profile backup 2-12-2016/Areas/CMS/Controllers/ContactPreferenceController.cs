using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ContactPreferenceController : Controller
    {
        /// <summary>
        /// IContactPreferenceService object reference
        /// </summary>
        private IContactPreferenceService _ContactPreference = null;

        /// <summary>
        /// ContactPreferenceController constructor For ContactPreferenceService
        /// </summary>
        public ContactPreferenceController()
        {
            _ContactPreference = new ContactPreferenceService();
        }

        //
        // GET: /CMS/ContactPreference/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ContactPreference";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ContactPreference.GetAll();
            ContactPreferenceViewModel model = new ContactPreferenceViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ContactPreference/Index.cshtml", model);
        }

        //
        // POST: /CMS/ContactPreference/AddEditContactPreference
        [HttpPost]
        public ActionResult AddEditContactPreference(string Code)
        {
            ContactPreferenceViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ContactPreference";
                model = new ContactPreferenceViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ContactPreference";
                model = _ContactPreference.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ContactPreference/_AddEditContactPreferenceForm.cshtml", model);
        }

        //
        // POST: /CMS/ContactPreference/SaveContactPreference
        [HttpPost]
        public ActionResult SaveContactPreference(ContactPreferenceViewModel ContactPreference)
        {
            if (ModelState.IsValid)
            {
                int TempID = ContactPreference.ContactPreferenceID;

                if (TempID == 0)
                {
                    ContactPreference.CreatedBy = "CMS Team";
                    ContactPreference.Source = "CMS Server";
                    ContactPreference = _ContactPreference.Create(ContactPreference);
                }
                else {
                    ContactPreference.LastModifiedBy = "CMS Team Update";
                    ContactPreference.Source = "CMS Server Update";
                    ContactPreference = _ContactPreference.Update(ContactPreference); }

                if (ContactPreference != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ContactPreference/_RowContactPreference.cshtml", ContactPreference);

                    if (TempID == 0)
                        return Json(new { Message = "New ContactPreference Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ContactPreference Updated Successfully", Status = true, Type = "Edit", Template = Template });
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