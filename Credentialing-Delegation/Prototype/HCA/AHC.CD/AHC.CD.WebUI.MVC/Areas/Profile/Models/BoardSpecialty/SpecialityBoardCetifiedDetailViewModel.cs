using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpeciality
{
    public class SpecialityBoardCetifiedDetailViewModel
    {
        public int SpecialityBoardCetifiedDetailID { get; set; }

        [Required]
        public int SpecialityBoardID { get; set; }

        [Display(Name = "Board Name *")]
        public SpecialityBoard SpecialityBoard { get; set; }

        [Required]
        [Display(Name = "Initial Certification Date *")]
        public DateTime InitialCertificationDate { get; set; }

        [Required]
        [Display(Name = "Last Re-Certification Date *")]
        public DateTime LastReCerificationDate { get; set; }

        [Required]
        [Display(Name = "Expiration Date *")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Board Certification Document")]
        public string BoardCertificatePath { get; set; }
    }

    //public class SpecialityBoardCetifiedDetailViewModel
    //{
    //    public int SpecialityBoardCetifiedDetailID { get; set; }

    //    [Display(Name="Board Name *")]
    //    public SpecialityBoard SpecialityBoard { get; set; }

    //    [Required]
    //    [Display(Name="Initial Certification Date *")]
    //    public DateTime InitialCertificationDate { get; set; }

    //    [Required]
    //    [Display(Name = "Last Re-Certification Date *")]
    //    public DateTime LastReCerificationDate { get; set; }

    //    [Required]
    //    [Display(Name = "Expiration Date *")]
    //    public DateTime ExpirationDate { get; set; }

    //    [Display(Name="Board Certificate")]
    //    public string BoardCertificatePath { get; set; }
    //}
}
