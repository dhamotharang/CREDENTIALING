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
        public string ProgramType { get; set; }

        [NotMapped]
        public ResidencyInternshipProgramType? ResidencyInternshipProgramType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProgramType))
                    return null;

                if (this.ProgramType.Equals("Not Available"))
                    return null;

                return (ResidencyInternshipProgramType)Enum.Parse(typeof(ResidencyInternshipProgramType), this.ProgramType);
            }
            set
            {
                this.ProgramType = value.ToString();
            }
        }

        #endregion

        public string Department { get; set; }

        #region Specialty

        [Required]
        public int SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public Specialty Specialty { get; set; }

        #endregion

        #region Preference

        [Required]
        public string Preference { get; set; }

        [NotMapped]
        public PreferenceType? PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Preference))
                    return null;

                if (this.Preference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.Preference);
            }
            set
            {
                this.Preference = value.ToString();
            }
        }

        #endregion        

        [Required]
        public string DirectorName { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string DocumentPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
