using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class POSTypeOfCareController : Controller
    {
        /// <summary>
        /// IPOSTypeOfCareService object reference
        /// </summary>
        private IPOSTypeOfCareService _POSTypeOfCare = null;

        /// <summary>
        /// POSTypeOfCareController constructor For POSTypeOfCareService
        /// </summary>
        public POSTypeOfCareController()
        {
            _POSTypeOfCare = new POSTypeOfCareService();
        }

        //
        // GET: /CMS/POSTypeOfCare/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "POSTypeOfCare";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _POSTypeOfCare.GetAll();
            POSTypeOfCareViewModel model = new POSTypeOfCareViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/POSTypeOfCare/Index.cshtml", model);
        }

        //
        // POST: /CMS/POSTypeOfCare/AddEditPOSTypeOfCare
        [HttpPost]
        public ActionResult AddEditPOSTypeOfCare(string Code)
        {
            POSTypeOfCareViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New POSTypeOfCare";
                model = new POSTypeOfCareViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit POSTypeOfCare";
                model = _POSTypeOfCare.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/POSTypeOfCare/_AddEditPOSTypeOfCareForm.cshtml", model);
        }

        //
        // POST: /CMS/POSTypeOfCare/SavePOSTypeOfCare
        [HttpPost]
        public ActionResult SavePOSTypeOfCare(POSTypeOfCareViewModel POSTypeOfCare)
        {
            if (ModelState.IsValid)
            {
                int TempID = POSTypeOfCare.POSTypeOfCareID;

                if (TempID == 0)
                {
                    POSTypeOfCare.CreatedBy = "CMS Team";
                    POSTypeOfCare.Source = "CMS Server";
                    POSTypeOfCare = _POSTypeOfCare.Create(POSTypeOfCare);
                }
                else {
                    POSTypeOfCare.LastModifiedBy = "CMS Team Update";
                    POSTypeOfCare.Source = "CMS Server Update";
                    POSTypeOfCare = _POSTypeOfCare.Update(POSTypeOfCare); }

                if (POSTypeOfCare != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/POSTypeOfCare/_RowPOSTypeOfCare.cshtml", POSTypeOfCare);

                    if (TempID == 0)
                        return Json(new { Message = "New POSTypeOfCare Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "POSTypeOfCare Updated Successfully", Status = true, Type = "Edit", Template = Template });
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