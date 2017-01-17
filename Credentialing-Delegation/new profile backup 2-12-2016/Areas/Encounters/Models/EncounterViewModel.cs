using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class EncounterViewModel
    {
        [DisplayName("Encounter ID")]
        public string EncounterID { get; set; }

        [DisplayName("Date Of Creation")]
        public string DateOfCreation { get; set; }

        [DisplayName("DOS From")]
        public DateTime DateOfServiceFrom { get; set; }

        [DisplayName("DOS To")]
        public DateTime DateOfServiceTo { get; set; }

        public MemberViewModel MemberInfo { get; set; }

        public ProviderViewModel ProviderInfo { get; set; }
        
        public FacilityViewModel FacilityInfo { get; set; }
        
        
        public string CreatedBy { get; set; }

        [DisplayName("Encounter Type")]
        public EncounterType EncounterType { get; set; }

        [DisplayName("Status")]
        public EncounterStatus EncounterStatus { get; set; }

        public ServiceInfoViewModel ServiceInfo { get; set; }

        public List<DocumentViewModel> Documents { get; set; }

        [DisplayName("Notes")]
        public string EncounterNotes { get; set; }

        [DisplayName("Schedule Time")]
        public string ScheduleTime { get; set; }

        [DisplayName("Schedule Date")]
        public string ScheduleDate { get; set; }
    }
}