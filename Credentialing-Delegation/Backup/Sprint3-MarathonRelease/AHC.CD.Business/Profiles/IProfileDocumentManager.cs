using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IProfileDocumentManager
    {
        string AddDocumentInPath(DocumentDTO document);
        string AddUpdateDocument(int profileId, DocumentDTO document);
        string AddUpdateDocumentInformation(int profileId, DocumentDTO document);
        string AddDocument(int profileId, DocumentDTO document);
    }
}
