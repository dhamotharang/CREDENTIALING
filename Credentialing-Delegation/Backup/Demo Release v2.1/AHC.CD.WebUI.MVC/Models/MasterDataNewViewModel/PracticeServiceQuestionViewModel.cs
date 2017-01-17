using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class PracticeServiceQuestionViewModel
    {
        public int FacilityServiceQuestionID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string ShortTitle { get; set; }

        public StatusType? StatusType { get; set; }
        
    }
}