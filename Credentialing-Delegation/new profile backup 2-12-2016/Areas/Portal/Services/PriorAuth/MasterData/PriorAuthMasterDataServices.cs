using PortalTemplate.Areas.Portal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Portal.Models.PriorAuthMasterDataEntities;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Portal.Services.PriorAuth.MasterData
{
    public class PriorAuthMasterDataServices : IPortalMasterDataServices
    {
        public List<DocumentNameViewModel> GetDocumentName()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DocumentNameViewModel> DocumentName = serial.Deserialize<List<DocumentNameViewModel>>(GetJSON("MasterDocumentNames.js"));
            return DocumentName;
        }

        public List<DocumentTypeViewModel> GetDocumentType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DocumentTypeViewModel> DocumentType = serial.Deserialize<List<DocumentTypeViewModel>>(GetJSON("MasterDocumentTypes.js"));
            return DocumentType;
        }

        public List<UMServiceGroupViewModel> GetUMServiceGroup()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<UMServiceGroupViewModel> UMServiceGroup = serial.Deserialize<List<UMServiceGroupViewModel>>(GetJSON("MasterUMSvcGroups.js"));
            return UMServiceGroup;
        }

        public List<RangeViewModel> GetRange()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<RangeViewModel> range = serial.Deserialize<List<RangeViewModel>>(GetJSON("MasterRangeTypes.js"));
            return range;
        }

        public List<DisciplineViewModel> GetDiscipline()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DisciplineViewModel> discipline = serial.Deserialize<List<DisciplineViewModel>>(GetJSON("MasterDisciplines.js"));
            return discipline;
        }

        private string GetJSON(string SourceFile)
        {
            string file = HostingEnvironment.MapPath(GetResourceLink(SourceFile));
            string json = System.IO.File.ReadAllText(file);
            return json;
        }

        private string GetResourceLink(string SourceFileName)
        {
            return "~/Areas/Portal/Resources/PriorAuthMasterData/" + SourceFileName;
        }
    }
}

