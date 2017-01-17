using PortalTemplate.Areas.Billing.Models.BillerProductivityReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Controllers
{
    public class BillerProductivityReportController : Controller
    {
        //
        // GET: /Billing/BillerProductivityReport/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBillerProductivityReport()
        {
            return PartialView("~/Areas/Billing/Views/BillerProductivityReport/Index.cshtml");
        }

        public ActionResult GetAvgBillerProductivityReport()
        {
            AverageBillerProductivityReportViewModel avgBillerProductivityReport = new AverageBillerProductivityReportViewModel();

            avgBillerProductivityReport.TotalBillers = 55;
            avgBillerProductivityReport.TotalClaims = 58168;
            avgBillerProductivityReport.TotalDays = 234;
            avgBillerProductivityReport.TotalBilledAmounts = 11364165.41;
            avgBillerProductivityReport.AverageClaims = 1057;
            avgBillerProductivityReport.FromDateofCreation = new DateTime(2014, 12, 06);
            avgBillerProductivityReport.ToDateofCreation = new DateTime(2015, 12, 06);

            return PartialView("~/Areas/Billing/Views/BillerProductivityReport/_BillerProductivityReport.cshtml", avgBillerProductivityReport);
        }

        public ActionResult GetBillerProductivityReportTable()
        {
            BillerProductivityReportViewModel billerProductivityReport = new BillerProductivityReportViewModel();

            List<BillerProductivityReportListViewModel> billerProductivityReportList = new List<BillerProductivityReportListViewModel>();

            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 1, FirstName = "BERNARDO", LastName = "HENAO", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 122 });
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 2, FirstName = "FLORINE", LastName = "BOHLMAN", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 547 });
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 3, FirstName = "WILMA", LastName = "MERWIN", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 754 });
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 4, FirstName = "RICHARD", LastName = "LOCKARD", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 1142 });
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 5, FirstName = "CLARA", LastName = "HENAO", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 142 });
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 6, FirstName = "YOLANDA", LastName = "", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims =214});
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 7, FirstName = "MILLE", LastName = "", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims =321});
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 8, FirstName = "DOROTHY", LastName = "MERWIN", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 44 });
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 9, FirstName = "WENDY", LastName = "HENAO", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 245 });
            billerProductivityReportList.Add(new BillerProductivityReportListViewModel { Id = 10, FirstName = "ALLENE", LastName = "BOHLMAN", Open = 0, OnHold = 0, Rejected = 0, Accepted = 0, Generated = 0, Translated = 0, ProviderOnHold = 0, CommitteeReview = 0, BilledAmount = 0, TotalClaims = 265 });

            billerProductivityReport.BillerProductivityReportList = billerProductivityReportList;

            billerProductivityReport.TotalOpen = 3;
            billerProductivityReport.TotalOnHold = 10;
            billerProductivityReport.TotalRejected = 5;
            billerProductivityReport.TotalAccepted = 14;
            billerProductivityReport.TotalGenerated = 12;
            billerProductivityReport.TotalTranslated = 11;
            billerProductivityReport.TotalCommitteeReview = 7;
            billerProductivityReport.TotalProviderOnHold = 10;
            billerProductivityReport.TotalBilledAmount = 124544.12;
            billerProductivityReport.TotalTotalClaims = 454;

            return PartialView("~/Areas/Billing/Views/BillerProductivityReport/_BillerProductivityReportTable.cshtml", billerProductivityReport);
        }
    }
}