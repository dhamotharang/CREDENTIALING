using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class HospitalContactPerson
    {
        public HospitalContactPerson()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int HospitalContactPersonID { get; set; }
        
        [Required]
        public string ContactPersonName { get; set; }

        [Required]
        public string ContactPersonPhone { get; set; }

        [Required]
        public string ContactPersonFax { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType StatusType
        {
            get
            {
                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
