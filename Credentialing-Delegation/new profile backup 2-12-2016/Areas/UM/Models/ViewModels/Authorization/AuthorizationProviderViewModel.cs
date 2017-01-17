using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class AuthorizationProviderViewModel
    {
        public string ProviderID { get; set; }

        public string NPI { get; set; }

        public string Name { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Tax ID")]
        public string TaxID { get; set; }

        public string Specialty { get; set; }

        public int MemberProviderID { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        public bool IsMemberSelected { get; set; }

        [Display(Name = "Provider Type")]
        public string ProviderType { get; set; }

        public string ProviderNetwork { get; set; }

        public bool IsNewlyAdded { get; set; }

        public int? AuthorizationID { get; set; }

        [Display(Name = "Provider Status")]
        public string ProviderStatus { get; set; }

        #region ProviderRole

        [Display(Name = "Provider Role")]
        public string ProviderRole { get; set; }

        #endregion

        [Display(Name = "Plan Name")]
        public string PlanName { get; set; }

        [Display(Name = "Physician Group Name")]
        public string PhysicianGroupName { get; set; }

        [Display(Name = "PCP")]
        public bool IsUsePCP { get; set; }

        [Display(Name = "MBR")]
        public bool IsUseMember { get; set; }

        public string ProviderMode { get; set; }
    }
}