using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Contract
{
    public class ContractInfo
    {
        public ContractInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContractInfoID { get; set; }

        #region Provider Relationship

        [Required]
        public string ProviderRelationship { get; set; }

        [NotMapped]
        public ProviderRelationshipOption? ProviderRelationshipOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ProviderRelationship))
                    return null;

                if (this.ProviderRelationship.Equals("Not Available"))
                    return null;

                return (ProviderRelationshipOption)Enum.Parse(typeof(ProviderRelationshipOption), this.ProviderRelationship);
            }
            set
            {
                this.ProviderRelationship = value.ToString();
            }
        }

        #endregion

        public int? OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? JoiningDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; }

        public string IndividualTaxId { get; set; }

        [NotMapped]
        public long ContractFileSize { get; set; }

        public string ContractFilePath { get; set; }

        public virtual ICollection<ContractGroupInfo> ContractGroupInfoes { get; set; }

        #region Contract Status

        public string ContractStatus { get; set; }

        [NotMapped]
        public ContractStatus? ContractStatusOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ContractStatus))
                    return null;

                if (this.ContractStatus.Equals("Not Available"))
                    return null;

                return (ContractStatus)Enum.Parse(typeof(ContractStatus), this.ContractStatus);
            }
            set
            {
                this.ContractStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }  
    }
}
