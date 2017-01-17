using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DocumentCategoryController : Controller
    {
        /// <summary>
        /// IDocumentCategoryService object reference
        /// </summary>
        private IDocumentCategoryService _DocumentCategory = null;

        /// <summary>
        /// DocumentCategoryController constructor For DocumentCategoryService
        /// </summary>
        public DocumentCategoryController()
        {
            _DocumentCategory = new DocumentCategoryService();
        }

        //
        // GET: /CMS/DocumentCategory/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DocumentCategory";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DocumentCategory.GetAll();
            DocumentCategoryViewModel model = new DocumentCategoryViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DocumentCategory/Index.cshtml", model);
        }

        //
        // POST: /CMS/DocumentCategory/AddEditDocumentCategory
        [HttpPost]
        public ActionResult AddEditDocumentCategory(string Code)
        {
            DocumentCategoryViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DocumentCategory";
                model = new DocumentCategoryViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DocumentCategory";
                model = _DocumentCategory.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DocumentCategory/_AddEditDocumentCategoryForm.cshtml", model);
        }

        //
        // POST: /CMS/DocumentCategory/SaveDocumentCategory
        [HttpPost]
        public ActionResult SaveDocumentCategory(DocumentCategoryViewModel DocumentCategory)
        {
            if (ModelState.IsValid)
            {
                int TempID = DocumentCategory.DocumentCategoryID;

                if (TempID == 0)
                {
                    DocumentCategory.CreatedBy = "CMS Team";
                    DocumentCategory.Source = "CMS Server";
                    DocumentCategory = _DocumentCategory.Create(DocumentCategory);
                }
                else {
                    DocumentCategory.LastModifiedBy = "CMS Team Update";
                    DocumentCategory.Source = "CMS Server Update";
                    DocumentCategory = _DocumentCategory.Update(DocumentCategory); }

                if (DocumentCategory != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DocumentCategory/_RowDocumentCategory.cshtml", DocumentCategory);

                    if (TempID == 0)
                        return Json(new { Message = "New DocumentCategory Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DocumentCategory Updated Successfully", Status = true, Type = "Edit", Template = Template });
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