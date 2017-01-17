using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace PortalTemplate.Areas.Billing.Models.CMS1500.Old
{
    public class HCFA1500ViewModel
    {

        public int ClaimForm_837_PID { get; set; }

        public long? ClaimNumber { get; set; }

        public string FromDOS { get; set; }

        public string PatientName { get; set; }

        public string TotalCharges { get; set; }

        public string InsuranceCompany { get; set; }

        public string SecondaryClaim { get; set; }

        public string Status { get; set; }

        public string ReasonForRejection { get; set; }

        public string ReasonToHold { get; set; }

        public string UserName { get; set; }
      

        public DateTime? FormCreatedDate { get; set; }

       

        public int formTypeID { get; set; }

        public int claimFormStatusID { get; set; }

        public int Claims_Professional_837ID { get; set; }

        public Claims_Professional_837ViewModel Claims_Professional { get; set; }

        public string FileType { get; set; }
        public string MemberID { get; set; }
        public string FacilityID { get; set; }
        public string RenderingProviderID { get; set; }
        public string BillingProviderID { get; set; }
        public string ReferringProviderID { get; set; }
        public string SupervisingProviderID { get; set; }
        public string AccountID { get; set; }
        public string Billername { get; set; }
        public string AccountName { get; set; }

        public string RoleID { get; set; }
        public string PCPID { get; set; }
        public string ReferenceTraceNo { get; set; }

        public string AccessableID { get; set; }

        public string PatientCtrlNo { get; set; }

        public string Pre_PatientCtrlNo { get; set; }

        public string SecondaryStatus { get; set; }
        public string Source { get; set; }

        //---------------------------------old-----------------------------------

      //  public int ClaimForm_837_PID { get; set; }

      //  public string FileType { get; set; }
      //  public long? ClaimNumber { get; set; }

      //  public string FromDOS { get; set; }

      //  public string PatientName { get; set; }

      //  public string TotalCharges { get; set; }

      //  public string InsuranceCompany { get; set; }

      //  public string SecondaryClaim { get; set; }

      //  public string Status { get; set; }

      //  public string ReasonForRejection { get; set; }

      //  public string ReasonToHold { get; set; }

      //  //[ForeignKey("PayerID")]
      //  //public MIRRA.DataLayer.Insurance.Insurance Payer { get; set; }

      //  //public long PatientID { get; set; }

      //  //[ForeignKey("PatientID")]
      //  //public MIRRA.DataLayer.Member.Member Patient { get; set; }

      //  public DateTime? FormCreatedDate { get; set; }

      //  //public long SubscriberID { get; set; }

      //  //[ForeignKey("SubscriberID")]
      //  //public MIRRA.DataLayer.Member.Member Subscriber { get; set; }

      //  //public int statusID { get; set; }

      //  //[ForeignKey("statusID")]
      //  //public Status status { get; set; }

      ////  public virtual Subscriber_Professional_837 Subscriber_Professional_837 { get; set; }

      //  public int formTypeID { get; set; }

      //  public int claimFormStatusID { get; set; }

      //  //public FormType formType { get; set; }

      //  //public ClaimFormStatus claimFormStatus { get; set; }

      //  public int Claims_Professional_837ID { get; set; }

      //  public Claims_Professional_837ViewModel Claims_Professional { get; set; }
      
      //  public string MemberID { get; set; }
      //  public string FacilityID { get; set; }
      //  public string RenderingProviderID { get; set; }
      //  public string BillingProviderID { get; set; }
      //  public string ReferringProviderID { get; set; }
      //  public string SupervisingProviderID { get; set; }
      //  public string AccountID { get; set; }
      //  public string RoleID { get; set; }
      //  public string PCPID { get; set; }


      //  public string ReferenceTraceNo { get; set; }

      //  public string AccessableID { get; set; }

      //  public string PatientCtrlNo { get; set; }
    }
}