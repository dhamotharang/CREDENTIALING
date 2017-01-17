using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticeLocationDetailViewModel
    {
        public int PracticeLocationDetailID { get; set; }
   
        
        public string IsPrimary { get; private set; }

        [Required]
        [Display(Name = "Is it your primary practice location? *")]
        public YesNoOption PrimaryYesNoOption { get; set; }

        [Display(Name = "Do you practice exclusively within inpatient setting ?")]
        public YesNoOption? PracticeExclusivelyYesNoOption { get; set; }
                
        public string CurrentlyPracticingAtThisAddress { get; private set; }

        [Required]
        [Display(Name = "Currently Practicing At This Address? *")]
        public YesNoOption CurrentlyPracticingYesNoOption { get; set; }

        public string SendGeneralCorrespondence { get; private set; }

        [Required]
        [Display(Name="Send General Correspondence Here? *")]
        public YesNoOption? GeneralCorrespondenceYesNoOption { get; set; }

        public string PrimaryTaxId { get; private set; }

        [Display(Name = "Select primary tax ID")]
        public PrimaryTaxId? PrimaryTax { get; set; }
        
        [Required]
        [Display(Name = "Corporate or Practice Name *")]
        [RegularExpression("([a-zA-Z0-9-,. ]+)", ErrorMessage = "Please enter valid Corporate or Practice Name.Only alpha-numerals, space, hyphen accepted")]
        public string PracticeLocationCorporateName { get; set; }

        [Required]
        public int FacilityId { get; set; }

        [Display(Name = "Start Date *")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage="Organization Name is required")]
        public int OrganizationId{get;set;}

        [Required(ErrorMessage = "IPA is required")]
        public int PracticingGroupId { get; set; }

        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
    }
}