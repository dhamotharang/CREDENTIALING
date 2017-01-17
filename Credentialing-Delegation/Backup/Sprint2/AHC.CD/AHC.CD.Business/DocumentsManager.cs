using AHC.CD.Business.DTO;
using AHC.CD.Data.Repository;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AHC.CD.Business
{
    internal class DocumentsManager : IDocumentsManager
    {
        private IProvidersRepository providersRepository = null;
        private IGenericRepository<Document> documentsRepository = null;
        private IGenericRepository<DocumentCategory> documentsCategoryRepository = null;
        private IGenericRepository<DocumentType> documentsTypeRepository = null;
        Document providerDocument = new Document();

        public DocumentsManager(IUnitOfWork uow)
        {
            this.providersRepository = uow.GetProvidersRepository();
            documentsRepository = uow.GetDocumentsRepository();
            documentsCategoryRepository = uow.GetDocumentsCategoryRepository();
            documentsTypeRepository = uow.GetDocumentsTypeRepository();
        }


        /// <summary>
        /// Saves Individual Provider Docuemnt file Async
        /// </summary>
        /// <param name="applicationRootFolder= {Application Root Folder}"></param>
        /// <param name="providerDocument.FileName=VenkatCV.pdf"></param>
        /// <param name="providerDocument.DocumentCategoryID = {number}"></param>
        /// 
        public bool SaveOrUpdateProviderDocument(DocumentDTO documentDTO)
        {
            string filePathToSave = "";
            using (TransactionScope scope = new TransactionScope())
            {
                string docType = documentsTypeRepository.Find(documentDTO.DocumentTypeID).Title;
                string docCategory = documentsTypeRepository.Find(documentDTO.DocumentTypeID).DocumentCategory.Title;

                //providerDocument.FilePath=\Documents\AHCP-{ProviderID}\{DocumentCategory}\{DocumentType}
                Document providerDocument = new Document();
                providerDocument.DocumentTypeID = documentDTO.DocumentTypeID;
                providerDocument.FilePath = @"Documents\AHCP-" + documentDTO.ProviderID.ToString() + @"\" + docCategory + @"\" + docType;
                if (documentDTO.ApplicationRootFolder != null)
                {
                    // Create the folder to save provider documents
                    filePathToSave = documentDTO.ApplicationRootFolder + providerDocument.FilePath;
                    if (!Directory.Exists(filePathToSave))
                    {
                        Directory.CreateDirectory(filePathToSave);
                    }
                }

                //Provider provider = await providersRepository.FindAsync(documentDTO.ProviderID);
                Provider provider = providersRepository.Find(documentDTO.ProviderID);

                // Set the old provider documents to Inactive
                var documents = from document in provider.Documents
                                where document.DocumentTypeID == providerDocument.DocumentTypeID
                                select document;

                foreach (var item in documents)
                {
                    item.DocumentStatus = DocumentStatus.Inactive;
                }

                // Save the provider document information into DB
                string uniqueKey = AHC.UtilityService.UniqueKeyGenerator.GetUniqueKey();
                providerDocument.FileName = uniqueKey + "-" + documentDTO.FileName;
                providerDocument.Extension = Path.GetExtension(providerDocument.FileName);
                providerDocument.DocumentStatus = DocumentStatus.Active;
                provider.Documents.Add(providerDocument);
                providersRepository.Update(provider);
                //await providersRepository.SaveAsync();
                

                // Save the provider document
                
                using (var fileStream = System.IO.File.Create(filePathToSave + @"\" + providerDocument.FileName))
                {
                    documentDTO.InputStream.Seek(0, SeekOrigin.Begin);
                    //await documentDTO.InputStream.CopyToAsync(fileStream);
                    documentDTO.InputStream.CopyTo(fileStream);
                    documentDTO.InputStream.Close();
                    //fileStream.Close();
                }
                providersRepository.Save();
                scope.Complete();
                
                return true;
            }
        }

        
        public IEnumerable<Document> GetAllDocumentsByProvider(int providerID)
        {
            return providersRepository.GetAll() .Where(p => p.ProviderID == providerID).SelectMany(p => p.Documents);
            
        }

        public async Task<IEnumerable<DocumentCategory>> GetAllDocumentCategoryAsync()
        {
            return await documentsCategoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypeAsync()
        {
            return await documentsTypeRepository.GetAllAsync();
        }
    }
}
