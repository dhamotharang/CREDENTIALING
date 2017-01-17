using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Queue
{
    public class QueueInfoViewModel
    {
        public string QueueButton { get; set; }

        [Display(Name = "STANDARD")]
        public string StandardCount { get; set; }

        [Display(Name = "EXPEDITED")]
        public string ExpeditedCount { get; set; }

        [Display(Name = "ALL")]
        public string TotalCount { get; set; }
    }
}