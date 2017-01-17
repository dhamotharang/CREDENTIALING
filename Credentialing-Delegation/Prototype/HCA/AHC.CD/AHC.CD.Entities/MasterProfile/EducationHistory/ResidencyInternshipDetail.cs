using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class ResidencyInternshipDetail
    {
        public ResidencyInternshipDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ResidencyInternshipDetailID { get; set; }

        #region ProgramType

        [Required]
        public string ProgramType { get; private set; }

        [NotMapped]
        public ResidencyInternshipProgramType ResidencyInternshipProgramType
        {
            get
            {
                return (ResidencyInternshipProgramType)Enum.Parse(typeof(ResidencyInternshipProgramType), this.ProgramType);
            }
            set
            {
                this.ProgramType = value.ToString();
            }
        }

        #endregion

        public string Department { get; set; }

        #region Speciality

        [Required]
        public int SpecialityID { get; set; }
        [ForeignKey("SpecialityID")]
        public Speciality Speciality { get; set; }

        #endregion

        public string DirectorName { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
