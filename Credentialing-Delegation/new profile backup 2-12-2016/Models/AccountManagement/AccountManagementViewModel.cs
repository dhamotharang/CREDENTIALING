using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.AccountManagement
{
    public class AccountManagementViewModel
    {
        public AccountManagementViewModel()
        {
            AccountsList = new List<AccountsListViewModel>();
        }
        public List<AccountsListViewModel> AccountsList { get; set; }
    }
}