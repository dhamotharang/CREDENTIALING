using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Facility.Models.MasterData
{
    public class CountryViewModel
    {
        public string CountryID { get; set; }
        public string FIPSCode { get; set; }
        public string ISOCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryDialCode { get; set; }
        public string Name { get; set; }
        public string Coordinates { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string Status { get; set; }
        public string StatusType { get; set; }
        public string Source { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}