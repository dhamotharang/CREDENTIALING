using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class DocumentNameController : Controller
    {
        /// <summary>
        /// IDocumentNameService object reference
        /// </summary>
        private IDocumentNameService _DocumentName = null;

        /// <summary>
        /// DocumentNameController constructor For DocumentNameService
        /// </summary>
        public DocumentNameController()
        {
            _DocumentName = new DocumentNameService();
        }

        //
        // GET: /CMS/DocumentName/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "DocumentName";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _DocumentName.GetAll();
            DocumentNameViewModel model = new DocumentNameViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/DocumentName/Index.cshtml", model);
        }

        //
        // POST: /CMS/DocumentName/AddEditDocumentName
        [HttpPost]
        public ActionResult AddEditDocumentName(string Code)
        {
            DocumentNameViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New DocumentName";
                model = new DocumentNameViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit DocumentName";
                model = _DocumentName.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/DocumentName/_AddEditDocumentNameForm.cshtml", model);
        }

        //
        // POST: /CMS/DocumentName/SaveDocumentName
        [HttpPost]
        public ActionResult SaveDocumentName(DocumentNameViewModel DocumentName)
        {
            if (ModelState.IsValid)
            {
                int TempID = DocumentName.DocumentNameID;

                if (TempID == 0)
                {
                    DocumentName.CreatedBy = "CMS Team";
                    DocumentName.Source = "CMS Server";
                    DocumentName = _DocumentName.Create(DocumentName);
                }
                else {
                    DocumentName.LastModifiedBy = "CMS Team Update";
                    DocumentName.Source = "CMS Server Update";
                    DocumentName = _DocumentName.Update(DocumentName); }

                if (DocumentName != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/DocumentName/_RowDocumentName.cshtml", DocumentName);

                    if (TempID == 0)
                        return Json(new { Message = "New DocumentName Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "DocumentName Updated Successfully", Status = true, Type = "Edit", Template = Template });
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