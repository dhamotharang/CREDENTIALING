using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class IncomeSourceController : Controller
    {
        /// <summary>
        /// IIncomeSourceService object reference
        /// </summary>
        private IIncomeSourceService _IncomeSource = null;

        /// <summary>
        /// IncomeSourceController constructor For IncomeSourceService
        /// </summary>
        public IncomeSourceController()
        {
            _IncomeSource = new IncomeSourceService();
        }

        //
        // GET: /CMS/IncomeSource/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "IncomeSource";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _IncomeSource.GetAll();
            IncomeSourceViewModel model = new IncomeSourceViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/IncomeSource/Index.cshtml", model);
        }

        //
        // POST: /CMS/IncomeSource/AddEditIncomeSource
        [HttpPost]
        public ActionResult AddEditIncomeSource(string Code)
        {
            IncomeSourceViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New IncomeSource";
                model = new IncomeSourceViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit IncomeSource";
                model = _IncomeSource.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/IncomeSource/_AddEditIncomeSourceForm.cshtml", model);
        }

        //
        // POST: /CMS/IncomeSource/SaveIncomeSource
        [HttpPost]
        public ActionResult SaveIncomeSource(IncomeSourceViewModel IncomeSource)
        {
            if (ModelState.IsValid)
            {
                int TempID = IncomeSource.IncomeSourceID;

                if (TempID == 0)
                {
                    IncomeSource.CreatedBy = "CMS Team";
                    IncomeSource.Source = "CMS Server";
                    IncomeSource = _IncomeSource.Create(IncomeSource);
                }
                else {
                    IncomeSource.LastModifiedBy = "CMS Team Update";
                    IncomeSource.Source = "CMS Server Update";
                    IncomeSource = _IncomeSource.Update(IncomeSource); }

                if (IncomeSource != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/IncomeSource/_RowIncomeSource.cshtml", IncomeSource);

                    if (TempID == 0)
                        return Json(new { Message = "New IncomeSource Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "IncomeSource Updated Successfully", Status = true, Type = "Edit", Template = Template });
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