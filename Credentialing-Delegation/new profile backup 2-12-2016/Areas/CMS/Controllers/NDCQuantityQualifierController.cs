using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class NDCQuantityQualifierController : Controller
    {
        /// <summary>
        /// INDCQuantityQualifierService object reference
        /// </summary>
        private INDCQuantityQualifierService _NDCQuantityQualifier = null;

        /// <summary>
        /// NDCQuantityQualifierController constructor For NDCQuantityQualifierService
        /// </summary>
        public NDCQuantityQualifierController()
        {
            _NDCQuantityQualifier = new NDCQuantityQualifierService();
        }

        //
        // GET: /CMS/NDCQuantityQualifier/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "NDCQuantityQualifier";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _NDCQuantityQualifier.GetAll();
            NDCQuantityQualifierViewModel model = new NDCQuantityQualifierViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/NDCQuantityQualifier/Index.cshtml", model);
        }

        //
        // POST: /CMS/NDCQuantityQualifier/AddEditNDCQuantityQualifier
        [HttpPost]
        public ActionResult AddEditNDCQuantityQualifier(string Code)
        {
            NDCQuantityQualifierViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New NDCQuantityQualifier";
                model = new NDCQuantityQualifierViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit NDCQuantityQualifier";
                model = _NDCQuantityQualifier.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/NDCQuantityQualifier/_AddEditNDCQuantityQualifierForm.cshtml", model);
        }

        //
        // POST: /CMS/NDCQuantityQualifier/SaveNDCQuantityQualifier
        [HttpPost]
        public ActionResult SaveNDCQuantityQualifier(NDCQuantityQualifierViewModel NDCQuantityQualifier)
        {
            if (ModelState.IsValid)
            {
                int TempID = NDCQuantityQualifier.NDCQuantityQualifierID;

                if (TempID == 0)
                {
                    NDCQuantityQualifier.CreatedBy = "CMS Team";
                    NDCQuantityQualifier.Source = "CMS Server";
                    NDCQuantityQualifier = _NDCQuantityQualifier.Create(NDCQuantityQualifier);
                }
                else {
                    NDCQuantityQualifier.LastModifiedBy = "CMS Team Update";
                    NDCQuantityQualifier.Source = "CMS Server Update";
                    NDCQuantityQualifier = _NDCQuantityQualifier.Update(NDCQuantityQualifier); }

                if (NDCQuantityQualifier != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/NDCQuantityQualifier/_RowNDCQuantityQualifier.cshtml", NDCQuantityQualifier);

                    if (TempID == 0)
                        return Json(new { Message = "New NDCQuantityQualifier Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "NDCQuantityQualifier Updated Successfully", Status = true, Type = "Edit", Template = Template });
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