using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class AdmittingPrivilegeController : Controller
    {
        /// <summary>
        /// IAdmittingPrivilegeService object reference
        /// </summary>
        private IAdmittingPrivilegeService _AdmittingPrivilege = null;

        /// <summary>
        /// AdmittingPrivilegeController constructor For AdmittingPrivilegeService
        /// </summary>
        public AdmittingPrivilegeController()
        {
            _AdmittingPrivilege = new AdmittingPrivilegeService();
        }

        //
        // GET: /CMS/AdmittingPrivilege/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "AdmittingPrivilege";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _AdmittingPrivilege.GetAll();
            AdmittingPrivilegeViewModel model = new AdmittingPrivilegeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/AdmittingPrivilege/Index.cshtml", model);
        }

        //
        // POST: /CMS/AdmittingPrivilege/AddEditAdmittingPrivilege
        [HttpPost]
        public ActionResult AddEditAdmittingPrivilege(string Code)
        {
            AdmittingPrivilegeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New AdmittingPrivilege";
                model = new AdmittingPrivilegeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit AdmittingPrivilege";
                model = _AdmittingPrivilege.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/AdmittingPrivilege/_AddEditAdmittingPrivilegeForm.cshtml", model);
        }

        //
        // POST: /CMS/AdmittingPrivilege/SaveAdmittingPrivilege
        [HttpPost]
        public ActionResult SaveAdmittingPrivilege(AdmittingPrivilegeViewModel AdmittingPrivilege)
        {
            if (ModelState.IsValid)
            {
                int TempID = AdmittingPrivilege.AdmittingPrivilegeID;

                if (TempID == 0)
                {
                    AdmittingPrivilege.CreatedBy = "CMS Team";
                    AdmittingPrivilege.Source = "CMS Server";
                    AdmittingPrivilege = _AdmittingPrivilege.Create(AdmittingPrivilege);
                }
                else {
                    AdmittingPrivilege.LastModifiedBy = "CMS Team Update";
                    AdmittingPrivilege.Source = "CMS Server Update";
                    AdmittingPrivilege = _AdmittingPrivilege.Update(AdmittingPrivilege); }

                if (AdmittingPrivilege != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/AdmittingPrivilege/_RowAdmittingPrivilege.cshtml", AdmittingPrivilege);

                    if (TempID == 0)
                        return Json(new { Message = "New AdmittingPrivilege Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "AdmittingPrivilege Updated Successfully", Status = true, Type = "Edit", Template = Template });
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