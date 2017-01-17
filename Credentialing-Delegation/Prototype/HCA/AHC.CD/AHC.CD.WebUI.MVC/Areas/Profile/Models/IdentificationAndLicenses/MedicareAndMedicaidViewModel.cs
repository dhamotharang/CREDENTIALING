using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class MedicareAndMedicaidViewModel
    {
        public int MedicareAndMedicaidID { get; set; }

        [Required]
       

        public bool IsApproved { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string LicenseNumber { get; set; }

        [Required]
        [Display(Name = "Issue State *")]

        public string State { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Issue Date *")]

        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Expiry Date *")]

        public DateTime ExpiryDate { get; set; }

        //[Required]
        //public bool AnySanctionImposed { get; set; }

        public string LicenseCertPath { get; set; }

        public string Type { get; set; }
        [NotMapped]
        public MedicareAndMedicaidType MedicareAndMedicaidType
        {
            get
            {
                return (MedicareAndMedicaidType)Enum.Parse(typeof(MedicareAndMedicaidType), this.Type);
            }
            set
            {
                this.Type = value.ToString();
            }
        }
    }

    public enum MedicareAndMedicaidType
    {
        Medicare=1,
        Medicaid
    }
}
