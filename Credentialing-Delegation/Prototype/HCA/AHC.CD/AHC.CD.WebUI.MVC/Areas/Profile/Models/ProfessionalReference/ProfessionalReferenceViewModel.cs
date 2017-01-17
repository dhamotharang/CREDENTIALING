using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference
{
    public class ProfessionalReferenceViewModel
    {
        public int ProfessionalRefereneInfoID { get; set; }
        [Required]
        [Display(Name="Title *")]
        public string Title { get; set; }

        public TitleType ProviderType { get; set; }

        [Required]
        [Display(Name= "First Name *")]
        public string FirstName { get; set; }

        [Display(Name="Middle Name ")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name="Last Name *")]
        public string LastName { get; set; }


        [Required]
        public string Degree { get; set; }

        [Display(Name = "Specialty")]
        public Speciality Speciality { get; set; }

        public string Relationship { get; set; }

        [Display(Name="Board Certified")]
        public bool IsBoardCerified { get; set; }

        public string Email { get; set; }
        

        [Required]
        [Display(Name="Unit Number *")]
        public string UnitNumber { get; set; }

        [Required]
        [Display(Name="Suite/Building *")]
        public string Building { get; set; }

        [Required]
        [Display(Name="Street *")]
        public string Street { get; set; }

        [Required]
        [Display(Name="State *")]
        public string State { get; set; }

        [Required]
        [Display(Name="Country *")]
        public string Country { get; set; }

        [Required]
        [Display(Name="County *")]
        public string County { get; set; }

        [Required]
        [Display(Name="City *")]
        public string City { get; set; }

        [Required]
        [Display(Name="Zip Code *")]
        public string Zipcode { get; set; }

        [Required]
        [Display(Name ="Telephone *")]
        public string Telephone { get; set; }

        [Required]
        [Display(Name="Fax *")]
        public string Fax { get; set; }
    }
}