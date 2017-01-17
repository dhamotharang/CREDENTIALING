using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CreateClaim
{
    public class ServiceLineViewModels
    {

        public string claimsProcedure { get; set; }

        public string Modifier1 { get; set; }

        public string Modifier2 { get; set; }

        public string Modifier3 { get; set; }

        public string Modifier4 { get; set; }

        public string DiagnosisPointer1 { get; set; }

        public string DiagnosisPointer2 { get; set; }

        public string DiagnosisPointer3 { get; set; }

        public string DiagnosisPointer4 { get; set; }

        public double UnitCharges { get; set; }

        public int Unit { get; set; }

        public double Charges { get; set; }

        public string EMG { get; set; }

        public string EPSDT { get; set; }

        //--------------------NUCC------------------------//
        [DisplayName("LINE NOTE")]
        public string LineNoteText { get; set; }

        [DisplayName("ANES START")]
        public string AnesStart { get; set; }

        [DisplayName("STOP")]
        public string AnesStop { get; set; }

        [DisplayName("NDC QUAL")]
        public double NationalDrugCodeQlfr { get; set; }

        [DisplayName("NDC CODE")]
        public int NationalDrugCode { get; set; }

        [DisplayName("NDC QTY")]
        public double NationalDrugUnitCount { get; set; }

        [DisplayName("NDC QTY QUAL")]
        public string NDCUnitMeasurementCode { get; set; }

        [DisplayName("NDC UNIT PRICE")]
        public string NationalDrugUnitPrice { get; set; }


    }
}