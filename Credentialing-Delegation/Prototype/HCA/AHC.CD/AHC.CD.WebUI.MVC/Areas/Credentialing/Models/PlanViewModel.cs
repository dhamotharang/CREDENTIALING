using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class PlanViewModel
    {
        public int PlanID { get; set; }

        public string PlanName { get; set; }

        public string PlanCode { get; set; }

        public LocationViewModel Location { get; set; }

        public ContactPersonViewModel ContactPerson { get; set; }

        public ICollection<PlanCategoryViewModel> PlanCategories { get; set; }
    }
}