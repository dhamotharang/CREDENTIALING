using PortalTemplate.Models.AccountManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class AccountManagementController : Controller
    {
        //
        // GET: /AccountManagement/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAccountsListingData()
        {
            try
            {
                AccountManagementViewModel Acclist = new AccountManagementViewModel();
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "InActive" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "InActive" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "InActive" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "InActive" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "InActive" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "InActive" });
                Acclist.AccountsList.Add(new AccountsListViewModel { AccountID = 12345, AccountName = "Access", AccountType = "Organization", ReportTo = "Parent Account", Reportee = "Child Account", Status = "Active" });
                return PartialView("~/Views/AccountManagement/_AccountsListing.cshtml", Acclist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

	}
}