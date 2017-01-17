using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ContactOutcomeController : Controller
    {
        /// <summary>
        /// IContactOutcomeService object reference
        /// </summary>
        private IContactOutcomeService _ContactOutcome = null;

        /// <summary>
        /// ContactOutcomeController constructor For ContactOutcomeService
        /// </summary>
        public ContactOutcomeController()
        {
            _ContactOutcome = new ContactOutcomeService();
        }

        //
        // GET: /CMS/ContactOutcome/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ContactOutcome";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ContactOutcome.GetAll();
            ContactOutcomeViewModel model = new ContactOutcomeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ContactOutcome/Index.cshtml", model);
        }

        //
        // POST: /CMS/ContactOutcome/AddEditContactOutcome
        [HttpPost]
        public ActionResult AddEditContactOutcome(string Code)
        {
            ContactOutcomeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ContactOutcome";
                model = new ContactOutcomeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ContactOutcome";
                model = _ContactOutcome.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ContactOutcome/_AddEditContactOutcomeForm.cshtml", model);
        }

        //
        // POST: /CMS/ContactOutcome/SaveContactOutcome
        [HttpPost]
        public ActionResult SaveContactOutcome(ContactOutcomeViewModel ContactOutcome)
        {
            if (ModelState.IsValid)
            {
                int TempID = ContactOutcome.ContactOutcomeID;

                if (TempID == 0)
                {
                    ContactOutcome.CreatedBy = "CMS Team";
                    ContactOutcome.Source = "CMS Server";
                    ContactOutcome = _ContactOutcome.Create(ContactOutcome);
                }
                else {
                    ContactOutcome.LastModifiedBy = "CMS Team Update";
                    ContactOutcome.Source = "CMS Server Update";
                    ContactOutcome = _ContactOutcome.Update(ContactOutcome); }

                if (ContactOutcome != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ContactOutcome/_RowContactOutcome.cshtml", ContactOutcome);

                    if (TempID == 0)
                        return Json(new { Message = "New ContactOutcome Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ContactOutcome Updated Successfully", Status = true, Type = "Edit", Template = Template });
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