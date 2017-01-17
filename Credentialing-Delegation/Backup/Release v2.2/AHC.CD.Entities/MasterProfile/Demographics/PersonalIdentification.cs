using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class PersonalIdentification
    {
        public PersonalIdentification()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PersonalIdentificationID { get; set; }

        [Required]
        public string SocialSecurityNumber { get; private set; }        
        
        [NotMapped]       
        public string SSN
        {
            get { return EncryptorDecryptor.Decrypt(this.SocialSecurityNumber); }
            set { this.SocialSecurityNumber = EncryptorDecryptor.Encrypt(value); }
        }

        public string DL { get; set; }

        public string DLState { get; set; }

        public string SSNCertificatePath { get; set; }
        public string DLCertificatePath { get; set; }

        //public ICollection<PersonalIdentificationHistory> IdentificationHistory { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }

    public class PersonalIdentificationHistory
    {
        public PersonalIdentificationHistory()
        {
            LastModifiedDate = DateTime.Now;
        }
        public int PersonalIdentificationHistoryID { get; set; }
        public string SSN { get; set; }
        public string DL { get; set; }
        public string DLState { get; set; }
        public string SSNCertificatePath { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
