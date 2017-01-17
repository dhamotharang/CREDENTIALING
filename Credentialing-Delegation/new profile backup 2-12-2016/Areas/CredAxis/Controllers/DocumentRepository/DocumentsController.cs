using Newtonsoft.Json;
using PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using PortalTemplate.Areas.CredAxis.Models.LicensesViewModel;

namespace PortalTemplate.Areas.CredAxis.Controllers.DocumentRepository
{
    public class DocumentsController : Controller
    {

        private IDocumentService _DocumentRepoService = null;
        public DocumentsController()
        {
            _DocumentRepoService = new DocumentRepoService();
        }

        public ActionResult ListOfDocuments()
        {
            DoumentRepoMainViewModel data = new DoumentRepoMainViewModel();
            data = _DocumentRepoService.GetDocRepoData();

            return PartialView("~/Areas/CredAxis/Views/DocumentRepository/_ListOfDocuments.cshtml", data);
        }
        public ActionResult PackageGeneration()
        {
            DoumentRepoMainViewModel data = new DoumentRepoMainViewModel();
            data = _DocumentRepoService.GetDocRepoData();

            return PartialView("~/Areas/CredAxis/Views/DocumentRepository/PackageGeneration/_PackageGeneration.cshtml", data);
        }
     

        public ActionResult Reports()
        {
            DoumentRepoMainViewModel data = new DoumentRepoMainViewModel();
            data = _DocumentRepoService.GetDocRepoData();

            return PartialView("~/Areas/CredAxis/Views/DocumentRepository/_Reports.cshtml", data);
        }
        //
        // GET: /CredAxis/Documents/
        public ActionResult DocumentsMainView()
        {
            return PartialView("~/Areas/CredAxis/Views/DocumentRepository/_IndexDocRepo.cshtml");
        }

        public ActionResult DocumentViewer()
        {
            DocumentViewerModel model = new DocumentViewerModel();
            model.FileName = "Iowa";
            model.FileType = "pdf";
            model.UploadedBy = "Sayak";
            model.UploadedOn = new DateTime(2016, 11, 29, 15, 36, 0);
            model.Comments = new List<DocumentCommentViewModel>
            {
                new DocumentCommentViewModel{ UserName = "Manoj", CommentBody = "Nice File! ", CommentDateTime = new DateTime(2016, 11, 29, 13, 59, 0)},
                new DocumentCommentViewModel{ UserName = "Manoj", CommentBody = "Great File! ", CommentDateTime = new DateTime(2016, 11, 29, 14, 23, 0)}
            };

            return PartialView("~/Areas/CredAxis/Views/DocumentRepository/_DocumentViewer.cshtml", model);
        }

        public ActionResult GetAddNewDocument(StateLicense state, FederalDea DEA, string DocumentName)
        {
            ViewBag.Name = DocumentName;
            List<FederalDea> DEAs = new List<FederalDea>();
            List<StateLicense> States = new List<StateLicense>();
            if (DocumentName == "STATE")
            {
                state = null;
                States.Add(state);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_AddEditStateLicense.cshtml", States);
            }
            else if (DocumentName == "CV")
            {
                return PartialView("~/Areas/CredAxis/Views/DocumentRepository/View/ModalforAddEdit/CommonNewModal.cshtml");
            }
            else if (DocumentName == "DEA")
            {
                DEA = null;
                DEAs.Add(DEA);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_AddEditFederalDEAInfo.cshtml", DEAs);
            }
            else
            {
                return PartialView("~/Areas/CredAxis/Views/DocumentRepository/View/ModalforAddEdit/CommonNewModal.cshtml");
            }
        }
    }
}