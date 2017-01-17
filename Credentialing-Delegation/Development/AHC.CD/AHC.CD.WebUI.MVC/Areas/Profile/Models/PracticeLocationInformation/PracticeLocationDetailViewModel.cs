using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticeLocationDetailViewModel
    {
        public int PracticeLocationDetailID { get; set; }
           
        public string IsPrimary { get; private set; }

        [Required(ErrorMessage="Please specify whether this is your primary practice location or not")]
        [Display(Name = "Is it your primary practice location?*")]
        public YesNoOption? PrimaryYesNoOption { get; set; }

        [Display(Name = "Do you practice exclusively within inpatient setting ?")]
        public YesNoOption? PracticeExclusivelyYesNoOption { get; set; }
                
        public string CurrentlyPracticingAtThisAddress { get; private set; }

        [Required(ErrorMessage="Please specify whether you are currently practicing at this address or not")]
        [Display(Name = "Currently Practicing At This Address? *")]
        public YesNoOption CurrentlyPracticingYesNoOption { get; set; }

        public string SendGeneralCorrespondence { get; private set; }

        //[Required(ErrorMessage="Please specify whether General Correspondence can be sent here or not")]
        [Display(Name="Send General Correspondence Here?")]
        public YesNoOption? GeneralCorrespondenceYesNoOption { get; set; }

        public string PrimaryTaxId { get; private set; }

        [Display(Name = "Select primary tax ID")]
        public PrimaryTaxId? PrimaryTax { get; set; }

        [Required(ErrorMessage = "Please enter Corporate or Practice Name")]
     //  [RegularExpression("([a-zA-Z0-9-,. ]+)", ErrorMessage = "Please enter valid Corporate or Practice Name.Only alpha-numerals, space, hyphen, comma and dot accepted")]
     //  [MaxLength(50, ErrorMessage = "Corporate or Practice Name must be between 2 and 50 characters")]
     //  [MinLength(2, ErrorMessage = "Corporate or Practice Name must be between 2 and 50 characters")]
        [Display(Name = "Corporate or Practice Name *")]
        public string PracticeLocationCorporateName { get; set; }

        [Required(ErrorMessage = "Please Select a Facility")]
        public int FacilityId { get; set; }

        [Display(Name = "Start Date")]
      //  [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Column(TypeName = "datetime2")]        
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage="Organization Name is required")]
        public int OrganizationId{get;set;}

        //[Required(ErrorMessage = "IPA is required")]
        public int? PracticingGroupId { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        public StatusType? StatusType { get; set; }

        public string UpdateHistory { get; set; }
    }
}