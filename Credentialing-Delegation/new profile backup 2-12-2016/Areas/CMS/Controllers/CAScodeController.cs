using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class CAScodeController : Controller
    {
        /// <summary>
        /// ICAScodeService object reference
        /// </summary>
        private ICAScodeService _CAScode = null;

        /// <summary>
        /// CAScodeController constructor For CAScodeService
        /// </summary>
        public CAScodeController()
        {
            _CAScode = new CAScodeService();
        }

        //
        // GET: /CMS/CAScode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "CAScode";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _CAScode.GetAll();
            CAScodeViewModel model = new CAScodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/CAScode/Index.cshtml", model);
        }

        //
        // POST: /CMS/CAScode/AddEditCAScode
        [HttpPost]
        public ActionResult AddEditCAScode(string Code)
        {
            CAScodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New CAScode";
                model = new CAScodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit CAScode";
                model = _CAScode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/CAScode/_AddEditCAScodeForm.cshtml", model);
        }

        //
        // POST: /CMS/CAScode/SaveCAScode
        [HttpPost]
        public ActionResult SaveCAScode(CAScodeViewModel CAScode)
        {
            if (ModelState.IsValid)
            {
                int TempID = CAScode.CAScodeID;

                if (TempID == 0)
                {
                    CAScode.CreatedBy = "CMS Team";
                    CAScode.Source = "CMS Server";
                    CAScode = _CAScode.Create(CAScode);
                }
                else {
                    CAScode.LastModifiedBy = "CMS Team Update";
                    CAScode.Source = "CMS Server Update";
                    CAScode = _CAScode.Update(CAScode); }

                if (CAScode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/CAScode/_RowCAScode.cshtml", CAScode);

                    if (TempID == 0)
                        return Json(new { Message = "New CAScode Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "CAScode Updated Successfully", Status = true, Type = "Edit", Template = Template });
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