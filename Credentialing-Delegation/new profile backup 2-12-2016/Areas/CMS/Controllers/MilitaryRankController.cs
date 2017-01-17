using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class MilitaryRankController : Controller
    {
        /// <summary>
        /// IMilitaryRankService object reference
        /// </summary>
        private IMilitaryRankService _MilitaryRank = null;

        /// <summary>
        /// MilitaryRankController constructor For MilitaryRankService
        /// </summary>
        public MilitaryRankController()
        {
            _MilitaryRank = new MilitaryRankService();
        }

        //
        // GET: /CMS/MilitaryRank/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "MilitaryRank";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _MilitaryRank.GetAll();
            MilitaryRankViewModel model = new MilitaryRankViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/MilitaryRank/Index.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryRank/AddEditMilitaryRank
        [HttpPost]
        public ActionResult AddEditMilitaryRank(string Code)
        {
            MilitaryRankViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New MilitaryRank";
                model = new MilitaryRankViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit MilitaryRank";
                model = _MilitaryRank.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/MilitaryRank/_AddEditMilitaryRankForm.cshtml", model);
        }

        //
        // POST: /CMS/MilitaryRank/SaveMilitaryRank
        [HttpPost]
        public ActionResult SaveMilitaryRank(MilitaryRankViewModel MilitaryRank)
        {
            if (ModelState.IsValid)
            {
                int TempID = MilitaryRank.MilitaryRankID;

                if (TempID == 0)
                {
                    MilitaryRank.CreatedBy = "CMS Team";
                    MilitaryRank.Source = "CMS Server";
                    MilitaryRank = _MilitaryRank.Create(MilitaryRank);
                }
                else {
                    MilitaryRank.LastModifiedBy = "CMS Team Update";
                    MilitaryRank.Source = "CMS Server Update";
                    MilitaryRank = _MilitaryRank.Update(MilitaryRank); }

                if (MilitaryRank != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/MilitaryRank/_RowMilitaryRank.cshtml", MilitaryRank);

                    if (TempID == 0)
                        return Json(new { Message = "New MilitaryRank Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "MilitaryRank Updated Successfully", Status = true, Type = "Edit", Template = Template });
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