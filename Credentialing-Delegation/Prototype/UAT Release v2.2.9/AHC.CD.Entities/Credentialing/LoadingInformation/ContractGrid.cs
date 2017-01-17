using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class ContractGrid
    {
        public ContractGrid()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContractGridID { get; set; }

        public int? ProfileSpecialtyID { get; set; }
        [ForeignKey("ProfileSpecialtyID")]
        public SpecialtyDetail ProfileSpecialty { get; set; }

        public int? ProfilePracticeLocationID { get; set; }
        [ForeignKey("ProfilePracticeLocationID")]
        public PracticeLocationDetail ProfilePracticeLocation { get; set; }

        public int? LOBID { get; set; }
        [ForeignKey("LOBID")]
        public LOB LOB { get; set; }

        public int? CredentialingInfoID { get; set; }
        [ForeignKey("CredentialingInfoID")]
        public CredentialingInfo CredentialingInfo { get; set; }

        public int? BusinessEntityID { get; set; }
        [ForeignKey("BusinessEntityID")]
        public OrganizationGroup BusinessEntity { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? InitialCredentialingDate { get; set; }

        public CredentialingContractInfoFromPlan Report { get; set; }

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
        public DateTime LastModifiedDate { get; set; }
    }
}
