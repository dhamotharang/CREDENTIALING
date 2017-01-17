using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.Queue
{
    public class PortalQueueViewModel
    {
        public string QueueLabel { get; set; }
        public List<QueueViewModel> AuthorizationsList { get; set; }
        public QueueCountViewModel AuthorizationsCount { get; set; }
        public QueueSubTabViewModel QueueSubTab { get; set; }
        public PortalQueueViewModel()
        {
            AuthorizationsList = new List<QueueViewModel>();
            AuthorizationsCount = new QueueCountViewModel();
            QueueSubTab = new QueueSubTabViewModel();
        }
    }
}