using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberRepresentativeViewModel
    {
        #region Fields For Personal Information
        [Display(Name = "GuarantorTypeRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string GuarantorType { get; set; }

        [Display(Name = "MemberIDRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberID { get; set; }

        [Display(Name = "FirstNameRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "LastNameRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        #endregion

        #region  Fields For Contact information
        [Display(Name = "EmailRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "PhoneRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Phone { get; set; }

        [Display(Name = "CellRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Cell { get; set; }

        #endregion

        #region  Fields For Address Information
        [Display(Name = "Add1Representative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Add1 { get; set; }

        [Display(Name = "Add2Representative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Add2 { get; set; }

        [Display(Name = "CityRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }


        [Display(Name = "StateRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "ZipRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Zip { get; set; }

        [Display(Name = "CountyRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        #endregion

        #region Field for label
        [Display(Name = "PersonalInformationRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PersonalInformation { get; set; }

        [Display(Name = "ContactinformationRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Contactinformation { get; set; }

        [Display(Name = "AddressInformationRepresentative", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressInformation { get; set; }
        #endregion
    }
}