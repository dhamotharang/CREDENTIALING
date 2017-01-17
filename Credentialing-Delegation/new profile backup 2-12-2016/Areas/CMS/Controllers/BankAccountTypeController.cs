using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class BankAccountTypeController : Controller
    {
        /// <summary>
        /// IBankAccountTypeService object reference
        /// </summary>
        private IBankAccountTypeService _BankAccountType = null;

        /// <summary>
        /// BankAccountTypeController constructor For BankAccountTypeService
        /// </summary>
        public BankAccountTypeController()
        {
            _BankAccountType = new BankAccountTypeService();
        }

        //
        // GET: /CMS/BankAccountType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "BankAccountType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _BankAccountType.GetAll();
            BankAccountTypeViewModel model = new BankAccountTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/BankAccountType/Index.cshtml", model);
        }

        //
        // POST: /CMS/BankAccountType/AddEditBankAccountType
        [HttpPost]
        public ActionResult AddEditBankAccountType(string Code)
        {
            BankAccountTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New BankAccountType";
                model = new BankAccountTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit BankAccountType";
                model = _BankAccountType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/BankAccountType/_AddEditBankAccountTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/BankAccountType/SaveBankAccountType
        [HttpPost]
        public ActionResult SaveBankAccountType(BankAccountTypeViewModel BankAccountType)
        {
            if (ModelState.IsValid)
            {
                int TempID = BankAccountType.BankAccountTypeID;

                if (TempID == 0)
                {
                    BankAccountType.CreatedBy = "CMS Team";
                    BankAccountType.Source = "CMS Server";
                    BankAccountType = _BankAccountType.Create(BankAccountType);
                }
                else {
                    BankAccountType.LastModifiedBy = "CMS Team Update";
                    BankAccountType.Source = "CMS Server Update";
                    BankAccountType = _BankAccountType.Update(BankAccountType); }

                if (BankAccountType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/BankAccountType/_RowBankAccountType.cshtml", BankAccountType);

                    if (TempID == 0)
                        return Json(new { Message = "New BankAccountType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "BankAccountType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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