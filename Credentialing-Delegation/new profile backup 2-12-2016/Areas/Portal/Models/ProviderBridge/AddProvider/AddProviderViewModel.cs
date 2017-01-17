using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Portal.Enums.ProviderBridge;
using System.ComponentModel;

namespace PortalTemplate.Areas.Portal.Models.ProviderBridge.AddProvider
{
    public class AddProviderViewModel
    {
        public int ProviderID { get; set; }
         [Display(Name = "Salutation")]
        public Salutation ProviderSalutation { get; set; }

         [Display(Name = "Provider Type")]
        public ProviderTypes ProviderType { get; set; }
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

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address { get; set; }

        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Location Type")]
        public locationType LocationType { get; set; }

        [Display(Name = "Tax ID Type")]
        public TaxIDTypes TaxIdType { get; set; }

        [Display(Name = "Individual Tax ID")]
        public string IndividualTaxID { get; set; }

        [Display(Name = "Group Tax ID")]
        public string GroupTaxID { get; set; }

        [Display(Name = "Group Name")]
        public groupName GroupName { get; set; }

        public specialty Specialty { get; set; }

        public int MemberProviderID { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Display(Name = "Contact Type")]
        public ContactTypes ContactType { get; set; }

        [Display(Name = "Phone Number")]
        public string ContactPhoneNumber { get; set; }

        [Display(Name = "FaxNumber")]
        public string ContactFaxNumber { get; set; }

        [Display(Name = "Email")]
        public string ContactEmail { get; set; }

        public bool IsMemberSelected { get; set; }

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
        public plan PlanName { get; set; }

        [Display(Name = "Physician Group Name")]
        public string PhysicianGroupName { get; set; }

        [Display(Name = "Group Contact Name")]
        public string GroupContactName { get; set; }

        [Display(Name="Reason")]
        public string Reason { get; set; }

        public HttpPostedFileBase ContractDocument { get; set; }
    }
    public enum Gender
    {
        MALE,FEMALE
    }
    public enum ProviderTypes {
        MD,DOCTOR,NURSE
    }
    public enum TaxIDTypes
    {
        Group,Individual
    }
    public enum Reasons {
    InValid_Details,Participated_In_Malpractice
    }
    public enum ContactTypes
    {
        [Description("BILLING CONTACT")] BILLINGCONTACT,
        [Description("CREDENTIAL CONTACT")]credentialcontact,
        [Description("OFFICE MANAGER")] officemanager
       
    }
    public enum facility
    {
        Lakeshore2, Tampa
    }
    public enum locationType
    {
        UNKNOWN, PRIMARY, SECONDARY
    }
    public enum groupName
    {
        Access, UNITY, MIRRA
    }
    public enum plan
    {
        ULTIMATE, HUMANA,FREEDOM
    }
    public enum specialty
    {
        Cardiology, Anesthesiology, Chiropractor
    }
    public enum City
    {
        TAMPA,MIAMI
    }


}