using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class FederalDEAInformation
    {
        public FederalDEAInformation()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int FederalDEAInformationID { get; set; }

        [Required]
        [MaxLength(100)]
        //[Index(IsUnique = true)]
        public string DEANumber { get; set; }

        [Required]
        public string StateOfReg { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ExpiryDate { get; set; }

        //[Required]
        public virtual ICollection<DEAScheduleInfo> DEAScheduleInfoes { get; set; }

        #region IsInGoodStanding

        //[Required]
        public string IsInGoodStanding { get; set; }

        [NotMapped]
        public YesNoOption? GoodStandingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsInGoodStanding))
                    return null;

                if (this.IsInGoodStanding.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsInGoodStanding);
            }
            set
            {
                this.IsInGoodStanding = value.ToString();
            }
        }

        #endregion

        public string DEALicenceCertPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

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

        public virtual ICollection<FederalDEAInfoHistory> FederalDEAInfoHistory { get; set; }
    }
}
