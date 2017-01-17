using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.AccountManagement
{
    public class AccountsListViewModel
    {
        public int AccountID { get; set; }

        public string AccountName { get; set; }

        public string AccountType { get; set; }

        public string ReportTo { get; set; }

        public string Reportee { get; set; }

        public string Status { get; set; }
    }
}