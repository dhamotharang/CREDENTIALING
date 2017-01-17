using AHC.CD.Business.BusinessModels.PDFGenerator;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AHC.CD.Entities.DocumentRepository;
using AHC.CD.Entities.PackageGenerate;
using AHC.CD.Business.DocumentWriter;


namespace AHC.CD.Business.PdfGeneration
{
    public interface IPdfMappingManager
    {
        //Task<Profile> GetProfileList(int profileID);
        //string readXml(PDFMappingDataBusinessModel pmodel, string templateName);
        //string CreatePDF(Dictionary<string, string> dataObj, string pdfName, string templateName);
        Task<string> GeneratePlanFormPDF(int profileId, string templateName, string UserAuthId);
        void AddDocument(int profileID,string documentTitle);
        string CombinePdfs(int profileID, List<string> pdflist, string UserAuthId, string name);
        List<PackageGeneratorBusinessModel> GenerateBulkPackage(List<int> ProfileIDs, List<string> GenricList, string UserAuthId, string name);
        Task<List<PlanFormGenerationBusinessModel>> GenerateBulkForm(List<int> ProfileIDs, List<string> TemplateName, string UserAuthId);
        List<PlanForm> GetAllPlanForms();
        List<string> GetAllPdfFields(string PlanFormName);
        string CreatePlanFormXml(string PlanFormName, List<string> GenericVariableList, List<string> PlanVariableList);
        Task<PlanForm> AddPlanForm(PlanForm planForm, DocumentDTO document);
    }
}
