using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization
{
    public class ProviderViewModal
    {
        public string ProviderID { get; set; }

        public string NPI { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string FacilityName { get; set; }

        public string FacilityNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string TaxID { get; set; }

        public string Speciality { get; set; }

        public int MemberProviderID { get; set; }

        public string ContactName { get; set; }

        public bool IsMemberSelected { get; set; }

        public string ProviderType { get; set; }

        public string ProviderNetwork { get; set; }

        public bool IsNewlyAdded { get; set; }

        public int? AuthorizationID { get; set; }

        #region ProviderRole Type

        public string ProviderRole { get; set; }

        #endregion
      
    }
}
