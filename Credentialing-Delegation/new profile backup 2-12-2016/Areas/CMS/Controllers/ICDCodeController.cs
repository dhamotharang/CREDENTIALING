using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ICDCodeController : Controller
    {
        /// <summary>
        /// IICDCodeService object reference
        /// </summary>
        private IICDCodeService _ICDCode = null;

        /// <summary>
        /// ICDCodeController constructor For ICDCodeService
        /// </summary>
        public ICDCodeController()
        {
            _ICDCode = new ICDCodeService();
        }

        //
        // GET: /CMS/ICDCode/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ICD Code";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ICDCode.GetAll();
            ICDCodeViewModel model = new ICDCodeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ICDCode/Index.cshtml", model);
        }

        //
        // POST: /CMS/ICDCode/AddEditICDCode
        [HttpPost]
        public ActionResult AddEditICDCode(string Code)
        {
            ICDCodeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ICD Code";
                model = new ICDCodeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ICD Code";
                model = _ICDCode.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ICDCode/_AddEditICDCodeForm.cshtml", model);
        }

        //
        // POST: /CMS/ICDCode/SaveICDCode
        [HttpPost]
        public ActionResult SaveICDCode(ICDCodeViewModel ICDCode)
        {
            if (ModelState.IsValid)
            {
                int TempID = ICDCode.ICDCodeID;

                if (TempID == 0)
                {
                    ICDCode.CreatedBy = "CMS Team";
                    ICDCode.Source = "CMS Server";
                    ICDCode = _ICDCode.Create(ICDCode);
                }
                else {
                    ICDCode.LastModifiedBy = "CMS Team Update";
                    ICDCode.Source = "CMS Server Update";
                    ICDCode = _ICDCode.Update(ICDCode); }

                if (ICDCode != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ICDCode/_RowICDCode.cshtml", ICDCode);

                    if (TempID == 0)
                        return Json(new { Message = "New ICD Code Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ICD Code Updated Successfully", Status = true, Type = "Edit", Template = Template });
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