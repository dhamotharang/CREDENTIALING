using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class CityController : Controller
    {
        /// <summary>
        /// ICityService object reference
        /// </summary>
        private ICityService _City = null;

        /// <summary>
        /// CityController constructor For CityService
        /// </summary>
        public CityController()
        {
            _City = new CityService();
        }

        //
        // GET: /CMS/City/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "City";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _City.GetAll();
            CityViewModel model = new CityViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/City/Index.cshtml", model);
        }

        //
        // POST: /CMS/City/AddEditCity
        [HttpPost]
        public ActionResult AddEditCity(string Code)
        {
            CityViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New City";
                model = new CityViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit City";
                model = _City.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/City/_AddEditCityForm.cshtml", model);
        }

        //
        // POST: /CMS/City/SaveCity
        [HttpPost]
        public ActionResult SaveCity(CityViewModel City)
        {
            if (ModelState.IsValid)
            {
                int TempID = City.CityID;

                if (TempID == 0)
                {
                    City.CreatedBy = "CMS Team";
                    City.Source = "CMS Server";
                    City = _City.Create(City);
                }
                else {
                    City.LastModifiedBy = "CMS Team Update";
                    City.Source = "CMS Server Update";
                    City = _City.Update(City); }

                if (City != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/City/_RowCity.cshtml", City);

                    if (TempID == 0)
                        return Json(new { Message = "New City Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "City Updated Successfully", Status = true, Type = "Edit", Template = Template });
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