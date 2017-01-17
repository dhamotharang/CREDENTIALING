using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList
{
    public class CredentialingCoveringPhysicianViewModel
    {
        public int CredentialingCoveringPhysicianID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public StatusType? StatusType { get; set; }
    }
}