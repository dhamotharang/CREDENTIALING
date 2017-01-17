using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DisabilitiesController : Controller
    {
        /// <summary>
        /// IDisabilitiesService object reference
        /// </summary>
        private IDisabilitiesService _Disabilities = null;

        /// <summary>
        /// DisabilitiesController constructor For DisabilitiesService
        /// </summary>
        public DisabilitiesController()
        {
            _Disabilities = new DisabilitiesService();
        }

        //
        // GET: /CMS/Disabilities/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Disabilities";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Disabilities.GetAll();
            DisabilitiesViewModel model = new DisabilitiesViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Disabilities/Index.cshtml", model);
        }

        //
        // POST: /CMS/Disabilities/AddEditDisabilities
        [HttpPost]
        public ActionResult AddEditDisabilities(string Code)
        {
            DisabilitiesViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Disabilities";
                model = new DisabilitiesViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Disabilities";
                model = _Disabilities.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Disabilities/_AddEditDisabilitiesForm.cshtml", model);
        }

        //
        // POST: /CMS/Disabilities/SaveDisabilities
        [HttpPost]
        public ActionResult SaveDisabilities(DisabilitiesViewModel Disabilities)
        {
            if (ModelState.IsValid)
            {
                int TempID = Disabilities.DisabilitiesID;

                if (TempID == 0)
                {
                    Disabilities.CreatedBy = "CMS Team";
                    Disabilities.Source = "CMS Server";
                    Disabilities = _Disabilities.Create(Disabilities);
                }
                else {
                    Disabilities.LastModifiedBy = "CMS Team Update";
                    Disabilities.Source = "CMS Server Update";
                    Disabilities = _Disabilities.Update(Disabilities); }

                if (Disabilities != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Disabilities/_RowDisabilities.cshtml", Disabilities);

                    if (TempID == 0)
                        return Json(new { Message = "New Disabilities Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Disabilities Updated Successfully", Status = true, Type = "Edit", Template = Template });
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