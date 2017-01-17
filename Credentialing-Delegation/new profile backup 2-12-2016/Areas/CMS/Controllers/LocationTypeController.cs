using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class LocationTypeController : Controller
    {
        /// <summary>
        /// ILocationTypeService object reference
        /// </summary>
        private ILocationTypeService _LocationType = null;

        /// <summary>
        /// LocationTypeController constructor For LocationTypeService
        /// </summary>
        public LocationTypeController()
        {
            _LocationType = new LocationTypeService();
        }

        //
        // GET: /CMS/LocationType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "LocationType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _LocationType.GetAll();
            LocationTypeViewModel model = new LocationTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/LocationType/Index.cshtml", model);
        }

        //
        // POST: /CMS/LocationType/AddEditLocationType
        [HttpPost]
        public ActionResult AddEditLocationType(string Code)
        {
            LocationTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New LocationType";
                model = new LocationTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit LocationType";
                model = _LocationType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/LocationType/_AddEditLocationTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/LocationType/SaveLocationType
        [HttpPost]
        public ActionResult SaveLocationType(LocationTypeViewModel LocationType)
        {
            if (ModelState.IsValid)
            {
                int TempID = LocationType.LocationTypeID;

                if (TempID == 0)
                {
                    LocationType.CreatedBy = "CMS Team";
                    LocationType.Source = "CMS Server";
                    LocationType = _LocationType.Create(LocationType);
                }
                else {
                    LocationType.LastModifiedBy = "CMS Team Update";
                    LocationType.Source = "CMS Server Update";
                    LocationType = _LocationType.Update(LocationType); }

                if (LocationType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/LocationType/_RowLocationType.cshtml", LocationType);

                    if (TempID == 0)
                        return Json(new { Message = "New LocationType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "LocationType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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