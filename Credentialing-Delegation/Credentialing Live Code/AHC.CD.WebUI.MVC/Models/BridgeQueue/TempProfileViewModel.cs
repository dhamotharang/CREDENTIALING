using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.BridgeQueue
{
    public class TempProfileViewModel
    {

        [Key]
        public int? Id { get; set; }

        [Display(Name = "Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Name { get; set; }

        [Display(Name = "NPI Number")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NPINumber { get; set; }

        [Display(Name = "Provider Type")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderType { get; set; }


        [Display(Name = "First Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "Facility Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FacilityName { get; set; }

        [Display(Name = "Specialty")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Specialty { get; set; }

        [Display(Name = "Requested By")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RequestedBy { get; set; }

        [Display(Name = "Requested From")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RequestedFrom { get; set; }

        [Display(Name = "Network Participation")]
        [DisplayFormat(NullDisplayText = "-")]
        public string NetworkPar { get; set; }

        [Display(Name = "Contact Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactName { get; set; }

        [Display(Name = "Phone Number")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "FaxNumber")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FaxNumber { get; set; }

        [Display(Name = "Email")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "Address1")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Address1 { get; set; }

        [Display(Name = "Address2")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "State")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "Assigned To")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AssignedTo { get; set; }

        [Display(Name = "Zip Code")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "TAX ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TAXID { get; set; }

        [Display(Name = "Plan Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanName { get; set; }

        [Display(Name = "Physician Group Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhysicianGroupName { get; set; }

        [Display(Name = "Individual Tax Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IndividualTaxId { get; set; }

        [Display(Name = "Group Contact Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GroupContactName { get; set; }

        [Display(Name = "Group Tax Id")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GroupTaxId { get; set; }
    }
}