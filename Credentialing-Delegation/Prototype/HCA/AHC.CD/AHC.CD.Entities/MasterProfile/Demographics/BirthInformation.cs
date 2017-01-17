using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]         
        public string CityOfBirth { get; set; }

        [Required]       
        public string CountryOfBirth { get; set; }

        [Required]        
        public string CountyOfBirth { get; set; }

        [Required]       
        [Column(TypeName="datetime2")]
        public DateTime DateOfBirth { get; set; }

        [Required]        
        public string StateOfBirth { get; set; }
              
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }       
        
        public string BirthCertificatePath { get; set; }
    }
}
