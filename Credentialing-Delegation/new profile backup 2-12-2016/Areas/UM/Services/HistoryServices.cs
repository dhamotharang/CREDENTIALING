using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.History;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services
{
    public class HistoryServices : IAuthHistoryServices
    {
        public List<MemberHistoriesViewModel> GetUMHistory(string SubscriberID)
        {
            return GetHistoryList();
        }
        private List<MemberHistoriesViewModel> GetHistoryList()
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/History/History.txt");
            string json = System.IO.File.ReadAllText(file);
            List<MemberHistoriesViewModel> HistoryModel = new List<MemberHistoriesViewModel>();
            HistoryModel = JsonConvert.DeserializeObject<List<MemberHistoriesViewModel>>(json);
            return HistoryModel;
        }      
    }
}