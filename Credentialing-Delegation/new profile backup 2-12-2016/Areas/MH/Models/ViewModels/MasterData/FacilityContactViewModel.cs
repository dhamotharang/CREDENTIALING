using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MasterData
{
    public class FacilityContactViewModel
    {
        public int ContactInformationID { get; set; }
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string Relationship { get; set; }
        public string TelephoneNumber { get; set; }
        public string Extension { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string FamilyID { get; set; }
    }
}