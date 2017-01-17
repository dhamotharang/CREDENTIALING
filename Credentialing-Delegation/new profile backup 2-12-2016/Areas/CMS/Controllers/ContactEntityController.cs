using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ContactEntityController : Controller
    {
        /// <summary>
        /// IContactEntityService object reference
        /// </summary>
        private IContactEntityService _ContactEntity = null;

        /// <summary>
        /// ContactEntityController constructor For ContactEntityService
        /// </summary>
        public ContactEntityController()
        {
            _ContactEntity = new ContactEntityService();
        }

        //
        // GET: /CMS/ContactEntity/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ContactEntity";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ContactEntity.GetAll();
            ContactEntityViewModel model = new ContactEntityViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ContactEntity/Index.cshtml", model);
        }

        //
        // POST: /CMS/ContactEntity/AddEditContactEntity
        [HttpPost]
        public ActionResult AddEditContactEntity(string Code)
        {
            ContactEntityViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ContactEntity";
                model = new ContactEntityViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ContactEntity";
                model = _ContactEntity.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ContactEntity/_AddEditContactEntityForm.cshtml", model);
        }

        //
        // POST: /CMS/ContactEntity/SaveContactEntity
        [HttpPost]
        public ActionResult SaveContactEntity(ContactEntityViewModel ContactEntity)
        {
            if (ModelState.IsValid)
            {
                int TempID = ContactEntity.ContactEntityID;

                if (TempID == 0)
                {
                    ContactEntity.CreatedBy = "CMS Team";
                    ContactEntity.Source = "CMS Server";
                    ContactEntity = _ContactEntity.Create(ContactEntity);
                }
                else {
                    ContactEntity.LastModifiedBy = "CMS Team Update";
                    ContactEntity.Source = "CMS Server Update";
                    ContactEntity = _ContactEntity.Update(ContactEntity); }

                if (ContactEntity != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ContactEntity/_RowContactEntity.cshtml", ContactEntity);

                    if (TempID == 0)
                        return Json(new { Message = "New ContactEntity Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ContactEntity Updated Successfully", Status = true, Type = "Edit", Template = Template });
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