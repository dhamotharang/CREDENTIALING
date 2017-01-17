using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class TextSnippetController : Controller
    {
        /// <summary>
        /// ITextSnippetService object reference
        /// </summary>
        private ITextSnippetService _TextSnippet = null;

        /// <summary>
        /// TextSnippetController constructor For TextSnippetService
        /// </summary>
        public TextSnippetController()
        {
            _TextSnippet = new TextSnippetService();
        }

        //
        // GET: /CMS/TextSnippet/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "TextSnippet";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _TextSnippet.GetAll();
            TextSnippetViewModel model = new TextSnippetViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/TextSnippet/Index.cshtml", model);
        }

        //
        // POST: /CMS/TextSnippet/AddEditTextSnippet
        [HttpPost]
        public ActionResult AddEditTextSnippet(string Code)
        {
            TextSnippetViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New TextSnippet";
                model = new TextSnippetViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit TextSnippet";
                model = _TextSnippet.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/TextSnippet/_AddEditTextSnippetForm.cshtml", model);
        }

        //
        // POST: /CMS/TextSnippet/SaveTextSnippet
        [HttpPost]
        public ActionResult SaveTextSnippet(TextSnippetViewModel TextSnippet)
        {
            if (ModelState.IsValid)
            {
                int TempID = TextSnippet.TextSnippetID;

                if (TempID == 0)
                {
                    TextSnippet.CreatedBy = "CMS Team";
                    TextSnippet.Source = "CMS Server";
                    TextSnippet = _TextSnippet.Create(TextSnippet);
                }
                else {
                    TextSnippet.LastModifiedBy = "CMS Team Update";
                    TextSnippet.Source = "CMS Server Update";
                    TextSnippet = _TextSnippet.Update(TextSnippet); }

                if (TextSnippet != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/TextSnippet/_RowTextSnippet.cshtml", TextSnippet);

                    if (TempID == 0)
                        return Json(new { Message = "New TextSnippet Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "TextSnippet Updated Successfully", Status = true, Type = "Edit", Template = Template });
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