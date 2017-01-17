using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.DTO
{
    public class ContractGridDTO
    {
        public int ContractGridID { get; set; }
        public string NPINumber { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderMiddleName { get; set; }
        public string ProviderLastName { get; set; }
        public string ProfileID { get; set; }
        public string PlanName { get; set; }
        public string BE { get; set; }
        public string LOBCode { get; set; }
        public string ProviderID { get; set; }
        public string InitiatedDate { get; set; }
        public string ParticipatingStatus { get; set; }
        public string GroupID { get; set; }
        public string IndividualID { get; set; }
        public string Status { get; set; }
        public string CredentialingContractInfoPlanID { get; set; }
        public string EffectiveDate { get; set; }
        public string TerminationDate { get; set; }
    }
}
