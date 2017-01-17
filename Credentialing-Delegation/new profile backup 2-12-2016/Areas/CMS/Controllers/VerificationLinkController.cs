using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class VerificationLinkController : Controller
    {
        /// <summary>
        /// IVerificationLinkService object reference
        /// </summary>
        private IVerificationLinkService _VerificationLink = null;

        /// <summary>
        /// VerificationLinkController constructor For VerificationLinkService
        /// </summary>
        public VerificationLinkController()
        {
            _VerificationLink = new VerificationLinkService();
        }

        //
        // GET: /CMS/VerificationLink/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "VerificationLink";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _VerificationLink.GetAll();
            VerificationLinkViewModel model = new VerificationLinkViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/VerificationLink/Index.cshtml", model);
        }

        //
        // POST: /CMS/VerificationLink/AddEditVerificationLink
        [HttpPost]
        public ActionResult AddEditVerificationLink(string Code)
        {
            VerificationLinkViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New VerificationLink";
                model = new VerificationLinkViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit VerificationLink";
                model = _VerificationLink.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/VerificationLink/_AddEditVerificationLinkForm.cshtml", model);
        }

        //
        // POST: /CMS/VerificationLink/SaveVerificationLink
        [HttpPost]
        public ActionResult SaveVerificationLink(VerificationLinkViewModel VerificationLink)
        {
            if (ModelState.IsValid)
            {
                int TempID = VerificationLink.VerificationLinkID;

                if (TempID == 0)
                {
                    VerificationLink.CreatedBy = "CMS Team";
                    VerificationLink.Source = "CMS Server";
                    VerificationLink = _VerificationLink.Create(VerificationLink);
                }
                else {
                    VerificationLink.LastModifiedBy = "CMS Team Update";
                    VerificationLink.Source = "CMS Server Update";
                    VerificationLink = _VerificationLink.Update(VerificationLink); }

                if (VerificationLink != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/VerificationLink/_RowVerificationLink.cshtml", VerificationLink);

                    if (TempID == 0)
                        return Json(new { Message = "New VerificationLink Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "VerificationLink Updated Successfully", Status = true, Type = "Edit", Template = Template });
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