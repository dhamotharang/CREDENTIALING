using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ReviewTypeController : Controller
    {
        /// <summary>
        /// IReviewTypeService object reference
        /// </summary>
        private IReviewTypeService _ReviewType = null;

        /// <summary>
        /// ReviewTypeController constructor For ReviewTypeService
        /// </summary>
        public ReviewTypeController()
        {
            _ReviewType = new ReviewTypeService();
        }

        //
        // GET: /CMS/ReviewType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ReviewType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ReviewType.GetAll();
            ReviewTypeViewModel model = new ReviewTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ReviewType/Index.cshtml", model);
        }

        //
        // POST: /CMS/ReviewType/AddEditReviewType
        [HttpPost]
        public ActionResult AddEditReviewType(string Code)
        {
            ReviewTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ReviewType";
                model = new ReviewTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ReviewType";
                model = _ReviewType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ReviewType/_AddEditReviewTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/ReviewType/SaveReviewType
        [HttpPost]
        public ActionResult SaveReviewType(ReviewTypeViewModel ReviewType)
        {
            if (ModelState.IsValid)
            {
                int TempID = ReviewType.ReviewTypeID;

                if (TempID == 0)
                {
                    ReviewType.CreatedBy = "CMS Team";
                    ReviewType.Source = "CMS Server";
                    ReviewType = _ReviewType.Create(ReviewType);
                }
                else {
                    ReviewType.LastModifiedBy = "CMS Team Update";
                    ReviewType.Source = "CMS Server Update";
                    ReviewType = _ReviewType.Update(ReviewType); }

                if (ReviewType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ReviewType/_RowReviewType.cshtml", ReviewType);

                    if (TempID == 0)
                        return Json(new { Message = "New ReviewType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ReviewType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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