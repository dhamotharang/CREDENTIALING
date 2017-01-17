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
            string documentFolder = documentRootLocator.GetDocumentRootFolder() + filePath;
            string uniqueKey = UniqueKeyGenerator.GetUniqueKey();
            string fileName = uniqueKey + "-" + documentDTO.FileName;

            SaveDocument(documentFolder, fileName, documentDTO.InputStream);
            return filePath + @"\" + fileName;
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
    }
}
