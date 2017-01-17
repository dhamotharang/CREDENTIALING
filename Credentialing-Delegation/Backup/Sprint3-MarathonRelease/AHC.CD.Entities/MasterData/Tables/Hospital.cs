using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class Hospital
    {
        public Hospital()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int HospitalID { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Index(IsUnique=true)]
        public string HospitalName { get; set; }

        [MaxLength(100)]
        //[Index(IsUnique = true)] // Do not uncomment
        public string Code { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        public ICollection<HospitalContactInfo> HospitalContactInfoes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }        
    }
}
