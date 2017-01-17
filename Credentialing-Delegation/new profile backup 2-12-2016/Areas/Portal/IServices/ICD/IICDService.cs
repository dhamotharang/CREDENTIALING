using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalTemplate.Areas.UM.Models.ViewModels.ICD;

namespace PortalTemplate.Areas.Portal.IServices.ICD
{
    public interface IICDService
    {
        List<ICDViewModel> GetICDCodesByVersion(string version);
        List<ICDViewModel> GetICDCodesByLimit(string version, string limit);
        List<ICDViewModel> GetICDCodeByDescription(string version, string codeString);
        List<ICDViewModel> GetICDCodesByDescString(string version, string descString);
        List<ICDViewModel> GetICDCodesByDescWithLimit(string version, string descString, int limit);
        List<ICDViewModel> GetAllIcdsByicdorcodeStringwithLimit(string version, string codeString, int limit);
    }
}
