using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class FacilityPracticeProviderTypeViewModel
    {
        public int? FacilityPracticeProviderTypeId { get; set; }

        public int ProviderTypeID { get; set; }

        public StatusType? StatusType { get; set; }
    }
}