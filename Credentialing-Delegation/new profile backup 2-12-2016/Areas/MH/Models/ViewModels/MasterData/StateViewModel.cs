using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MasterData
{
    public class StateViewModel
    {
        public string StateID {get;set;}
        public string Name {get;set;}
        public string Coordinates {get;set;}
        public string FIPSCode {get;set;}
        public string ISOcode {get;set;}
        public string StateCode {get;set;}
        public string Latitute {get;set;}
        public string Longitute {get;set;}
        public string CountryID {get;set;}
        public CountryViewModel country {get;set;}
        public string Status { get; set; }
        public string StatusType { get; set; }
        public string Source { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}