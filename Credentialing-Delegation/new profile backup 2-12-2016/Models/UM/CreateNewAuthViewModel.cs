using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.UM
{
    public class CreateNewAuthViewModel
    {
        public int AuthorizationID { get; set; }

        public string PlaceOfService { get; set; }

        public string RequestType { get; set; }
             
        public string AuthorizationType { get; set; }

        public string TypeOfCare { get; set; }       
 
        public DateTime? ReceivedDate  { get; set; }
                
        public DateTime? FromDate  { get; set; }
                
        public DateTime? ToDate  { get; set; }
                
        public string OutPatientType { get; set; }


        
        public string PCP  { get; set; }
                
        public string RequestingProvider { get; set; }
                
        public string ServiceProvider { get; set; }
                
        public string Facility { get; set; }
                
        public string TypeOfService { get; set; }
                
        public string UMServiceGroup { get; set; }
                
        public string LevelOfCare { get; set; }
                
        public decimal? ExpectedCharge { get; set; }
                
        public DateTime? ExpectedDOS { get; set; }
                
        public string AttendingProvider { get; set; }

        public string AdmissionProvider { get; set; }

        public string Surgeon { get; set; }

        public string RoomType { get; set; }

        public string DRGCode { get; set; }
        
        public string DRGDescription { get; set; }
        
        public string MDCCode { get; set; }
        
        public string MDCDescription { get; set; }
        
        public string ReviewType { get; set; }
        
        public DateTime? NextReviewDate  { get; set; }
        
        public string LevelRate { get; set; }
        
        public int? DaysUsed { get; set; }
        
        public int? RemainingDays { get; set; }



        public DateTime? AdmissionReceivedDate { get; set; }

        public DateTime? AdmissionRequestedDate { get; set; }

        public DateTime? AdmissionFromDate { get; set; }

        public int? AdmissionApprovedDays { get; set; }

        public DateTime? ExpectedDischargeDate  { get; set; }

        public DateTime? DischargeToDate { get; set; }

        public int? TotalActualLOS  { get; set; }

        public int? TotalRequestedLOS  { get; set; }

        public int? TotalDenialLOS  { get; set; }


        
        public string ICDCode { get; set; }
        
        public string ICDDesc { get; set; }
        
        public string CPTCode { get; set; }
        
        public string CPTDesc { get; set; }
        
        public string Modifier { get; set; }
        
        public int? RequestedUnits { get; set; }
        
        public string Discipline { get; set; }
        
        public string Range1 { get; set; }
        
        public string NoPer { get; set; }
        
        public string Range2 { get; set; }

        public int? TotalUnits { get; set; }

        public bool IncludeLetter { get; set; }



        #region Document 

        public int DocumentID { get; set; }

        public string DocumentType { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public bool IncludeFax { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        #endregion



        #region Contacts

        public int ContactID { get; set; }

        public string ContactType { get; set; }

        public string PrimaryContact { get; set; }

        public string AlternateContact { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string ZipCode { get; set; }

        #endregion



        #region Notes

        public int NoteID { get; set; }

        public string NoteType { get; set; }

        public DateTime? Date { get; set; }

        public string UserName { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        //public bool IncludeFax { get; set; }

        public string NoteStatus { get; set; }

        public string RationaleDescription { get; set; }

        public string AlternatePlanDescription { get; set; }

        public string CriteriaUsedDescription { get; set; }

        public string ServiceSubjectToNotice { get; set; }

        public string MedicalNecessaries { get; set; }

        public bool IsPrivate { get; set; }

        #endregion


        
        #region ODAG

        public int ODAGID { get; set; }

        public int QuestionID { get; set; }

        public string OptionAnswer { get; set; }

        public DateTime? OptionDate { get; set; }

        #endregion

    }
}