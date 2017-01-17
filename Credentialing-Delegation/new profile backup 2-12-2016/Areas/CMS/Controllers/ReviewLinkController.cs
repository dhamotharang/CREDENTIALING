using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class ReviewLinkController : Controller
    {
        /// <summary>
        /// IReviewLinkService object reference
        /// </summary>
        private IReviewLinkService _ReviewLink = null;

        /// <summary>
        /// ReviewLinkController constructor For ReviewLinkService
        /// </summary>
        public ReviewLinkController()
        {
            _ReviewLink = new ReviewLinkService();
        }

        //
        // GET: /CMS/ReviewLink/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "ReviewLink";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _ReviewLink.GetAll();
            ReviewLinkViewModel model = new ReviewLinkViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/ReviewLink/Index.cshtml", model);
        }

        //
        // POST: /CMS/ReviewLink/AddEditReviewLink
        [HttpPost]
        public ActionResult AddEditReviewLink(string Code)
        {
            ReviewLinkViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New ReviewLink";
                model = new ReviewLinkViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit ReviewLink";
                model = _ReviewLink.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/ReviewLink/_AddEditReviewLinkForm.cshtml", model);
        }

        //
        // POST: /CMS/ReviewLink/SaveReviewLink
        [HttpPost]
        public ActionResult SaveReviewLink(ReviewLinkViewModel ReviewLink)
        {
            if (ModelState.IsValid)
            {
                int TempID = ReviewLink.ReviewLinkID;

                if (TempID == 0)
                {
                    ReviewLink.CreatedBy = "CMS Team";
                    ReviewLink.Source = "CMS Server";
                    ReviewLink = _ReviewLink.Create(ReviewLink);
                }
                else {
                    ReviewLink.LastModifiedBy = "CMS Team Update";
                    ReviewLink.Source = "CMS Server Update";
                    ReviewLink = _ReviewLink.Update(ReviewLink); }

                if (ReviewLink != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/ReviewLink/_RowReviewLink.cshtml", ReviewLink);

                    if (TempID == 0)
                        return Json(new { Message = "New ReviewLink Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "ReviewLink Updated Successfully", Status = true, Type = "Edit", Template = Template });
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