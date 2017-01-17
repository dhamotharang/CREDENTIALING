
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Member
{
    public class SearchMember
    {
        [Display(Name = "AccessMemberID", ResourceType = typeof(App_LocalResources.Content))]               
        public string EntityMemberID { get; set; } //Will store access/ultimate member id

        [Display(Name = "MemberID", ResourceType = typeof(App_LocalResources.Content))]
        public string SubscriberID { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(App_LocalResources.Content))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(App_LocalResources.Content))]
        public string LastName { get; set; }

        [Display(Name = "HICN", ResourceType = typeof(App_LocalResources.Content))]
         public string HICN { get; set; }

        [Display(Name = "Medicaid", ResourceType = typeof(App_LocalResources.Content))]
         public string MEDICAID { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(App_LocalResources.Content))]
         public string Phone { get; set; }

         public string DateOfBirth { get; set; }

        [Display(Name = "ReferenceID", ResourceType = typeof(App_LocalResources.Content))]
        public string ReferenceAuthID { get; set; }
    }
}	