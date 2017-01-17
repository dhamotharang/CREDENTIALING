using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Portal.Enums.ProviderBridge;

namespace PortalTemplate.Areas.Portal.Models.FacilityBridge.AddFacility
{
    public class AddFacilityViewModel
    {
        public string FacilityID { get; set; }
         [Display(Name = "Salutation")]
        public Salutation FacilitySalutation { get; set; }

         [Display(Name = "Facility Type")]
         public FacilityTypes FacilityType { get; set; }
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

        [Display(Name = "Email Address")]
        public string Email { get; set; }

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
        [Display(Name = "Tax ID Type")]
        public TaxIDTypes TaxIdType { get; set; }

        [Display(Name = "Tax ID")]
        public string IndividualTaxID { get; set; }
        [Display(Name = "Group Tax ID")]
        public string GroupTaxID { get; set; }

        public string Specialty { get; set; }

        public int MemberProviderID { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        public bool IsMemberSelected { get; set; }

      

        public string ProviderNetwork { get; set; }

        public bool IsNewlyAdded { get; set; }

        public int? AuthorizationID { get; set; }

        [Display(Name = "Facility Status")]
        public string FacilityStatus { get; set; }

        #region ProviderRole

        [Display(Name = "Facility Role")]
        public string FacilityRole { get; set; }

        #endregion

        [Display(Name = "Plan Name")]
        public string PlanName { get; set; }

        [Display(Name = "Physician Group Name")]
        public string PhysicianGroupName { get; set; }
        [Display(Name = "Group Contact Name")]
        public string GroupContactName { get; set; }
        [Display(Name="Reason")]
        public Reasons Reason { get; set; }
    }
    public enum FacilityTypes
    {
    MD
    }
    public enum TaxIDTypes
    {
        Group,Individual
    }
    public enum Reasons {
    InValid_Details,Participated_In_Malpractice
    } 
 
}