using Newtonsoft.Json;
using PortalTemplate.Areas.UM.CustomHelpers;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.CalypsoAI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.UM.Services.CalypsoAI
{
    public class CalypsoAIService : IAICalypsoService
    {
        private readonly string baseURL;
        private readonly ServiceUtility serviceUtility;
        public CalypsoAIService()
        {
            this.baseURL = ConfigurationManager.AppSettings["UMService"].ToString();
            this.serviceUtility = new ServiceUtility();
        }

        public async Task<AIListViewModel> CheckCPTDuplicacy(AICalypsoInputViewModel input)
        {
            AIListViewModel AIList = new AIListViewModel();
            Task<string> CalypsoAIList = Task.Run(async () =>
            {
                try
                {
                    string msg = await serviceUtility.PostDataToService(baseURL, "api/Member/CheckCPTDuplicacy", input);
                    return msg;
                }
                catch (Exception )
                {
                    
                    throw;
                }
            });
            await Task.WhenAll(CalypsoAIList);
            if (CalypsoAIList.Result != null)
            {
                AIList = JsonConvert.DeserializeObject<AIListViewModel>(CalypsoAIList.Result);
            }
            return AIList;
        }
    }
}