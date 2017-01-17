using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.Queue
{
    public class QueueSubTabViewModel
    {
        public bool IsSubTabs { get; set; }
        public bool IsWorkTab { get; set; }
        public bool IsSubmittedTab { get; set; }
        public bool IsCompletedTab { get; set; }
        public bool IsCTFUTab { get; set; }
        [Display(Name = "FROM")]
        public DateTime FilterFromDate { get; set; }

        [Display(Name = "TO")]
        public DateTime FilterToDate { get; set; }

        public QueueSubTabViewModel()
        {

        }
        public QueueSubTabViewModel(bool Flag)
        {
            IsSubTabs = Flag;
            IsWorkTab = Flag;
            IsSubmittedTab = Flag;
            IsCompletedTab = Flag;
            IsCTFUTab = Flag;
        }
    }
}