using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Queue
{
    public class FacilityQueueModuleViewModel
    {
        public string QueueLabel { get; set; }
        public List<FacilityQueueViewModel> AuthorizationsList { get; set; }
        public QueueInfoViewModel QueueInfo { get; set; }
        public List<QueueSubTabViewModel> QueueSubTab { get; set; }
        public FacilityQueueModuleViewModel()
        {
            AuthorizationsList = new List<FacilityQueueViewModel>();
            QueueInfo = new QueueInfoViewModel();
          //QueueSubTab = new QueueSubTabViewModel();
        }
    }
}