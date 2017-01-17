using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class PrimaryEncounterViewModel
    {

        public string RenderingProviderNPI { get; set; }

        public string RenderingProviderFirstName { get; set; }

        public string RenderingProviderMiddleName { get; set; }

        public string RenderingProviderLastOrOrganizationName { get; set; }

        public string RenderingProviderSpeciality { get; set; }

        public string RenderingProviderTaxonomy { get; set; }


        public string SubscriberID { get; set; }

        public string PatientLastOrOrganizationName { get; set; }

        public string PatientMiddleName { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientGender { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? PatientBirthDate { get; set; }

        public string PatientFirstAddress { get; set; }

        public string PatientSecondAddress { get; set; }

        public string PatientCity { get; set; }

        public string PatientState { get; set; }

        public string PatientZip { get; set; }

        public string PayerName { get; set; }



        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DOSFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DOSTo { get; set; }

        public string PlaceOfService { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? NextAppointmentDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? CheckInTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? CheckOutTime { get; set; }


        public string VisitLength { get; set; }

        public string VisitReason { get; set; }

        public string EncounterId { get; set; }

        public string EncounterType { get; set; }

        public string EncounterStatus { get; set; }

        public string EncounterNotes { get; set; }


        public List<DocumentDetailsViewModel> DocumentDetails { get; set; }
    }
}