using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberICEViewModel
    {

        #region Fields For Personal Information
        [Display(Name = "GuarantorTypeICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string GuarantorType { get; set; }

        [Display(Name = "MemberIDICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberID { get; set; }

        [Display(Name = "FirstNameICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "LastNameICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "RelationshipICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Relationship { get; set; }

        #endregion

        #region  Fields For Contact information
        [Display(Name = "EmailICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "PhoneICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Phone { get; set; }

        [Display(Name = "CellICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Cell { get; set; }

        #endregion

        #region  Fields For Address Information
        [Display(Name = "Add1ICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Add1 { get; set; }

        [Display(Name = "Add2ICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Add2 { get; set; }

        [Display(Name = "CityICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }


        [Display(Name = "StateICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "ZipICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Zip { get; set; }

        [Display(Name = "CountyICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        #endregion

        #region Field for label
        [Display(Name = "PersonalInformationICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PersonalInformation { get; set; }

        [Display(Name = "ContactinformationICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Contactinformation { get; set; }

        [Display(Name = "AddressInformationICE", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressInformation { get; set; }
        #endregion
    }
}