using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class OtherPersonInformationViewModel
    {
        public int OtherPersonId { get; set; }

        public OtherPersonInformationViewModel()
        {
            OtherContactInformation = new List<OtherContactInformationViewModel>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public string SSN { get; set; }

        public string RelationshipCode { get; set; }

        public string status { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

        public OtherAddressInformationViewModel OtherAddressInformation { get; set; }

        public List<OtherContactInformationViewModel> OtherContactInformation { get; set; }
    }
}