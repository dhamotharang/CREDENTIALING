using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class MilitaryBranchController : Controller
    {
        /// <summary>
        /// IMilitaryBranchService object reference
        /// </summary>
        private IMilitaryBranchService _MilitaryBranch = null;

        /// <summary>
        /// MilitaryBranchController constructor For MilitaryBranchService
        /// </summary>
        public MilitaryBranchController()
        {
            _MilitaryBranch = new MilitaryBranchService();
        }

        //
        // GET: /CMS/MilitaryBranch/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "MilitaryBranch";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _MilitaryBranch.GetAll();
            MilitaryBranchViewModel model = new MilitaryBranchViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/MilitaryBranch/Index.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryBranch/AddEditMilitaryBranch
        [HttpPost]
        public ActionResult AddEditMilitaryBranch(string Code)
        {
            MilitaryBranchViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New MilitaryBranch";
                model = new MilitaryBranchViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit MilitaryBranch";
                model = _MilitaryBranch.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/MilitaryBranch/_AddEditMilitaryBranchForm.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryBranch/SaveMilitaryBranch
        [HttpPost]
        public ActionResult SaveMilitaryBranch(MilitaryBranchViewModel MilitaryBranch)
        {
            if (ModelState.IsValid)
            {
                int TempID = MilitaryBranch.MilitaryBranchID;

                if (TempID == 0)
                {
                    MilitaryBranch.CreatedBy = "CMS Team";
                    MilitaryBranch.Source = "CMS Server";
                    MilitaryBranch = _MilitaryBranch.Create(MilitaryBranch);
                }
                else {
                    MilitaryBranch.LastModifiedBy = "CMS Team Update";
                    MilitaryBranch.Source = "CMS Server Update";
                    MilitaryBranch = _MilitaryBranch.Update(MilitaryBranch); }

                if (MilitaryBranch != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/MilitaryBranch/_RowMilitaryBranch.cshtml", MilitaryBranch);

                    if (TempID == 0)
                        return Json(new { Message = "New MilitaryBranch Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "MilitaryBranch Updated Successfully", Status = true, Type = "Edit", Template = Template });
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