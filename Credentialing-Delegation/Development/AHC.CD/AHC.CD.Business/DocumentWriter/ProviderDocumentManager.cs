using AHC.CD.Resources.Document;
using AHC.CD.Resources.Messages;
using AHC.UtilityService;
using System;
using System.IO;

namespace AHC.CD.Business.DocumentWriter
{
    public class ProviderDocumentManager : IDocumentsManager
    {
        private IDocumentRootLocator documentRootLocator = null;
        public ProviderDocumentManager(IDocumentRootLocator documentRootLocator)
        {
            this.documentRootLocator = documentRootLocator;
        }
        
        private bool SaveDocument(string documentFolder, string fileName, Stream stream)
        {
            if (!Directory.Exists(documentFolder))
            {
                try
                {
                    Directory.CreateDirectory(documentFolder);
                }
                catch (Exception ex)
                {
                    throw new DocumentCreationException(ExceptionMessage.FOLDER_CREATE_EXCEPTION, ex);
                }
            }

            using (var fileStream = System.IO.File.Create(documentFolder + "\\" + fileName))
            {
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                    stream.Close();
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    throw new DocumentCreationException(ExceptionMessage.CREATE_FILE_EXCEPTION, ex);
                }
            }
            return true;
        }

        public string SaveDocument(DocumentDTO documentDTO, string filePath)
        {
            string documentFolder = documentRootLocator.GetDocumentRootFolder() + documentDTO.DocRootPath;
            string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
            string fileName = uniqueKey + "-" + documentDTO.FileName;

            SaveDocument(documentFolder, fileName, documentDTO.InputStream);
            return documentDTO.DocRootPath + @"\" + fileName;
        }
        public string SaveDocumentWithoutUniqueKey(DocumentDTO documentDTO, string filePath)
        {
            string documentFolder = documentRootLocator.GetDocumentRootFolder() + documentDTO.DocRootPath;
            string fileName = null;
            if (File.Exists(documentDTO.DocRootPath + @"\" + documentDTO.FileName))
            {
                string date = DateTime.Now.ToString("MM-dd-yyyy");
                string timeHour = DateTime.Now.Hour.ToString();
                string timeMin = DateTime.Now.Minute.ToString();
                string timeSec = DateTime.Now.Second.ToString();
                fileName = documentDTO.FileName+"_"+date+"_"+timeHour+"_"+timeMin+"_"+timeSec;                
            }
            else
            {
                //string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
                fileName = documentDTO.FileName;
            }
            
            SaveDocument(documentFolder, fileName, documentDTO.InputStream);
            return documentDTO.DocRootPath + @"\" + fileName;
        }

        public void DeleteFile(string fullDocPath)
        {
            try
            {
                string documentPath = documentRootLocator.GetDocumentRootFolder() + fullDocPath;
                File.Delete(documentPath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public string DelegatedLoadToPlanPDF(byte[] pdfbytes, string DocPath,int ccID)
        {
            string documentFolder = documentRootLocator.GetDocumentRootFolder() + DocPath;
            if (!Directory.Exists(documentFolder))
            {
                try
                {
                    Directory.CreateDirectory(documentFolder);
                }
                catch (Exception ex)
                {
                    throw new DocumentCreationException(ExceptionMessage.FOLDER_CREATE_EXCEPTION, ex);
                }
            }
            //string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
            string fileName = ccID + "-" + "DelegatedPlan.pdf";
            try
            {
                
                using (var fs = new FileStream(Path.Combine(documentFolder, fileName), FileMode.Create))
                {
                    using (var writer = new BinaryWriter(fs))
                    {
                        writer.Write(pdfbytes, 0, pdfbytes.Length);
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return DocPath + @"\" + fileName;
        }

        public string DocumentationCheckListPDF(byte[] pdfbytes, string DocPath, int ccID)
        {
            string documentFolder = documentRootLocator.GetDocumentRootFolder() + DocPath;
            if (!Directory.Exists(documentFolder))
            {
                try
                {
                    Directory.CreateDirectory(documentFolder);
                }
                catch (Exception ex)
                {
                    throw new DocumentCreationException(ExceptionMessage.FOLDER_CREATE_EXCEPTION, ex);
                }
            }
            //string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
            string fileName = ccID + "-" + "DocumentCheckList.pdf";
            try
            {

                using (var fs = new FileStream(Path.Combine(documentFolder, fileName), FileMode.Create))
                {
                    using (var writer = new BinaryWriter(fs))
                    {
                        writer.Write(pdfbytes, 0, pdfbytes.Length);
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return DocPath + @"\" + fileName;
        }

        public string DocumentCheckListPDF(string pdfFile, string DocPath)
        {
            string documentFolder = documentRootLocator.GetDocumentRootFolder() + DocPath;
            if (!Directory.Exists(documentFolder))
            {
                try
                {
                    Directory.CreateDirectory(documentFolder);
                }
                catch (Exception ex)
                {
                    throw new DocumentCreationException(ExceptionMessage.FOLDER_CREATE_EXCEPTION, ex);
                }
            }
            string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
            string fileName = uniqueKey + "-" + "DocumentCheckList.pdf";
            try
            {
                var pdfBinary = Convert.FromBase64String(pdfFile);
                using (var fs = new FileStream(Path.Combine(documentFolder, fileName), FileMode.Create))
                {
                    using (var writer = new BinaryWriter(fs))
                    {
                        writer.Write(pdfBinary, 0, pdfBinary.Length);
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            return DocPath + @"\" + fileName;
        }

        public string SaveProfileReportPDF(byte[] pdfbytes, string DocPath)
        {
            string documentFolder = documentRootLocator.GetDocumentRootFolder() + DocPath;
            if (!Directory.Exists(documentFolder))
            {
                try
                {
                    Directory.CreateDirectory(documentFolder);
                }
                catch (Exception ex)
                {
                    throw new DocumentCreationException(ExceptionMessage.FOLDER_CREATE_EXCEPTION, ex);
                }
            }
            string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
            string fileName = uniqueKey + "-" + "ProfileReport.pdf";
            try
            {

                using (var fs = new FileStream(Path.Combine(documentFolder, fileName), FileMode.Create))
                {
                    using (var writer = new BinaryWriter(fs))
                    {
                        writer.Write(pdfbytes, 0, pdfbytes.Length);
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return DocPath + @"\" + fileName;
        }
    }
}
