using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class AddressInformationViewModel
    {
        public int AddressId { get; set; }

        [Display(Name = "Address Line 2")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ApartmentOrSuiteNumber { get; set; }

        [Display(Name = "Address Line 1")]
        [DisplayFormat(NullDisplayText = "-")]
        public string HomeAddress { get; set; }

        [Display(Name = "City")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "City Code")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CityCode { get; set; }

        [Display(Name = "State")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "State Code")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StateCode { get; set; }

        [Display(Name = "County")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "County Code")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CountyCode { get; set; }

        [Display(Name = "Country")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Country { get; set; }

        [Display(Name = "Country Code")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CountryCode { get; set; }

        [Display(Name = "ZipCode")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "Address Type")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressType { get; set; }

        [Display(Name = "Status")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }

        public string SourceCode { get; set; }
      
        public string SourceName { get; set; }
     
        public string TimeStamp { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}