using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class PlanDetailViewModel
    {
        public int PlanDetailID { get; set; }

        public string Title { get; set; }

        public string Code { get; set; }

        public ICollection<GroupViewModel> Groups { get; set; }

        public PlanDataViewModel PlanData { get; set; } 
    }
}
