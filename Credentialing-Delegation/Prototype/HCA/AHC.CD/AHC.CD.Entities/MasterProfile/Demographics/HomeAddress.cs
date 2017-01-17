using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class HomeAddress
    {
        public HomeAddress()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int HomeAddressID { get; set; }

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

        [Column(TypeName = "datetime2")]        
        [Required]
        public DateTime LivingFromDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime LivingEndDate { get; set; }

        #region IsPresentlyStaying

        [Required]
        public string IsPresentlyStaying { get; private set; }

        [NotMapped]
        public YesNoOption IsPresentlyStayingYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsPresentlyStaying);
            }
            set
            {
                this.IsPresentlyStaying = value.ToString();
            }
        }

        #endregion

        #region AddressPreference

        [Required]
        public string AddressPreference { get; private set; }

        [NotMapped]
        public PreferenceType AddressPreferenceType
        {
            get
            {
                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.AddressPreference);
            }
            set
            {
                this.AddressPreference = value.ToString();
            }
        }
        
        #endregion

        #region Status

        [Required]
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
