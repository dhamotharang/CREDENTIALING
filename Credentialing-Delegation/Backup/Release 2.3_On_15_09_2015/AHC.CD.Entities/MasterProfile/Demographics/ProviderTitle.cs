using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class ProviderTitle
    {
        public ProviderTitle()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        
        public int ProviderTitleID { get; set; }

        [Required]
        public int ProviderTypeId { get; set; }
        [ForeignKey("ProviderTypeId")]
        public ProviderType ProviderType { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
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
        public virtual DateTime LastUpdatedDateTime { get; private set; }
    }
}
