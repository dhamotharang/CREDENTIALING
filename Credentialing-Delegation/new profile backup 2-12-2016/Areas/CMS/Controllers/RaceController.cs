using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class RaceController : Controller
    {
        /// <summary>
        /// IRaceService object reference
        /// </summary>
        private IRaceService _Race = null;

        /// <summary>
        /// RaceController constructor For RaceService
        /// </summary>
        public RaceController()
        {
            _Race = new RaceService();
        }

        //
        // GET: /CMS/Race/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Race";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Race.GetAll();
            RaceViewModel model = new RaceViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Race/Index.cshtml", model);
        }

        //
        // POST: /CMS/Race/AddEditRace
        [HttpPost]
        public ActionResult AddEditRace(string Code)
        {
            RaceViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Race";
                model = new RaceViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Race";
                model = _Race.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Race/_AddEditRaceForm.cshtml", model);
        }

        //
        // POST: /CMS/Race/SaveRace
        [HttpPost]
        public ActionResult SaveRace(RaceViewModel Race)
        {
            if (ModelState.IsValid)
            {
                int TempID = Race.RaceID;

                if (TempID == 0)
                {
                    Race.CreatedBy = "CMS Team";
                    Race.Source = "CMS Server";
                    Race = _Race.Create(Race);
                }
                else {
                    Race.LastModifiedBy = "CMS Team Update";
                    Race.Source = "CMS Server Update";
                    Race = _Race.Update(Race); }

                if (Race != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Race/_RowRace.cshtml", Race);

                    if (TempID == 0)
                        return Json(new { Message = "New Race Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Race Updated Successfully", Status = true, Type = "Edit", Template = Template });
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