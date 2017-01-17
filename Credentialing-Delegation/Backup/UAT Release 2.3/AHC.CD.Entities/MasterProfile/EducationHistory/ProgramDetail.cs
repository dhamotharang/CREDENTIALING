using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class ProgramDetail
    {
        public ProgramDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProgramDetailID { get; set; }

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

        //[Required]
        public int? SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public Specialty Specialty { get; set; }

        #endregion

        #region Preference

        //[Required]
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

        public string HospitalName { get; set; }

        //[Required]
        public string DirectorName { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? EndDate { get; set; }

        //[Required]
        public string DocumentPath { get; set; }
        
        public virtual SchoolInformation SchoolInformation { get; set; }

        #region IsCompleted

        //[Required]
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

        public string InCompleteReason { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion        

        public ICollection<ProgramDetailHistory> ProgramDetailHistory { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}