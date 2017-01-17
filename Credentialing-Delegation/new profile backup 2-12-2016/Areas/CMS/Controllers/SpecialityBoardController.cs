using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class SpecialityBoardController : Controller
    {
        /// <summary>
        /// ISpecialityBoardService object reference
        /// </summary>
        private ISpecialityBoardService _SpecialityBoard = null;

        /// <summary>
        /// SpecialityBoardController constructor For SpecialityBoardService
        /// </summary>
        public SpecialityBoardController()
        {
            _SpecialityBoard = new SpecialityBoardService();
        }

        //
        // GET: /CMS/SpecialityBoard/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "SpecialityBoard";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _SpecialityBoard.GetAll();
            SpecialityBoardViewModel model = new SpecialityBoardViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/SpecialityBoard/Index.cshtml", model);
        }

        //
        // POST: /CMS/SpecialityBoard/AddEditSpecialityBoard
        [HttpPost]
        public ActionResult AddEditSpecialityBoard(string Code)
        {
            SpecialityBoardViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New SpecialityBoard";
                model = new SpecialityBoardViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit SpecialityBoard";
                model = _SpecialityBoard.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/SpecialityBoard/_AddEditSpecialityBoardForm.cshtml", model);
        }

        //
        // POST: /CMS/SpecialityBoard/SaveSpecialityBoard
        [HttpPost]
        public ActionResult SaveSpecialityBoard(SpecialityBoardViewModel SpecialityBoard)
        {
            if (ModelState.IsValid)
            {
                int TempID = SpecialityBoard.SpecialityBoardID;

                if (TempID == 0)
                {
                    SpecialityBoard.CreatedBy = "CMS Team";
                    SpecialityBoard.Source = "CMS Server";
                    SpecialityBoard = _SpecialityBoard.Create(SpecialityBoard);
                }
                else {
                    SpecialityBoard.LastModifiedBy = "CMS Team Update";
                    SpecialityBoard.Source = "CMS Server Update";
                    SpecialityBoard = _SpecialityBoard.Update(SpecialityBoard); }

                if (SpecialityBoard != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/SpecialityBoard/_RowSpecialityBoard.cshtml", SpecialityBoard);

                    if (TempID == 0)
                        return Json(new { Message = "New SpecialityBoard Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "SpecialityBoard Updated Successfully", Status = true, Type = "Edit", Template = Template });
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