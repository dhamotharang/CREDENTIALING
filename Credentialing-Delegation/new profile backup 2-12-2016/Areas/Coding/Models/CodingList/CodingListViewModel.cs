using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CodingList
{
    public class CodingListViewModel
    {
        public string EncounterID { get; set; }
        public string MemberID { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public string Facility { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateOfService { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateOfCreation { get; set; }
        public string CreatedBy { get; set; }
        public string EncounterType { get; set; }
        public string Status { get; set; }
     
        public string MemberGender { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime MemberDOB { get; set; }
        public string Address { get; set; }
        public string PlanName { get; set; }
    }
}