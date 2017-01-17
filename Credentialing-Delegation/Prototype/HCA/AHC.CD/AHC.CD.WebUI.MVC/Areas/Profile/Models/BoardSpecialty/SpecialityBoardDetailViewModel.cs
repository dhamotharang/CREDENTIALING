using AHC.CD.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpeciality
{
    public class SpecialityBoardDetailViewModel
    {
        public int SpecialityBoardDetailID { get; set; }

        [Required]
        [Display(Name = "Board Certified? *")]
        public string IsBoardCertified { get; private set; }
        
        public SpecialityBoardCetifiedDetailViewModel SpecialityBoardCetifiedDetail { get; set; }

        public SpecialityBoardNotCertifiedDetailViewModel SpecialityBoardNotCertifiedDetail { get; set; }
    }

    //public class SpecialityBoardDetailViewModel
    //{
    //    public int SpecialityBoardDetailID { get; set; }

    //    [Required]
    //    [Display(Name = "Board Certified? *")]
    //    public bool IsBoardCertified { get; set; }

    //    public SpecialityBoardCetifiedDetailViewModel SpecialityBoardCetifiedDetail { get; set; }

    //    public SpecialityBoardNotCertifiedDetailViewModel SpecialityBoardNotCertifiedDetail { get; set; }
    //}
}
