using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.Provider
{
    public class ProviderAccountViewModel
    {
        public string AccountID { get; set; }

        public string AccountName { get; set; }

        public string AccountType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}