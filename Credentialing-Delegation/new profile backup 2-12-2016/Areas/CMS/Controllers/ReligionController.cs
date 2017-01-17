using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ReligionController : Controller
    {
        /// <summary>
        /// IReligionService object reference
        /// </summary>
        private IReligionService _Religion = null;

        /// <summary>
        /// ReligionController constructor For ReligionService
        /// </summary>
        public ReligionController()
        {
            _Religion = new ReligionService();
        }

        //
        // GET: /CMS/Religion/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Religion";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Religion.GetAll();
            ReligionViewModel model = new ReligionViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Religion/Index.cshtml", model);
        }

        //
        // POST: /CMS/Religion/AddEditReligion
        [HttpPost]
        public ActionResult AddEditReligion(string Code)
        {
            ReligionViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Religion";
                model = new ReligionViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Religion";
                model = _Religion.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Religion/_AddEditReligionForm.cshtml", model);
        }

        //
        // POST: /CMS/Religion/SaveReligion
        [HttpPost]
        public ActionResult SaveReligion(ReligionViewModel Religion)
        {
            if (ModelState.IsValid)
            {
                int TempID = Religion.ReligionID;

                if (TempID == 0)
                {
                    Religion.CreatedBy = "CMS Team";
                    Religion.Source = "CMS Server";
                    Religion = _Religion.Create(Religion);
                }
                else {
                    Religion.LastModifiedBy = "CMS Team Update";
                    Religion.Source = "CMS Server Update";
                    Religion = _Religion.Update(Religion); }

                if (Religion != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Religion/_RowReligion.cshtml", Religion);

                    if (TempID == 0)
                        return Json(new { Message = "New Religion Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Religion Updated Successfully", Status = true, Type = "Edit", Template = Template });
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