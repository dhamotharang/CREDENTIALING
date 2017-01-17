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

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string NPINumber { get; set; }

        public string CAQHNumber { get; set; }

        #region NPI UserName & Password

        public string NPIUserName { get; set; }

        public string NPIPassword { get; set; }

        #endregion

        #region CAQH UserName & Password

        public string CAQHUserName { get; set; }

        public string CAQHPassword { get; set; }

        #endregion

        public string UPINNumber { get; set; }

        public string USMLENumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
