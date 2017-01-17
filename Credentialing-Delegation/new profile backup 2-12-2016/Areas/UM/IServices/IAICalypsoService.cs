using PortalTemplate.Areas.UM.Models.ViewModels.CalypsoAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IAICalypsoService
    {
        Task<AIListViewModel> CheckCPTDuplicacy(AICalypsoInputViewModel input);
    }
}