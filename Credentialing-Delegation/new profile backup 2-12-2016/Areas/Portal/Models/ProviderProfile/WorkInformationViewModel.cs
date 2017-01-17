using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.ProviderProfile
{
    public class WorkInformationViewModel
    {
        //public WorkInformationViewModel()
        //{
        //    PlanName = new List<string>();
        //}
        [Display(Name = "FACILITY")]
        public string facility { get; set; }
        [Display(Name = "LOCATION TYPE")]
        public string locationtype { get; set; }
        [Display(Name = "GROUP NAME")]
        public string PhysicianGroupName { get; set; }
        [Display(Name = "GROUP TAX ID")]
        public string GroupTaxId { get; set; }
        [Display(Name = "PLAN")]
        public string plan { get; set; }
        [Display(Name = "SPECIALTY")]
        public string specialty { get; set; }

    }
}