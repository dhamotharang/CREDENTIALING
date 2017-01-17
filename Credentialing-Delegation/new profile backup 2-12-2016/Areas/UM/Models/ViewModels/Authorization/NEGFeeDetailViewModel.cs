using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class NEGFeeDetailViewModel
    {
        public NEGFeeDetailViewModel()
        {
            Medications = new List<CarveOutDetailViewModel>();
            SpecialtyBedMats = new List<CarveOutDetailViewModel>();
            WoundCares = new List<CarveOutDetailViewModel>();
            LifeVests = new List<CarveOutDetailViewModel>();
            Others = new List<CarveOutDetailViewModel>();

        }

        public int? NegFeeDetailID { get; set; }

        public string RoomType { get; set; }

        [Display(Name="COST")]
        public double? BaseCost { get; set; }

        public string Range { get; set; }

        public List<CarveOutDetailViewModel> Medications { get; set; }

        public List<CarveOutDetailViewModel> SpecialtyBedMats { get; set; }

        public List<CarveOutDetailViewModel> WoundCares { get; set; }

        public List<CarveOutDetailViewModel> LifeVests { get; set; }

        public List<CarveOutDetailViewModel> Others { get; set; }

        [Display(Name = "% OF DRG")]
        public decimal? DRGPercentage { get; set; }

        [Display(Name = "ALOS")]
        public string ALOS { get; set; }

        [Display(Name = "GMLOS")]
        public string GMLOS { get; set; }

        [Display(Name = "LOW TRIM")]
        public DateTime? LowTrimPoint { get; set; }

        [Display(Name = "TOTAL COST")]
        public double? TotalCost { get; set; }

        [Display(Name = "DRG/RATE")]
        public string DrugAndRate { get; set; }
    }
}
