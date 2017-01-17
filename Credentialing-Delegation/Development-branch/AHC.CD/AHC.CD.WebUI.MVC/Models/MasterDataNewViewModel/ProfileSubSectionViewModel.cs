using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class ProfileSubSectionViewModel
    {
        public int ProfileSubSectionId { get; set; }

        [Required]
        public string SubSectionName { get; set; }

        public string TabName { get; set; }

        public string SubSectionId { get; set; }

        public StatusType? StatusType { get; set; } 

    }
}