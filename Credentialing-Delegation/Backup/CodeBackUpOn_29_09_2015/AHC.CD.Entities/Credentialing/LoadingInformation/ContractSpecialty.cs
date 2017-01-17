using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class ContractSpecialty
    {
        public ContractSpecialty()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContractSpecialtyID { get; set; }

        public int? ProfileSpecialtyID { get; set; }
        [ForeignKey("ProfileSpecialtyID")]
        public SpecialtyDetail ProfileSpecialty { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
