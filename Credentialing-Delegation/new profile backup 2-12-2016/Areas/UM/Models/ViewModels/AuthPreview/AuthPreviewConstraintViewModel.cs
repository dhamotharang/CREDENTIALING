using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class AuthPreviewConstraintViewModel
    {
        public bool IsAgencyOrSupplier { get; set; }

        public bool IsAdmissionAuthorization { get; set; }

        public bool IsOutPatient { get; set; }

        public bool HasMDCCode { get; set; }

        public bool HasDRGCode { get; set; }

        public bool HasRUGCode { get; set; }
         
    }
}