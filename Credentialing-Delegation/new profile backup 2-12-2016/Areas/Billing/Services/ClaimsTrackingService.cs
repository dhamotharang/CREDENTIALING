using PortalTemplate.Areas.Billing.Models.Claims_Tracking;
using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Services.IServices;
using PortalTemplate.Areas.Billing.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Billing.Services
{
    public class ClaimsTrackingService : IClaimsTrackingService
    {
        public List<OpenClaimViewModel> GetOpenClaimsList()
        {
            //List<OpenClaimViewModel> OpenList = new List<OpenClaimViewModel>();

            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM1", Payer = "Ultimate", Member = "Howard Robertson", Provider = "Dr. SANDY AANONSEN", ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM2", Payer = "Ultimate", Member = "Alex", Provider = "DR. V", ClaimedAmount = "1560.00", CreatedOn = new DateTime(2016, 10, 04), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM3", Payer = "Ultimate", Member = "Jimmy", Provider = "Dr. RUBY ADAM", ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM4", Payer = "Ultimate", Member = "Howard Robot", Provider = "Dr. LORENE JHONY", ClaimedAmount = "250.00", CreatedOn = new DateTime(2016, 10, 19), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM5", Payer = "Ultimate", Member = "Selena", Provider = "DR. V", ClaimedAmount = "350.00", CreatedOn = new DateTime(2016, 10, 05), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM6", Payer = "Ultimate", Member = "Nene", Provider = "Dr. PEARL NICOLE", ClaimedAmount = "480.00", CreatedOn = new DateTime(2016, 10, 06), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM7", Payer = "Ultimate", Member = "Brock", Provider = "DR. V", ClaimedAmount = "60.00", CreatedOn = new DateTime(2016, 10, 06), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM8", Payer = "Ultimate", Member = "Gray", Provider = "Dr. FORD KARLA", ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 05), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM9", Payer = "Ultimate", Member = "Phillip Carr", Provider = "DR. V", ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), Age = 180, Account = "Access Health Care" });
            //OpenList.Add(new OpenClaimViewModel { ClaimId = "CLM10", Payer = "Ultimate", Member = "Jimmy", Provider = "DR. V", ClaimedAmount = "66.00", CreatedOn = new DateTime(2016, 10, 03), Age = 180, Account = "Access Health Care" });

            //return OpenList;

            try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<List<OpenClaimViewModel>>("api/ClaimList?Status=OPEN&SortBy=DateCreated&SortOrder=asc&StartIndex=0", "BilllingServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<OnHoldClaimViewModel> GetOnHoldClaimsList()
        {
            List<OnHoldClaimViewModel> OnHoldList = new List<OnHoldClaimViewModel>();
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Phillip Carr", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Howard Robertson", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Beverly Perry", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Deborah Rose", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Bobby Perez", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Emily Wheeler", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Larry Austin", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Timothy Howell", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Tina Morris", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            OnHoldList.Add(new OnHoldClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Margaret James", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });

            return OnHoldList;
        }

        public List<RejectedClaimViewModel> GetRejectedClaimsList()
        {
            List<RejectedClaimViewModel> RejectedList = new List<RejectedClaimViewModel>();
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joe Austin", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Ralph Gray", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Lori Gilbert", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Melissa Mcdonald", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Robert Torres", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Carlos James", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Jack Evans", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Barbara White", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Frances Morris", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });
            RejectedList.Add(new RejectedClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Peter Powell", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", RejectedOn = new DateTime(2016, 03, 02), RejectionReason = "" });

            return RejectedList;
        }

        public List<CommitteeReviewClaimViewModel> GetComitteeReviewListClaimsList()
        {
            List<CommitteeReviewClaimViewModel> ComitteeReviewList = new List<CommitteeReviewClaimViewModel>();
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Emily Franklin", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Wanda Jenkins", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Diane Cole", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Juan Fowler", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joyce Taylor", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Todd Patterson", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "John White", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Ann Frazier", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Samuel Wright", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            ComitteeReviewList.Add(new CommitteeReviewClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Julie Perkins", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });

            return ComitteeReviewList;
        }

        public List<PhysicianOnHoldClaimViewModel> GetPhysicianOnHoldListClaimsList()
        {
            List<PhysicianOnHoldClaimViewModel> PhysicianOnHoldList = new List<PhysicianOnHoldClaimViewModel>();
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Phillip Carr", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Michael Collins", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Dorothy Coleman", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Charles Knight", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Kathleen Washington", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Steven King", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Doris Jordan", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Craig Elliott", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Jack Butler", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });
            PhysicianOnHoldList.Add(new PhysicianOnHoldClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Ruth Hanson", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", CreatedOn = new DateTime(2016, 10, 03), CreatedBy = "", ReviewedBy = "", OnHoldOn = new DateTime(2016, 03, 02), OnHoldReason = "" });

            return PhysicianOnHoldList;
        }

        public List<AcceptedClaimViewModel> GetAcceptedClaimsList()
        {
            List<AcceptedClaimViewModel> AcceptedList = new List<AcceptedClaimViewModel>();
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM1", Payer = "Ultimate", Member = "Judy Wheeler", Provider = "Dr. SANDY AANONSEN", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "true", ClaimId = "CLM2", Payer = "Ultimate", Member = "Fred Hudson", Provider = "DR. V", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM3", Payer = "Ultimate", Member = "Earl Robinson", Provider = "Dr. RUBY ADAM", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM4", Payer = "Ultimate", Member = "Heather Oliver", Provider = "Dr. LORENE JHONY", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM5", Payer = "Ultimate", Member = "Julia Hart", Provider = "DR. V", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM6", Payer = "Ultimate", Member = "Jacqueline Martinez", Provider = "Dr. PEARL NICOLE", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM7", Payer = "Ultimate", Member = "Thomas Ellis", Provider = "DR. V", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM8", Payer = "Ultimate", Member = "Peter Bennett", Provider = "Dr. FORD KARLA", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM9", Payer = "Ultimate", Member = "Adam Barnes", Provider = "DR. V", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", Age = 6, Account = "Account1" });
            AcceptedList.Add(new AcceptedClaimViewModel { IsProcessing = "false", ClaimId = "CLM10", Payer = "Ultimate", Member = "Phillip Perez", Provider = "DR. V", CreatedOn = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", Age = 6, Account = "Account1" });

            return AcceptedList;
        }

        public List<DispatchedClaimViewModel> GetDispatchedClaimsList()
        {
            List<DispatchedClaimViewModel> DispatchedList = new List<DispatchedClaimViewModel>();
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Billy Stone", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joe Bell", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joseph Garza", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Louis Ellis", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Emily Day", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "James James", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Samuel Gonzalez", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joan Dixon", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Arthur Turner", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });
            DispatchedList.Add(new DispatchedClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Nicole Stone", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), SentTo = "Emdeon" });

            return DispatchedList;
        }

        public List<UnAckByCHClaimViewModel> GetUnAckByCHClaimsList()
        {
            List<UnAckByCHClaimViewModel> UnAckByCHList = new List<UnAckByCHClaimViewModel>();
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Billy Stone", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joe Bell", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joseph Garza", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Louis Ellis", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Emily Day", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "James James", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Samuel Gonzalez", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joan Dixon", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Arthur Turner", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByCHList.Add(new UnAckByCHClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Nicole Stone", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });

            return UnAckByCHList;
        }

        public List<RejectedByCHClaimViewModel> GetRejectedByCHClaimsList()
        {
            List<RejectedByCHClaimViewModel> RejectedByCHList = new List<RejectedByCHClaimViewModel>();

            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Paul Rivera", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Theresa Vasquez", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Margaret Gardner", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Ryan Jenkins", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Marilyn Hall", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Walter Willis", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joan Jacobs", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Lisa Fisher", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Jean Davis", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByCHList.Add(new RejectedByCHClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Janet Knight", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", CreatedOn = new DateTime(2016, 10, 03), SentTo = "Humana", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });


            return RejectedByCHList;
        }

        public List<AcceptedByCHClaimViewModel> GetAcceptedByCHClaimsList()
        {
            List<AcceptedByCHClaimViewModel> AcceptedByCHList = new List<AcceptedByCHClaimViewModel>();
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "John Boyd", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Michael Austin", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Lillian Gilbert", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Gregory Matthews", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Jessica Carroll", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Raymond Miller", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Dorothy Lopez", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Michelle Torres", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Jane Hayes", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByCHList.Add(new AcceptedByCHClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Fred Hanson", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });

            return AcceptedByCHList;
        }

        public List<UnAckByPayerClaimViewModel> GetUnackByPayerClaimsList()
        {

            List<UnAckByPayerClaimViewModel> UnAckByPayerList = new List<UnAckByPayerClaimViewModel>();
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Frank Banks", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Carl Cooper", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Dean", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Amanda Hill", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Phillip Washington", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Janet Arnold", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Elliott", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Lawrence White", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joseph Carr", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            UnAckByPayerList.Add(new UnAckByPayerClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Todd Reid", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });

            return UnAckByPayerList;
        }

        public List<RejectedByPayerClaimViewModel> GetRejectedByPayerClaimsList()
        {
            List<RejectedByPayerClaimViewModel> RejectedByPayerList = new List<RejectedByPayerClaimViewModel>();
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Rachel Ellis", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Ruth Mitchell", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Johnny Watkins", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Gregory Sullivan", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Willie Barnes", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Kathryn Patterson", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Louise Morales", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Kathy Marshall", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Keith Gray", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });
            RejectedByPayerList.Add(new RejectedByPayerClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Stephen Tucker", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), RejectedOn = new DateTime(2016, 2, 1), RejectionReason = "" });

            return RejectedByPayerList;
        }

        public List<AcceptedByPayerClaimViewModel> GetAcceptedByPayerClaimsList()
        {
            List<AcceptedByPayerClaimViewModel> AcceptedByPayerList = new List<AcceptedByPayerClaimViewModel>();
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Frank Gibson", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Kelly Sims", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Ashley Patterson", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Richard Chapman", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Sandra Clark", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Bonnie Cruz", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Rose Thomas", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Ruth Peters", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Brian Gardner", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });
            AcceptedByPayerList.Add(new AcceptedByPayerClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Thomas Hughes", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), AcceptedOn = new DateTime(2016, 2, 1), AcceptedBy = "" });

            return AcceptedByPayerList;
        }

        public List<PendingClaimViewModel> GetPendingClaimsList()
        {
            List<PendingClaimViewModel> PendingList = new List<PendingClaimViewModel>();
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Frank Banks", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Carl Cooper", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Dean", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Amanda Hill", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Phillip Washington", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Janet Arnold", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Elliott", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Lawrence White", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joseph Carr", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            PendingList.Add(new PendingClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Todd Reid", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });

            return PendingList;
        }

        public List<DeniedByPayerClaimViewModel> GetDeniedByPayerClaimsList()
        {
            List<DeniedByPayerClaimViewModel> DeniedByPayerList = new List<DeniedByPayerClaimViewModel>();
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Frank Banks", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Carl Cooper", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Dean", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Amanda Hill", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Phillip Washington", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Janet Arnold", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Elliott", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Lawrence White", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joseph Carr", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });
            DeniedByPayerList.Add(new DeniedByPayerClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Todd Reid", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), CreatedOn = new DateTime(2016, 2, 1) });

            return DeniedByPayerList;
        }

        public List<EobReceivedClaimViewModel> GetEOBReceivedClaimsList()
        {
            List<EobReceivedClaimViewModel> EOBReceivedList = new List<EobReceivedClaimViewModel>();
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM1", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Frank Banks", RenderingProvider = "Dr. SANDY AANONSEN", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM2", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Carl Cooper", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "1560.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM3", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Dean", RenderingProvider = "Dr. RUBY ADAM", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM4", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Amanda Hill", RenderingProvider = "Dr. LORENE JHONY", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "250.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM5", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Phillip Washington", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "350.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM6", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Janet Arnold", RenderingProvider = "Dr. PEARL NICOLE", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "480.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM7", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Helen Elliott", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "60.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM8", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Lawrence White", RenderingProvider = "Dr. FORD KARLA", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "50.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM9", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Joseph Carr", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "650.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });
            EOBReceivedList.Add(new EobReceivedClaimViewModel { ClaimId = "CLM10", PrimaryPayer = "Ultimate", SecondaryPayer = "Freedom", Member = "Todd Reid", RenderingProvider = "DR. V", BillingProvider = "Access 2 Health Care", DosFrom = new DateTime(2016, 10, 03), DosTo = new DateTime(2016, 10, 03), ClaimedAmount = "66.00", SentOn = new DateTime(2016, 10, 03), PaidOn = new DateTime(2016, 2, 1), PaidAmount = "500" });

            return EOBReceivedList;
        }

        public Cms1500ViewModels GetCms1500Form(int ClaimId)
        {
            Cms1500ViewModels Cms1500Form = new Cms1500ViewModels();

            Cms1500Form.PatientLastOrOrganizationName = "KJELL";
            Cms1500Form.PatientMiddleName = "";
            Cms1500Form.PatientFirstName = "AANONSEN";
            Cms1500Form.PatientBirthDate = new DateTime(1935, 04, 26);
            Cms1500Form.PatientFirstAddress = "6046";
            Cms1500Form.PatientSecondAddress = "NEWMARK ST";

            Cms1500Form.PayerName = "FREEDOM HEALTH INS";
            Cms1500Form.PayerFirstAddress = "P.O. BOX 151348";
            Cms1500Form.PayerSecondAddress = "SPRING HILL";
            Cms1500Form.PayerState = "AA";
            Cms1500Form.PayerCity = "TEMPA";
            Cms1500Form.PayerID = "41212";
            Cms1500Form.PayerZip = "336840401";

            Cms1500Form.BillingGroupNumber = "1851689947";
            Cms1500Form.BillingProviderLastOrOrganizationName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.BillingProviderPhoneNo = "587977444";
            Cms1500Form.BillingProviderZip = "346098102";
            Cms1500Form.BillingProviderTaxonomy = "207RG0300X";
            Cms1500Form.BillingProviderCity = "SPRING HILL";
            Cms1500Form.BillingProviderState = "FL";
            Cms1500Form.BillingProviderFirstAddress = "14690 SPRING HILL DR";
            Cms1500Form.ReferringProviderLastName = "BENSON";
            Cms1500Form.RenderingProviderFirstName = "DALTON";
            Cms1500Form.RenderingProviderTaxonomy = "207RG0300X";
            Cms1500Form.RenderingProviderNPI = "4125412451";
            Cms1500Form.ReferringProviderIdentifier = "1164477618";

            Cms1500Form.FacilityName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.FacilityAddress1 = "1903 W HIGHWAY 44";
            Cms1500Form.FacilityCity = "INVERNESS";
            Cms1500Form.FacilityState = "FL";
            Cms1500Form.FacilityZip = "344533801";
            Cms1500Form.FacilityIdentifier1 = "1851689947";
            Cms1500Form.FacilityIdentifier2 = "454545544";

            Cms1500Form.PatientAccountNumber = "1851689947";
            Cms1500Form.SubscriberFirstAddress = "1230 SILVERTHORN LOOP";
            Cms1500Form.SubscriberCity = "HERNANDO";
            Cms1500Form.SubscriberState = "FL";
            Cms1500Form.SubscriberZip = "34442";
            Cms1500Form.SubscriberPhoneNo = "3524650375";

            Cms1500Form.CurrentServiceFrom = new DateTime(1935, 04, 26);
            Cms1500Form.CurrentServiceTo = new DateTime(1935, 04, 26);
            Cms1500Form.ClaimsNatureOfIllness1 = "V11.5XXA";
            Cms1500Form.PlaceOfService = "11";
            //----------------service line-----------------------------


            List<ServiceLineViewModels> serviceLines = new List<ServiceLineViewModels>();
            serviceLines.Add(new ServiceLineViewModels { claimsProcedure = "99214", Modifier1 = "11", UnitCharges = 0.3, Unit = 1, DiagnosisPointer1 = "1" });
            Cms1500Form.ServiceLines = serviceLines;

            return Cms1500Form;
        }

        public Cms1500ViewModels ViewCms1500Form(int ClaimId)
        {
            Cms1500ViewModels Cms1500Form = new Cms1500ViewModels();

            Cms1500Form.PatientLastOrOrganizationName = "KJELL";
            Cms1500Form.PatientMiddleName = "";
            Cms1500Form.PatientFirstName = "AANONSEN";
            Cms1500Form.PatientBirthDate = new DateTime(1935, 04, 26);
            Cms1500Form.PatientFirstAddress = "6046";
            Cms1500Form.PatientSecondAddress = "NEWMARK ST";



            Cms1500Form.PayerName = "FREEDOM HEALTH INS";
            Cms1500Form.PayerFirstAddress = "P.O. BOX 151348";
            Cms1500Form.PayerSecondAddress = "SPRING HILL";
            Cms1500Form.PayerState = "AA";
            Cms1500Form.PayerCity = "TEMPA";
            Cms1500Form.PayerID = "41212";
            Cms1500Form.PayerZip = "336840401";

            Cms1500Form.BillingGroupNumber = "1851689947";
            Cms1500Form.BillingProviderLastOrOrganizationName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.BillingProviderPhoneNo = "587977444";
            Cms1500Form.BillingProviderZip = "346098102";
            Cms1500Form.BillingProviderTaxonomy = "207RG0300X";
            Cms1500Form.BillingProviderCity = "SPRING HILL";
            Cms1500Form.BillingProviderState = "FL";
            Cms1500Form.BillingProviderFirstAddress = "14690 SPRING HILL DR";
            Cms1500Form.ReferringProviderLastName = "BENSON";
            Cms1500Form.RenderingProviderFirstName = "DALTON";
            Cms1500Form.RenderingProviderTaxonomy = "207RG0300X";
            Cms1500Form.RenderingProviderNPI = "4125412451";
            Cms1500Form.ReferringProviderIdentifier = "1164477618";

            Cms1500Form.FacilityName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.FacilityAddress1 = "1903 W HIGHWAY 44";
            Cms1500Form.FacilityCity = "INVERNESS";
            Cms1500Form.FacilityState = "FL";
            Cms1500Form.FacilityZip = "344533801";
            Cms1500Form.FacilityIdentifier1 = "1851689947";
            Cms1500Form.FacilityIdentifier2 = "454545544";

            Cms1500Form.PatientAccountNumber = "1851689947";
            Cms1500Form.SubscriberFirstAddress = "1230 SILVERTHORN LOOP";
            Cms1500Form.SubscriberCity = "HERNANDO";
            Cms1500Form.SubscriberState = "FL";
            Cms1500Form.SubscriberZip = "34442";
            Cms1500Form.SubscriberPhoneNo = "3524650375";

            Cms1500Form.CurrentServiceFrom = new DateTime(1935, 04, 26);
            Cms1500Form.CurrentServiceTo = new DateTime(1935, 04, 26);
            Cms1500Form.ClaimsNatureOfIllness1 = "V11.5XXA";
            Cms1500Form.PlaceOfService = "11";
            //----------------service line-----------------------------


            List<ServiceLineViewModels> serviceLines = new List<ServiceLineViewModels>();
            serviceLines.Add(new ServiceLineViewModels { claimsProcedure = "99214", Modifier1 = "11", UnitCharges = 0.3, Unit = 1, DiagnosisPointer1 = "1" });
            Cms1500Form.ServiceLines = serviceLines;

            return Cms1500Form;
        }

        public Cms1500ViewModels GetCms1500FormInstance(int ClaimId)
        {
            Cms1500ViewModels Cms1500Form = new Cms1500ViewModels();

            Cms1500Form.PatientLastOrOrganizationName = "KJELL";
            Cms1500Form.PatientMiddleName = "";
            Cms1500Form.PatientFirstName = "AANONSEN";
            Cms1500Form.PatientBirthDate = new DateTime(1935, 04, 26);
            Cms1500Form.PatientFirstAddress = "6046";
            Cms1500Form.PatientSecondAddress = "NEWMARK ST";

            Cms1500Form.PayerName = "FREEDOM HEALTH INS";
            Cms1500Form.PayerFirstAddress = "P.O. BOX 151348";
            Cms1500Form.PayerSecondAddress = "SPRING HILL";
            Cms1500Form.PayerState = "AA";
            Cms1500Form.PayerCity = "TEMPA";
            Cms1500Form.PayerID = "41212";
            Cms1500Form.PayerZip = "336840401";

            Cms1500Form.BillingGroupNumber = "1851689947";
            Cms1500Form.BillingProviderLastOrOrganizationName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.BillingProviderPhoneNo = "587977444";
            Cms1500Form.BillingProviderZip = "346098102";
            Cms1500Form.BillingProviderTaxonomy = "207RG0300X";
            Cms1500Form.BillingProviderCity = "SPRING HILL";
            Cms1500Form.BillingProviderState = "FL";
            Cms1500Form.BillingProviderFirstAddress = "14690 SPRING HILL DR";
            Cms1500Form.ReferringProviderLastName = "BENSON";
            Cms1500Form.RenderingProviderFirstName = "DALTON";
            Cms1500Form.RenderingProviderTaxonomy = "207RG0300X";
            Cms1500Form.RenderingProviderNPI = "4125412451";
            Cms1500Form.ReferringProviderIdentifier = "1164477618";

            Cms1500Form.FacilityName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.FacilityAddress1 = "1903 W HIGHWAY 44";
            Cms1500Form.FacilityCity = "INVERNESS";
            Cms1500Form.FacilityState = "FL";
            Cms1500Form.FacilityZip = "344533801";
            Cms1500Form.FacilityIdentifier1 = "1851689947";
            Cms1500Form.FacilityIdentifier2 = "454545544";

            Cms1500Form.PatientAccountNumber = "1851689947";
            Cms1500Form.SubscriberFirstAddress = "1230 SILVERTHORN LOOP";
            Cms1500Form.SubscriberCity = "HERNANDO";
            Cms1500Form.SubscriberState = "FL";
            Cms1500Form.SubscriberZip = "34442";
            Cms1500Form.SubscriberPhoneNo = "3524650375";

            Cms1500Form.CurrentServiceFrom = new DateTime(1935, 04, 26);
            Cms1500Form.CurrentServiceTo = new DateTime(1935, 04, 26);
            Cms1500Form.ClaimsNatureOfIllness1 = "V11.5XXA";
            Cms1500Form.PlaceOfService = "11";
            //----------------service line-----------------------------


            List<ServiceLineViewModels> serviceLines = new List<ServiceLineViewModels>();
            serviceLines.Add(new ServiceLineViewModels { claimsProcedure = "99214", Modifier1 = "11", UnitCharges = 0.3, Unit = 1, DiagnosisPointer1 = "1" });
            Cms1500Form.ServiceLines = serviceLines;


            return Cms1500Form;
        }

        public List<EobReport> GetEobReport(int ClaimId)
        {
            List<EobReport> EOBReportList = new List<EobReport>();

            EOBReportList.Add(new EobReport { Id = 1, Cpt = "99214", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SANTIAGO TORR HERIBERT", Dos = new DateTime(2014, 12, 06), Billed = "554.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-45", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  45-Charge exceeds fee schedule/maximum allowable " });
            EOBReportList.Add(new EobReport { Id = 2, Cpt = "G6453", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SANTIAGO TORR HERIBERT", Dos = new DateTime(2014, 11, 13), Billed = "199.00", Allowed = "0.00", Adjusted = "199.00", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-45", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  45-Charge exceeds fee schedule/maximum allowable" });
            EOBReportList.Add(new EobReport { Id = 3, Cpt = "G2342", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SANTIAGO TORR HERIBERT", Dos = new DateTime(2014, 12, 31), Billed = "324.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-16", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  16-Charge exceeds fee schedule/maximum allowable" });
            EOBReportList.Add(new EobReport { Id = 4, Cpt = "G5437", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SANTIAGO TORR HERIBERT", Dos = new DateTime(2014, 10, 22), Billed = "124.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-45", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  45-Charge exceeds fee schedule/maximum allowable" });
            EOBReportList.Add(new EobReport { Id = 5, Cpt = "G7555", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SANTIAGO TORR HERIBERT", Dos = new DateTime(2014, 12, 12), Billed = "104.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-16", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  16-Charge exceeds fee schedule/maximum allowable" });
            EOBReportList.Add(new EobReport { Id = 6, Cpt = "1232F", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SANTIAGO TORR HERIBERT", Dos = new DateTime(2014, 11, 30), Billed = "542.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-54", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  54-Charge exceeds fee schedule/maximum allowable" });
            EOBReportList.Add(new EobReport { Id = 7, Cpt = "G6710", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SANTIAGO TORR HERIBERT", Dos = new DateTime(2014, 12, 29), Billed = "244.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-256", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  256-Charge exceeds fee schedule/maximum allowable" });
            EOBReportList.Add(new EobReport { Id = 8, Cpt = "6543G", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SIMPSON ALEX B", Dos = new DateTime(2014, 11, 16), Billed = "140.00", Allowed = "0.00", Adjusted = "140.00", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-32", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  32-Charge exceeds fee schedule/maximum allowabl" });
            EOBReportList.Add(new EobReport { Id = 9, Cpt = "H6754", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SIMPSON ALEX B", Dos = new DateTime(2014, 09, 26), Billed = "223.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-16", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  16-Charge exceeds fee schedule/maximum allowable"  });
            EOBReportList.Add(new EobReport { Id = 10, Cpt = "J6533", PrimaryPayerName = "STATE OF FLORIDA MADICAID", SecondaryPayerName = "", Check = "0.00", PatientName = "SIMPSON ALEX B", Dos = new DateTime(2014, 09, 23), Billed = "130.00", Allowed = "0.00", Adjusted = "0.01", Denied = "0.00", CoInsurence = "0.00", Amount = "0.00", Penalty = "0.00", Paid = "0.00", Reason = "CO-16", ReasonDescription = "CO-Contractual Obligation(Provider Write-off)  16-Charge exceeds fee schedule/maximum allowable" });

            return EOBReportList;
        }
    }
}