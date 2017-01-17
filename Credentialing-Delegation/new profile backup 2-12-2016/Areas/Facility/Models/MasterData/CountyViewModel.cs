using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Facility.Models.MasterData
{
    public class CountyViewModel
    {
        public string CountyID { get; set; }
        public string FIPSCode { get; set; }
        public string ISOCode { get; set; }
        public string CountyCode { get; set; }
        public string Name { get; set; }
        public string StateID { get; set; }
        public StateViewModel State { get; set; }
        public string Status { get; set; }
        public string StatusType { get; set; }
        public string Source { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}