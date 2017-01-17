using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.EducationViewModel
{
    public class ResidencyViewModel
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = " PREFERENCE TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PreferenceType { get; set; }


        [Display(Name = "PROGRAM TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProgramType { get; set; }

        [Display(Name = "SPECIALTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Speciality { get; set; }

        [Display(Name = "DIRECTOR NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DirectorName { get; set; }

        [Display(Name = "DEPARTMENT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Department { get; set; }

        [Display(Name = "START DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StartDate { get; set; }

        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "DOCUMENT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Document { get; set; }

        [Display(Name = "DID YOU COMPLETE YOUR TRAINING AT THIS SCHOOL ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsTrainingSchool { get; set; }

        [Display(Name = "USE EXISTING SCHOOL DETAILS ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsExistingSchoolDetail { get; set; }

        [Display(Name = "SCHOOL NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SchoolName { get; set; }

        [Display(Name = "AFF UNIVERSITY/HOSPITAL")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AffiliatedHospital { get; set; }

        [Display(Name = "LOCATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Location { get; set; }

        [Display(Name = "SUITE/BUILDING")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SuitNumber { get; set; }

        [Display(Name = "STREET NO")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StreetNumber { get; set; }

        [Display(Name = "CITY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "STATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "COUNTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "COUNTRY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Country { get; set; }

        [Display(Name = "ZIP CODE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "EMAIL")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "TELEPHONE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TelephoneNumber { get; set; }

        [Display(Name = "FAX NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FaxNumber { get; set; }

    }
}