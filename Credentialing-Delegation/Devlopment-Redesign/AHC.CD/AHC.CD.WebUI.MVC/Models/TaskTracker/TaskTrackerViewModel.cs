using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.TaskTracker
{
    public class TaskTrackerViewModel
    {     

        public int TaskTrackerId { get; set; }
        
        
        public int? ProfileID { get; set; }
        //[Required(ErrorMessage = "Please select Provider Name")]
        public string ProviderName { get; set; }

        //[Required(ErrorMessage = "Please select Sub Section Name")]
        public string SubSectionName { get; set; }

        [Required(ErrorMessage = "Please select Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter next Followup Date")]
        public DateTime NextFollowUpDate { get; set; }

        //[Required(ErrorMessage = "Please enter next Mode Of FollowUp")]
        public string ModeOfFollowUp { get; set; }

        //[Required(ErrorMessage = "Please select Assigned To User")]
        public string AssignedToAuthId { get; set; }

        //[Required(ErrorMessage = "Please select Insurance Company")]
        public string InsuranceCompanyName { get; set; }

        public string AssignedByAuthId { get; set; }

        //[Required(ErrorMessage = "Please enter hospital name")]
        public int? HospitalID { get; set; }

        [Required(ErrorMessage = "Please enter Notes")]
        //[StringLength(6, MinimumLength = 3)]
        public string Notes { get; set; }

        public StatusType? StatusType{get;set;}        

        
    }
}