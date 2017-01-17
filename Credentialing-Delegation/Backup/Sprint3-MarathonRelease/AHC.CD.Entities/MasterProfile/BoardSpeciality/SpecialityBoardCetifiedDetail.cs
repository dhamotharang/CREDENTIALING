using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpecialty
{
    public class SpecialtyBoardCertifiedDetail
    {
        public SpecialtyBoardCertifiedDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int SpecialtyBoardCertifiedDetailID { get; set; }

        public string CertificateNumber { get; set; }      

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime InitialCertificationDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? LastReCerificationDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpirationDate { get; set; }

        //[Required]
        public string BoardCertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<SpecialtyBoardCertifiedDetailHistory> SpecialtyBoardCertifiedDetailHistory { get; set; }
    }
}
