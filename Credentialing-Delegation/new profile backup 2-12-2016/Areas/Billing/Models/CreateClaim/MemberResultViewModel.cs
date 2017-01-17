using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class MemberResultViewModel
    {
        //public int MemberId { get; set; }

        //public int ProviderId { get; set; }

        //public string SubscriberID { get; set; }

        //public string PatientLastOrOrganizationName { get; set; }

        //public string PatientMiddleName { get; set; }

        //public string PatientFirstName { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        //public DateTime? PatientBirthDate { get; set; }

        //public string PatientFirstAddress { get; set; }

        //public string PatientSecondAddress { get; set; }

        //public string PatientCity { get; set; }

        //public string PatientState { get; set; }

        //public string PatientZip { get; set; }

        //public string PCP { get; set; }

        //public string PayerName { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        //public DateTime? StartDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        //public DateTime? EndDate { get; set; }


        public string MemberUniqueId { get; set; }
        public string SubscriberID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PCPFullName { get; set; }
        public string PCPPhoneNumber { get; set; }
        public string UniqueProviderID { get; set; }
        public string PlanName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime PlanEffectiveDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime PlanTerminationDate { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string PatientRelationToInsured { get; set; }

        public string ContactNumber { get; set; }

        public string MemberFullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string MemberFullAddress
        {
            get
            {
                return AddressLine1 + " " + City + " " + State + " " + ZipCode;
            }
        }
    }
}