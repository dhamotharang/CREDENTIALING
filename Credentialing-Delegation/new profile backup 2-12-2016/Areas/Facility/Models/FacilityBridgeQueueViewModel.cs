using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Facility.Models
{
    public class FacilityBridgeQueueViewModel
    {
        [Display(Name = "Facility ID")]
        public string FacilityID { get; set; }

        [Display(Name = "Source Name")]
        public string SourceName { get; set; }

        [Display(Name = "Assigned To")]
        public string Assigned { get; set; }

        [Display(Name = "Approved By")]
        public string AppBy { get; set; }

        [Display(Name = "Approved Date")]
        public string AppDate { get; set; }

        [Display(Name = "Req By")]
        public string ReqBy { get; set; }

        [Display(Name = "Req From")]
        public string ReqFrom { get; set; }

        [Display(Name = "Time")]
        public string Time { get; set; }

        [Display(Name = "Req Date")]
        public string ReqDate { get; set; }

        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Display(Name = "Organization NPI")]
        public string OrganizationNPI { get; set; }

        [Display(Name = "Organization Type")]
        public string OrganizationType { get; set; }

        [Display(Name = "LAST NAME")]
        public string CLastName { get; set; }

        [Display(Name = "FIRST NAME")]
        public string CFirstName { get; set; }

        [Display(Name = "Fax")]
        public string CFax { get; set; }

        [Display(Name = "PHONE")]
        public string CPhoneNumber { get; set; }

        [Display(Name = "Reason Description")]
        public string ReasonDescription { get; set; }

        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        [Display(Name = "Facility Type")]
        public string FacilityType { get; set; }

        [Display(Name = "Corporate Name")]
        public string CorporateName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Tax ID")]
        public string TaxID { get; set; }

        [Display(Name = "Print On Claim")]
        public string PrintOnClaim { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address Line1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "County")]
        public string County { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

    }
}