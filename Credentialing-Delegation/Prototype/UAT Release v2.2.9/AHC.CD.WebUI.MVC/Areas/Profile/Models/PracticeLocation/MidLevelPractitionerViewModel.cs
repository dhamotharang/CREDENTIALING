using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class MidLevelPractitionerViewModel
    {
        public int MidLevelPractitionerID { get; set; }

        [Display(Name = "Practitioner First Name ")]
        public string FirstName { get; set; }

        [Display(Name = "Practitioner Middle Name ")]
        public string MiddleName { get; set; }

        [Display(Name = "Practitioner Last Name ")]
        public string LastName { get; set; }

        [Display(Name = "Practitioner License/ Certificate Number")]
        public string LicenceNumber { get; set; }

        [Display(Name = "Practitioner State")]
        public string State { get; set; }

        [Display(Name = "Practitioner Type")]
        public string PractitionerType { get; set; }

        [Required]
        [Display(Name = "Do Mid-Level Practitioners (Nurse Practitioners, Physician Assistants, etc.) Care For Patients In Your Practice? *")]
        public bool MidLevelPractitionersCareForPatients { get; set; }
    }
}