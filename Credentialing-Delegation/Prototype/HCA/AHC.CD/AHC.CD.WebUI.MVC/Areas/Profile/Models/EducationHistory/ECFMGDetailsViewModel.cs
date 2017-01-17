using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class ECFMGDetailsViewModel
    {
        public ECFMGDetailsViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ECFMGDetailID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Display(Name = "ECFMG Number")]
        public string ECFMGNumber { get; set; }
        
        [Required]
        [Display(Name = "ECFMG Issue Date")]
        public DateTime ECFMGIssueDate { get; set; }
        [Display(Name = "Select ECFMG Certificate")]
        public string ECFMGCertPath { get; set; }
        
        public DateTime LastModifiedDate { get; set; }
    }
}
