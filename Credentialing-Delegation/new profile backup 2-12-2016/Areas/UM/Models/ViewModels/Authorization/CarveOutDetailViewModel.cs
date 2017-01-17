using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class CarveOutDetailViewModel
    {
        public int CarveOutDetailID { get; set; }

        [Display(Name = "CARVE OUT(S)")]
        public string CarveOutType { get; set; }

        public bool IsActive { get; set; }

        public string Reason { get; set; }

        public double? Cost { get; set; }

        public int? Dose { get; set; }

         [Display(Name = "RANGE")]
        public string Range1 { get; set; }

        public int Unit { get; set; }

        [Display(Name = "RANGE")]
        public string Range2 { get; set; }

        [Display(Name = "FROM DATE")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "TO DATE")]
        public DateTime? ToDate { get; set; }

        [Display(Name = "TOTAL COST")]
        public double? TotalCost { get; set; }

        public int? NegFeeDetailID { get; set; }
        
        
    }
}
