using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Facility
{
    public class FacilitityGeneralInfoViewModel
    {
        public int MasterEmployeeID { get; set; }

        public int PracticeLocationDetailID { get; set; }
      

        //[Display(Name = "Last Name *")]
        //public string LastName { get; set; }

        //[Display(Name = "MiddleName")]
        //public string MiddleName { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Practice Name")]
        public string CorporateName { get; set; }

       
        [Display(Name = "Building")]
        public string Building { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        
        [Display(Name = "Country ")]
        public string Country { get; set; }

       
        [Display(Name = "State ")]
        public string State { get; set; }

      
        [Display(Name = "County")]
        public string County { get; set; }

        
        [Display(Name = "City ")]
        public string City { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "P.O Box ")]
        public string POBoxAddress { get; set; }

        
        [Display(Name = "Telephone")]
        public string Telephone { get; set; }

        [Display(Name = "CountryCodeTelephone")]
        public string CountryCodeTelephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }
 
        [Display(Name = "E-Mail")]
        public string EmailAddress { get; set; }

       
    }
}