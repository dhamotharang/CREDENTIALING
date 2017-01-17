using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class MilitaryPresentDutyController : Controller
    {
        /// <summary>
        /// IMilitaryPresentDutyService object reference
        /// </summary>
        private IMilitaryPresentDutyService _MilitaryPresentDuty = null;

        /// <summary>
        /// MilitaryPresentDutyController constructor For MilitaryPresentDutyService
        /// </summary>
        public MilitaryPresentDutyController()
        {
            _MilitaryPresentDuty = new MilitaryPresentDutyService();
        }

        //
        // GET: /CMS/MilitaryPresentDuty/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "MilitaryPresentDuty";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _MilitaryPresentDuty.GetAll();
            MilitaryPresentDutyViewModel model = new MilitaryPresentDutyViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/MilitaryPresentDuty/Index.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryPresentDuty/AddEditMilitaryPresentDuty
        [HttpPost]
        public ActionResult AddEditMilitaryPresentDuty(string Code)
        {
            MilitaryPresentDutyViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New MilitaryPresentDuty";
                model = new MilitaryPresentDutyViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit MilitaryPresentDuty";
                model = _MilitaryPresentDuty.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/MilitaryPresentDuty/_AddEditMilitaryPresentDutyForm.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryPresentDuty/SaveMilitaryPresentDuty
        [HttpPost]
        public ActionResult SaveMilitaryPresentDuty(MilitaryPresentDutyViewModel MilitaryPresentDuty)
        {
            if (ModelState.IsValid)
            {
                int TempID = MilitaryPresentDuty.MilitaryPresentDutyID;

                if (TempID == 0)
                {
                    MilitaryPresentDuty.CreatedBy = "CMS Team";
                    MilitaryPresentDuty.Source = "CMS Server";
                    MilitaryPresentDuty = _MilitaryPresentDuty.Create(MilitaryPresentDuty);
                }
                else {
                    MilitaryPresentDuty.LastModifiedBy = "CMS Team Update";
                    MilitaryPresentDuty.Source = "CMS Server Update";
                    MilitaryPresentDuty = _MilitaryPresentDuty.Update(MilitaryPresentDuty); }

                if (MilitaryPresentDuty != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/MilitaryPresentDuty/_RowMilitaryPresentDuty.cshtml", MilitaryPresentDuty);

                    if (TempID == 0)
                        return Json(new { Message = "New MilitaryPresentDuty Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "MilitaryPresentDuty Updated Successfully", Status = true, Type = "Edit", Template = Template });
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