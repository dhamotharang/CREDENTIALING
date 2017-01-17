using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability
{
    public class ProfessionalLiabilityInsuranceViewModel
    {
        public int ProfessionalLiabilityInsuranceID { get; set; }

        public InsuranceInfoViewModel InsuranceInfo { get; set; }
        public InsuranceAddressViewModel InsuranceAddress { get; set; }
    }
}