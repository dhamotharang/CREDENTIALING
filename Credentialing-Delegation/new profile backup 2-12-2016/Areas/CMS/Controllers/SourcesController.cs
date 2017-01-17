using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class SourcesController : Controller
    {
        /// <summary>
        /// ISourcesService object reference
        /// </summary>
        private ISourcesService _Sources = null;

        /// <summary>
        /// SourcesController constructor For SourcesService
        /// </summary>
        public SourcesController()
        {
            _Sources = new SourcesService();
        }

        //
        // GET: /CMS/Sources/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Sources";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Sources.GetAll();
            SourcesViewModel model = new SourcesViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Sources/Index.cshtml", model);
        }

        //
        // POST: /CMS/Sources/AddEditSources
        [HttpPost]
        public ActionResult AddEditSources(string Code)
        {
            SourcesViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Sources";
                model = new SourcesViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Sources";
                model = _Sources.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Sources/_AddEditSourcesForm.cshtml", model);
        }

        //
        // POST: /CMS/Sources/SaveSources
        [HttpPost]
        public ActionResult SaveSources(SourcesViewModel Sources)
        {
            if (ModelState.IsValid)
            {
                int TempID = Sources.SourcesID;

                if (TempID == 0)
                {
                    Sources.CreatedBy = "CMS Team";
                    Sources.Source = "CMS Server";
                    Sources = _Sources.Create(Sources);
                }
                else {
                    Sources.LastModifiedBy = "CMS Team Update";
                    Sources.Source = "CMS Server Update";
                    Sources = _Sources.Update(Sources); }

                if (Sources != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Sources/_RowSources.cshtml", Sources);

                    if (TempID == 0)
                        return Json(new { Message = "New Sources Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Sources Updated Successfully", Status = true, Type = "Edit", Template = Template });
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