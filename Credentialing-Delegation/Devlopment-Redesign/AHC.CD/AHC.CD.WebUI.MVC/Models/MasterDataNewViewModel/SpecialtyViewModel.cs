using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class SpecialtyViewModel
    {
        public int SpecialtyID { get; set; }

        [Required]
        [MaxLength(100)]       
        public string Name { get; set; }
        public string TaxonomyCode { get; set; }
        public StatusType? StatusType { get; set; }
        
    }
}