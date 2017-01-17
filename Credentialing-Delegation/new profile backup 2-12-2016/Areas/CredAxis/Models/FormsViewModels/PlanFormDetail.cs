using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.FormsViewModels
{
    public class PlanFormDetail
    {
        [Key]
        public int PlanFormDetailID { get; set; }

        [Display(Name = "Plan Form Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanFormName { get; set; }       
                
        public PlanFormPayer PlanFormPayer { get; set; }

        public PlanFormRegion PlanFormRegion { get; set; }
    }
}