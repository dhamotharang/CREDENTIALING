using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.IManager.CPT
{
    public interface ICPTManager
    {
        List<CPTViewModel> GetAllCPTCodes();
        List<CPTViewModel> GetCPTCodeByDescription(string descString, int limit);
        List<CPTViewModel> GetAllCPTCodesByCodeString(string codeString, int limit);
    }
}