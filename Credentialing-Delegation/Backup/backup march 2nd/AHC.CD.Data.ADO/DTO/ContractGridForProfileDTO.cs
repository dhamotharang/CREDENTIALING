using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.DTO
{
    public class ContractGridForProfileDTO
    {
        public int ContractGridID { get; set; }


        public string ProfileID { get; set; }
        public string PlanName { get; set; }
        public string BE { get; set; }
        public string LOBCode { get; set; }
        public string LOBName { get; set; }

        public string InitiatedDate { get; set; }
        public string ParticiPationStatus { get; set; }
        public string GroupID { get; set; }
        public string IndividualID { get; set; }
        public string Status { get; set; }
        public string ContractGridStatus { get; set; }
        public StatusType? ContractGridStatusType { get; set; }
        public string CredentialingContractInfoPlanID { get; set; }
        public string EffectiveDate { get; set; }
        public string TerminationDate { get; set; }
        public string PracticeLocationCorporateName { get; set; }
        public string PanelStatus { get; set; }
        public string FacilityName { get; set; }
        public string FacilityStreet { get; set; }
        public string FacilityCity { get; set; }
        public string FacilityState { get; set; }
        public string FacilityCountry { get; set; }
    }
}
