using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DisciplineController : Controller
    {
        /// <summary>
        /// IDisciplineService object reference
        /// </summary>
        private IDisciplineService _Discipline = null;

        /// <summary>
        /// DisciplineController constructor For DisciplineService
        /// </summary>
        public DisciplineController()
        {
            _Discipline = new DisciplineService();
        }

        //
        // GET: /CMS/Discipline/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Discipline";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Discipline.GetAll();
            DisciplineViewModel model = new DisciplineViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Discipline/Index.cshtml", model);
        }

        //
        // POST: /CMS/Discipline/AddEditDiscipline
        [HttpPost]
        public ActionResult AddEditDiscipline(string Code)
        {
            DisciplineViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Discipline";
                model = new DisciplineViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Discipline";
                model = _Discipline.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Discipline/_AddEditDisciplineForm.cshtml", model);
        }

        //
        // POST: /CMS/Discipline/SaveDiscipline
        [HttpPost]
        public ActionResult SaveDiscipline(DisciplineViewModel Discipline)
        {
            if (ModelState.IsValid)
            {
                int TempID = Discipline.DisciplineID;

                if (TempID == 0)
                {
                    Discipline.CreatedBy = "CMS Team";
                    Discipline.Source = "CMS Server";
                    Discipline = _Discipline.Create(Discipline);
                }
                else {
                    Discipline.LastModifiedBy = "CMS Team Update";
                    Discipline.Source = "CMS Server Update";
                    Discipline = _Discipline.Update(Discipline); }

                if (Discipline != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Discipline/_RowDiscipline.cshtml", Discipline);

                    if (TempID == 0)
                        return Json(new { Message = "New Discipline Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Discipline Updated Successfully", Status = true, Type = "Edit", Template = Template });
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