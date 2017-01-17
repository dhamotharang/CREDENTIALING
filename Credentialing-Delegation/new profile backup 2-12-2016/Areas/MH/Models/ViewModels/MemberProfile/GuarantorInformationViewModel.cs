using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class GuarantorInformationViewModel
    {
        public int GuarantorId { get; set; }

        public OtherPersonInformationViewModel OtherPersonInformation { get; set; }

        public string SignedDate { get; set; }

        public string SignaturePath { get; set; }

        public string Status { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}