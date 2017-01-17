using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MasterData
{
    public class FacilityServiceViewModel
    {
        public int FacilityInformationID { get; set; }
        public string FacilityName { get; set; }
        public string FacilityID { get; set; }
        public string PrintOnClaim { get; set; }
        public string LegalEntity { get; set; }
        public string FacilityType { get; set; }
        public string TaxID { get; set; }
        public FacilityContactViewModel Contact { get; set; }
    }
}