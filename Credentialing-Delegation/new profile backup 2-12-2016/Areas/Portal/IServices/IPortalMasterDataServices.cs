using PortalTemplate.Areas.Portal.Models.PriorAuthMasterDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Portal.IServices
{
    public interface IPortalMasterDataServices
    {
        //It will call cms services in future
         List<DocumentNameViewModel> GetDocumentName();
         List<DocumentTypeViewModel> GetDocumentType();
         List<UMServiceGroupViewModel> GetUMServiceGroup();
         List<RangeViewModel> GetRange();
         List<DisciplineViewModel> GetDiscipline();
      }
}