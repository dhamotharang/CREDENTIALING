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

        #region IsUSGraduate

        [Required]
        public string IsUSGraduate { get; set; }

        [NotMapped]
        public YesNoOption? USGraduateYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsUSGraduate))
                    return null;

                if (this.IsUSGraduate.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsUSGraduate);
            }
            set
            {
                this.IsUSGraduate = value.ToString();
            }
        }

        #endregion

        #region GraduationType

        [Required]
        public string GraduationType { get; set; }

        [NotMapped]
        public EducationGraduateType? GraduateType
        {
            get
            {
                if (String.IsNullOrEmpty(this.GraduationType))
                    return null;

                if (this.GraduationType.Equals("Not Available"))
                    return null;

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
        public string QualificationType { get; set; }

        [NotMapped]
        public EducationQualificationType? EducationQualificationType
        {
            get
            {
                if (String.IsNullOrEmpty(this.QualificationType))
                    return null;

                if (this.QualificationType.Equals("Not Available"))
                    return null;

                return (EducationQualificationType)Enum.Parse(typeof(EducationQualificationType), this.QualificationType);
            }
            set
            {
                this.QualificationType = value.ToString();
            }
        }

        #endregion        

        [Required]
        public string QualificationDegree { get; set; }

        public virtual SchoolInformation SchoolInformation { get; set; }        

        #region IsCompleted

        [Required]
        public string IsCompleted { get; set; }

        [NotMapped]
        public YesNoOption? CompletedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsCompleted))
                    return null;

                if (this.IsCompleted.Equals("Not Available"))
                    return null;

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

        //[Required]
        public string CertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
