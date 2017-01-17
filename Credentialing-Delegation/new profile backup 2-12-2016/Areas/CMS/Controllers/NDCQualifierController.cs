using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class NDCQualifierController : Controller
    {
        /// <summary>
        /// INDCQualifierService object reference
        /// </summary>
        private INDCQualifierService _NDCQualifier = null;

        /// <summary>
        /// NDCQualifierController constructor For NDCQualifierService
        /// </summary>
        public NDCQualifierController()
        {
            _NDCQualifier = new NDCQualifierService();
        }

        //
        // GET: /CMS/NDCQualifier/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "NDCQualifier";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _NDCQualifier.GetAll();
            NDCQualifierViewModel model = new NDCQualifierViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/NDCQualifier/Index.cshtml", model);
        }

        //
        // POST: /CMS/NDCQualifier/AddEditNDCQualifier
        [HttpPost]
        public ActionResult AddEditNDCQualifier(string Code)
        {
            NDCQualifierViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New NDCQualifier";
                model = new NDCQualifierViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit NDCQualifier";
                model = _NDCQualifier.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/NDCQualifier/_AddEditNDCQualifierForm.cshtml", model);
        }

        //
        // POST: /CMS/NDCQualifier/SaveNDCQualifier
        [HttpPost]
        public ActionResult SaveNDCQualifier(NDCQualifierViewModel NDCQualifier)
        {
            if (ModelState.IsValid)
            {
                int TempID = NDCQualifier.NDCQualifierID;

                if (TempID == 0)
                {
                    NDCQualifier.CreatedBy = "CMS Team";
                    NDCQualifier.Source = "CMS Server";
                    NDCQualifier = _NDCQualifier.Create(NDCQualifier);
                }
                else {
                    NDCQualifier.LastModifiedBy = "CMS Team Update";
                    NDCQualifier.Source = "CMS Server Update";
                    NDCQualifier = _NDCQualifier.Update(NDCQualifier); }

                if (NDCQualifier != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/NDCQualifier/_RowNDCQualifier.cshtml", NDCQualifier);

                    if (TempID == 0)
                        return Json(new { Message = "New NDCQualifier Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "NDCQualifier Updated Successfully", Status = true, Type = "Edit", Template = Template });
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