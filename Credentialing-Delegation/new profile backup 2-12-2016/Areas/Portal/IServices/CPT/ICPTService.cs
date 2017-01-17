using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;


namespace PortalTemplate.Areas.Portal.IServices.CPT
{
    public interface ICPTService
    {
        List<CPTViewModel> GetAllCPTCodes();
        List<CPTViewModel> GetCPTCodeByDescription(string descString,int limit);
        List<CPTViewModel> GetAllCPTCodesByCodeString(string codeString,int limit);
    }
}