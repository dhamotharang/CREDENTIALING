using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class InsuranceCompanyController : Controller
    {
        /// <summary>
        /// IInsuranceCompanyService object reference
        /// </summary>
        private IInsuranceCompanyService _InsuranceCompany = null;

        /// <summary>
        /// InsuranceCompanyController constructor For InsuranceCompanyService
        /// </summary>
        public InsuranceCompanyController()
        {
            _InsuranceCompany = new InsuranceCompanyService();
        }

        //
        // GET: /CMS/InsuranceCompany/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "InsuranceCompany";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _InsuranceCompany.GetAll();
            InsuranceCompanyViewModel model = new InsuranceCompanyViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/InsuranceCompany/Index.cshtml", model);
        }

        //
        // POST: /CMS/InsuranceCompany/AddEditInsuranceCompany
        [HttpPost]
        public ActionResult AddEditInsuranceCompany(string Code)
        {
            InsuranceCompanyViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New InsuranceCompany";
                model = new InsuranceCompanyViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit InsuranceCompany";
                model = _InsuranceCompany.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/InsuranceCompany/_AddEditInsuranceCompanyForm.cshtml", model);
        }

        //
        // POST: /CMS/InsuranceCompany/SaveInsuranceCompany
        [HttpPost]
        public ActionResult SaveInsuranceCompany(InsuranceCompanyViewModel InsuranceCompany)
        {
            if (ModelState.IsValid)
            {
                int TempID = InsuranceCompany.InsuranceCompanyID;

                if (TempID == 0)
                {
                    InsuranceCompany.CreatedBy = "CMS Team";
                    InsuranceCompany.Source = "CMS Server";
                    InsuranceCompany = _InsuranceCompany.Create(InsuranceCompany);
                }
                else {
                    InsuranceCompany.LastModifiedBy = "CMS Team Update";
                    InsuranceCompany.Source = "CMS Server Update";
                    InsuranceCompany = _InsuranceCompany.Update(InsuranceCompany); }

                if (InsuranceCompany != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/InsuranceCompany/_RowInsuranceCompany.cshtml", InsuranceCompany);

                    if (TempID == 0)
                        return Json(new { Message = "New InsuranceCompany Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "InsuranceCompany Updated Successfully", Status = true, Type = "Edit", Template = Template });
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