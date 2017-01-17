using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ContactEntityTypeController : Controller
    {
        /// <summary>
        /// IContactEntityTypeService object reference
        /// </summary>
        private IContactEntityTypeService _ContactEntityType = null;

        /// <summary>
        /// ContactEntityTypeController constructor For ContactEntityTypeService
        /// </summary>
        public ContactEntityTypeController()
        {
            _ContactEntityType = new ContactEntityTypeService();
        }

        //
        // GET: /CMS/ContactEntityType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ContactEntityType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ContactEntityType.GetAll();
            ContactEntityTypeViewModel model = new ContactEntityTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ContactEntityType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ContactEntityType/AddEditContactEntityType
        [HttpPost]
        public ActionResult AddEditContactEntityType(string Code)
        {
            ContactEntityTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ContactEntityType";
                model = new ContactEntityTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ContactEntityType";
                model = _ContactEntityType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ContactEntityType/_AddEditContactEntityTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ContactEntityType/SaveContactEntityType
        [HttpPost]
        public ActionResult SaveContactEntityType(ContactEntityTypeViewModel ContactEntityType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ContactEntityType.ContactEntityTypeID;

                if (TempID == 0)
                {
                    ContactEntityType.CreatedBy = "CMS Team";
                    ContactEntityType.Source = "CMS Server";
                    ContactEntityType = _ContactEntityType.Create(ContactEntityType);
                }
                else {
                    ContactEntityType.LastModifiedBy = "CMS Team Update";
                    ContactEntityType.Source = "CMS Server Update";
                    ContactEntityType = _ContactEntityType.Update(ContactEntityType); }

                if (ContactEntityType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ContactEntityType/_RowContactEntityType.cshtml", ContactEntityType);

                    if (TempID == 0)
                        return Json(new { Message = "New ContactEntityType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ContactEntityType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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