using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class LevelOfCareController : Controller
    {
        /// <summary>
        /// ILevelOfCareService object reference
        /// </summary>
        private ILevelOfCareService _LevelOfCare = null;

        /// <summary>
        /// LevelOfCareController constructor For LevelOfCareService
        /// </summary>
        public LevelOfCareController()
        {
            _LevelOfCare = new LevelOfCareService();
        }

        //
        // GET: /CMS/LevelOfCare/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "LevelOfCare";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _LevelOfCare.GetAll();
            LevelOfCareViewModel model = new LevelOfCareViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/LevelOfCare/Index.cshtml", model);
        }

        //
        // POST: /CMS/LevelOfCare/AddEditLevelOfCare
        [HttpPost]
        public ActionResult AddEditLevelOfCare(string Code)
        {
            LevelOfCareViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New LevelOfCare";
                model = new LevelOfCareViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit LevelOfCare";
                model = _LevelOfCare.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/LevelOfCare/_AddEditLevelOfCareForm.cshtml", model);
        }

        //
        // POST: /CMS/LevelOfCare/SaveLevelOfCare
        [HttpPost]
        public ActionResult SaveLevelOfCare(LevelOfCareViewModel LevelOfCare)
        {
            if (ModelState.IsValid)
            {
                int TempID = LevelOfCare.LevelOfCareID;

                if (TempID == 0)
                {
                    LevelOfCare.CreatedBy = "CMS Team";
                    LevelOfCare.Source = "CMS Server";
                    LevelOfCare = _LevelOfCare.Create(LevelOfCare);
                }
                else {
                    LevelOfCare.LastModifiedBy = "CMS Team Update";
                    LevelOfCare.Source = "CMS Server Update";
                    LevelOfCare = _LevelOfCare.Update(LevelOfCare); }

                if (LevelOfCare != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/LevelOfCare/_RowLevelOfCare.cshtml", LevelOfCare);

                    if (TempID == 0)
                        return Json(new { Message = "New LevelOfCare Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "LevelOfCare Updated Successfully", Status = true, Type = "Edit", Template = Template });
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