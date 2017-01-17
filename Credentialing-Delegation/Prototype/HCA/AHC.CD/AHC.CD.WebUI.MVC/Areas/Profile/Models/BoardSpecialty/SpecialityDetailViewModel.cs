using AHC.CD.Entities.MasterData.Enums;
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
    public class SpecialityDetailViewModel
    {
        public int SpecialityDetailID { get; set; }

        [Required]
        [Display(Name = "Specialty Type *")]
        public string SpecialityPreference { get; private set; }

        [Required]
        public int SpecialityID { get; set; }

        [Display(Name = "Specialty Name *")]
        public Speciality Speciality { get; set; }

        [Required]
        [Display(Name = "% Of Time *")]
        public Double PercentageOfTime { get; set; }

        [Required]
        [Display(Name = "HMO *")]
        public string ListedInHMO { get; private set; }
        
        [Required]
        [Display(Name = "PPO *")]
        public string ListedInPPO { get; private set; }
        
        [Required]
        [Display(Name = "POS *")]
        public string ListedInPOS { get; set; }

        [Display(Name = "Specialty Certificate")]
        public string CertificatePath { get; set; }
        
        [Required]
        public int SpecialityBoardDetailID { get; set; }

        public SpecialityBoardDetailViewModel SpecialityBoardDetail { get; set; }
    }
}