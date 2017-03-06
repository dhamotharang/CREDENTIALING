using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class UpcomingRecredential
    {
        public UpcomingRecredential()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        public int UpcomingRecredentialID { get; set; }
        public int CredentialingInfoID { get; set; }
        public int CredentialingContractRequestID { get; set; }
        public int CredentialingContractInfoFromPlanID { get; set; }
        public int ContractGridID { get; set; }
        public int? PlanID { get; set; }
        //public int? LOBID { get; set; }
        public DateTime? InitialCredentialingDate { get; set; }
        public DateTime? InitiatedDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime? ReCredentialingDate { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string PanelStatus { get; set; }
        public string PlanName { get; set; }        
        public string LOBName { get; set; }
        public string ParticipatingStatus { get; set; }
        public string GroupID { get; set; }
        public string ProviderID { get; set; }
        [NotMapped]
        public List<string> SpecialityName { get; set; }
        public string IPA { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
