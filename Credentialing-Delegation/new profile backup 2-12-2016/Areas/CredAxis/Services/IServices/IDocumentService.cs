using PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    interface IDocumentService
    {
        DoumentRepoMainViewModel GetDocRepoData();
    }
}
