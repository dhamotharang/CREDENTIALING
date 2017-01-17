using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ContactTypeController : Controller
    {
        /// <summary>
        /// IContactTypeService object reference
        /// </summary>
        private IContactTypeService _ContactType = null;

        /// <summary>
        /// ContactTypeController constructor For ContactTypeService
        /// </summary>
        public ContactTypeController()
        {
            _ContactType = new ContactTypeService();
        }

        //
        // GET: /CMS/ContactType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ContactType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ContactType.GetAll();
            ContactTypeViewModel model = new ContactTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ContactType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ContactType/AddEditContactType
        [HttpPost]
        public ActionResult AddEditContactType(string Code)
        {
            ContactTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ContactType";
                model = new ContactTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ContactType";
                model = _ContactType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ContactType/_AddEditContactTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ContactType/SaveContactType
        [HttpPost]
        public ActionResult SaveContactType(ContactTypeViewModel ContactType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ContactType.ContactTypeID;

                if (TempID == 0)
                {
                    ContactType.CreatedBy = "CMS Team";
                    ContactType.Source = "CMS Server";
                    ContactType = _ContactType.Create(ContactType);
                }
                else {
                    ContactType.LastModifiedBy = "CMS Team Update";
                    ContactType.Source = "CMS Server Update";
                    ContactType = _ContactType.Update(ContactType); }

                if (ContactType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ContactType/_RowContactType.cshtml", ContactType);

                    if (TempID == 0)
                        return Json(new { Message = "New ContactType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ContactType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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