using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.DTO
{
    public class ContractGridDTO
    {
        public int ContractGridID { get; set; }
        public string PlanName { get; set; }
        public string LOB { get; set; }
        public string ParticiPationStatus { get; set; }
        public string GroupID { get; set; }
        public string IndividualID { get; set; }
        public string EffectiveDate { get; set; }
        public string TerminationDate { get; set; }
        public StatusType? ContractGridStatusType { get; set; }
    }
}

