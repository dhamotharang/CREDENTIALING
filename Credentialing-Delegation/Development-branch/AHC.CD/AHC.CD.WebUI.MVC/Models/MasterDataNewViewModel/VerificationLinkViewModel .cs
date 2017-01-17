using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class VerificationLinkViewModel 
    {
        public int VerificationLinkID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Link { get; set; }        

        public StatusType? StatusType { get; set; } 

    }
}