using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class DecredemtialingReasonViewModel
    {
        public int DecredentialingReasonId { get; set; }

        [Required]
        public string Reason { get; set; }

        public StatusType? StatusType { get; set; } 
    }
}