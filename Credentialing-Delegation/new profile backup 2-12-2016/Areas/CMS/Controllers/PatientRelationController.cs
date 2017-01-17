using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class PatientRelationController : Controller
    {
        /// <summary>
        /// IPatientRelationService object reference
        /// </summary>
        private IPatientRelationService _PatientRelation = null;

        /// <summary>
        /// PatientRelationController constructor For PatientRelationService
        /// </summary>
        public PatientRelationController()
        {
            _PatientRelation = new PatientRelationService();
        }

        //
        // GET: /CMS/PatientRelation/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "PatientRelation";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _PatientRelation.GetAll();
            PatientRelationViewModel model = new PatientRelationViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/PatientRelation/Index.cshtml", model);
        }

        //
        // POST: /CMS/PatientRelation/AddEditPatientRelation
        [HttpPost]
        public ActionResult AddEditPatientRelation(string Code)
        {
            PatientRelationViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New PatientRelation";
                model = new PatientRelationViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit PatientRelation";
                model = _PatientRelation.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/PatientRelation/_AddEditPatientRelationForm.cshtml", model);
        }

        //
        // POST: /CMS/PatientRelation/SavePatientRelation
        [HttpPost]
        public ActionResult SavePatientRelation(PatientRelationViewModel PatientRelation)
        {
            if (ModelState.IsValid)
            {
                int TempID = PatientRelation.PatientRelationID;

                if (TempID == 0)
                {
                    PatientRelation.CreatedBy = "CMS Team";
                    PatientRelation.Source = "CMS Server";
                    PatientRelation = _PatientRelation.Create(PatientRelation);
                }
                else {
                    PatientRelation.LastModifiedBy = "CMS Team Update";
                    PatientRelation.Source = "CMS Server Update";
                    PatientRelation = _PatientRelation.Update(PatientRelation); }

                if (PatientRelation != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/PatientRelation/_RowPatientRelation.cshtml", PatientRelation);

                    if (TempID == 0)
                        return Json(new { Message = "New PatientRelation Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "PatientRelation Updated Successfully", Status = true, Type = "Edit", Template = Template });
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