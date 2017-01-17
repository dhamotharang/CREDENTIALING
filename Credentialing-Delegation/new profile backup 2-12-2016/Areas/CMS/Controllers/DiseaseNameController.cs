using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DiseaseNameController : Controller
    {
        /// <summary>
        /// IDiseaseNameService object reference
        /// </summary>
        private IDiseaseNameService _DiseaseName = null;

        /// <summary>
        /// DiseaseNameController constructor For DiseaseNameService
        /// </summary>
        public DiseaseNameController()
        {
            _DiseaseName = new DiseaseNameService();
        }

        //
        // GET: /CMS/DiseaseName/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DiseaseName";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DiseaseName.GetAll();
            DiseaseNameViewModel model = new DiseaseNameViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DiseaseName/Index.cshtml", model);
        }

        //
        // POST: /CMS/DiseaseName/AddEditDiseaseName
        [HttpPost]
        public ActionResult AddEditDiseaseName(string Code)
        {
            DiseaseNameViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DiseaseName";
                model = new DiseaseNameViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DiseaseName";
                model = _DiseaseName.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DiseaseName/_AddEditDiseaseNameForm.cshtml", model);
        }

        //
        // POST: /CMS/DiseaseName/SaveDiseaseName
        [HttpPost]
        public ActionResult SaveDiseaseName(DiseaseNameViewModel DiseaseName)
        {
            if (ModelState.IsValid)
            {
                int TempID = DiseaseName.DiseaseNameID;

                if (TempID == 0)
                {
                    DiseaseName.CreatedBy = "CMS Team";
                    DiseaseName.Source = "CMS Server";
                    DiseaseName = _DiseaseName.Create(DiseaseName);
                }
                else {
                    DiseaseName.LastModifiedBy = "CMS Team Update";
                    DiseaseName.Source = "CMS Server Update";
                    DiseaseName = _DiseaseName.Update(DiseaseName); }

                if (DiseaseName != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DiseaseName/_RowDiseaseName.cshtml", DiseaseName);

                    if (TempID == 0)
                        return Json(new { Message = "New DiseaseName Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DiseaseName Updated Successfully", Status = true, Type = "Edit", Template = Template });
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