using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.EducationViewModel
{
    public class GraduateSchoolViewModel
    {
        [Key]
        public int? Id { get; set; }

        [Display(Name = "US GRADUATE ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsUSGraduate { get; set; }

        [Display(Name = "DID YOU COMPLETE YOUR EDUCATION AT THIS SCHOOL ? ")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsEducationSchool { get; set; }


        [Display(Name = "GRADUATE TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GraduationType { get; set; }

        [Display(Name = "DEGREE AWARDED")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Degree { get; set; }

        [Display(Name = "SCHOOL NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SchoolName { get; set; }

        [Display(Name = "LOCATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Location { get; set; }

        [Display(Name = "SUITE/BUILDING")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SuitNumber { get; set; }

        [Display(Name = "STREET /P.O BOX NO")]
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

        [Display(Name = "START DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StartDate { get; set; }

        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "DOCUMENT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Document { get; set; }

    }
}