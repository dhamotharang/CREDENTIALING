using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.UtilityService;
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

        #region Visa Number

        public string VisaNumberStored { get; private set; }

        [NotMapped]
        public string VisaNumber
        {
            get { return EncryptorDecryptor.Decrypt(this.VisaNumberStored); }
            set { this.VisaNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region Green Card Number

        public string GreenCardNumberStored { get; private set; }

        [NotMapped]
        public string GreenCardNumber
        {
            get { return EncryptorDecryptor.Decrypt(this.GreenCardNumberStored); }
            set { this.GreenCardNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region National ID Number

        public string NationalIDNumberNumberStored { get; private set; }

        [NotMapped]
        public string NationalIDNumber
        {
            get { return EncryptorDecryptor.Decrypt(this.NationalIDNumberNumberStored); }
            set { this.NationalIDNumberNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region VisaStatus
        
        public int? VisaStatusID { get; set; }
        [ForeignKey("VisaStatusID")]
        public VisaStatus VisaStatus { get; set; }

        #endregion

        #region VisaType

        public int? VisaTypeID { get; set; }
        [ForeignKey("VisaTypeID")]
        public VisaType VisaType { get; set; }

        #endregion

        public string VisaSponsor { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime VisaExpirationDate { get; set; }

        public string VisaCertificatePath { get; set; }

        public string CountryOfIssue { get; set; }

        public string GreenCardCertificatePath { get; set; }

        public string NationalIDCertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
