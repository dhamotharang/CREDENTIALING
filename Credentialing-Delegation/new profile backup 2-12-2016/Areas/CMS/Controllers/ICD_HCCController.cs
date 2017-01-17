using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ICD_HCCController : Controller
    {
        /// <summary>
        /// IICD_HCCService object reference
        /// </summary>
        private IICD_HCCService _ICD_HCC = null;

        /// <summary>
        /// ICD_HCCController constructor For ICD_HCCService
        /// </summary>
        public ICD_HCCController()
        {
            _ICD_HCC = new ICD_HCCService();
        }

        //
        // GET: /CMS/ICD_HCC/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ICD_HCC";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ICD_HCC.GetAll();
            ICD_HCCViewModel model = new ICD_HCCViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ICD_HCC/Index.cshtml", model);
        }

        //
        // POST: /CMS/ICD_HCC/AddEditICD_HCC
        [HttpPost]
        public ActionResult AddEditICD_HCC(string Code)
        {
            ICD_HCCViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ICD_HCC";
                model = new ICD_HCCViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ICD_HCC";
                model = _ICD_HCC.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ICD_HCC/_AddEditICD_HCCForm.cshtml", model);
        }

        //
        // POST: /CMS/ICD_HCC/SaveICD_HCC
        [HttpPost]
        public ActionResult SaveICD_HCC(ICD_HCCViewModel ICD_HCC)
        {
            if (ModelState.IsValid)
            {
                int TempID = ICD_HCC.ICD_HCCID;

                if (TempID == 0)
                {
                    ICD_HCC.CreatedBy = "CMS Team";
                    ICD_HCC.Source = "CMS Server";
                    ICD_HCC = _ICD_HCC.Create(ICD_HCC);
                }
                else {
                    ICD_HCC.LastModifiedBy = "CMS Team Update";
                    ICD_HCC.Source = "CMS Server Update";
                    ICD_HCC = _ICD_HCC.Update(ICD_HCC); }

                if (ICD_HCC != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ICD_HCC/_RowICD_HCC.cshtml", ICD_HCC);

                    if (TempID == 0)
                        return Json(new { Message = "New ICD_HCC Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ICD_HCC Updated Successfully", Status = true, Type = "Edit", Template = Template });
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