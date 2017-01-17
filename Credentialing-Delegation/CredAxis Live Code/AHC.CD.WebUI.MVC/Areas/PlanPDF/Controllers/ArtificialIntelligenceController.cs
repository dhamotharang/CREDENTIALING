using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.PdfGeneration;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Entities.PackageGenerate;
using AHC.CD.WebUI.MVC.Areas.PlanPDF.Models;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace AHC.CD.WebUI.MVC.Areas.PlanPDF.Controllers
{
    public class ArtificialIntelligenceController : Controller
    {
        //
        // GET: /PlanPDF/ArtificialIntelligence/
        IPdfMappingManager PDFManager=null;
        private IProfileRepository profileRepository = null;
        private IUnitOfWork uow = null;

        public ArtificialIntelligenceController(IUnitOfWork uow, IPdfMappingManager PDFManager)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
            this.PDFManager = PDFManager;
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllPdfFields(string PlanFormName)
        {
            try
            {
                List<string> pdfFields = new List<string>();
                pdfFields = PDFManager.GetAllPdfFields(PlanFormName);
                return Json(pdfFields, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public JsonResult GetAllPlanForms()
        {
            try
            {
                var pdfFields = PDFManager.GetAllPlanForms();

                return Json(pdfFields, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        
        public string CreatePlanFormXml(string PlanFormName, List<string> GenericVariableList, List<string> PlanVariableList)
       {
           try
           {
               string s = PDFManager.CreatePlanFormXml(PlanFormName, GenericVariableList, PlanVariableList);
               return s;
           }
           catch (Exception)
           {
               throw;
           }
        }

        public async Task<JsonResult> AddPlanForm(PlanFormViewModel planForm)
        {
            try
            {
                PlanForm PlanForm = new PlanForm();
                if (ModelState.IsValid)
                {
                    PlanForm = AutoMapper.Mapper.Map<PlanFormViewModel, PlanForm>(planForm);
                    DocumentDTO document = CreateDocument(planForm.PlanFormFile);
                    var addedPlanForm = await PDFManager.AddPlanForm(PlanForm, document);
                    return Json(addedPlanForm, JsonRequestBehavior.AllowGet);
                }
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private DocumentDTO CreateDocument(HttpPostedFileBase file, bool isRemoved = false)
        {
            DocumentDTO document = new DocumentDTO() { IsRemoved = isRemoved };
            if (file != null)
            {
                document.FileName = file.FileName;
                document.InputStream = file.InputStream;
            }

            return document;
        }

    }
}