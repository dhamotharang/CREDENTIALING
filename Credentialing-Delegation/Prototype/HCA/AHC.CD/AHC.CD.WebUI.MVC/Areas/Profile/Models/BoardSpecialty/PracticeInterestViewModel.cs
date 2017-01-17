using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpeciality
{
    public class PracticeInterestViewModel
    {
        public int PracticeInterestID { get; set; }

        [Display(Name="Practice Interest")]
        public string Description { get; set; }
    }
}
