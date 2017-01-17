using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.Loading
{
    public class PlanReportHistory
    {
        public PlanReportHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanReportHistoryID { get; set; }

        public string ProviderID { get; set; }

        public string GroupID { get; set; }

        public int? InitiatedByID { get; set; }
        [ForeignKey("InitiatedByID")]
        public CDUser InitiatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime InitiatedDate { get; set; }

        public int? CredentialedByID { get; set; }
        [ForeignKey("CredentialedByID")]
        public CDUser CredentialedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CredentialedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TerminationDate { get; set; }

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
