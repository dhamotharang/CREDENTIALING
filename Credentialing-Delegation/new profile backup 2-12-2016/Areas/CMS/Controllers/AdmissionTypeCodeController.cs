using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class AdmissionTypeCodeController : Controller
    {
        /// <summary>
        /// IAdmissionTypeCodeService object reference
        /// </summary>
        private IAdmissionTypeCodeService _AdmissionTypeCode = null;

        /// <summary>
        /// AdmissionTypeCodeController constructor For AdmissionTypeCodeService
        /// </summary>
        public AdmissionTypeCodeController()
        {
            _AdmissionTypeCode = new AdmissionTypeCodeService();
        }

        //
        // GET: /CMS/AdmissionTypeCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Admission Type Code";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _AdmissionTypeCode.GetAll();
            AdmissionTypeCodeViewModel model = new AdmissionTypeCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/AdmissionTypeCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/AdmissionTypeCode/AddEditAdmissionTypeCode
        [HttpPost]
        public ActionResult AddEditAdmissionTypeCode(string Code)
        {
            AdmissionTypeCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New AdmissionTypeCode";
                model = new AdmissionTypeCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit AdmissionTypeCode";
                model = _AdmissionTypeCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/AdmissionTypeCode/_AddEditAdmissionTypeCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/AdmissionTypeCode/SaveAdmissionTypeCode
        [HttpPost]
        public ActionResult SaveAdmissionTypeCode(AdmissionTypeCodeViewModel AdmissionTypeCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = AdmissionTypeCode.AdmissionTypeCodeID;

                if (TempID == 0)
                {
                    AdmissionTypeCode.CreatedBy = "CMS Team";
                    AdmissionTypeCode.Source = "CMS Server";
                    AdmissionTypeCode = _AdmissionTypeCode.Create(AdmissionTypeCode);
                }
                else {
                    AdmissionTypeCode.LastModifiedBy = "CMS Team Update";
                    AdmissionTypeCode.Source = "CMS Server Update";
                    AdmissionTypeCode = _AdmissionTypeCode.Update(AdmissionTypeCode); }

                if (AdmissionTypeCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/AdmissionTypeCode/_RowAdmissionTypeCode.cshtml", AdmissionTypeCode);

                    if (TempID == 0)
                        return Json(new { Message = "New Admission Type Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Admission Type Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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