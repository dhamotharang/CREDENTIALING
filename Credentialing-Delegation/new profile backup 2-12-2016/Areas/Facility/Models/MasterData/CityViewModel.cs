using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Facility.Models.MasterData
{
    public class CityViewModel
    {
        public string CityID { get; set; }
        public string CityCode { get; set; }
        public string Name { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string CountyID { get; set; }
      //  public CountyViewModel County { get; set; }
        public string Status { get; set; }
        public string StatusType { get; set; }
        public string Source { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}