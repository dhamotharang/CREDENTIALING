using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpeciality
{
    public class SpecialityBoardCetifiedDetail
    {
        public SpecialityBoardCetifiedDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int SpecialityBoardCetifiedDetailID { get; set; }

        #region SpecialityBoard

        [Required]
        public int SpecialityBoardID { get; set; }
        [ForeignKey("SpecialityBoardID")]
        public SpecialityBoard SpecialityBoard { get; set; }
        
        #endregion        

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime InitialCertificationDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime LastReCerificationDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpirationDate { get; set; }

        public string BoardCertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
