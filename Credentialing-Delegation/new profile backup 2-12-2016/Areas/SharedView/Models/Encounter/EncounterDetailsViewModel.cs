using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.SharedView.Models.Encounter
{
    public class EncounterDetailsViewModel
    {
        public EncounterDetailsViewModel()
        {
            this.DocumentDetails = new DocumentListViewModel();
        }
        [DisplayName("Provider NPI")]
        public string RenderingProviderNPI { get; set; }

        [DisplayName("Provider First Name")]
        public string RenderingProviderFirstName { get; set; }

        public string RenderingProviderMiddleName { get; set; }
        [DisplayName("Provider Last Name")]
        public string RenderingProviderLastOrOrganizationName { get; set; }
        [DisplayName("Specialty")]
        public string RenderingProviderSpeciality { get; set; }
        [DisplayName("Taxonomy Code")]
        public string RenderingProviderTaxonomy { get; set; }

        [DisplayName("Subscriber Id")]
        public string SubscriberID { get; set; }
        [DisplayName("Member Last Name")]
        public string PatientLastOrOrganizationName { get; set; }

        public string PatientMiddleName { get; set; }
        [DisplayName("Member First Name")]
        public string PatientFirstName { get; set; }
        [DisplayName("Gender")]
        public string PatientGender { get; set; }

        [DisplayName("DOB")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PatientBirthDate { get; set; }

        public string PatientFirstAddress { get; set; }

        public string PatientSecondAddress { get; set; }

        public string PatientCity { get; set; }

        public string PatientState { get; set; }

        public string PatientZip { get; set; }

        [DisplayName("Plan Name")]
        public string PlanName { get; set; }

        [DisplayName("Referring Provider")]
        public string ReferringProvider { get; set; }
         [DisplayName("Billing Provider")]
        public string BillingProvider { get; set; }
        [DisplayName("Service Facility")]
        public string ServiceFacility { get; set; }
          [DisplayName("DOS From")]
          [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DOSFrom { get; set; }
          [DisplayName("DOS To")]
          [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DOSTo { get; set; }
          [DisplayName("Place of Service")]
        public string PlaceOfService { get; set; }
          [DisplayName("Next Appointment Date")]
          [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NextAppointmentDate { get; set; }

          [DisplayName("Check In Time")]
       
        public string CheckInTime { get; set; }
          [DisplayName("Check Out Time")]
       
        public string CheckOutTime { get; set; }

          [DisplayName("Visit Length")]
        public string VisitLength { get; set; }
          [DisplayName("Visit Reason")]
        public string VisitReason { get; set; }
      
        public string EncounterId { get; set; }
          [DisplayName("Encounter Type")]
        public string EncounterType { get; set; }
          [DisplayName("Status")]
        public string EncounterStatus { get; set; }
          //[DisplayName("Notes")]
        //public string EncounterNotes { get; set; }
        public DocumentListViewModel DocumentDetails { get; set; }
        public List<Notes.EncounterNotesViewModel> EncounterNotes { get; set; }
    }
}