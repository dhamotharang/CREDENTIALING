using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class MilitaryRankMilitaryBranchController : Controller
    {
        /// <summary>
        /// IMilitaryRankMilitaryBranchService object reference
        /// </summary>
        private IMilitaryRankMilitaryBranchService _MilitaryRankMilitaryBranch = null;

        /// <summary>
        /// MilitaryRankMilitaryBranchController constructor For MilitaryRankMilitaryBranchService
        /// </summary>
        public MilitaryRankMilitaryBranchController()
        {
            _MilitaryRankMilitaryBranch = new MilitaryRankMilitaryBranchService();
        }

        //
        // GET: /CMS/MilitaryRankMilitaryBranch/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "MilitaryRankMilitaryBranch";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _MilitaryRankMilitaryBranch.GetAll();
            MilitaryRankMilitaryBranchViewModel model = new MilitaryRankMilitaryBranchViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/MilitaryRankMilitaryBranch/Index.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryRankMilitaryBranch/AddEditMilitaryRankMilitaryBranch
        [HttpPost]
        public ActionResult AddEditMilitaryRankMilitaryBranch(string Code)
        {
            MilitaryRankMilitaryBranchViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New MilitaryRankMilitaryBranch";
                model = new MilitaryRankMilitaryBranchViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit MilitaryRankMilitaryBranch";
                model = _MilitaryRankMilitaryBranch.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/MilitaryRankMilitaryBranch/_AddEditMilitaryRankMilitaryBranchForm.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryRankMilitaryBranch/SaveMilitaryRankMilitaryBranch
        [HttpPost]
        public ActionResult SaveMilitaryRankMilitaryBranch(MilitaryRankMilitaryBranchViewModel MilitaryRankMilitaryBranch)
        {
            if (ModelState.IsValid)
            {
                int TempID = MilitaryRankMilitaryBranch.MilitaryRankMilitaryBranchID;

                if (TempID == 0)
                {
                    MilitaryRankMilitaryBranch.CreatedBy = "CMS Team";
                    MilitaryRankMilitaryBranch.Source = "CMS Server";
                    MilitaryRankMilitaryBranch = _MilitaryRankMilitaryBranch.Create(MilitaryRankMilitaryBranch);
                }
                else {
                    MilitaryRankMilitaryBranch.LastModifiedBy = "CMS Team Update";
                    MilitaryRankMilitaryBranch.Source = "CMS Server Update";
                    MilitaryRankMilitaryBranch = _MilitaryRankMilitaryBranch.Update(MilitaryRankMilitaryBranch); }

                if (MilitaryRankMilitaryBranch != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/MilitaryRankMilitaryBranch/_RowMilitaryRankMilitaryBranch.cshtml", MilitaryRankMilitaryBranch);

                    if (TempID == 0)
                        return Json(new { Message = "New MilitaryRankMilitaryBranch Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "MilitaryRankMilitaryBranch Updated Successfully", Status = true, Type = "Edit", Template = Template });
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