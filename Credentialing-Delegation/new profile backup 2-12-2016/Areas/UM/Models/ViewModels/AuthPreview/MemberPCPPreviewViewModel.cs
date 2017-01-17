using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class MemberPCPPreviewViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "PCP: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Name
        {
            get
            {
                var ProviderName = "Dr." + FirstName + " " + LastName;
                return ProviderName;
            }
        }

        [Display(Name = "PCP PH: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "PCP FAX: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FaxNumber { get; set; }
    }
}