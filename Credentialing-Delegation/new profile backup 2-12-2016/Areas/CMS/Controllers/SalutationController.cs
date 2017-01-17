using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class SalutationController : Controller
    {
        /// <summary>
        /// ISalutationService object reference
        /// </summary>
        private ISalutationService _Salutation = null;

        /// <summary>
        /// SalutationController constructor For SalutationService
        /// </summary>
        public SalutationController()
        {
            _Salutation = new SalutationService();
        }

        //
        // GET: /CMS/Salutation/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Salutation";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Salutation.GetAll();
            SalutationViewModel model = new SalutationViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Salutation/Index.cshtml", model);
        }

        //
        // POST: /CMS/Salutation/AddEditSalutation
        [HttpPost]
        public ActionResult AddEditSalutation(string Code)
        {
            SalutationViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Salutation";
                model = new SalutationViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Salutation";
                model = _Salutation.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Salutation/_AddEditSalutationForm.cshtml", model);
        }

        //
        // POST: /CMS/Salutation/SaveSalutation
        [HttpPost]
        public ActionResult SaveSalutation(SalutationViewModel Salutation)
        {
            if (ModelState.IsValid)
            {
                int TempID = Salutation.SalutationID;

                if (TempID == 0)
                {
                    Salutation.CreatedBy = "CMS Team";
                    Salutation.Source = "CMS Server";
                    Salutation = _Salutation.Create(Salutation);
                }
                else {
                    Salutation.LastModifiedBy = "CMS Team Update";
                    Salutation.Source = "CMS Server Update";
                    Salutation = _Salutation.Update(Salutation); }

                if (Salutation != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Salutation/_RowSalutation.cshtml", Salutation);

                    if (TempID == 0)
                        return Json(new { Message = "New Salutation Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Salutation Updated Successfully", Status = true, Type = "Edit", Template = Template });
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