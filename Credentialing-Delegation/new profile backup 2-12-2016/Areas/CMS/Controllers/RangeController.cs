using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class RangeController : Controller
    {
        /// <summary>
        /// IRangeService object reference
        /// </summary>
        private IRangeService _Range = null;

        /// <summary>
        /// RangeController constructor For RangeService
        /// </summary>
        public RangeController()
        {
            _Range = new RangeService();
        }

        //
        // GET: /CMS/Range/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Range";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Range.GetAll();
            RangeViewModel model = new RangeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Range/Index.cshtml", model);
        }

        //
        // POST: /CMS/Range/AddEditRange
        [HttpPost]
        public ActionResult AddEditRange(string Code)
        {
            RangeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Range";
                model = new RangeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Range";
                model = _Range.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Range/_AddEditRangeForm.cshtml", model);
        }

        //
        // POST: /CMS/Range/SaveRange
        [HttpPost]
        public ActionResult SaveRange(RangeViewModel Range)
        {
            if (ModelState.IsValid)
            {
                int TempID = Range.RangeID;

                if (TempID == 0)
                {
                    Range.CreatedBy = "CMS Team";
                    Range.Source = "CMS Server";
                    Range = _Range.Create(Range);
                }
                else {
                    Range.LastModifiedBy = "CMS Team Update";
                    Range.Source = "CMS Server Update";
                    Range = _Range.Update(Range); }

                if (Range != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Range/_RowRange.cshtml", Range);

                    if (TempID == 0)
                        return Json(new { Message = "New Range Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Range Updated Successfully", Status = true, Type = "Edit", Template = Template });
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