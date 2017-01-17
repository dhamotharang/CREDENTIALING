using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class File835EOBList
    {
        #region unused
        //public int FileID { get; set; }

        //public string PayerName { get; set; }

        //public string SecondaryPayerName { get; set; }

        //public string PatientName { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        //public DateTime? DateOfService { get; set; }

        //public string CPT { get; set; }

        //public string Billed { get; set; }

        //public string Allowed { get; set; }

        //public string Adjusted { get; set; }

        //public string Denied { get; set; }

        //public string CoInsurence { get; set; }

        //public string Amount { get; set; }

        //public string Penalty { get; set; }

        //public string Paid { get; set; }

        //public string Reason { get; set; }

        //public string ReasonDescription { get; set; } 
        #endregion

        public File835EOBList()
        {
            PenaltyAdjDetails = new List<AdjustmentDetails>();
            NonPenaltyAdjDetails = new List<AdjustmentDetails>();
            ContractualObligationsAdjustmentDetails = new List<AdjustmentDetails>();
            OtherAdjustmentDetails = new List<AdjustmentDetails>();
            PayerInitiatedAdjustmentDetails = new List<AdjustmentDetails>();
            PayerResponsiblityDetails = new List<AdjustmentDetails>();
        }
        public int? SSRSOnRenderingProviderID { get; set; }

        public string PayerName { get; set; }

        public string PatientName { get; set; }

        public string SecondaryPayerName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DateOfService { get; set; }

        public string PaymentDate { get; set; }

        public string CPT { get; set; }

        public string Billed { get; set; } // Charge

        public double? Allowed { get; set; } //Allow

        public double? Adj { get; set; }

        public double? Ded { get; set; }

        public double? CoIns { get; set; }

        public double? PaymentAmount { get; set; }

        public double? Penalty { get; set; }

        public double? Check { get; set; }

        public string Paid { get; set; } //Payment

        public string HICN { get; set; }
        public string AccountNumber { get; set; }
        public string ICN { get; set; }

        public List<AdjustmentDetails> PenaltyAdjDetails { get; set; }
        public List<AdjustmentDetails> NonPenaltyAdjDetails { get; set; }
        public List<AdjustmentDetails> ContractualObligationsAdjustmentDetails { get; set; }
        public List<AdjustmentDetails> OtherAdjustmentDetails { get; set; }
        public List<AdjustmentDetails> PayerInitiatedAdjustmentDetails { get; set; }
        public List<AdjustmentDetails> PayerResponsiblityDetails { get; set; }
        public List<AdjustmentDetails> DedDetails { get; set; }


    }
}