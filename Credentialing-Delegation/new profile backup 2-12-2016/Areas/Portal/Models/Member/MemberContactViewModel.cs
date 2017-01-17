using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberContactViewModel
    {
        public int ContactID { get; set; }

        [Display(Name = "PrimaryContact", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PrimaryContact { get; set; }

        [Display(Name = "AlternateContact", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AlternateContact { get; set; }
        
        [Display(Name = "AddressLine1", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }
        
        [Display(Name = "City", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }
        
        [Display(Name = "State", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        public string County { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }
       
        [Display(Name = "Email", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        #region Contact Type

        public string ContactType { get; set; }

        public string ContactType1 { get; set; }

        #endregion
    }
}