using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class SchoolViewModel
    {
        public SchoolViewModel()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        public int SchoolID { get; set; }

        [Required]
        [MaxLength(200)]        
        public string Name { get; set; }

        public StatusType? StatusType { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public SchoolContactInfoViewModel SchoolContactInfoViewModel { get; set; }
       
    }
}