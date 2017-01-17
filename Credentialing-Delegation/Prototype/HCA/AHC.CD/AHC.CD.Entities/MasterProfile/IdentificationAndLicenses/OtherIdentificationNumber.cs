using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class OtherIdentificationNumber
    {
        public OtherIdentificationNumber()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int OtherIdentificationNumberID { get; set; }

        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string NPINumber { get; set; }

        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string CAQHNumber { get; set; }

        #region NPI UserName & Password

        //[Required]
        //public int NPIAuthenticationDetailID { get; set; }
        //[ForeignKey("NPIAuthenticationDetailID")]
        public OtherAuthenticationDetail NPIAuthenticationDetail { get; set; }

        #endregion

        #region CAQH UserName & Password

        //[Required]
        //public int CAQHAuthenticationDetailID { get; set; }
        //[ForeignKey("CAQHAuthenticationDetailID")]
        public OtherAuthenticationDetail CAQHAuthenticationDetail { get; set; }

        #endregion

        public string UPINNumber { get; set; }

        public string USMLENumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
