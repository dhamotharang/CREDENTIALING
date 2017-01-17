using PortalTemplate.Areas.UM.Models.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IAuthHistoryServices
    {
        List<MemberHistoriesViewModel> GetUMHistory(string SubscriberID);
    }
}