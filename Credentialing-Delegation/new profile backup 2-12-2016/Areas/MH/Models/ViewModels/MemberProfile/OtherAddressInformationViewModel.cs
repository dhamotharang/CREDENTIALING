using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class OtherAddressInformationViewModel
    {
        public int OtherPersonAddressId { get; set; }

        public string ApartmentOrSuitNumber { get; set; }

        public string HomeAddress { get; set; }

        public string City { get; set; }

        public string CityCode { get; set; }

        public string State { get; set; }

        public string StateCode { get; set; }

        public string County { get; set; }

        public string CountyCode { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string ZipCode { get; set; }

        public string AddressType { get; set; }

        public string Status { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}