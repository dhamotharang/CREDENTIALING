using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class ProviderType
    {
        public ProviderType()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ProviderTypeID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique=true)]
        public string Title { get; set; }

        public string Description { get; set; }

        // [Required]
        [MaxLength(100)]
        //[Index(IsUnique = true)]
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

        public ProviderLevel ProviderLevel { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        
        public ICollection<Specialty> Specialities { get; set; }
        public ICollection<SpecialtyBoard> SpecialtyBoards { get; set; }
    }
}
