using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class CPTCodeController : Controller
    {
        /// <summary>
        /// ICPTCodeService object reference
        /// </summary>
        private ICPTCodeService _CPTCode = null;

        /// <summary>
        /// CPTCodeController constructor For CPTCodeService
        /// </summary>
        public CPTCodeController()
        {
            _CPTCode = new CPTCodeService();
        }

        //
        // GET: /CMS/CPTCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "CPTCode";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _CPTCode.GetAll();
            CPTCodeViewModel model = new CPTCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/CPTCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/CPTCode/AddEditCPTCode
        [HttpPost]
        public ActionResult AddEditCPTCode(string Code)
        {
            CPTCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New CPTCode";
                model = new CPTCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit CPTCode";
                model = _CPTCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/CPTCode/_AddEditCPTCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/CPTCode/SaveCPTCode
        [HttpPost]
        public ActionResult SaveCPTCode(CPTCodeViewModel CPTCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = CPTCode.CPTCodeID;

                if (TempID == 0)
                {
                    CPTCode.CreatedBy = "CMS Team";
                    CPTCode.Source = "CMS Server";
                    CPTCode = _CPTCode.Create(CPTCode);
                }
                else {
                    CPTCode.LastModifiedBy = "CMS Team Update";
                    CPTCode.Source = "CMS Server Update";
                    CPTCode = _CPTCode.Update(CPTCode); }

                if (CPTCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/CPTCode/_RowCPTCode.cshtml", CPTCode);

                    if (TempID == 0)
                        return Json(new { Message = "New CPT Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "CPT Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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