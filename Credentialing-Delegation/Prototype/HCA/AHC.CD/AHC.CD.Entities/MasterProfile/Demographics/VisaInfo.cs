using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class VisaInfo
    {
        public VisaInfo()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int VisaInfoID { get; set; }

        #region IsAuthorizedToWorkInUS
        
        [Required]
        public string IsAuthorizedToWorkInUS { get; private set; }

        [NotMapped]
        public YesNoOption IsAuthorizedToWorkInUSYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsAuthorizedToWorkInUS);
            }
            set
            {
                this.IsAuthorizedToWorkInUS = value.ToString();
            }
        }

        #endregion

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string VisaNumber { get; set; }

        #region VisaStatus
        
        [Required]
        public int VisaStatusID { get; set; }
        [ForeignKey("VisaStatusID")]
        public VisaStatus VisaStatus { get; set; }

        #endregion

        #region VisaType

        [Required]
        public int VisaTypeID { get; set; }
        [ForeignKey("VisaTypeID")]
        public VisaType VisaType { get; set; }

        #endregion

        [Required]
        public string VisaSponsor { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime VisaExpirationDate { get; set; }

        [Required]
        public string VisaCertificatePath { get; set; }

        public string GreenCardNumber { get; set; }

        public string NationalIDNumber { get; set; }

        [Required]
        public string CountryOfIssue { get; set; }

        public string GreenCardCertificatePath { get; set; }

        public string NationalIDCertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
