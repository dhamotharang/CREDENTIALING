using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class COBPreferenceController : Controller
    {
        /// <summary>
        /// ICOBPreferenceService object reference
        /// </summary>
        private ICOBPreferenceService _COBPreference = null;

        /// <summary>
        /// COBPreferenceController constructor For COBPreferenceService
        /// </summary>
        public COBPreferenceController()
        {
            _COBPreference = new COBPreferenceService();
        }

        //
        // GET: /CMS/COBPreference/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "COBPreference";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _COBPreference.GetAll();
            COBPreferenceViewModel model = new COBPreferenceViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/COBPreference/Index.cshtml", model);
        }

        //
        // POST: /CMS/COBPreference/AddEditCOBPreference
        [HttpPost]
        public ActionResult AddEditCOBPreference(string Code)
        {
            COBPreferenceViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New COBPreference";
                model = new COBPreferenceViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit COBPreference";
                model = _COBPreference.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/COBPreference/_AddEditCOBPreferenceForm.cshtml", model);
        }

        //
        // POST: /CMS/COBPreference/SaveCOBPreference
        [HttpPost]
        public ActionResult SaveCOBPreference(COBPreferenceViewModel COBPreference)
        {
            if (ModelState.IsValid)
            {
                int TempID = COBPreference.COBPreferenceID;

                if (TempID == 0)
                {
                    COBPreference.CreatedBy = "CMS Team";
                    COBPreference.Source = "CMS Server";
                    COBPreference = _COBPreference.Create(COBPreference);
                }
                else {
                    COBPreference.LastModifiedBy = "CMS Team Update";
                    COBPreference.Source = "CMS Server Update";
                    COBPreference = _COBPreference.Update(COBPreference); }

                if (COBPreference != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/COBPreference/_RowCOBPreference.cshtml", COBPreference);

                    if (TempID == 0)
                        return Json(new { Message = "New COBPreference Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "COBPreference Updated Successfully", Status = true, Type = "Edit", Template = Template });
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