using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.ProfileManagement
{
    public class GeneralInformationViewModel
    {
        [Display(Name = "Salutation")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Prefix { get; set; }

        [Display(Name = "Suffix")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Suffix { get; set; }

        [Display(Name = "First Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MiddleInitial { get; set; }

        [Display(Name = "Last Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "DOB")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DOB { get; set; }

        [Display(Name = "Gender")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "Religion")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Religion { get; set; }

        [Display(Name = "Ethnicity")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Ethnicity { get; set; }

        [Display(Name = "SSN")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SSN { get; set; }

        [Display(Name = "Race")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Race { get; set; }

        [Display(Name = "Marital Status")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Status")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        [Display(Name = "Created By Email")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CreatedByEmail { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CreatedDate { get; set; }

        [Display(Name = "Last Modified By Email")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastModifiedByEmail { get; set; }

        [Display(Name = "Last Modified Date")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastModifiedDate { get; set; }


        // Apart from PersonInformationViewModel Properties
        [Display(Name = "DOD")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DOD { get; set; }

        [Display(Name = "Place Of Birth")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Policy Number")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PolicyNumber { get; set; }

        [Display(Name = "Last Modified Date")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PatientAccountNumber { get; set; }

        [Display(Name = "Employment Status")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EmploymentStatus { get; set; }

        [Display(Name = "Family Link ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FamilyLinkID { get; set; }

        [Display(Name = "Preferred Language")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PreferredLanguage { get; set; }

        [Display(Name = "Is Citizen Of US")]
        [DisplayFormat(NullDisplayText = "-")]
        public string USCitizen { get; set; }

        [Display(Name = "HICN")]
        [DisplayFormat(NullDisplayText = "-")]
        public string HICN { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MedicareEffectiveDate { get; set; }

        [Display(Name = "Is Entitled To")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EntitledTo { get; set; }
    }
}