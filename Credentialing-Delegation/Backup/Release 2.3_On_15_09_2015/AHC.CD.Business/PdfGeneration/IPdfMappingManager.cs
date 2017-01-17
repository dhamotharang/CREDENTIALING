using AHC.CD.Business.BusinessModels.PDFGenerator;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AHC.CD.Entities.DocumentRepository;


namespace AHC.CD.Business.PdfGeneration
{
    public interface IPdfMappingManager
    {
        //Task<Profile> GetProfileList(int profileID);
        //string readXml(PDFMappingDataBusinessModel pmodel, string templateName);
        //string CreatePDF(Dictionary<string, string> dataObj, string pdfName, string templateName);
        string GeneratePlanFormPDF(int profileId, string templateName);
        void AddDocument(int profileID,string documentTitle);
        string CombinePdfs(int profileID,List<string> pdflist);
        List<PackageGeneratorBusinessModel> GenerateBulkPackage(List<int> ProfileIDs, List<string> GenricList);
        List<PlanFormGenerationBusinessModel> GenerateBulkForm(List<int> ProfileIDs, List<string> TemplateName);
    }
}
