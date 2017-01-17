using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class EthnicityController : Controller
    {
        /// <summary>
        /// IEthnicityService object reference
        /// </summary>
        private IEthnicityService _Ethnicity = null;

        /// <summary>
        /// EthnicityController constructor For EthnicityService
        /// </summary>
        public EthnicityController()
        {
            _Ethnicity = new EthnicityService();
        }

        //
        // GET: /CMS/Ethnicity/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Ethnicity";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Ethnicity.GetAll();
            EthnicityViewModel model = new EthnicityViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Ethnicity/Index.cshtml", model);
        }

        //
        // POST: /CMS/Ethnicity/AddEditEthnicity
        [HttpPost]
        public ActionResult AddEditEthnicity(string Code)
        {
            EthnicityViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Ethnicity";
                model = new EthnicityViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Ethnicity";
                model = _Ethnicity.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Ethnicity/_AddEditEthnicityForm.cshtml", model);
        }

        //
        // POST: /CMS/Ethnicity/SaveEthnicity
        [HttpPost]
        public ActionResult SaveEthnicity(EthnicityViewModel Ethnicity)
        {
            if (ModelState.IsValid)
            {
                int TempID = Ethnicity.EthnicityID;

                if (TempID == 0)
                {
                    Ethnicity.CreatedBy = "CMS Team";
                    Ethnicity.Source = "CMS Server";
                    Ethnicity = _Ethnicity.Create(Ethnicity);
                }
                else {
                    Ethnicity.LastModifiedBy = "CMS Team Update";
                    Ethnicity.Source = "CMS Server Update";
                    Ethnicity = _Ethnicity.Update(Ethnicity); }

                if (Ethnicity != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Ethnicity/_RowEthnicity.cshtml", Ethnicity);

                    if (TempID == 0)
                        return Json(new { Message = "New Ethnicity Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Ethnicity Updated Successfully", Status = true, Type = "Edit", Template = Template });
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