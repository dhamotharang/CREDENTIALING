using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class ServiceInfoViewModel
    {
        public int ServiceInfoID { get; set; }

        [DisplayName("Referring Provider")]
        public ProviderViewModel ReferringProvider { get; set; }

        [DisplayName("Rendering Provider")]
        public ProviderViewModel RenderingProvider { get; set; }

        [DisplayName("Billing Provider")]
        public ProviderViewModel BillingProvider { get; set; }

        [DisplayName("Service Facility")]
        public FacilityViewModel ServiceFacility { get; set; }

        public PlaceOfServiceViewModel PlaceOfService { get; set; }

        [DisplayName("DOS From")]
        public string DOSFrom { get; set; }

        [DisplayName("DOS To")]
        public string DOSTo { get; set; }

        [DisplayName("Check In Time")]
        public string CheckInTime { get; set; }

        [DisplayName("Check Out Time")]
        public string CheckOutTime { get; set; }

        [DisplayName("Visit Category")]
        public string VisitCategory { get; set; }

        [DisplayName("Visit Reason")]
        public string ReasonForVisit { get; set; }

        [DisplayName("Next Appointment Date")]
        public string NextAppointmentDate { get; set; }

        [DisplayName("Visit Length")]
        public string VisitLength { get; set; }

        public List<ProviderViewModel> ProviderList { get; set; }
        public List<FacilityViewModel> FacilityList { get; set; }
        public List<PlaceOfServiceViewModel> PlaceOfServiceList { get; set; }
    }
}
