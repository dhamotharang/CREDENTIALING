using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Portal.Models.PriorAuth.Queue;

namespace PortalTemplate.Areas.Portal.IServices.Queues
{
    public interface IQueueServices
    {
        PortalQueueViewModel GetAuthList(string QueueType, string QueueSubTab, string UserID = null, string RequestType = "", int? StartIndex = null, int? DataLength = null, List<string> filterData = null, string from = null, string to = null);
    }
}