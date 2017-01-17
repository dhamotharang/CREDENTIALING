using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class CountryController : Controller
    {
        /// <summary>
        /// ICountryService object reference
        /// </summary>
        private ICountryService _Country = null;

        /// <summary>
        /// CountryController constructor For CountryService
        /// </summary>
        public CountryController()
        {
            _Country = new CountryService();
        }

        //
        // GET: /CMS/Country/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Country";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Country.GetAll();
            CountryViewModel model = new CountryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Country/Index.cshtml", model);
        }

        //
        // POST: /CMS/Country/AddEditCountry
        [HttpPost]
        public ActionResult AddEditCountry(string Code)
        {
            CountryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Country";
                model = new CountryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Country";
                model = _Country.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Country/_AddEditCountryForm.cshtml", model);
        }

        //
        // POST: /CMS/Country/SaveCountry
        [HttpPost]
        public ActionResult SaveCountry(CountryViewModel Country)
        {
            if (ModelState.IsValid)
            {
                int TempID = Country.CountryID;

                if (TempID == 0)
                {
                    Country.CreatedBy = "CMS Team";
                    Country.Source = "CMS Server";
                    Country = _Country.Create(Country);
                }
                else {
                    Country.LastModifiedBy = "CMS Team Update";
                    Country.Source = "CMS Server Update";
                    Country = _Country.Update(Country); }

                if (Country != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Country/_RowCountry.cshtml", Country);

                    if (TempID == 0)
                        return Json(new { Message = "New Country Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Country Updated Successfully", Status = true, Type = "Edit", Template = Template });
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