using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.UM.Models.ViewModels.Queue;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IQueueServices
    {
        QueueModuleViewModel GetAuthList(string QueueType, string QueueSubTab, string UserID = null,string RequestType ="", int? StartIndex =null, int? DataLength = null, List<string> filterData = null, string from = null, string to = null);
        FacilityQueueModuleViewModel GetFacilityAuthList(string QueueType, string QueueSubTab, string UserID = null,string RequestType ="", int? StartIndex =null, int? DataLength = null, List<string> filterData = null, string from = null, string to = null);
        

    }
}