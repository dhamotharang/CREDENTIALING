using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class GraduationDetailViewModel
    {
        public int GraduationDetailID { get; set; }
        public GraduationType GraduationType { get; set; }
        public string SchoolCode { get; set; }
        [Required]
        public string SchoolName { get; set; }
        public string DegreeAwarded { get; set; }
        public string FaxNumber { get; set; }
        [Required]
        public string Telephone { get; set; }
         [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }
         [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }
        public string GraduationCertPath { get; set; }

        public SchoolAddressViewModel SchoolAddress { get; set; }
    }

    public enum GraduationType
    {
        USGraduate = 1,
        NonUSGraduate
    }
}
