using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class ContractPracticeLocation
    {
        public ContractPracticeLocation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContractPracticeLocationID { get; set; }

        public int? ProfilePracticeLocationID { get; set; }
        [ForeignKey("ProfilePracticeLocationID")]
        public PracticeLocationDetail ProfilePracticeLocation { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
