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
        [Index(IsUnique = true)]
        public string DEANumber { get; set; }

        [Required]
        public string StateOfReg { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        public ICollection<DEAScheduleInfo> DEAScheduleInfoes { get; set; }

        #region IsInGoodStanding

        [Required]
        public string IsInGoodStanding { get; private set; }

        [NotMapped]
        public YesNoOption GoodStandingYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsInGoodStanding);
            }
            set
            {
                this.IsInGoodStanding = value.ToString();
            }
        }

        #endregion

        //[Required]
        //public string LimitedOrRestricted { get; private set; }

        //[NotMapped]
        //public LimitedRestricted LimitedRestricted
        //{
        //    get
        //    {
        //        return (LimitedRestricted)Enum.Parse(typeof(LimitedRestricted), this.LimitedOrRestricted);
        //    }
        //    set
        //    {
        //        this.LimitedOrRestricted = value.ToString();
        //    }
        //}

        //public string RestrictionExplanation { get; set; }

        //[Required]
        //public bool HasStateControlledSubstanceRegCertificate { get; set; }

        //public CDSCInformation CDSInformation { get; set; }

        public string DEALicenceCertPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType StatusType
        {
            get
            {
                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        public virtual ICollection<FederalDEAInfoHistory> FederalDEAInfoHistories { get; set; }
    }
}
