using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.ProviderProfile
{
    public class PersonalInformationViewModel
    {
        public PersonalInformationViewModel()
        {
            FacilityName = new List<string>();
            Specialty = new List<string>();
        }
        [Display(Name = "NPI")]
        public string NPI { get; set; }
        [Display(Name = "PROVIDER TYPE")]
        public string ProviderType { get; set; }
        [Display(Name = "FIRST NAME")]
        public string FirstName { get; set; }
        [Display(Name = "MIDDLE NAME")]
        public string MiddleName { get; set; }
        [Display(Name = "LAST NAME")]
        public string LastName { get; set; }
        [Display(Name = "GENDER")]
        public string Gender { get; set; }
        [Display(Name = "FACILITY NAME")]
        public List<string> FacilityName { get; set; }
        [Display(Name = "SPECIALTY")]
        public List<string> Specialty { get; set; }
        [Display(Name = "REQUESTED BY")]
        public string RequestedBy { get; set; }
        [Display(Name = "REQUESTED FROM")]
        public string RequestedFrom { get; set; }
        [Display(Name = "IPA PARTICIPATION")]
        public string NetworkParticipation { get; set; }

        [Display(Name = "PHONE NUMBER")]
        public string phonenumber { get; set; }

        [Display(Name = "EMAIL")]
        public string email { get; set; }

        [Display(Name = "INDIVIDUAL TAX ID")]
        public string TAXID { get; set; }

    }
}
