using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class GroupViewModel
    {
        public int GroupID { get; set; }

        public string GroupName { get; set; }

        public ICollection<PlanCategoryViewModel> PlanCategories { get; set; } 

        public ICollection<PlanDetailViewModel> PlanDetails { get; set; }   
    }
}
