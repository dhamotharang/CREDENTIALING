using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class EducationDetail
    {
        public EducationDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EducationDetailID { get; set; }

        #region GraduationType

        [Required]
        public string GraduationType { get; private set; }

        [NotMapped]
        public EducationGraduateType GraduateType
        {
            get
            {
                return (EducationGraduateType)Enum.Parse(typeof(EducationGraduateType), this.GraduationType);
            }
            set
            {
                this.GraduationType = value.ToString();
            }
        }

        #endregion

        #region QualificationType

        [Required]
        public string QualificationType { get; private set; }

        [NotMapped]
        public EducationQualificationType EducationQualificationType
        {
            get
            {
                return (EducationQualificationType)Enum.Parse(typeof(EducationQualificationType), this.QualificationType);
            }
            set
            {
                this.QualificationType = value.ToString();
            }
        }

        #endregion

        [Required]
        public string SchoolName { get; set; }

        #region QualificationDegree

        [Required]
        public int QualificationDegreeID { get; set; }
        [ForeignKey("QualificationDegreeID")]
        public QualificationDegree QualificationDegree { get; set; }

        #endregion

        public EducationAddress EducationAddress { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }

        #region IsCompleted

        public string IsCompleted { get; private set; }

        [NotMapped]
        public YesNoOption CompletedYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsCompleted);
            }
            set
            {
                this.IsCompleted = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }

        public ECFMGDetail ECFMGDetail { get; set; }

        public string CertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
