using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class TypeOfCareController : Controller
    {
        /// <summary>
        /// ITypeOfCareService object reference
        /// </summary>
        private ITypeOfCareService _TypeOfCare = null;

        /// <summary>
        /// TypeOfCareController constructor For TypeOfCareService
        /// </summary>
        public TypeOfCareController()
        {
            _TypeOfCare = new TypeOfCareService();
        }

        //
        // GET: /CMS/TypeOfCare/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "TypeOfCare";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _TypeOfCare.GetAll();
            TypeOfCareViewModel model = new TypeOfCareViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/TypeOfCare/Index.cshtml", model);
        }

        //
        // POST: /CMS/TypeOfCare/AddEditTypeOfCare
        [HttpPost]
        public ActionResult AddEditTypeOfCare(string Code)
        {
            TypeOfCareViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New TypeOfCare";
                model = new TypeOfCareViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit TypeOfCare";
                model = _TypeOfCare.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/TypeOfCare/_AddEditTypeOfCareForm.cshtml", model);
        }

        //
        // POST: /CMS/TypeOfCare/SaveTypeOfCare
        [HttpPost]
        public ActionResult SaveTypeOfCare(TypeOfCareViewModel TypeOfCare)
        {
            if (ModelState.IsValid)
            {
                int TempID = TypeOfCare.TypeOfCareID;

                if (TempID == 0)
                {
                    TypeOfCare.CreatedBy = "CMS Team";
                    TypeOfCare.Source = "CMS Server";
                    TypeOfCare = _TypeOfCare.Create(TypeOfCare);
                }
                else {
                    TypeOfCare.LastModifiedBy = "CMS Team Update";
                    TypeOfCare.Source = "CMS Server Update";
                    TypeOfCare = _TypeOfCare.Update(TypeOfCare); }

                if (TypeOfCare != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/TypeOfCare/_RowTypeOfCare.cshtml", TypeOfCare);

                    if (TempID == 0)
                        return Json(new { Message = "New TypeOfCare Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "TypeOfCare Updated Successfully", Status = true, Type = "Edit", Template = Template });
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