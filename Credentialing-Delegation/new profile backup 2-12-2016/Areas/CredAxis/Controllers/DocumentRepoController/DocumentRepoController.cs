using PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.DocumentRepoController
{
    public class DocumentRepoController : Controller
    {

        private IDocumentService _DocumentRepoService = null;
        public DocumentRepoController()
        {
            _DocumentRepoService = new DocumentRepoService();
        }
        //
        // GET: /CredAxis/DocumentRepo/
        public ActionResult Index()
        {
            DoumentRepoMainViewModel data = new DoumentRepoMainViewModel();
            data = _DocumentRepoService.GetDocRepoData();
            return View(data);
        }
    }
}