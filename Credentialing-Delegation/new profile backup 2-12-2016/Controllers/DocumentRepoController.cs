using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using PortalTemplate.Areas.CredAxis.Models.LicensesViewModel;

namespace PortalTemplate.Controllers
{
    public class DocumentRepoController : Controller
    {
        //
        // GET: /DocumentRepo/
       
        public ActionResult Index()
        {
            string file = HostingEnvironment.MapPath("~/Resources/JSONData/DocRepo.json");
            string json = System.IO.File.ReadAllText(file);
            ViewBag.DocList = JsonConvert.DeserializeObject(json);
            //Folders = new List<FolderViewModel>{
            //    new FolderViewModel{ FolderName = "STATE LICENSE", Documents = new List<DocumentViewModel>{
            //        new DocumentViewModel{ FileName="Florida", FilePath = "~/Areas/CredAxis/Resources/DocRepofiles/Board%20Certified_new.pdf", FileType="pdf"},
            //        new DocumentViewModel{ FileName = "Alaska", FilePath="", FileType="pdf"},
            //        new DocumentViewModel{ FileName = "Iowa", FilePath="", FileType="pdf"}
            //    }},
            //    new FolderViewModel{ FolderName = "DEA", Documents = new List<DocumentViewModel>{
            //        new DocumentViewModel{ FileName="DEA1", FilePath = "", FileType="pdf"},
            //        new DocumentViewModel{ FileName = "DEA2", FilePath="", FileType="xlsx"}
            //    }},
            //    new FolderViewModel{ FolderName = "CV", Documents = new List<DocumentViewModel>{
            //        new DocumentViewModel{ FileName="CV1", FilePath = "", FileType="docx"}
            //    }},
            //    new FolderViewModel{ FolderName = "DRIVING LICENSE", Documents = new List<DocumentViewModel>{
            //        new DocumentViewModel{ FileName="DL", FilePath = "", FileType="jpg"}
            //    }}
            //};

            return PartialView("~/Views/DocumentRepo/_ListOfDocuments.cshtml");
        }

        public ActionResult PreviewDocument()
        {
            return View();
        }
        public ActionResult GetAddNewDocument(StateLicense state,FederalDea DEA, string DocumentName)
        {
            List<FederalDea> DEAs = new List<FederalDea>();     
            List<StateLicense> States = new List<StateLicense>();
            if(DocumentName == "STATE")
            {
                state = null;
                States.Add(state);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_AddEditStateLicense.cshtml",States);
            }
            else if(DocumentName == "CV")
            {
               
                return PartialView();
            }
            else if(DocumentName == "DEA")
            {
                DEA = null;
                DEAs.Add(DEA);
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/FederalDea/_AddEditFederalDEAInfo.cshtml",DEAs);
            }
            else
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/StateLicense/_AddEditStateLicense.cshtml");
            }
        }
	}
}