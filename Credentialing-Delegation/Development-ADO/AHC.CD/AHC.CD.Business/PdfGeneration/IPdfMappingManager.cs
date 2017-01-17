using AHC.CD.Business.BusinessModels.PDFGenerator;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace AHC.CD.Business.PdfGeneration
{
    public interface IPdfMappingManager
    {
        //Task<Profile> GetProfileList(int profileID);
        //string readXml(PDFMappingDataBusinessModel pmodel, string templateName);
        //string CreatePDF(Dictionary<string, string> dataObj, string pdfName, string templateName);
        string GeneratePlanFormPDF(int profileId, string templateName);
    }
}
