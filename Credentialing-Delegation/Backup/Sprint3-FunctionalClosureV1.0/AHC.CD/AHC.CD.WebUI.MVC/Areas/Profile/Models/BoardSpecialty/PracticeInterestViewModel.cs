using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty
{
    public class PracticeInterestViewModel
    {
        public int PracticeInterestID { get; set; }

        [Display(Name="Practice Interest")]
        [Required]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "{0} should not exceed {1} characters.")]
        public string Interest { get; set; }
    }
}
