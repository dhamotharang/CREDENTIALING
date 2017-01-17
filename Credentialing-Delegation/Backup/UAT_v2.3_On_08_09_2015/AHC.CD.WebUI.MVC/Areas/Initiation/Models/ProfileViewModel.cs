using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Initiation.Models;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Initiation.Models
{
    public class ProfileViewModel
    {
        public OtherIdentificationViewModel OtherIdentificationNumber { get; set; }

        public PersonalDetailViewModel PersonalDetail { get; set; }

        public IList<HomeAddressViewModel> HomeAddresses { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }

        public IList<ContractInfoViewModel> ContractInfoes { get; set; }

        public CVInformationViewModel CVInformation { get; set; }

        public StatusType? StatusType { get; set; }

    }
}