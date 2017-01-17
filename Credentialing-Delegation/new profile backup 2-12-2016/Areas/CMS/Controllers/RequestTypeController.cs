using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class RequestTypeController : Controller
    {
        /// <summary>
        /// IRequestTypeService object reference
        /// </summary>
        private IRequestTypeService _RequestType = null;

        /// <summary>
        /// RequestTypeController constructor For RequestTypeService
        /// </summary>
        public RequestTypeController()
        {
            _RequestType = new RequestTypeService();
        }

        //
        // GET: /CMS/RequestType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "RequestType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _RequestType.GetAll();
            RequestTypeViewModel model = new RequestTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/RequestType/Index.cshtml", model);
        }

        //
        // POST: /CMS/RequestType/AddEditRequestType
        [HttpPost]
        public ActionResult AddEditRequestType(string Code)
        {
            RequestTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New RequestType";
                model = new RequestTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit RequestType";
                model = _RequestType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/RequestType/_AddEditRequestTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/RequestType/SaveRequestType
        [HttpPost]
        public ActionResult SaveRequestType(RequestTypeViewModel RequestType)
        {
            if (ModelState.IsValid)
            {
                int TempID = RequestType.RequestTypeID;

                if (TempID == 0)
                {
                    RequestType.CreatedBy = "CMS Team";
                    RequestType.Source = "CMS Server";
                    RequestType = _RequestType.Create(RequestType);
                }
                else {
                    RequestType.LastModifiedBy = "CMS Team Update";
                    RequestType.Source = "CMS Server Update";
                    RequestType = _RequestType.Update(RequestType); }

                if (RequestType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/RequestType/_RowRequestType.cshtml", RequestType);

                    if (TempID == 0)
                        return Json(new { Message = "New RequestType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "RequestType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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