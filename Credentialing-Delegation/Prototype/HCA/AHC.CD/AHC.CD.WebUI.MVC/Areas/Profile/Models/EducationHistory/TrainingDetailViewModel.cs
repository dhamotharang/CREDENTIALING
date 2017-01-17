using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class TrainingDetailViewModel
    {
        public TrainingDetailViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int TrainingDetailID { get; set; }

        [Display(Name = "Medical School Name")]
        public string InstitutionName { get; set; }
        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }

        public EducationAddressViewModel EducationAddress { get; set; }
        [Display(Name = "Telephone Number")]
        public string Telephone { get; set; }
        [Display(Name = "Fax Number")]
        public string Fax { get; set; }
        [Display(Name = "Did you completed the training in this institution?")]
        public YesNoOption IsCompleted { get; set; }
        [Display(Name = "If No, Give the reason")]
        public string InCompleteReason { get; set; }

        public ICollection<ResidencyInternshipDetailViewModel> ResidencyInternshipDetails { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
