using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Portal.IManager.ICD
{
    public interface IICDManager
    {
        List<ICDViewModel> GetICDCodesByVersion(string version);
        List<ICDViewModel> GetICDCodesByLimit(string version, string limit);
        List<ICDViewModel> GetICDCodeByDescription(string version, string codeString);
        List<ICDViewModel> GetICDCodesByDescString(string version, string descString);
        List<ICDViewModel> GetICDCodesByDescWithLimit(string version, string desc, int limit);
        List<ICDViewModel> GetAllIcdsByicdorcodeStringwithLimit(string version, string codeString, int limit);
    }
}
