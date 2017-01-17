using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.Queue
{
    public class QueueCountViewModel
    {
        public int StandardCount { get; set; }
        public int ExpeditedCount { get; set; }
        public int TotalCount { get; set; }
        public int CTFUCount { get; set; }
        public int CompletedCount { get; set; }
        public int WorkCount { get; set; }
        public int SubmittedCount { get; set; }

        public QueueCountViewModel()
        {

        }
        public QueueCountViewModel(int initCount)
        {
            StandardCount = initCount;
            ExpeditedCount = initCount;
            TotalCount = initCount;
            CTFUCount = initCount;
            CompletedCount = initCount;
            WorkCount = initCount;
            SubmittedCount = initCount;
        }
    }
}