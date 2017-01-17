using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class BirthInformation
    {
        public BirthInformation()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int BirthInformationID { get; set; }

        //[Required]         
        public string CityOfBirth { get; set; }

        //[Required]       
        public string CountryOfBirth { get; set; }

        public string CountyOfBirth { get; set; }

        [Required]
        public string DateOfBirthStored { get; private set; }        
        
        [NotMapped]
        public string DateOfBirth
        {
            get 
            {
                var encryptData = EncryptorDecryptor.Decrypt(this.DateOfBirthStored);
                //CultureInfo culture = new CultureInfo("en-US");
                // return date as string
                //return Convert.ToDateTime(encryptData,culture);
                return encryptData;
            }
            set { this.DateOfBirthStored = EncryptorDecryptor.Encrypt(value.ToString()); }
        }

        //[Required]        
        public string StateOfBirth { get; set; }
              
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        [NotMapped]
        public long BirthCertificateSize { get; set; }

        public string BirthCertificatePath { get; set; }
    }
}
