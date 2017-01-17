using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList
{
    public class CredentialingSpecialityListViewModel
    {
        public int CredentialingSpecialityListID { get; set; }

        public int? SpecialtyID { get; set; }

        public string Name { get; set; }

        public StatusType? StatusType { get; set; }
        
    }
}