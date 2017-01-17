using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization
{
    public class AuthorizationProviderViewModel
    {
        public string ProviderID { get; set; }

        public string NPI { get; set; }

        public string Name { get; set; }

        [Display(Name = "FIRST NAME ")]
        public string FirstName { get; set; }

        [Display(Name = "MIDDLE NAME")]
        public string MiddleName { get; set; }

        [Display(Name = "LAST NAME")]
        public string LastName { get; set; }

        [Display(Name = "FULL NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FullName { get; set; }

        [Display(Name = "PHONE NUMBER")]
        public string PhoneNumber { get; set; }

        [Display(Name = "FAX NUMBER")]
        public string FaxNumber { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Display(Name = "FACILITY NAME")]
        public string FacilityName { get; set; }

        [Display(Name = "FACILITY NUMBER")]
        public string FacilityNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(Name = "ZIP CODE")]
        public string ZipCode { get; set; }

        [Display(Name = "TAX ID")]
        public string TaxID { get; set; }

        public string Specialty { get; set; }

        public int MemberProviderID { get; set; }

        [Display(Name = "CONTACT NAME")]
        public string ContactName { get; set; }

        public bool IsMemberSelected { get; set; }

        [Display(Name = "PROVIDER TYPE")]
        public string ProviderType { get; set; }

        public string ProviderNetwork { get; set; }

        public bool IsNewlyAdded { get; set; }

        public int? AuthorizationID { get; set; }

        [Display(Name = "PROVIDER STATUS")]
        public string ProviderStatus { get; set; }

        #region ProviderRole

        [Display(Name = "PROVIDER ROLE")]
        public string ProviderRole { get; set; }

        #endregion
    }
}