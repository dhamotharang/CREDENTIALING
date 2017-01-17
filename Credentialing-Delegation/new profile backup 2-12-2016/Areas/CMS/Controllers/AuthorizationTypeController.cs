using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class AuthorizationTypeController : Controller
    {
        /// <summary>
        /// IAuthorizationTypeService object reference
        /// </summary>
        private IAuthorizationTypeService _AuthorizationType = null;

        /// <summary>
        /// AuthorizationTypeController constructor For AuthorizationTypeService
        /// </summary>
        public AuthorizationTypeController()
        {
            _AuthorizationType = new AuthorizationTypeService();
        }

        //
        // GET: /CMS/AuthorizationType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "AuthorizationType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _AuthorizationType.GetAll();
            AuthorizationTypeViewModel model = new AuthorizationTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/AuthorizationType/Index.cshtml", model);
        }

        //
        // POST: /CMS/AuthorizationType/AddEditAuthorizationType
        [HttpPost]
        public ActionResult AddEditAuthorizationType(string Code)
        {
            AuthorizationTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New AuthorizationType";
                model = new AuthorizationTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit AuthorizationType";
                model = _AuthorizationType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/AuthorizationType/_AddEditAuthorizationTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/AuthorizationType/SaveAuthorizationType
        [HttpPost]
        public ActionResult SaveAuthorizationType(AuthorizationTypeViewModel AuthorizationType)
        {
            if (ModelState.IsValid)
            {
                int TempID = AuthorizationType.AuthorizationTypeID;

                if (TempID == 0)
                {
                    AuthorizationType.CreatedBy = "CMS Team";
                    AuthorizationType.Source = "CMS Server";
                    AuthorizationType = _AuthorizationType.Create(AuthorizationType);
                }
                else {
                    AuthorizationType.LastModifiedBy = "CMS Team Update";
                    AuthorizationType.Source = "CMS Server Update";
                    AuthorizationType = _AuthorizationType.Update(AuthorizationType); }

                if (AuthorizationType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/AuthorizationType/_RowAuthorizationType.cshtml", AuthorizationType);

                    if (TempID == 0)
                        return Json(new { Message = "New AuthorizationType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "AuthorizationType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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