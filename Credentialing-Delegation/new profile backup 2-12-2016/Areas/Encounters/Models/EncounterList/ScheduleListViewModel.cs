using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.EncounterList
{
    public class ScheduleListViewModel
    {
        [DisplayName("Subscriber Id")]
        public string SubscriberID { get; set; }

        [DisplayName("Member last Name")]
        public string MemberLastName { get; set; }

        [DisplayName("Member First Name")]
        public string MemberFirstName { get; set; }

        [DisplayName("Provider NPI")]
        public string ProviderNPI { get; set; }

        [DisplayName("Provider Last Name")]
        public string ProviderLastName { get; set; }

        [DisplayName("Provider First Name")]
        public string ProviderFirstName { get; set; }

        [DisplayName("Place of Service")]
        public string FacilityName { get; set; }

        [DisplayName("Date Of Creation")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DateOfCreation { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Status")]
        public string EncounterStatus { get; set; }
    }
}