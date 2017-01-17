using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.CalypsoAI
{
    public class AICalypsoOutputViewModel
    {
        [Display(Name = "REF NO")]
        public string ReferenceNumber { get; set; }
        [Display(Name = "PROC")]
        public string CPTCode { get; set; }
        [Display(Name = "PROC DESC")]
        public string CPTDescription { get; set; }
        [Display(Name = "SVC PROVIDER")]
        public string SVCorAttendingProvider { get; set; }
        public int? Units { get; set; }
        public DateTime? DOS { get; set; }
        [Display(Name = "Admission Date")]
        public DateTime? AdmissionDate { get; set; }
        [Display(Name = "Discharge Date")]
        public DateTime? DischargeDate { get; set; }
        public string Facility { get; set; }
        public string Specialty { get; set; } 
    }
}