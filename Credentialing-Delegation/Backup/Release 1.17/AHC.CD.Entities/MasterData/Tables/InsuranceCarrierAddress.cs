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
    public class InsuranceCarrierAddress
    {
        public InsuranceCarrierAddress()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int InsuranceCarrierAddressID { get; set; }

        public string LocationName { get; set; }

        #region Address

        [Required]
        public string Building { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        public string County { get; set; }

        [Required]
        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion        

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

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
