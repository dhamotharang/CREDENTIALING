using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation
{
    public class GroupInformationViewModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        [Display(Name = "Join Date")]
        public DateTime JoiningDate { get; set; }

        [Display(Name = "Join Date")]
        public DateTime EndDate { get; set; }

        public ContractGroupStatus GroupStatusOption { get; set; }
    }
}