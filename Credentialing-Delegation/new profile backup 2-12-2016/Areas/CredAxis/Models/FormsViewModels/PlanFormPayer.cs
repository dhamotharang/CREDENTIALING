using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.FormsViewModels
{
    public class PlanFormPayer
    {
        [Key]
        public int PlanFormPayerID { get; set; }

        [Display(Name = "Payer")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Payer { get; set; }
    }
}