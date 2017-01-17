using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MasterData
{
    public class PatientRelationshipViewModel
    {
        public string PatientRelationID { get; set; }
        public string PatientRealtionType { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string StatusType { get; set; }
        public string Source { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
    }
}