using AHC.CD.Business.DTO;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    public interface IDocumentsManager
    {
        bool SaveOrUpdateProviderDocument(DocumentDTO documentDTO);
        IEnumerable<Document> GetAllDocumentsByProvider(int providerID);
        Task<IEnumerable<DocumentCategory>> GetAllDocumentCategoryAsync();
        Task<IEnumerable<DocumentType>> GetAllDocumentTypeAsync();
    }
}
