using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DocumentWriter
{
    public interface IDocumentsManager
    {
        void DeleteFile(string fullDocPath);
        string SaveDocument(DocumentDTO document, string rootLocation);
        string SaveDocumentWithoutUniqueKey(DocumentDTO document, string rootLocation);
    }
}
