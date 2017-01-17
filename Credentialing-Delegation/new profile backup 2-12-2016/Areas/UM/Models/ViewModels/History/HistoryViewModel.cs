using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.History
{
    public class HistoryViewModel
    {
        public int AuthorizationId { get; set; }
        public int? AuthorizationParentID { get; set; }
        public string ServiceProviderName { get; set; }
        public string ServiceProvierNPI { get; set; }
        public string ServiceProvierSpeciality { get; set; }
        public string AttendingProviderName { get; set; }
        public string AttendingProvierNPI { get; set; }
        public string AttendingProvierSpeciality { get; set; }
        public string InitialStatus { get; set; }
        public string LetterStatus { get; set; }
        public string PlanAuthNumber { get; set; }
        public string PCP { get; set; }
        public string NPI { get; set; }
        public string ReferenceNumber { get; set; }
        public string ParentIDReason { get; set; }
        public string RequestType { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string AuthorizationType { get; set; }
        public DateTime? DateOfService { get; set; }
        public string Pos { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string status { get; set; }
        public bool IsActive { get; set; }
        public int? TotalUnits { get; set; }
        public int? ActaulLOS { get; set; }
        public int? ApprovedDays { get; set; }
        public string PrimaryDx { get; set; }
        public string DXDescription { get; set; }
        public string AuthUpdateStatus { get; set; }
        public string LastStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string role { get; set; }
        public double Seconds { get; set; }
        public int? Requestedunit { get; set; }
        public List<int> AuthUnits { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string SubscriberID { get; set; }
        public string Entry { get; set; }
        public string TypeOfCare { get; set; }
        public string AuthStatus { get; set; }
        public string OutPatientType { get; set; }
        public string POSAbb { get; set; }
        public string POSNumnber { get; set; }
        public string TypeOfService { get; set; }
        public DateTime AuthorizationCreatedDate { get; set; }
        public DateTime? ActionDate { get; set; }
        public string AbbreviatedPOS { get; set; }
        public string ReferUserName { get; set; }
        public string ReasonForUpdate { get; set; }
        public bool EnableOverTurn { get; set; }
    }
}