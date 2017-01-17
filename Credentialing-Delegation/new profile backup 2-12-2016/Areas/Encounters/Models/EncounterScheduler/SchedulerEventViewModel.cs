using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.EncounterScheduler
{
    public class SchedulerEventViewModel
    {
        public SchedulerEventViewModel()
        {
        
        }
        public SchedulerEventViewModel(EncounterViewModel encounter)
        {
            this.EventID = long.Parse(encounter.EncounterID);
            this.ProviderNPI = encounter.ProviderInfo.ProviderNPI;
            this.ProviderName = encounter.ProviderInfo.ProviderFirstName + " " + encounter.ProviderInfo.ProviderLastName;
            this.SubscriberID = encounter.MemberInfo.SubscriberID;
            this.MemberName = encounter.MemberInfo.MemberFirstName + " " + encounter.MemberInfo.MemberLastName;
            this.FacilityID = encounter.FacilityInfo.FacilityID;
            this.FacilityName = encounter.FacilityInfo.FacilityName;
            this.FacilityAddress = encounter.FacilityInfo.FacilityAddress;
            this.AppointmentDateTimeStart = encounter.DateOfServiceFrom;
            this.AppointmentDateTimeEnd = encounter.DateOfServiceTo;
        }
        public long EventID { get; set; }
        
        //Provider Information
        public string ProviderNPI { get; set; }
        public string ProviderName { get; set; }
        
        //Member Information
        public string SubscriberID { get; set; }
        public string MemberName { get; set; }
        
        //Facility Information
        public string FacilityID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }

        //Meeting Time
        public DateTime AppointmentDateTimeStart { get; set; }
        public DateTime AppointmentDateTimeEnd { get; set; }
    }
}