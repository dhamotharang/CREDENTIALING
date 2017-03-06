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
using AHC.CD.Business.BusinessModels.WelcomeLetter;


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
        Task<List<PlanFormGenerationBusinessModel>> GenerateFormsUsingADO(List<int> ProfileIDs, List<string> TemplateName, string UserAuthId);
        Task<string> GenerateGenericPlanFormPDF(int profileId, string templateName, string UserAuthId);
        List<PlanForm> GetAllPlanForms();
        List<string> GetAllPdfFields(string PlanFormName);
        string CreatePlanFormXml(string PlanFormName, List<string> GenericVariableList, List<string> PlanVariableList);
        Task<PlanForm> AddPlanForm(PlanForm planForm, DocumentDTO document);
        WelcomeLetterBusinessModel GenerateWelcomeLetterPDF(WelcomeLetterBusinessModel welcomeletterdata, string templateName, string cduserid, int credLogId);
        //string readXmlForWelcomeLetter(WelcomeLetterBusinessModel pmodel, string templateName, string CDUserId);
    }
}
