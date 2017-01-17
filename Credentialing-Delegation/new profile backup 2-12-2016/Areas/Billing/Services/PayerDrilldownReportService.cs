using PortalTemplate.Areas.Billing.Models.PayerDrilldownReport;
using PortalTemplate.Areas.Billing.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Services
{
    public class PayerDrilldownReportService : IPayerDrilldownReportService
    {
        readonly List<CPayerListViewModel> cPayerList;
        readonly List<APayerListViewModel> aPayerList;
        readonly List<CMemberListViewModel> cMemberList;
        readonly List<AMemberListViewModel> aMemberList;
        readonly List<CBillingProviderListViewModel> cBillingProviderList;
        readonly List<ABillingProviderListViewModel> aBillingProviderList;
        readonly List<CRenderingProviderListViewModel> cRenderingProvider;
        readonly List<ARenderingProviderListViewModel> aRenderingProvider;

        public PayerDrilldownReportService()
        {
            cPayerList = new List<CPayerListViewModel>();
            aPayerList = new List<APayerListViewModel>();
            cMemberList = new List<CMemberListViewModel>();
            aMemberList = new List<AMemberListViewModel>();
            cBillingProviderList = new List<CBillingProviderListViewModel>();
            aBillingProviderList = new List<ABillingProviderListViewModel>();
            cRenderingProvider = new List<CRenderingProviderListViewModel>();
            aRenderingProvider = new List<ARenderingProviderListViewModel>();

            cPayerList.Add(new CPayerListViewModel { ID = 1, Payer = "FREEDOM HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "0", ClaimsRejected = "542" });
            cPayerList.Add(new CPayerListViewModel { ID = 2, Payer = "ULTIMATE HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "532", ClaimsRejected = "1" });
            cPayerList.Add(new CPayerListViewModel { ID = 3, Payer = "COVENTRY HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "0", ClaimsRejected = "542" });
            cPayerList.Add(new CPayerListViewModel { ID = 4, Payer = "HUMANA HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "0", ClaimsRejected = "542" });
            cPayerList.Add(new CPayerListViewModel { ID = 1, Payer = "FREEDOM HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "0", ClaimsRejected = "542" });
            cPayerList.Add(new CPayerListViewModel { ID = 2, Payer = "ULTIMATE HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "532", ClaimsRejected = "1" });
            cPayerList.Add(new CPayerListViewModel { ID = 3, Payer = "COVENTRY HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "0", ClaimsRejected = "542" });
            cPayerList.Add(new CPayerListViewModel { ID = 4, Payer = "HUMANA HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "0", ClaimsRejected = "542" });
            cPayerList.Add(new CPayerListViewModel { ID = 1, Payer = "FREEDOM HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "0", ClaimsRejected = "542" });
            cPayerList.Add(new CPayerListViewModel { ID = 2, Payer = "ULTIMATE HEALTH PLAN", BillingProviderCount = "1", ClaimsSubmitted = "542", ClaimsAccepted = "0", ClaimsPending = "532", ClaimsRejected = "1" });

            aPayerList.Add(new APayerListViewModel { ID = 1, Payer = "FREEDOM HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 2, Payer = "ULTIMATE HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 3, Payer = "COVENTRY HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 4, Payer = "HUMANA HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 1, Payer = "FREEDOM HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 2, Payer = "ULTIMATE HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 3, Payer = "COVENTRY HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 4, Payer = "HUMANA HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 1, Payer = "FREEDOM HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });
            aPayerList.Add(new APayerListViewModel { ID = 2, Payer = "ULTIMATE HEALTH PLAN", BillingProviderCount = "1", Billed = "542", Allowed = "0", Paid = "0", Adj = "542", Ded = "0", Pending = "0", Denied = "0" });



            cMemberList.Add(new CMemberListViewModel { ID = 1, MemberName = "BERNARDO D HENAO", ClaimAccepted = "20", ClaimSubmitted = "20", ClaimPending = "0", ClaimRejected = "0" });
            cMemberList.Add(new CMemberListViewModel { ID = 2, MemberName = "FLORINE MCLEAN", ClaimAccepted = "12", ClaimSubmitted = "22", ClaimPending = "10", ClaimRejected = "0" });
            cMemberList.Add(new CMemberListViewModel { ID = 3, MemberName = "WILMA M MARESSA", ClaimAccepted = "30", ClaimSubmitted = "33", ClaimPending = "0", ClaimRejected = "3" });
            cMemberList.Add(new CMemberListViewModel { ID = 4, MemberName = "RICHARD H BOHLMAN", ClaimAccepted = "24", ClaimSubmitted = "24", ClaimPending = "0", ClaimRejected = "0" });
            cMemberList.Add(new CMemberListViewModel { ID = 5, MemberName = "CLARA M POFI", ClaimAccepted = "5", ClaimSubmitted = "22", ClaimPending = "10", ClaimRejected = "7" });
            cMemberList.Add(new CMemberListViewModel { ID = 6, MemberName = "YOLANDA K MERWIN", ClaimAccepted = "8", ClaimSubmitted = "8", ClaimPending = "0", ClaimRejected = "0" });
            cMemberList.Add(new CMemberListViewModel { ID = 7, MemberName = "MILLE J LOCKARD", ClaimAccepted = "9", ClaimSubmitted = "11", ClaimPending = "1", ClaimRejected = "1" });
            cMemberList.Add(new CMemberListViewModel { ID = 8, MemberName = "DOROTHY C FLYNN", ClaimAccepted = "3", ClaimSubmitted = "13", ClaimPending = "7", ClaimRejected = "3" });
            cMemberList.Add(new CMemberListViewModel { ID = 9, MemberName = "WENDY C LEE", ClaimAccepted = "14", ClaimSubmitted = "14", ClaimPending = "0", ClaimRejected = "0" });
            cMemberList.Add(new CMemberListViewModel { ID = 10, MemberName = "ALLENE STRICKLAND", ClaimAccepted = "10", ClaimSubmitted = "10", ClaimPending = "0", ClaimRejected = "0" });

            aMemberList.Add(new AMemberListViewModel { ID = 1, MemberName = "BERNARDO D HENAO", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aMemberList.Add(new AMemberListViewModel { ID = 2, MemberName = "FLORINE MCLEAN", Billed = "55.21", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "0.00", Pending = "55.21" });
            aMemberList.Add(new AMemberListViewModel { ID = 3, MemberName = "WILMA M MARESSA", Billed = "214.00", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "214.00", Pending = "0.00" });
            aMemberList.Add(new AMemberListViewModel { ID = 4, MemberName = "RICHARD H BOHLMAN", Billed = "542.00", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "542.00", Pending = "0.00" });
            aMemberList.Add(new AMemberListViewModel { ID = 5, MemberName = "CLARA M POFI", Billed = "332.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "332.15", Pending = "0.00" });
            aMemberList.Add(new AMemberListViewModel { ID = 6, MemberName = "YOLANDA K MERWIN", Billed = "112.00", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "112.00", Pending = "0.00" });
            aMemberList.Add(new AMemberListViewModel { ID = 7, MemberName = "MILLE J LOCKARD", Billed = "233.20", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "233.00", Pending = "0.00" });
            aMemberList.Add(new AMemberListViewModel { ID = 8, MemberName = "DOROTHY C FLYNN", Billed = "110.00", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "110.00", Pending = "0.00" });
            aMemberList.Add(new AMemberListViewModel { ID = 9, MemberName = "WENDY C LEE", Billed = "187.50", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "187.00", Pending = "0.00" });
            aMemberList.Add(new AMemberListViewModel { ID = 10, MemberName = "ALLENE STRICKLAND", Billed = "246.90", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "246.90", Pending = "0.00" });

            // for rendering provider
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 1, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", ClaimsSubmitted = "23", ClaimsAccepted = "12", ClaimsRejected = "0", ClaimsPending = "11" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 2, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", ClaimsSubmitted = "36", ClaimsAccepted = "22", ClaimsRejected = "11", ClaimsPending = "3" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 3, RenderingProvider = "ASIF MASOOD", MemberCount = "10", ClaimsSubmitted = "12", ClaimsAccepted = "0", ClaimsRejected = "0", ClaimsPending = "12" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 4, RenderingProvider = "BONNIE GIAMMATTEI", MemberCount = "10", ClaimsSubmitted = "34", ClaimsAccepted = "24", ClaimsRejected = "0", ClaimsPending = "10" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 5, RenderingProvider = "BONNIE GIAMMATTEI", MemberCount = "10", ClaimsSubmitted = "3", ClaimsAccepted = "1", ClaimsRejected = "0", ClaimsPending = "2" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 7, RenderingProvider = "ASIF MASOOD", MemberCount = "10", ClaimsSubmitted = "22", ClaimsAccepted = "11", ClaimsRejected = "11", ClaimsPending = "0" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 6, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", ClaimsSubmitted = "20", ClaimsAccepted = "9", ClaimsRejected = "6", ClaimsPending = "5" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 8, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", ClaimsSubmitted = "28", ClaimsAccepted = "11", ClaimsRejected = "8", ClaimsPending = "9" });
            cRenderingProvider.Add(new CRenderingProviderListViewModel { ID = 9, RenderingProvider = "ASIF MASOOD", MemberCount = "10", ClaimsSubmitted = "23", ClaimsAccepted = "23", ClaimsRejected = "0", ClaimsPending = "0" });

            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 1, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 2, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 3, RenderingProvider = "ASIF MASOOD", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 4, RenderingProvider = "BONNIE GIAMMATTEI", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 5, RenderingProvider = "BONNIE GIAMMATTEI", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 7, RenderingProvider = "ASIF MASOOD", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 8, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 9, RenderingProvider = "ASIF MASOOD", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 10, RenderingProvider = "BONNIE GIAMMATTEI", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });
            aRenderingProvider.Add(new ARenderingProviderListViewModel { ID = 11, RenderingProvider = "CHRISTOPHER COPPOLA", MemberCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });


            cBillingProviderList.Add(new CBillingProviderListViewModel { ID = 1, BillingProvider = "ACCESS 2 HEALTHCARE PHYSICIANS", RenderingProviderCount = "10", ClaimsSubmitted = "10", ClaimsAccepted = "10", ClaimsRejected = "0", ClaimsPending = "0" });

            aBillingProviderList.Add(new ABillingProviderListViewModel { ID = 1, BillingProvider = "ACCESS 2 HEALTHCARE PHYSICIANS", RenderingProviderCount = "10", Billed = "186.15", Allowed = "0.00", Adj = "0.00", Ded = "0.00", Denied = "0.00", Paid = "121.00", Pending = "65.15" });

        }
        public Models.PayerDrilldownReport.CPayerReportViewModel GetPayerClaimsCountReport()
        {
            CPayerReportViewModel PayerReport = new CPayerReportViewModel();
            PayerReport.SubmittedClaims = "34,354,853";
            PayerReport.AcceptedClaims = "643,434";
            PayerReport.RejectedClaims = "1,001";
            PayerReport.PendingClaims = "2,227";
            PayerReport.PayerList = cPayerList;
            return PayerReport;
        }

        public Models.PayerDrilldownReport.APayerReportViewModel GetPayerClaimsAmountReport()
        {
            APayerReportViewModel PayerReport = new APayerReportViewModel();
            PayerReport.AmountBilled = "34,354,853";
            PayerReport.AmountPaid = "643,434";
            PayerReport.AmountPending = "1,001";
            PayerReport.AmountDenied = "2,227";
            PayerReport.PayerList = aPayerList;
            return PayerReport;
        }

        public Models.PayerDrilldownReport.CMemberReportViewModel GetMemberClaimsCountReport(int PayerId, int BillingProviderId, int RenderingProviderId)
        {
            CMemberReportViewModel MemberReport = new CMemberReportViewModel();
            MemberReport.ClaimSubmitted = "34,354,853";
            MemberReport.ClaimAccepted = "643,434";
            MemberReport.ClaimRejected = "1,001";
            MemberReport.ClaimPending = "2,227";
            MemberReport.Members = cMemberList;
            return MemberReport;
        }

        public Models.PayerDrilldownReport.AMemberReportViewModel GetMemberClaimsAmountReport(int PayerId, int BillingProviderId, int RenderingProviderId)
        {
            AMemberReportViewModel MemberReport = new AMemberReportViewModel();
            MemberReport.AmountBilled = "22,354,853";
            MemberReport.AmountPaid = "2,643,434";
            MemberReport.AmountDenied = "1,658";
            MemberReport.AmountPending = "3,233";
            MemberReport.Members = aMemberList;
            return MemberReport;
        }

        public Models.PayerDrilldownReport.CRenderingProviderReportViewModel GetRenderingProviderCountReport(int PayerId, int BillingProviderId)
        {
            CRenderingProviderReportViewModel renderingProviderList = new CRenderingProviderReportViewModel();
            renderingProviderList.SubmittedClaims = "34,354,853";
            renderingProviderList.AcceptedClaims = "643,434";
            renderingProviderList.RejectedClaims = "1,001";
            renderingProviderList.PendingClaims = "2,227";
            renderingProviderList.RenderingProvider = cRenderingProvider;
            return renderingProviderList;
        }

        public Models.PayerDrilldownReport.ARenderingProviderReportViewModel GetRenderingProviderAmountReport(int PayerId, int BillingProviderId)
        {
            ARenderingProviderReportViewModel renderingProviderList = new ARenderingProviderReportViewModel();
            renderingProviderList.AmountBilled = "34,354,853";
            renderingProviderList.AmountPaid = "643,434";
            renderingProviderList.AmountDenied = "1,001";
            renderingProviderList.AmountPending = "2,227";
            renderingProviderList.RenderingProvider = aRenderingProvider;
            return renderingProviderList;
        }

        public Models.PayerDrilldownReport.CBillingProviderReportViewModel GetBillingClaimsCountReport(int PayerId)
        {
            CBillingProviderReportViewModel billingProviderList = new CBillingProviderReportViewModel();
            billingProviderList.SubmittedClaims = "34,354,853";
            billingProviderList.AcceptedClaims = "643,434";
            billingProviderList.RejectedClaims = "1,001";
            billingProviderList.PendingClaims = "2,227";
            billingProviderList.BillingProvider = cBillingProviderList;
            return billingProviderList;
        }

        public Models.PayerDrilldownReport.ABillingProviderReportViewModel GetBillingClaimsAmountReport(int PayerId)
        {
            ABillingProviderReportViewModel billingProviderList = new ABillingProviderReportViewModel();
            billingProviderList.AmountBilled = "34,354,853";
            billingProviderList.AmountPaid = "643,434";
            billingProviderList.AmountDenied = "1,001";
            billingProviderList.AmountPending = "2,227";
            billingProviderList.BillingProvider = aBillingProviderList;
            return billingProviderList;
        }
    }
}