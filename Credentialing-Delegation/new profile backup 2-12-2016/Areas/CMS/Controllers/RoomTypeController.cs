using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class RoomTypeController : Controller
    {
        /// <summary>
        /// IRoomTypeService object reference
        /// </summary>
        private IRoomTypeService _RoomType = null;

        /// <summary>
        /// RoomTypeController constructor For RoomTypeService
        /// </summary>
        public RoomTypeController()
        {
            _RoomType = new RoomTypeService();
        }

        //
        // GET: /CMS/RoomType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "RoomType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _RoomType.GetAll();
            RoomTypeViewModel model = new RoomTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/RoomType/Index.cshtml", model);
        }

        //
        // POST: /CMS/RoomType/AddEditRoomType
        [HttpPost]
        public ActionResult AddEditRoomType(string Code)
        {
            RoomTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New RoomType";
                model = new RoomTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit RoomType";
                model = _RoomType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/RoomType/_AddEditRoomTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/RoomType/SaveRoomType
        [HttpPost]
        public ActionResult SaveRoomType(RoomTypeViewModel RoomType)
        {
            if (ModelState.IsValid)
            {
                int TempID = RoomType.RoomTypeID;

                if (TempID == 0)
                {
                    RoomType.CreatedBy = "CMS Team";
                    RoomType.Source = "CMS Server";
                    RoomType = _RoomType.Create(RoomType);
                }
                else {
                    RoomType.LastModifiedBy = "CMS Team Update";
                    RoomType.Source = "CMS Server Update";
                    RoomType = _RoomType.Update(RoomType); }

                if (RoomType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/RoomType/_RowRoomType.cshtml", RoomType);

                    if (TempID == 0)
                        return Json(new { Message = "New RoomType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "RoomType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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