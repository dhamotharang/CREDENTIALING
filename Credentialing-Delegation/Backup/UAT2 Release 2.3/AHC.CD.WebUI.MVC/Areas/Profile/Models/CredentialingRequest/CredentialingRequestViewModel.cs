using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.CredentialingRequest
{
    public class CredentialingRequestViewModel
    {
        public int? CredentialingRequestID { get; set; }

        public int ProfileID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NPINumber { get; set; }

        public string CAQHNumber { get; set; }

        public int PlanID { get; set; }

        public IsDelegated? IsDelegated { get; set; }

        public StatusType? StatusType { get; set; }
    }
}