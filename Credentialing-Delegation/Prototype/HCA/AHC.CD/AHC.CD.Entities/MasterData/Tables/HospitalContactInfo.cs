using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class HospitalContactInfo
    {
        public HospitalContactInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int HospitalContactInfoID { get; set; }

        public string LocationName { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string UnitNumber { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Phone { get; set; }
        
        [Required]
        public string Fax { get; set; }
        
        [Required]
        public string Email { get; set; }

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

        public virtual ICollection<HospitalContactPerson> HospitalContactPersons { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
