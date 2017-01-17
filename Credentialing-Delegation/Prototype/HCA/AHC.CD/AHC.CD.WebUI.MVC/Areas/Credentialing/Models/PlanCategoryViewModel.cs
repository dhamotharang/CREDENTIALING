using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class PlanCategoryViewModel
    {
        public int PlanCategoryID { get; set; }

        public string PlanCategoryName { get; set; }

        public ICollection<GroupViewModel> Groups { get; set; }
    }
}
