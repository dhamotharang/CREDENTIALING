using PortalTemplate.Areas.Portal.IManager.CPT;
using PortalTemplate.Areas.Portal.IServices.CPT;
using PortalTemplate.Areas.Portal.Services.CPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Manager.Manager
{
    public class CPTManager :ICPTManager
    {
        private readonly ICPTService cptManager;
        public CPTManager()
        {
            this.cptManager = new CPTService();
        }
        public List<UM.Models.ViewModels.CPT.CPTViewModel> GetAllCPTCodes()
        {
            throw new NotImplementedException();
        }

        public List<UM.Models.ViewModels.CPT.CPTViewModel> GetCPTCodeByDescription(string descString, int limit)
        {
            try
            {
                if (descString.Any(c => char.IsDigit(c)))
                {
                    return cptManager.GetCPTCodeByDescription(descString, limit);
                }
                else
                {
                    return cptManager.GetAllCPTCodesByCodeString(descString, limit);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<UM.Models.ViewModels.CPT.CPTViewModel> GetAllCPTCodesByCodeString(string codeString, int limit)
        {
            try
            {
                if (codeString.Any(c => char.IsDigit(c)))
                {
                    return cptManager.GetCPTCodeByDescription(codeString, limit);
                }
                else
                {
                    return cptManager.GetAllCPTCodesByCodeString(codeString, limit);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}