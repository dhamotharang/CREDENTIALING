using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class CountyController : Controller
    {
        /// <summary>
        /// ICountyService object reference
        /// </summary>
        private ICountyService _County = null;

        /// <summary>
        /// CountyController constructor For CountyService
        /// </summary>
        public CountyController()
        {
            _County = new CountyService();
        }

        //
        // GET: /CMS/County/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "County";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _County.GetAll();
            CountyViewModel model = new CountyViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/County/Index.cshtml", model);
        }

        //
        // POST: /CMS/County/AddEditCounty
        [HttpPost]
        public ActionResult AddEditCounty(string Code)
        {
            CountyViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New County";
                model = new CountyViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit County";
                model = _County.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/County/_AddEditCountyForm.cshtml", model);
        }

        //
        // POST: /CMS/County/SaveCounty
        [HttpPost]
        public ActionResult SaveCounty(CountyViewModel County)
        {
            if (ModelState.IsValid)
            {
                int TempID = County.CountyID;

                if (TempID == 0)
                {
                    County.CreatedBy = "CMS Team";
                    County.Source = "CMS Server";
                    County = _County.Create(County);
                }
                else {
                    County.LastModifiedBy = "CMS Team Update";
                    County.Source = "CMS Server Update";
                    County = _County.Update(County); }

                if (County != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/County/_RowCounty.cshtml", County);

                    if (TempID == 0)
                        return Json(new { Message = "New County Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "County Updated Successfully", Status = true, Type = "Edit", Template = Template });
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