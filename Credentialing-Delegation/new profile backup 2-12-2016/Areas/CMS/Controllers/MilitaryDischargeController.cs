using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class MilitaryDischargeController : Controller
    {
        /// <summary>
        /// IMilitaryDischargeService object reference
        /// </summary>
        private IMilitaryDischargeService _MilitaryDischarge = null;

        /// <summary>
        /// MilitaryDischargeController constructor For MilitaryDischargeService
        /// </summary>
        public MilitaryDischargeController()
        {
            _MilitaryDischarge = new MilitaryDischargeService();
        }

        //
        // GET: /CMS/MilitaryDischarge/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "MilitaryDischarge";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _MilitaryDischarge.GetAll();
            MilitaryDischargeViewModel model = new MilitaryDischargeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/MilitaryDischarge/Index.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryDischarge/AddEditMilitaryDischarge
        [HttpPost]
        public ActionResult AddEditMilitaryDischarge(string Code)
        {
            MilitaryDischargeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New MilitaryDischarge";
                model = new MilitaryDischargeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit MilitaryDischarge";
                model = _MilitaryDischarge.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/MilitaryDischarge/_AddEditMilitaryDischargeForm.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryDischarge/SaveMilitaryDischarge
        [HttpPost]
        public ActionResult SaveMilitaryDischarge(MilitaryDischargeViewModel MilitaryDischarge)
        {
            if (ModelState.IsValid)
            {
                int TempID = MilitaryDischarge.MilitaryDischargeID;

                if (TempID == 0)
                {
                    MilitaryDischarge.CreatedBy = "CMS Team";
                    MilitaryDischarge.Source = "CMS Server";
                    MilitaryDischarge = _MilitaryDischarge.Create(MilitaryDischarge);
                }
                else {
                    MilitaryDischarge.LastModifiedBy = "CMS Team Update";
                    MilitaryDischarge.Source = "CMS Server Update";
                    MilitaryDischarge = _MilitaryDischarge.Update(MilitaryDischarge); }

                if (MilitaryDischarge != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/MilitaryDischarge/_RowMilitaryDischarge.cshtml", MilitaryDischarge);

                    if (TempID == 0)
                        return Json(new { Message = "New MilitaryDischarge Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "MilitaryDischarge Updated Successfully", Status = true, Type = "Edit", Template = Template });
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