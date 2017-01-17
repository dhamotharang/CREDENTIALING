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
    public class ProgramDetailHistory
    {
        public ProgramDetailHistory()
        {
            LastModifiedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now.ToUniversalTime();
        }

        public int ProgramDetailHistoryID { get; set; }

        #region ProgramType

        //[Required]
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

        #region History Status

        public string HistoryStatus { get; private set; }

        [NotMapped]
        public HistoryStatusType? HistoryStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.HistoryStatus))
                    return null;

                if (this.HistoryStatus.Equals("Not Available"))
                    return null;

                return (HistoryStatusType)Enum.Parse(typeof(HistoryStatusType), this.HistoryStatus);
            }
            set
            {
                this.HistoryStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public int? DeletedById { get; set; }
        [ForeignKey("DeletedById")]
        public CDUser DeletedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedDate { get; set; }
    }
}
