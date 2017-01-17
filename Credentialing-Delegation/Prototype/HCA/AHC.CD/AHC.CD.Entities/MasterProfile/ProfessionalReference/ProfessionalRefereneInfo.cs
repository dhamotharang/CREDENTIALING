using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfessionalReference
{
    public class ProfessionalReferenceInfo
    {
        public ProfessionalReferenceInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfessionalReferenceInfoID { get; set; }

        #region ProviderType

        [Required]
        public int ProviderTypeID { get; set; }
        [ForeignKey("ProviderTypeID")]
        public ProviderType ProviderType { get; set; }

        #endregion

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Degree { get; set; }

        #region Speciality

        [Required]
        public int SpecialityID { get; set; }
        [ForeignKey("SpecialityID")]
        public Speciality Speciality { get; set; }

        #endregion
        
        public string Relationship { get; set; }

        #region IsBoardCerified

        public string IsBoardCerified { get; private set; }

        [NotMapped]
        public YesNoOption BoardCerifiedOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsBoardCerified);
            }
            set
            {
                this.IsBoardCerified = value.ToString();
            }
        }

        #endregion

        public string Email { get; set; }
        
        [Required]
        public string UnitNumber { get; set; }
        
        [Required]
        public string Building { get; set; }
        
        [Required]
        public string Street { get; set; }
        
        [Required]
        public string State { get; set; }
        
        [Required]
        public string County { get; set; }
        
        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
        
        [Required]
        public string Zipcode { get; set; }
        
        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Fax { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
