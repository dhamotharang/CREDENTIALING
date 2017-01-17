using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ContactDirectionController : Controller
    {
        /// <summary>
        /// IContactDirectionService object reference
        /// </summary>
        private IContactDirectionService _ContactDirection = null;

        /// <summary>
        /// ContactDirectionController constructor For ContactDirectionService
        /// </summary>
        public ContactDirectionController()
        {
            _ContactDirection = new ContactDirectionService();
        }

        //
        // GET: /CMS/ContactDirection/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ContactDirection";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ContactDirection.GetAll();
            ContactDirectionViewModel model = new ContactDirectionViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ContactDirection/Index.cshtml", model);
        }

        //
        // POST: /CMS/ContactDirection/AddEditContactDirection
        [HttpPost]
        public ActionResult AddEditContactDirection(string Code)
        {
            ContactDirectionViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ContactDirection";
                model = new ContactDirectionViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ContactDirection";
                model = _ContactDirection.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ContactDirection/_AddEditContactDirectionForm.cshtml", model);
        }

        //
        // POST: /CMS/ContactDirection/SaveContactDirection
        [HttpPost]
        public ActionResult SaveContactDirection(ContactDirectionViewModel ContactDirection)
        {
            if (ModelState.IsValid)
            {
                int TempID = ContactDirection.ContactDirectionID;

                if (TempID == 0)
                {
                    ContactDirection.CreatedBy = "CMS Team";
                    ContactDirection.Source = "CMS Server";
                    ContactDirection = _ContactDirection.Create(ContactDirection);
                }
                else {
                    ContactDirection.LastModifiedBy = "CMS Team Update";
                    ContactDirection.Source = "CMS Server Update";
                    ContactDirection = _ContactDirection.Update(ContactDirection); }

                if (ContactDirection != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ContactDirection/_RowContactDirection.cshtml", ContactDirection);

                    if (TempID == 0)
                        return Json(new { Message = "New ContactDirection Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ContactDirection Updated Successfully", Status = true, Type = "Edit", Template = Template });
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