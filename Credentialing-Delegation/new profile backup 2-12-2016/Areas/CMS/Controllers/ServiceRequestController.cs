using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ServiceRequestController : Controller
    {
        /// <summary>
        /// IServiceRequestService object reference
        /// </summary>
        private IServiceRequestService _ServiceRequest = null;

        /// <summary>
        /// ServiceRequestController constructor For ServiceRequestService
        /// </summary>
        public ServiceRequestController()
        {
            _ServiceRequest = new ServiceRequestService();
        }

        //
        // GET: /CMS/ServiceRequest/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ServiceRequest";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ServiceRequest.GetAll();
            ServiceRequestViewModel model = new ServiceRequestViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ServiceRequest/Index.cshtml", model);
        }

        //
        // POST: /CMS/ServiceRequest/AddEditServiceRequest
        [HttpPost]
        public ActionResult AddEditServiceRequest(string Code)
        {
            ServiceRequestViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ServiceRequest";
                model = new ServiceRequestViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ServiceRequest";
                model = _ServiceRequest.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ServiceRequest/_AddEditServiceRequestForm.cshtml", model);
        }

        //
        // POST: /CMS/ServiceRequest/SaveServiceRequest
        [HttpPost]
        public ActionResult SaveServiceRequest(ServiceRequestViewModel ServiceRequest)
        {
            if (ModelState.IsValid)
            {
                int TempID = ServiceRequest.ServiceRequestID;

                if (TempID == 0)
                {
                    ServiceRequest.CreatedBy = "CMS Team";
                    ServiceRequest.Source = "CMS Server";
                    ServiceRequest = _ServiceRequest.Create(ServiceRequest);
                }
                else {
                    ServiceRequest.LastModifiedBy = "CMS Team Update";
                    ServiceRequest.Source = "CMS Server Update";
                    ServiceRequest = _ServiceRequest.Update(ServiceRequest); }

                if (ServiceRequest != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ServiceRequest/_RowServiceRequest.cshtml", ServiceRequest);

                    if (TempID == 0)
                        return Json(new { Message = "New ServiceRequest Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ServiceRequest Updated Successfully", Status = true, Type = "Edit", Template = Template });
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