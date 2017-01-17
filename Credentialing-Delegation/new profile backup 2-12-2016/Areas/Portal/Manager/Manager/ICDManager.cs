using PortalTemplate.Areas.Portal.IManager.ICD;
using PortalTemplate.Areas.Portal.IServices.ICD;
using PortalTemplate.Areas.Portal.Services.ICD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Manager.Manager
{
    public class ICDManager : IICDManager
    {
        private readonly IICDService icdService;
        public ICDManager()
        {
            this.icdService = new ICDService();
        }


        public List<UM.Models.ViewModels.ICD.ICDViewModel> GetICDCodesByVersion(string version)
        {
            throw new NotImplementedException();
        }

        public List<UM.Models.ViewModels.ICD.ICDViewModel> GetICDCodesByLimit(string version, string limit)
        {
            throw new NotImplementedException();
        }

        public List<UM.Models.ViewModels.ICD.ICDViewModel> GetICDCodeByDescription(string version, string codeString)
        {
            throw new NotImplementedException();
        }

        public List<UM.Models.ViewModels.ICD.ICDViewModel> GetICDCodesByDescString(string version, string descString)
        {
            throw new NotImplementedException();
        }

        public List<UM.Models.ViewModels.ICD.ICDViewModel> GetAllIcdsByicdorcodeStringwithLimit(string version, string codeString, int limit)
        {
            try
            {
                if (codeString.Any(c => char.IsDigit(c)))
                {
                    return icdService.GetICDCodesByDescWithLimit(version, codeString, limit);
                }
                else
                {
                    return icdService.GetAllIcdsByicdorcodeStringwithLimit(version, codeString, limit);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<UM.Models.ViewModels.ICD.ICDViewModel> GetICDCodesByDescWithLimit(string version, string desc, int limit)
        {
            try
            {
                if (desc.Any(c => char.IsDigit(c)))
                {
                    return icdService.GetAllIcdsByicdorcodeStringwithLimit(version, desc, limit);
                }
                else
                {
                    return icdService.GetICDCodesByDescWithLimit(version,desc,limit);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }


        public List<UM.Models.ViewModels.ICD.ICDViewModel> GetICDCodesByDescString(string version, string desc, int limit)
        {
            throw new NotImplementedException();
        }
    }
}