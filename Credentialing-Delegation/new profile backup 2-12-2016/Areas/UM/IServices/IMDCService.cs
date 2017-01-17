using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IMDCService
    {
        List<MDCCodeViewModel> GetAllMDCCodesByCode(string code, int limit);
        List<MDCCodeViewModel> GetAllMDCCodesByDesc(string desc, int limit);
    }
}
