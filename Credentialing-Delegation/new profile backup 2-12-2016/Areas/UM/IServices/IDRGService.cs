using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IDRGService
    {
        List<DRGCodeViewModel> GetAllDRGCodesByCode(string code, int limit);
        List<DRGCodeViewModel> GetAllDRGCodesByDesc(string desc, int limit);
    }
}
