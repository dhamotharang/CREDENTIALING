using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.CredAxisProduct.Models.ProviderProfileViewModel.WorkHistoryViewModel
{
    public class ProfessionalWorkExperienceViewModel
    {
        [Key]
        public int? ID { get; set; }

        [Display(Name = "EMPLOYER NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EmployerInformation { get; set; }



        [Display(Name = "STREET/P. O. BOX NO")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Street { get; set; }

        [Display(Name = "SUITE/BUILDING ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SuiteBuilding { get; set; }

        [Display(Name = "CITY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "STATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "ZIP CODE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "COUNTRY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Country { get; set; }

        [Display(Name = "COUNTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "PHONE NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "FAX NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FaxNumber { get; set; }

        [Display(Name = "SUPERVISOR NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SupervisorName { get; set; }

        [Display(Name = "JOB TITLE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string JobTitle { get; set; }

        [Display(Name = "EMAIL")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "START DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StartDate { get; set; }

        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "DEPARTMENT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Department { get; set; }

        [Display(Name = "PROVIDER TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderType { get; set; }

        [Display(Name = "CAN CONTACT EMPLOYER OPTION ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsAbleToContactEMployer { get; set; }
    }
}