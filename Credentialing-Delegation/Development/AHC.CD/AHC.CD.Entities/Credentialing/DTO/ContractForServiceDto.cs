using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Account.Branch;

namespace AHC.CD.Entities.Credentialing.DTO
{
  public  class ContractForServiceDto
    {
        public string LOB { get; set; }
        public string LOBName { get; set; }
        public string Status { get; set; }
        public string PlanName { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Speciality { get; set; }
        public FacilityDTOForMobileApp Facility { get; set; }
        public string PanelStatus { get; set; }
    }
    public class FacilityDTOForMobileApp
    {
        public string           City { get;set; }
      public string      State        {get;set;}
      public string      Country      {get;set;}
      public string      County       {get;set;}
      public string      FacilityName {get;set;}
      public string      Building     {get;set;}
      public string      Street       {get;set;}
      public string    ZipCode { get; set; }
    }
}
