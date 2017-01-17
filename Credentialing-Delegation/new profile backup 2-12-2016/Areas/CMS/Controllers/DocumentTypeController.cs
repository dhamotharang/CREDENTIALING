using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DocumentTypeController : Controller
    {
        /// <summary>
        /// IDocumentTypeService object reference
        /// </summary>
        private IDocumentTypeService _DocumentType = null;

        /// <summary>
        /// DocumentTypeController constructor For DocumentTypeService
        /// </summary>
        public DocumentTypeController()
        {
            _DocumentType = new DocumentTypeService();
        }

        //
        // GET: /CMS/DocumentType/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DocumentType";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DocumentType.GetAll();
            DocumentTypeViewModel model = new DocumentTypeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DocumentType/Index.cshtml", model);
        }

        //
        // POST: /CMS/DocumentType/AddEditDocumentType
        [HttpPost]
        public ActionResult AddEditDocumentType(string Code)
        {
            DocumentTypeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DocumentType";
                model = new DocumentTypeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DocumentType";
                model = _DocumentType.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DocumentType/_AddEditDocumentTypeForm.cshtml", model);
        }

        //
        // POST: /CMS/DocumentType/SaveDocumentType
        [HttpPost]
        public ActionResult SaveDocumentType(DocumentTypeViewModel DocumentType)
        {
            if (ModelState.IsValid)
            {
                int TempID = DocumentType.DocumentTypeID;

                if (TempID == 0)
                {
                    DocumentType.CreatedBy = "CMS Team";
                    DocumentType.Source = "CMS Server";
                    DocumentType = _DocumentType.Create(DocumentType);
                }
                else {
                    DocumentType.LastModifiedBy = "CMS Team Update";
                    DocumentType.Source = "CMS Server Update";
                    DocumentType = _DocumentType.Update(DocumentType); }

                if (DocumentType != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DocumentType/_RowDocumentType.cshtml", DocumentType);

                    if (TempID == 0)
                        return Json(new { Message = "New DocumentType Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DocumentType Updated Successfully", Status = true, Type = "Edit", Template = Template });
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