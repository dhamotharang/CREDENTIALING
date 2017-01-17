using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class POSRoomTypeController : Controller
    {
        /// <summary>
        /// IPOSRoomTypeService object reference
        /// </summary>
        private IPOSRoomTypeService _POSRoomType = null;

        /// <summary>
        /// POSRoomTypeController constructor For POSRoomTypeService
        /// </summary>
        public POSRoomTypeController()
        {
            _POSRoomType = new POSRoomTypeService();
        }

        //
        // GET: /CMS/POSRoomType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "POSRoomType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _POSRoomType.GetAll();
            POSRoomTypeViewModel model = new POSRoomTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/POSRoomType/Index.cshtml", model);
        }

        //
        // POST: /CMS/POSRoomType/AddEditPOSRoomType
        [HttpPost]
        public ActionResult AddEditPOSRoomType(string Code)
        {
            POSRoomTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New POSRoomType";
                model = new POSRoomTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit POSRoomType";
                model = _POSRoomType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/POSRoomType/_AddEditPOSRoomTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/POSRoomType/SavePOSRoomType
        [HttpPost]
        public ActionResult SavePOSRoomType(POSRoomTypeViewModel POSRoomType)
        {
            if (ModelState.IsValid)
            {
                int TempID = POSRoomType.POSRoomTypeID;

                if (TempID == 0)
                {
                    POSRoomType.CreatedBy = "CMS Team";
                    POSRoomType.Source = "CMS Server";
                    POSRoomType = _POSRoomType.Create(POSRoomType);
                }
                else {
                    POSRoomType.LastModifiedBy = "CMS Team Update";
                    POSRoomType.Source = "CMS Server Update";
                    POSRoomType = _POSRoomType.Update(POSRoomType); }

                if (POSRoomType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/POSRoomType/_RowPOSRoomType.cshtml", POSRoomType);

                    if (TempID == 0)
                        return Json(new { Message = "New POSRoomType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "POSRoomType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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