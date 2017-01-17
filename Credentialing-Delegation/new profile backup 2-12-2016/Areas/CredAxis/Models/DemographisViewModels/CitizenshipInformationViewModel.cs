using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class CitizenshipInformationViewModel
    {
        [Key]
        public int? CitizenshipInformationId { get; set; }

        [Display(Name = "ARE YOU CITIZEN OF US")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool CitizenOfUS { get; set; }


        [Display(Name = "ARE YOU AUTHORIZED TO WORK IN US?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool AthorizedToWorkInUS { get; set; }

        [Display(Name = "VISA")]
        [DisplayFormat(NullDisplayText = "-")]
        public string VisaNumber { get; set; }

        [Display(Name = "VISA TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string VisaType { get; set; }

        [Display(Name = "VISA STATUS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string VisaStatus { get; set; }

        [Display(Name = "VISA SPONSOR")]
        [DisplayFormat(NullDisplayText = "-")]
        public string VisaSponsor { get; set; }

        [Display(Name = "VISA EXP.")]
        [DisplayFormat(NullDisplayText = "-")]
        public string VisaExpiration { get; set; }

        [Display(Name = "GREEN CARD #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GreenCardNumber { get; set; }

        [Display(Name = "NATIONAL ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NationalId { get; set; }

        [Display(Name = "COUNTRY OF ISSUE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CountryOfIssue { get; set; }

    }
}