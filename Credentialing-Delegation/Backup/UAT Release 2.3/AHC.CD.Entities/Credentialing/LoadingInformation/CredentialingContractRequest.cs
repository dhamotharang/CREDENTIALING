using AHC.CD.Entities.Credentialing.DelegationProfileReport;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.PackageGenerate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class  CredentialingContractRequest
    {
        public CredentialingContractRequest()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingContractRequestID { get; set; }

        public int CredentialingContractLoadedByID { get; set; }
        [ForeignKey("CredentialingContractLoadedByID")]
        public CDUser CredentialingContractLoadedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? InitialCredentialingDate { get; set; }

        public ICollection<ContractSpecialty> ContractSpecialties { get; set; }

        #region All Specialties

        public string AllSpecialtiesSelected { get; private set; }

        [NotMapped]
        public YesNoOption? AllSpecialtiesSelectedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.AllSpecialtiesSelected))
                    return null;

                if (this.AllSpecialtiesSelected.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AllSpecialtiesSelected);
            }
            set
            {
                this.AllSpecialtiesSelected = value.ToString();
            }
        }

        #endregion

        public ICollection<ContractPracticeLocation> ContractPracticeLocations { get; set; }

        #region All Practice Locations

        public string AllPracticeLocationsSelected { get; private set; }

        [NotMapped]
        public YesNoOption? AllPracticeLocationsSelectedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.AllPracticeLocationsSelected))
                    return null;

                if (this.AllPracticeLocationsSelected.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AllPracticeLocationsSelected);
            }
            set
            {
                this.AllPracticeLocationsSelected = value.ToString();
            }
        }

        #endregion

        public ICollection<ContractLOB> ContractLOBs { get; set; }

        #region All LOBs

        public string AllLOBsSelected { get; private set; }

        [NotMapped]
        public YesNoOption? AllLOBsSelectedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.AllLOBsSelected))
                    return null;

                if (this.AllLOBsSelected.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AllLOBsSelected);
            }
            set
            {
                this.AllLOBsSelected = value.ToString();
            }
        }

        #endregion

        public int? BusinessEntityID { get; set; }
        [ForeignKey("BusinessEntityID")]
        public OrganizationGroup BusinessEntity { get; set; }

        public ICollection<ContractGrid> ContractGrid { get; set; }

        public ICollection<ProfileReport> ProfileReports { get; set; }

        public PackageGeneratorReport PackageGeneratorReport { get; set; }

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

        #region ContractRequestStatus

        public string ContractRequestStatus { get; private set; }

        [NotMapped]
        public StatusType? ContractRequestStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ContractRequestStatus))
                    return null;

                if (this.ContractRequestStatus.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.ContractRequestStatus);
            }
            set
            {
                this.ContractRequestStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
