using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Queue
{
    public class QueueModuleViewModel
    {
        public string QueueLabel { get; set; }
        public List<QueueViewModel> AuthorizationsList { get; set; }
        public QueueInfoViewModel QueueInfo { get; set; }
        public List<QueueSubTabViewModel> QueueSubTab { get; set; }
        public QueueModuleViewModel()
        {
            AuthorizationsList = new List<QueueViewModel>();
            QueueInfo =new QueueInfoViewModel();
            QueueSubTab = new List<QueueSubTabViewModel>();
        }
    }
}